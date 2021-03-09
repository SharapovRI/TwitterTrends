using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace TwitterTrends.Parsers
{
    public static class SantimentsParser
    {
        //public static Dictionary<string, float> ParseWords(string filePath)
        public static Hashtable ParseWords(string filePath)
        {
            /*Dictionary<string, float> dictionary = new Dictionary<string, float>();
            string line;
            using (StreamReader sr = new StreamReader(filePath))
            {
                while(!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    var wordWithValue = line.Split(',');
                    string word = wordWithValue[0];
                    float value = float.Parse(wordWithValue[1].Replace('.', ','));
                    dictionary.Add(word, value);
                }
            }
            return dictionary;*/

            Hashtable mainHashtable = new Hashtable(); 
            string line;
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    var wordWithValue = line.Split(',');
                    string word = wordWithValue[0];
                    float value = float.Parse(wordWithValue[1].Replace('.', ','));
                    if (mainHashtable.ContainsKey(word[0].ToString()))
                    {
                        Hashtable hashT = (Hashtable)mainHashtable[word[0].ToString()];
                        hashT.Add(word, value);
                        mainHashtable[word[0].ToString()] = hashT;
                    }
                    else
                    {
                        Hashtable hashT = new Hashtable();
                        hashT.Add(word, value);
                        mainHashtable.Add(word[0].ToString(), hashT);
                    }                    
                }
            }

            return mainHashtable;
        }
    }
}
