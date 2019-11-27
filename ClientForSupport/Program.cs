using ClientForSupport.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientForSupport
{

        class Program
        {
            static HttpClient client = new HttpClient();




            static async Task<bool> CreateRefer(Refer refer)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest) WebRequest.Create("http://localhost:50978/api/Support/CreateRefer");
                    request.Method = "Post";
              
                    var json = JsonConvert.SerializeObject(refer);
                        var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                await requestWriter.WriteAsync(refer.ToString());
                    requestWriter.Close();
                //var json = JsonConvert.SerializeObject(refer);
                //    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                //HttpResponseMessage response = await client.PostAsync(
                //        "api/Support/CreateRefer/", stringContent);
                //response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    return false;
                }

                // return URI of the created resource.
                return true;
            }

            //static async Task<Refer> GetReferAsync(string path)
            //{
            //    Refer refer = null;
            //    HttpResponseMessage response = await client.GetAsync(path);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        refer = await response.Content.ReadAsAsync<Refer>();
            //    }
            //    return refer;
            //}

            //static async Task<Refer> UpdateReferAsync(Refer refer)
            //{
            //    HttpResponseMessage response = await client.PutAsJsonAsync(
            //        $"api/refers/{refer.Id}", refer);
            //    response.EnsureSuccessStatusCode();

            //    // Deserialize the updated refer from the response body.
            //    refer = await response.Content.ReadAsAsync<Refer>();
            //    return refer;
            //}

            //static async Task<HttpStatusCode> DeleteReferAsync(string id)
            //{
            //    HttpResponseMessage response = await client.DeleteAsync(
            //        $"api/refers/{id}");
            //    return response.StatusCode;
            //}

            static void Main()
            {
                RunAsync();
                //    TimerCallback timeCB = new TimerCallback(RunAsync);
                //Timer t = new Timer(timeCB, null, 0, 2000);

                ////Timer t = new Timer(RunAsync, 5, 0, 2000);
                //    Console.WriteLine("Main thread: Doing other work here...");
                //    Thread.Sleep(10000);
            }

            static async void RunAsync()
            {
                // Update port # in the following line.
                client.BaseAddress = new Uri("http://localhost:50978/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    // Create a new refer
                    Refer refer = new Refer
                    {
                        ClientName = "Test",
                        Email = "example_test@gmail.ru",
                        Phone = "7999999999",
                        ReferText = "Test text",

                    };

                    var url = await CreateRefer(refer);
                    Console.WriteLine($"Created at {url}");

                // Get the refer
               // refer = await GetReferAsync(url.PathAndQuery);
               // ShowRefer(refer);

               // Update the refer
               //     Console.WriteLine("Updating price...");
               // refer.Price = 80;
               // await UpdateReferAsync(refer);

               // Get the updated refer
               //     refer = await GetReferAsync(url.PathAndQuery);
               // ShowRefer(refer);

               // Delete the refer
               //var statusCode = await DeleteReferAsync(refer.Id);
               // Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.ReadLine();
            }
        }
}

