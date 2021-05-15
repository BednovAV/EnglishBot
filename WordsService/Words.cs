using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WordsService
{
    public class Words
    {
        List<string> _words;

        public Words()
        {
            string wordsJson;

            using (var webClient = new WebClient())
            {
                wordsJson = webClient.DownloadString(@"https://drive.google.com/uc?export=download&confirm=no_antivirus&id=12xjxj2S0OZJozamFnp-e2760h8sXTcQk");
            }
            _words = JsonConvert.DeserializeObject<List<string>>(wordsJson);
        }

        public string Get() => _words[new Random().Next(_words.Count)];
    }
}
