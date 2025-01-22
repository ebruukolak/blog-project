namespace Blogs.API
{
    public static class ApiEndpoints
    {
        private const string ApiBase = "api";

        public static class Category
        {
            private const string Base = $"{ApiBase}/categories";
            public const string Create = Base;
            public const string Get = $"{Base}/{{id:guid}}";
            public const string GetAll = Base;
            public const string Update = $"{Base}/{{id:guid}}";
            public const string Delete = $"{Base}/{{id:guid}}";
        }

        public static class  Post
        {
            private const string Base = $"{ApiBase}/posts";
            public const string Create = Base;
            public const string Get = $"{Base}/{{id:guid}}";
            public const string GetAll = Base;
            public const string Update = $"{Base}/{{id:guid}}";
            public const string Delete = $"{Base}/{{id:guid}}";
        }
    }
}
