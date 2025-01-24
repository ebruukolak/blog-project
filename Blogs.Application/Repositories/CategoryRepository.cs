using Blogs.Application.Database;
using Blogs.Application.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public CategoryRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<bool> CreateAsync(Category category)
        {

            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            using var transaction = connection.BeginTransaction();

            var result = await connection.ExecuteAsync(new CommandDefinition("""
                insert into Categories(id,name,slug,description,parentCategoryId)
                values(@Id,@Name,@Slug,@Description,@ParentCategoryId)
                """, category, transaction));

            transaction.Commit();

            return result > 0;


        }
        public async Task<Category?> GetByIdAsync(Guid id)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var category = await connection.QuerySingleOrDefaultAsync<Category>(new CommandDefinition("""
                select * from Categories where id=@id
                """, new {id}));
            if (category is null)
                return null;

            return category;
        }
        public async Task<Category?> GetBySlugAsync(string slug)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var category = await connection.QuerySingleOrDefaultAsync<Category>(new CommandDefinition("""
                select * from Categories where slug=@slug
                """, new { slug }));
            if (category is null)
                return null;

            return category;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var categories = await connection.QueryAsync<Category>(new CommandDefinition("""
                select * from Categories  
                """));
            return categories;
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            using var transaction = connection.BeginTransaction();

            var result = await connection.ExecuteAsync(new CommandDefinition("""
                update Categories set name = @Name , slug = @Slug , description = @Description, parentCategoryId = @ParentCategoryId
                where id=@Id
                """,category,transaction));

            transaction.Commit();

            return result > 0;
        }
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            using var transaction = connection.BeginTransaction();

            var result = await connection.ExecuteAsync(new CommandDefinition("""
                delete from Categories where id=@id
                """, new {id},transaction));

            transaction.Commit();

            return result > 0;
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteScalarAsync<bool>(new CommandDefinition("""
                select top(1) from Category where id = @id
                """, new {id}));

            return result;
        }
    }
}
