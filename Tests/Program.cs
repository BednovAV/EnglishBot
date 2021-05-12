using PhraseologicalLibrary;
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
            Phraseological ph = new Phraseological();
            Console.WriteLine(ph.Get());
            Console.WriteLine(ph.Get());
            Console.WriteLine(ph.Get());
            Console.WriteLine(ph.Get());
            Console.WriteLine(ph.Get());

        }
    }
}
