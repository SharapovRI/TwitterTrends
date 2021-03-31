using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterTrends.Models;

namespace DataObjects
{
    public interface IDataDao
    {
        List<State> ParseStates(string pathToFile);

        Hashtable ParseSantiments(string filePath, ref HashSet<string> hashset);

        List<Tweet> Twittparce(string path);
    }
}
