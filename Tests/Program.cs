using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            string WIT_API_HOST = "https://api.wit.ai";
            string WIT_API_VERSION = "20210510";

            RestClient client = new RestClient(WIT_API_HOST);

            string accessToken = "Y7G3T2HIFXOB7VL57BLSLOULBBHFKIE3";


            client.AddDefaultHeader("Authorization", $"Bearer {accessToken}");
            client.AddDefaultHeader("Content-Type", "audio/ogg");
            client.AddDefaultParameter("v", WIT_API_VERSION, ParameterType.QueryString);

            WebRequest requestStream = WebRequest.Create("https://api.telegram.org/file/bot1703819356:AAEUjQlD8znMe-jNIGulCD_nnFUYuNcoDcE/voice/file_0.oga");

            byte[] data;
            using (StreamReader stream = new StreamReader(requestStream.GetResponse().GetResponseStream()))
            {
                data = Encoding.UTF8.GetBytes(stream.ReadToEnd());
            }

            

            var request = new RestRequest("speech", Method.POST);
            
            request.AddFileBytes("data-binary", data, "file_0.ogg", "audio/ogg");
            request.
            //request.AddQueryParameter("q", "Hello");
            //request.AddFile("data-binary", @"C:\Users\ОАО РЖД\Downloads\file_0.wav");

            IRestResponse responseObject = client.Execute(request);
            Console.WriteLine(responseObject.StatusCode);
            Console.WriteLine(responseObject.Content);
            //MessageResponse response = JsonConvert.DeserializeObject<MessageResponse>(responseObject.Content);

        }
    }
}
