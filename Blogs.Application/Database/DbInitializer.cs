using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Database
{
    public class DbInitializer
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public DbInitializer(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task InitializeAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            await connection.ExecuteAsync("""
                               IF NOT EXISTS 
                               (
                                    SELECT * FROM INFORMATION_SCHEMA.TABLES
                                    WHERE TABLE_SCHEMA = 'dbo'
                                    AND TABLE_TYPE = 'BASE TABLE'
                                    AND TABLE_NAME = 'Categories'
                                ) 
                            BEGIN
                                    CREATE TABLE  Categories
                                    (
                                        id UNIQUEIDENTIFIER primary key,
                                        name nvarchar(100) not null,
                                        slug nvarchar(150),
                                        description nvarchar(max),
                                        parentCategoryId UNIQUEIDENTIFIER
                                    );
                            END
                """);

            await connection.ExecuteAsync("""
                      IF NOT EXISTS 
                      (
                        SELECT * FROM INFORMATION_SCHEMA.TABLES
                        WHERE TABLE_SCHEMA = 'dbo'
                        AND TABLE_TYPE = 'BASE TABLE'
                        AND TABLE_NAME = 'Posts'
                      ) 
                    BEGIN
                          CREATE TABLE Posts
                          (
                              id UNIQUEIDENTIFIER primary key,
                              categoryId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Categories(id),
                              title nvarchar(100) not null,
                              slug nvarchar(150),
                              content nvarchar(max),
                              isDraft bit not null,
                              publishedDate datetime

                          );
                    END
                """);

            await connection.ExecuteAsync("""
                      IF NOT EXISTS 
                      (
                        SELECT * FROM INFORMATION_SCHEMA.TABLES
                        WHERE TABLE_SCHEMA = 'dbo'
                        AND TABLE_TYPE = 'BASE TABLE'
                        AND TABLE_NAME = 'Tags'
                      ) 
                    BEGIN
                          CREATE TABLE Tags
                          (
                              postId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Posts(id),
                              name nvarchar(100) not null,
                          );
                    END
                """);
        }
    }
}
