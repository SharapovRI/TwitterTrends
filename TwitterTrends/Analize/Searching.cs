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
        Dictionary<State, float> sentim = new Dictionary<State, float>();
        Hashtable hashtable = new Hashtable();
        Hashtable hashtableValue = new Hashtable();

        List<Twitt> twitts = new List<Twitt>();

        public Searching(List<Twitt> twitts, Hashtable hashtable)
        {
            this.twitts = twitts;
            this.hashtable = hashtable;
            /*hashtableValue.Add("Aladin", 60);
            hashtableValue.Add("Aladin qwe", 23423);
            hashtable.Add("A", hashtableValue);
            Hashtable hashtableValue2 = new Hashtable();
            hashtableValue2.Add("qwe", 13);
            hashtable.Add("q", hashtableValue2);*/

            foreach (var item in twitts)
            {
                CheckSame(item.Text.ToLower());
            }
        }

        private void CheckSame(string text)
        {
            string comp;
            float weight = 0;                                     ///////вот эта вещь, это вес твитта.   Она должна передаваться и записываться в штат соответственно
                                                                  // так вот проблема, у меня нет ни парсера чтобы проверить работу
                                                                  //ни принадлежности твитта штату, чтобы доделать
            while (text.Length > 0)
            {                                                                                               //ЮЛИК ИЛЬЯ АЛООООООООООООООООООО
                string pat = @"(\a\s)?\w+(\-\w+)?";

                Regex regex = new Regex(pat);
                Match match = regex.Match(text);
                comp = match.Value;

                if (string.IsNullOrWhiteSpace(comp))
                {
                    break;
                }

                if (hashtable.ContainsKey(comp[0].ToString()))
                {
                    string key;
                    weight += OQIWje(comp, text, out key);
                    text = text.Remove(0, text.IndexOf(key) + key.Length);
                }
                else
                {
                    text = text.Remove(0, text.IndexOf(comp) + comp.Length);
                }
            }
        }

        private float OQIWje(string comp, string text, out string lenght)
        {
            Hashtable variables;
            Hashtable variables2;
            string mostClose = string.Empty;
            string pat = @"(\w+)(\-\w+)?";
            lenght = comp;

            hashtableValue = (Hashtable)hashtable[comp[0].ToString()];
            variables = new Hashtable(hashtableValue);
            variables2 = new Hashtable(hashtableValue);

            while (variables.Count > 0)
            {
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

            return Convert.ToSingle(hashtableValue[mostClose]);
        }
    }
}
