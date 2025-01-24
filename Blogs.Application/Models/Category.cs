using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blogs.Application.Models
{
    public partial class Category
    {
        public required Guid Id { get; init; }
        public required string Name { get; set; }
        public string Slug => GenerateSlug();
        public string Description { get; set; }
        public Guid? ParentCategoryId { get; set; }

        private string GenerateSlug()
        {
            var sluggedName = SluggedNameRegex().Replace(Name, string.Empty)
                .ToLower().Replace(" ", "-");

           return sluggedName;
       }

        [GeneratedRegex("^[a-z0-9-]", RegexOptions.NonBacktracking,5)]
        private static partial Regex SluggedNameRegex();
    }
}
