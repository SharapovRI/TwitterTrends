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
        public static Hashtable ParseWords(string filePath, ref HashSet<string> hashset)
        {
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
                    mainHashtable.Add(word, value);
                    var words = word.Split(' ');
                    foreach (var item in words)
                    {
                        hashset.Add(item);
                    }
                }
            }

            return mainHashtable;
        }
    }
}
