using System;
using TranslateService;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var translate = new GoogleTranslator();
            Console.WriteLine(translate.Translate("hello", Language.en, Language.ru));

        }
    }
}
