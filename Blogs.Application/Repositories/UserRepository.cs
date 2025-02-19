using Blogs.Application.Database;
using Blogs.Application.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public UserRepository(IDbConnectionFactory dbConnectionFactory)
        {
                _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<bool> CreateAsync(User user, CancellationToken cancellationToken)
        {
                using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
                using var transaction = connection.BeginTransaction();

                var result = await connection.ExecuteAsync(new CommandDefinition("""
                    insert into users(id,roleId,email,firstName,lastName,passwordHash,isDeleted,createdAt)
                    values(@Id,@RoleId,@Email,@FirstName,@LastName,@PasswordHash,@IsDeleted,@CreatedAt)
                """, user, transaction, cancellationToken: cancellationToken));

                transaction.Commit();

                return result > 0;
            
            
        }
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

            var user = await connection.QuerySingleOrDefaultAsync<User>(new CommandDefinition ( """
                Select * from User where id = @id
                """, new{id },cancellationToken:cancellationToken));

            if(user is null)
            {
                return null;
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

            var users = await connection.QueryAsync<User>(new CommandDefinition("""
                select * from Users
                """));

            return users;
        }

        public async Task<bool> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
            using var transaction = connection.BeginTransaction();

            var result = await connection.ExecuteAsync(new CommandDefinition("""
                update Users set roleId = @RoleId, firstName = @firstName, lastName = @LastName, email = @Email,
                passwordHash = @PasswordHash, isDeleted = @IsDeleted, updatedAt = @UpdatedAt
                """,user,transaction,cancellationToken:cancellationToken));

            transaction.Commit();
            return result > 0;
        }

        public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
            using var transaction = connection.BeginTransaction();

            var result = await connection.ExecuteAsync(new CommandDefinition("""
                delete from users where id = @id
                """, new {id},transaction,cancellationToken:cancellationToken));

            transaction.Commit();

            return result > 0;
        }

        public async Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
            var result = await connection.ExecuteScalarAsync<bool>(new CommandDefinition("""
                select count(1) from Users where id = @id
                """, new { id }, cancellationToken: cancellationToken));

            return result;
        }

        public async Task<bool> ExistByEmailAsync(string email, CancellationToken cancellationToken)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
            var result = await connection.ExecuteScalarAsync<bool>(new CommandDefinition("""
                select count(1) from Users where email = @email
                """, new { email }, cancellationToken: cancellationToken));

            return result;
        }
    }
}
