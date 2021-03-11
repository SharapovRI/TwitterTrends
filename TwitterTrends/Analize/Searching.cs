using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TwitterTrends.Models;

namespace TwitterTrends.Analize
{
    class Searching
    {
        Hashtable hashtable;

        List<Twitt> twitts;

        public Searching(List<Twitt> twitts, Hashtable hashtable, List<State> states)
        {
            this.twitts = twitts;
            this.hashtable = hashtable;

            foreach (var item in twitts)
            {
                var state = states.FindAll(u => u.StateId == item.idState).FirstOrDefault();
                if (state != null)
                {
                    state.weight += CheckSame(item.Text.ToLower());
                }
            }
        }

        private float CheckSame(string text)
        {
            string comp;
            float weight = 0;


            while (text.Length > 0)
            {
                string pat = @"(\a\s)?\w+(\-\w+)?";

                Regex regex = new Regex(pat);
                Match match = regex.Match(text);
                comp = match.Value;

                if (string.IsNullOrWhiteSpace(comp))
                {
                    break;
                }

                string key;
                weight += FindSame(comp, text, out key);
                text = text.Remove(0, text.IndexOf(key) + key.Length);

            }

            return weight;
        }

        private float FindSame(string comp, string text, out string lenght)
        {
            lenght = comp;
            if (comp.Length == 1 && comp != "a")
            {
                return 0;
            }
            string mostClose = string.Empty;
            string pat = @"(\w+)(\-\w+)?";

            while (comp.Length > 0 && comp.Length != text.Trim().Length && !string.IsNullOrWhiteSpace(comp))
            {
                if (hashtable.ContainsKey(comp))
                {
                    mostClose = comp;
                }

                pat += @"\s(\a\s)?(\w+)(\-\w+)?";

                Regex regex = new Regex(pat);
                Match match = regex.Match(text);
                comp = match.Value;
            }
            return Convert.ToSingle(hashtable[mostClose]);
        }
    }
}
