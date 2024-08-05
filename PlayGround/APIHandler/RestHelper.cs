using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;



namespace PlayGround.APIHandler
{

    internal class RestHelper
    {
        private static readonly HttpClient client = new HttpClient();

        private static readonly string baseURL = "https://reqres.in/api/";

        static RestHelper()
        {
            client.BaseAddress = new Uri("https://reqres.in/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<string> PostLoginAuth(string username, string password)
        {
            var inputData = new Dictionary<string, string>
            {
                { "username" , username },
                { "password" , password }
            };

          
            var input = new FormUrlEncodedContent(inputData);

           

                using (HttpResponseMessage res = await client.PostAsync(baseURL + "login", input))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();

                        if (data != null)
                        {
                            Console.WriteLine(data);

                            return data;
                        }
                    }
                }
            
            return string.Empty;
        }

    }
}