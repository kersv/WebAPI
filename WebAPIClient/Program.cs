using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace WebAPIClient
{

    class Ghibli
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("age")]
        public string Age { get; set; }

        [JsonProperty("hair_color")]
        public string HairColor { get; set; }
    }


    class Program
    {

        private static readonly HttpClient client = new HttpClient();

        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {

                    Console.WriteLine("Enter Ghibli character's ID. Press Enter without writing an ID will quit the program.");
                    var ghibliID = Console.ReadLine();
                    if (string.IsNullOrEmpty(ghibliID))
                    {
                        Console.WriteLine("Terminated!");
                        break;
                    }
                    var result = await client.GetAsync("https://ghibliapi.herokuapp.com/people/" + ghibliID);
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var res = JsonConvert.DeserializeObject<Ghibli>(resultRead);
                
                    Console.WriteLine("---");
                    Console.WriteLine("ghibli id: " + res.Id);
                    Console.WriteLine("Name: " + res.Name);
                    Console.WriteLine("Age: " + res.Age);
                    Console.WriteLine("Hair Color: " + res.HairColor);
                    Console.WriteLine("\n---");
                }

                catch (Exception)
                {
                    Console.WriteLine("Error. Please enter valid Ghibli ID");
                }
                
            }
        }
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }
    }

}
