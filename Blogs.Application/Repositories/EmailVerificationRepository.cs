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
    public class EmailVerificationRepository : IEmailVerificationRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public EmailVerificationRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<bool> CreateAsync(EmailVerificationToken emailVerification, CancellationToken token)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
            using var transaction = connection.BeginTransaction();

            var result = await connection.ExecuteAsync(new CommandDefinition("""
                 insert into EmailVerificationTokens(id,userId,token,createdAt)
                 values (@Id,@UserId,@Token,@CreatedAt)
                """, emailVerification, transaction, cancellationToken: token));

            transaction.Commit();

            return result > 0;

        }

        public async Task<EmailVerificationToken?> GetByTokenAsync(string token, CancellationToken cancellationToken)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
            var emailVerificationToken = await connection.QuerySingleOrDefaultAsync<EmailVerificationToken>(new CommandDefinition("""
                select * from EmailVerificationTokens where token=@token
                """, new { token }, cancellationToken: cancellationToken));
            if (emailVerificationToken is null)
                return null;

            return emailVerificationToken;
        }

        public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
            using var transaction = connection.BeginTransaction();

            var result = await connection.ExecuteAsync(new CommandDefinition("""
                delete from EmailVerificationTokens where id=@id
                """, new { id }, transaction, cancellationToken: token));

            transaction.Commit();

            return result > 0;
        }

    }
}
