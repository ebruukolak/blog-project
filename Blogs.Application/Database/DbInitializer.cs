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
                                        parentCategoryId UNIQUEIDENTIFIER,
                                        createdAt datetime not null,
                                        updatedAt datetime
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
                              publishedDate datetime,
                              createdAt datetime not null,
                              updatedAt datetime

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

            await connection.ExecuteAsync("""
                   IF NOT EXISTS 
                   (
                        SELECT * FROM INFORMATION_SCHEMA.TABLES
                        WHERE TABLE_SCHEMA = 'dbo'
                        AND TABLE_TYPE = 'BASE TABLE'
                        AND TABLE_NAME = 'Roles'
                    ) 
                	BEGIN
                			CREATE TABLE  Roles
                			(
                				id UNIQUEIDENTIFIER primary key,
                				name nvarchar(256) not null unique,
                			);
                	END

                """);

            await connection.ExecuteAsync("""
                   IF NOT EXISTS 
                   (
                        SELECT * FROM INFORMATION_SCHEMA.TABLES
                        WHERE TABLE_SCHEMA = 'dbo'
                        AND TABLE_TYPE = 'BASE TABLE'
                        AND TABLE_NAME = 'Users'
                    ) 
                  BEGIN
                        CREATE TABLE  Users
                        (
                            id UNIQUEIDENTIFIER primary key,
                			roleId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Roles(id),
                            email nvarchar(256) not null unique,
                            firstName nvarchar(256) not null,
                            lastName nvarchar(256) not null,
                			passwordHash nvarchar(256) not null,
                            isEmailConfirmed bit,
                			isDeleted bit not null,                           
                            createdAt datetime not null,
                            updatedAt datetime
                        );
                    END
                """);

            await connection.ExecuteAsync("""
                 IF NOT EXISTS 
                 (
                      SELECT * FROM INFORMATION_SCHEMA.TABLES
                      WHERE TABLE_SCHEMA = 'dbo'
                      AND TABLE_TYPE = 'BASE TABLE'
                      AND TABLE_NAME = 'EmailVerificationTokens'
                  ) 
                BEGIN
                      CREATE TABLE  EmailVerificationTokens
                      (
                          id UNIQUEIDENTIFIER primary key,
                       	  userId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Users(id),
                          token nvarchar(max) not null,
                          createdAt datetime not null
                      );
                  END
                """);
        }
    }
}
