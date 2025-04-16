using Blogs.Application.Database;
using Blogs.Application.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public RoleRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<bool> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
            var transaction = connection.BeginTransaction();

            var result = await connection.ExecuteAsync(new CommandDefinition("""
                    insert into Roles (id,name,createdAdt)
                    values(@Id,@Name,@createdAdt)
                """, role,transaction,cancellationToken:cancellationToken));

            transaction.Commit();
            return result > 0;
        }

        public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

            var role = await connection.QuerySingleAsync<Role>(new CommandDefinition("""
                select * from Roles where id = @id
                """,new { id},cancellationToken:cancellationToken));

            if (role is null)
                return null;
            return role;
        }

        public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

            var role = await connection.QuerySingleAsync<Role>(new CommandDefinition("""
                select * from Roles where name = @name
                """, new { name }, cancellationToken: cancellationToken));

            if (role is null)
                return null;
            return role;
        }

        public async Task<bool> ExistByNameAsync(string name, CancellationToken cancellationToken)
        {
            var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

            var isExist = await connection.ExecuteScalarAsync<bool>(new CommandDefinition("""
                select count(1) from Roles where name = @name
                """, new { name }, cancellationToken: cancellationToken));

            return isExist;
        }
              

        public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
            using var transaction = connection.BeginTransaction();

            var result = await connection.ExecuteAsync(new CommandDefinition("""
                delete from Roles where id = @id
                """, new { id }, transaction, cancellationToken: cancellationToken));

            transaction.Commit();

            return result > 0;
        }

        public async Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

            var roles = await connection.QueryAsync<Role>(new CommandDefinition("""
                select * from Roles
                """));

            return roles;
        }
    }
}
