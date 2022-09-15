using Microsoft.AspNet.SignalR.Client;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Trigger
{
    class Reader
    {
        private int TagCount = 0;
        private List<string> Tags = new List<string>();
        private List<Asset> NotAllowedTags  = new List<Asset>();
        System.Timers.Timer CollectingTimer = new System.Timers.Timer(1000);

        public void AddTagtoCollections(string Tag, string Ant)
        {
            if (!Tags.Contains(Tag + "," + Ant))
            {
                Tags.Add(Tag + "," + Ant);

            }

        }
        public async void StartConnection()
        {
            try
            {
                CollectingTimer.Elapsed += new ElapsedEventHandler(TimerCallback);
                CollectingTimer.Start();
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(Tags);
                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");


                    var response = await client.GetAsync("https://localhost:7259/asset/GetNotAllowed");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();

                         NotAllowedTags = JsonConvert.DeserializeObject<List<Asset>>(content);
                    }

                }

                var connection = new HubConnection("http://192.168.0.251:5501/signalr");
                var myHub = connection.CreateHubProxy("connectionHub");

                connection.Start().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                    }
                    else
                    {
                        Console.WriteLine("Listening...");
                    }

                }).Wait();

                if (NotAllowedTags.Count > 0)
                {

                    myHub.On("ListenToData", (string Tag, string Ant) =>
                    {
                        Console.WriteLine(Tag + ":" + Ant);

                        if (Ant == "Ant3" || Ant == "Ant4")
                        {
                            var count = NotAllowedTags.Where(a => a.Tag == Tag).ToList();
                            if (count.Count() > 0)
                            {

                                 File.Create(@"C:\NVS\notallowed.txt").Close() ;

                                using (StreamWriter sw = File.CreateText(@"C:\NVS\notallowed.txt"))
                                {
                                    sw.WriteLine(count[0].Itemname);
                                }
                            }
                        }


                    });

                }
            }
            catch (Exception ex)
            {

            }
        }
        private async void TimerCallback(object source, ElapsedEventArgs e)
        {
            NotAllowedTags.Clear();

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(Tags);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await client.GetAsync("https://localhost:7259/asset/GetNotAllowed");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    NotAllowedTags = JsonConvert.DeserializeObject<List<Asset>>(content);
                }

            }
        }

    }
}
