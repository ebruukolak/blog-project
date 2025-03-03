using Blogs.Application.Database;
using Blogs.Application.Models;
using Dapper;

namespace Blogs.Application.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public PostRepository(IDbConnectionFactory dbConnectionFactory)
        {
           _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<bool> CreateAsync(Post post, CancellationToken token = default)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
            using var transaction = connection.BeginTransaction();
          
            var result = await connection.ExecuteAsync(new CommandDefinition("""
                insert into Posts(id,categoryId,title,slug,content,isDraft,publishedDate,createdAt,updatedAt)
                values(@Id,@CategoryId,@Title,@Slug,@Content,@IsDraft,@PublishedDate,@CreatedAt,@UpdatedAt)
                """, post,transaction,cancellationToken:token));
          
            if (result > 0)
            {
                foreach (var tag in post.Tags)
                {
                    await connection.ExecuteAsync(new CommandDefinition("""
                        insert into Tags(postId,name)
                        values(@postId,@name)
                        """, new {postId=post.Id,name=tag},transaction, cancellationToken: token));
                }
            }

            transaction.Commit();
            return result>0;
        }

        public async Task<Post?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

            var post = await connection.QuerySingleOrDefaultAsync<Post>(new CommandDefinition("""
                select * from Posts where id = @id
                """, new { id}, cancellationToken: token));

            if (post is null)
            {
                return null;
            }

            var tags = await connection.QueryAsync<string>(new CommandDefinition("""
                select name from Tags where postId= @postId
                """,new { postId=id}, cancellationToken: token));

            foreach (var tag in tags)
            {
                post.Tags.Add(tag);
            }

            return post;
        }
        public async Task<Post?> GetBySlugAsync(string slug, CancellationToken token = default)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

            var post = await connection.QuerySingleOrDefaultAsync<Post>(new CommandDefinition("""
                select * from Posts where slug = @slug
                """, new { slug }, cancellationToken: token));

            if (post is null)
            {
                return null;
            }

            var tags = await connection.QueryAsync<string>(new CommandDefinition("""
                select name from Tags where postId= @postId
                """, new { postId = post.Id }, cancellationToken: token));

            foreach (var tag in tags)
            {
                post.Tags.Add(tag);
            }

            return post;
        }

        public async Task<IEnumerable<Post>> GetAllAsync(CancellationToken token = default)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
            var result = await connection.QueryAsync(new CommandDefinition("""
                select p.*, t.tags
                from Posts p
                left join (
                    select t.postId, string_agg(t.name, ',') as tags
                    from Tags t
                    group by t.postId
                ) t on p.id = t.postId;
                """, cancellationToken: token));

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

        public async Task<bool> UpdateAsync(Post post, CancellationToken token = default)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
            using var transaction = connection.BeginTransaction();

            await connection.ExecuteAsync(new CommandDefinition("""
                delete from Tags where postId = @postId
                """, new {postId=post.Id},transaction, cancellationToken: token));

            foreach (var tag in post.Tags)
            {
                await connection.ExecuteAsync(new CommandDefinition("""
                        insert into Tags(postId,name)
                        values(@postId,@name)
                        """, new { postId = post.Id, name = tag }, transaction, cancellationToken: token));
            }

            var result = await connection.ExecuteAsync(new CommandDefinition("""
                                update Posts set categoryId = @CategoryId, 
                                 title = @Title, slug = @Slug, 
                				 content = @Content, 
                				 isDraft = @IsDraft, 
                				 publishedDate = @PublishedDate,
                                 updatedAt = @UpdatedAt
                	where id = @Id
                """,post,transaction, cancellationToken: token));

            transaction.Commit();

            return result > 0;

        }

        public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
            using var transaction = connection.BeginTransaction();

            await connection.ExecuteAsync(new CommandDefinition("""
                delete from Tags where postId = @postId
                """, new { postId = id }, transaction, cancellationToken: token));
            var result = await connection.ExecuteAsync(new CommandDefinition("""
                delete from Posts where id = @id
                """, new {id},transaction, cancellationToken: token));
            transaction.Commit();
            return result > 0;
        }

        public async Task<bool> ExistByIdAsync(Guid id, CancellationToken token = default)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
            var result = await connection.ExecuteScalarAsync<bool>(new CommandDefinition("""
                select count(1) from Posts where id = @id
                """, new {id}, cancellationToken: token));
            return result;
        }
    }
}
