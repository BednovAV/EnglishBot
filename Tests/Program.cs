using System;
using TranslateService;
using WordsService;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            GoogleTranslator tr = new GoogleTranslator();
            Console.WriteLine(tr.TranslateEnToRu("hello"));

        }
    }
}
