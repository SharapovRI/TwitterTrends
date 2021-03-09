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
            hashtableValue.Add("Aladin", 60);
            hashtableValue.Add("Aladin qwe", 23423);
            hashtable.Add("A", hashtableValue);
            Hashtable hashtableValue2 = new Hashtable();
            hashtableValue2.Add("qwe", 13);
            hashtable.Add("q", hashtableValue2);

            foreach (var item in twitts)
            {
                CheckSame(item.Text);
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
                string pat = @"\S\w*";

                Regex regex = new Regex(pat);
                Match match = regex.Match(text);
                comp = match.Value;

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
            string mostClose = string.Empty;
            string pat = @"\w*";
            lenght = comp;

            hashtableValue = (Hashtable)hashtable[comp[0].ToString()];
            variables = new Hashtable(hashtableValue);

            while (variables.Count > 0)
            {
                //variables.Sort();

                foreach (var item in hashtableValue.Keys)
                {
                    if (item.ToString().IndexOf(comp) != 0)
                    {
                        variables.Remove(item);
                    }
                    if (item.ToString() == comp)
                    {
                        mostClose = item.ToString();
                    }
                }

                if (variables.Count == 0)
                {
                    break;
                }

                //mostClose = variables[0];

                pat = pat + @"\s(\a\s)?\w*";

                Regex regex = new Regex(pat);
                Match match = regex.Match(text);
                comp = match.Value;
            }

            lenght = mostClose;
            return Convert.ToSingle(hashtableValue[mostClose]);
        }
    }
}
