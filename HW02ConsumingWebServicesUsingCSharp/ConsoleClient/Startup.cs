namespace ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;

    using Newtonsoft.Json;

    public class Startup
    {
        public static void Main(string[] args)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://jsonplaceholder.typicode.com/");

            Console.WriteLine("This is a Console client for searching online posts.");
            Console.WriteLine("Enter a title: (you can try those titles: facere, iusto, sed, sunt)");

            var inputTitle = Console.ReadLine();
            inputTitle = inputTitle.Trim();

            var posts = GetPosts(client)
                .Where(p => p.Title.Contains(inputTitle))
                .ToList();

            if (posts.Count == 0)
            {
                Console.WriteLine("There is no posts matching the title '{0}'", inputTitle);
            }
            else
            {
                Console.WriteLine("There are {0} posts matching the title '{1}'\n", posts.Count, inputTitle);

                foreach (var post in posts)
                {
                    Console.WriteLine(new string('-', 20));
                    Console.WriteLine("Id - {0}", post.Id);
                    Console.WriteLine("Title - {0}", post.Title);
                    Console.WriteLine(new string('-', 20));
                }
            }
        }

        private static List<Post> GetPosts(HttpClient client)
        {
            var response = client.GetAsync("posts").Result;
            var json = response.Content.ReadAsStringAsync().Result;

            var list = JsonConvert.DeserializeObject<List<Post>>(json);

            return list;
        }
    }
}
