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

            static async Task<bool> CallPostMethod(string method, string postString)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest) WebRequest.Create($"http://localhost:50978/Support/{method}/");
                    request.Method = "Post";
                    request.ContentType = "application/x-www-form-urlencoded";




                    StreamWriter requestWriter = new StreamWriter(await request.GetRequestStreamAsync());
                    requestWriter.Write(postString);
                    requestWriter.Close();
                    request.GetResponse().Close();


                }
                catch (Exception ex)
                {
                    return false;
                }

                return true;
            }

            

            static void Main()
            {
            var timerRefer = new Timer( async (object e)=>{
                    await CreateReferAsync();
                }, null, 0, 1000000);

                var timerWorker = new Timer(async (object e) => {
                    await CreateWorkerAsync();
                }, null, 0, 2000000);
            Console.ReadLine();
        }

            static async Task CreateReferAsync()
            {
                client.BaseAddress = new Uri("http://localhost:50978/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var ran = new Random();
                    var ran_a = ran.Next(1, 100);
                    var ran_b = ran.Next(10, 99);
                    // Create a new refer
                    Refer refer = new Refer
                    {
                        ClientName = "Test" + ran_a,
                        Email = $"example_test{ran_a}@gmail.ru",
                        Phone = $"7999999{ran_b}{ran_b}",
                        ReferText = $"Test text. {ran_a}!",

                    };

                string postString = string.Format("ClientName={0}&Email={1}&Phone={2}&ReferText={3}", refer.ClientName, refer.Email, refer.Phone, refer.ReferText);
                var result = await CallPostMethod("CreateRefer", postString);
                    Console.WriteLine($"Created? {result}!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.ReadLine();
            }
            static async Task CreateWorkerAsync()
            {
                client.BaseAddress = new Uri("http://localhost:50978/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var ran = new Random();
                    var ranName = ran.Next(1, 10000);
                    var ranType = ran.Next(0, 2);
                // Create a new refer
                    Worker worker = new Worker()
                    {
                        Name = "TestWorker" + ranName,
                        Type = ranType

                    };
                    string postString = string.Format("Name={0}&Type={1}", worker.Name, worker.Type);
                    var result = await CallPostMethod("CreateEditWorker", postString);
                    Console.WriteLine($"Created? {result}!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.ReadLine();
            }
    }
}

