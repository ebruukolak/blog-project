using Blogs.Application.Database;
using Blogs.Application.Models;
using Dapper;
using Microsoft.Identity.Client;

namespace Blogs.Application.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public PostRepository(IDbConnectionFactory dbConnectionFactory)
        {
           _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<bool> CreateAsync(Post post)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            using var transaction = connection.BeginTransaction();

            var result = await connection.ExecuteAsync(new CommandDefinition("""
                insert into Posts(id,categoryId,title,slug,content,isDraft,publishedDate)
                values(@Id,@CategoryId,@Title,@Slug,@Content,@IsDraft,@PublishedDate)
                """, post,transaction));
            if (result > 0)
            {
                foreach (var tag in post.Tags)
                {
                    await connection.ExecuteAsync(new CommandDefinition("""
                        insert into Tags(postId,name)
                        values(@postId,@name)
                        """, new {postId=post.Id,name=tag},transaction));
                }
            }

            transaction.Commit();
            return result>0;
        }

        public async Task<Post?> GetByIdAsync(Guid id)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();

            var post = await connection.QuerySingleOrDefaultAsync<Post>(new CommandDefinition("""
                select * from Posts where id = @id
                """, new { id}));

            if (post is null)
            {
                return null;
            }

            var tags = await connection.QueryAsync<string>(new CommandDefinition("""
                select name from Tags where postId= @postId
                """,new { postId=id}));

            foreach (var tag in tags)
            {
                post.Tags.Add(tag);
            }

            return post;
        }
        public async Task<Post?> GetBySlugAsync(string slug)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();

            var post = await connection.QuerySingleOrDefaultAsync<Post>(new CommandDefinition("""
                select * from Posts where slug = @slug
                """, new { slug }));

            if (post is null)
            {
                return null;
            }

            var tags = await connection.QueryAsync<string>(new CommandDefinition("""
                select name from Tags where postId= @postId
                """, new { postId = post.Id }));

            foreach (var tag in tags)
            {
                post.Tags.Add(tag);
            }

            return post;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var result = await connection.QueryAsync(new CommandDefinition("""
                select p.*, t.tags
                from Posts p
                left join (
                    select t.postId, string_agg(t.name, ',') as tags
                    from Tags t
                    group by t.postId
                ) t on p.id = t.postId;
                """));

            return result.Select(s => new Post
            {
                Id = s.id,
                CategoryId =  s.categoryId,
                Title = s.title,
                Content = s.content,
                IsDraft = s.isDraft,
                PublishedDate = s.publishedDate,
                Tags = Enumerable.ToList(s.tags.Split(','))
            });

        }

        public async Task<bool> UpdateAsync(Post post)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            using var transaction = connection.BeginTransaction();

            await connection.ExecuteAsync(new CommandDefinition("""
                delete from Tags where postId = @postId
                """, new {postId=post.Id},transaction));

            foreach (var tag in post.Tags)
            {
                await connection.ExecuteAsync(new CommandDefinition("""
                        insert into Tags(postId,name)
                        values(@postId,@name)
                        """, new { postId = post.Id, name = tag }, transaction));
            }

            var result = await connection.ExecuteAsync(new CommandDefinition("""
                                update Posts set categoryId = @CategoryId, 
                                 title = @Title, slug = @Slug, 
                				 content = @Content, 
                				 isDraft = @IsDraft, 
                				 publishedDate = @PublishedDate
                	where id = @Id
                """,post,transaction));

            transaction.Commit();

            return result > 0;

        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            using var transaction = connection.BeginTransaction();

            await connection.ExecuteAsync(new CommandDefinition("""
                delete from Tags where postId = @postId
                """, new { postId = id }, transaction));
            var result = await connection.ExecuteAsync(new CommandDefinition("""
                delete from Posts where id = @id
                """, new {id},transaction));
            transaction.Commit();
            return result > 0;
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteScalarAsync<bool>(new CommandDefinition("""
                select count(1) from Posts where id = @id
                """, new {id}));
            return result;
        }
    }
}
