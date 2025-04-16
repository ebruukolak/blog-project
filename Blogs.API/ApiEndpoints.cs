using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Blogs.API
{
    public static class ApiEndpoints
    {
        private const string ApiBase = "api";

        public static class Category
        {
            private const string Base = $"{ApiBase}/categories";
            public const string Create = Base;
            public const string Get = $"{Base}/{{idOrSlug}}";
            public const string GetAll = Base;
            public const string Update = $"{Base}/{{id:guid}}";
            public const string Delete = $"{Base}/{{id:guid}}";
        }

        public static class  Post
        {
            private const string Base = $"{ApiBase}/posts";
            public const string Create = Base;
            public const string Get = $"{Base}/{{idOrSlug}}";
            public const string GetAll = Base;
            public const string Update = $"{Base}/{{id:guid}}";
            public const string Delete = $"{Base}/{{id:guid}}";
        }

        public static class Auth
        {
            private const string Base = $"{ApiBase}/auth";
            public const string Register = $"{Base}/register";
            public const string EmailConfirmation = $"{Base}/confirm-email";
            public const string ConfirmEmail = $"{EmailConfirmation}/{{userId:guid}}&{{token}}";
            public const string Login = $"{Base}/login";
        }

        public static class Role
        {
            private const string Base = $"{ApiBase}/roles";
            public const string Create = Base;
            public const string Get = $"{Base}/{{id:guid}}";
            public const string GetAll = Base;
            public const string Delete = $"{Base}/{{id:guid}}";
        }
    }
}
