using CsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhraseologicalLibrary
{
    public class Phraseological
    {
        public List<string> Phraseologiсals { get; }

        public Phraseological()
        {
            Phraseologiсals = new List<string>();
            CQ cq = CQ.CreateFromUrl("https://kuzminaolga.jimdofree.com/%D0%B1%D0%B8%D0%B1%D0%BB%D0%B8%D0%BE%D1%82%D0%B5%D0%BA%D0%B0/%D1%84%D1%80%D0%B0%D0%B7%D0%B5%D0%BE%D0%BB%D0%BE%D0%B3%D0%B8%D0%B7%D0%BC%D1%8B/");
            foreach (var i in cq.Find("p"))
            {
                Phraseologiсals.Add(i.Cq().Text());
            }

            Phraseologiсals.RemoveAt(0);
            Phraseologiсals.RemoveAt(Phraseologiсals.Count() - 1);
        }

        public string Get() => Phraseologiсals[new Random().Next(0, Phraseologiсals.Count)];
        
    }
}
