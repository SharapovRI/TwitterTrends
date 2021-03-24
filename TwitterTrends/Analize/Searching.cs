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
        HashSet<string> hashset;
        List<Twitt> twitts;

        public Searching(List<Twitt> twitts, Hashtable hashtable, List<State> states, HashSet<string> hashset)
        {
            this.twitts = twitts;               //твиты в которых есть текст
            this.hashtable = hashtable;         //хештаблица
            this.hashset = hashset;             //все слова из сантиментов

            foreach (var item in twitts)
            {
                /*var state = states.FindAll(u => u.StateId == item.idState).FirstOrDefault();
                if (state != null)
                {*/
                if (item.weight != null)
                {
                    item.weight += CheckSame(item.Text.ToLower());
                }
                else item.weight = CheckSame(item.Text.ToLower());
                //}
            }
        }

        private float? CheckSame(string text)
        {
            string comp;
            float? weight = 0;
            bool isWeightNull = true;

            var sentences = text.Split('.', '!', '?', ':', ';', ',', '"', '(', ')');
            foreach (var item in sentences)
            {
                text = item;
                var words = text.Trim().Split(' ');
                int i = 0;
                while (i < words.Length)
                {
                    if (isWeightNull && weight != 0)
                    {
                        isWeightNull = false;
                    }
                    comp = words[i];

                    if (string.IsNullOrWhiteSpace(comp))
                    {
                        break;
                    }
                    if (!hashset.Contains(comp))
                    {
                        i++;
                        continue;
                    }
                    weight += FindSame(comp, words, ref i);
                    i++;
                }
            }

            if (!isWeightNull)
            {
                return weight;
            }
            else return null;
        }

        private float FindSame(string comp, string[] text, ref int lenght)
        {
            int i = lenght;
            string mostClose = string.Empty;
            if (hashtable.ContainsKey(comp))
            {
                mostClose = comp;
            }

            while (comp.Length > 0 && !string.IsNullOrWhiteSpace(comp) && i < text.Length - 1)
            {
                if (hashtable.ContainsKey(comp))
                {
                    mostClose = comp;
                    lenght = i;
                }
                comp += ' ' + text[++i];
                if (!hashset.Contains(text[i]))
                {
                    break;
                }
            }

            return Convert.ToSingle(hashtable[mostClose]);
        }
    }
}
