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
        //Dictionary<State, float> sentim = new Dictionary<State, float>();
        Hashtable hashtable;
        Hashtable hashtableValue;

        List<Twitt> twitts;

        public Searching(List<Twitt> twitts, Hashtable hashtable)
        {
            this.twitts = twitts;
            this.hashtable = hashtable;

            foreach (var item in twitts)
            {
                item.weight = CheckSame(item.Text.ToLower());
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

                //if (hashtable.ContainsKey(comp[0].ToString()))
                //{
                    string key;
                    weight += OQIWje(comp, text, out key);
                    text = text.Remove(0, text.IndexOf(key) + key.Length);
                //}
                //else
                //{
                    //text = text.Remove(0, text.IndexOf(comp) + comp.Length);
                //}
            }

            return weight;
        }

        private float OQIWje(string comp, string text, out string lenght)
        {
            lenght = comp;
            if (comp.Length == 1 && comp != "a")
            {
                return 0;
            }
            //Hashtable variables;
            //Hashtable variables2;
            string mostClose = string.Empty;
            string pat = @"(\w+)(\-\w+)?";

            hashtableValue = (Hashtable)hashtable[comp[0].ToString()];
            //variables = new Hashtable(hashtableValue);
            /*variables2 = new Hashtable(hashtableValue);

            while (variables.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(comp)) return 0;
                foreach (var item in variables.Keys)
                {
                    if (item.ToString().IndexOf(comp) != 0)
                    {
                        variables2.Remove(item);
                    }
                    if (item.ToString() == comp)
                    {
                        mostClose = item.ToString();
                    }
                }

                variables = new Hashtable(variables2);

                if (variables.Count == 0 || mostClose == text.Trim())
                {
                    break;
                }

                pat = pat + @"\s(\a\s)?(\w+)(\-\w+)?";

                Regex regex = new Regex(pat);
                Match match = regex.Match(text);
                comp = match.Value;
            }

            if (!string.IsNullOrWhiteSpace(mostClose))
            {
                lenght = mostClose;
            }

            return Convert.ToSingle(hashtableValue[mostClose]);*/
            if (hashtableValue is null) return 0;

            while(comp.Length > 0 && comp.Length != text.Trim().Length && !string.IsNullOrWhiteSpace(comp))
            {
                if (hashtableValue.ContainsKey(comp))
                {
                    mostClose = comp;
                }

                pat += @"\s(\a\s)?(\w+)(\-\w+)?";

                Regex regex = new Regex(pat);
                Match match = regex.Match(text);
                comp = match.Value;
            }
            return Convert.ToSingle(hashtableValue[mostClose]);
        }
    }
}
