using System;
namespace TweetBook.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string ApiVersion = "v1";

        public const string Base = Root+"/"+ApiVersion;

        public static class Posts
        {
            public const string GetAll = Base+"/posts";
            public const string Get = Base+"/posts/{postId}";
            public const string Create = Base + "/posts";
            public const string Update = Base+"/posts/{postId}";
            public const string Delete = Base+"/posts/{postId}";


        }
    }
}
