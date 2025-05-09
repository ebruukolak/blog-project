﻿using Blogs.Application.Helpers;


namespace Blogs.Application.Models
{
    public class User
    {
        public required Guid Id { get; init; }
        public required string FirstName {  get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required Guid RoleId { get; set; }
        public string PasswordHash{ get; set; }
        //TODO: change logic Password logic
        public required string Password { get; set; }
        public required bool IsDeleted { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public Role role { get; set; }
        public required DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; set; }

       
    }
}
