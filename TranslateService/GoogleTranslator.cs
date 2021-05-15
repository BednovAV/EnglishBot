using System;
using System.Net;
using System.Web;

namespace TranslateService
{
    public class GoogleTranslator
    {

        public string Translate(string word, Language languageFrom, Language languageTo)
        {
            var fromLanguage = languageFrom.ToString();
            var toLanguage = languageTo.ToString();

            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                return result;
            }
            catch
            {
                return "Error";
            }
        }
    }
}
