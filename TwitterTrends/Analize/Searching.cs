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

        Searching(List<Twitt> twitts, Hashtable hashtable)
        {
            this.twitts = twitts;
            this.hashtable = hashtable;

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
                                                                                                    //ЮЛИК ИЛЬЯ АЛООООООООООООООООООО
            string pat = @"\w*";

            Regex regex = new Regex(pat);
            Match match = regex.Match(text);
            comp = match.Value;

            if (hashtable.ContainsKey(comp[0]))
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

        private float OQIWje(string comp, string text, out string lenght)
        {
            List<string> variables = new List<string>();
            string mostClose = string.Empty;
            string pat = @"\w*";
            lenght = comp;

            hashtableValue = (Hashtable)hashtable[comp[0]];
            variables = (List<string>)hashtableValue.Keys;

            while (variables.Count > 0)
            {
                variables.Sort();

                foreach (var item in variables)
                {
                    if (item.IndexOf(comp) != 0)
                    {
                        variables.Remove(item);
                    }
                }

                if (variables.Count == 0)
                {
                    break;
                }

                mostClose = variables[0];

                pat = pat + @"\s(\a\s)?\w*";

                Regex regex = new Regex(pat);
                Match match = regex.Match(text);
                comp = match.Value;
            }

            lenght = mostClose;
            return (float)hashtableValue[mostClose];
        }
    }
}
