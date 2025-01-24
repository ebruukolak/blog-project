using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blogs.Application.Models
{
    public partial class Post
    {
        public required Guid Id { get; init; }
        public required string Title { get; set; }
        public string Slug => GenerateSlug();
               
        public required string Content { get; set; }
        //public required int AuthorId { get; set; }
        public required Guid CategoryId { get; set; }
        public required List<string> Tags { get; init; } = new();
        public required bool IsDraft { get; set; }
        public DateTime PublishedDate { get; set; }

        private string GenerateSlug()
        {
            var sluggedTitle = SluggedRegex().Replace(Title, string.Empty)
                .ToLower().Replace(" ","-");

            if (!sluggedTitle.EndsWith("-"))
            {
                sluggedTitle += "-";
            }
            return $"{sluggedTitle}{PublishedDate.Year}";
        }
      
        [GeneratedRegex("^[a-z0-9-]", RegexOptions.NonBacktracking,5)]
        private static partial Regex SluggedRegex();
    }
}
