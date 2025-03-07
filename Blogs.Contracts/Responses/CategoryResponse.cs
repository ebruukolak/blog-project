﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Contracts.Responses
{
    public class CategoryResponse
    {
        public required Guid Id { get; init; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string Description { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
