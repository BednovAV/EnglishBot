using System;
using TranslateService;
using WordsService;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var words = new Words();

            Console.Write(words.Get());

        }
    }
}
