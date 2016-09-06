using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deferred_Operators
{
    public class MyVowelToConsonantRatioComparer : IComparer<string>
    {

        public int Compare(string x, string y)
        {
            int vCount1 = 0;
            int cCount1 = 0;
            int vCount2 = 0;
            int cCount2 = 0;

            GetVowelConsonantCount(x, ref vCount1, ref cCount1);
            GetVowelConsonantCount(y, ref vCount2, ref cCount2);

            double dRatio1 = (double)vCount1 / (double)cCount1;
            double dRatio2 = (double)vCount2 / (double)cCount2;
            if (dRatio1 < dRatio2)
                return (-1);
            else if (dRatio1 > dRatio2)
                return (1);
            else return (0);
        }

        //This method is public so our code using this comparer can get the values if it wants.
        public void GetVowelConsonantCount(string s, ref int vowelCount, ref int consonantCount)
        {
            string vowels = "AEIOUY";

            //Initialize the counts.
            vowelCount = 0;
            consonantCount = 0;

            //Convert to uppercase so we are case insensitive
            string sUpper = s.ToUpper();

            foreach (char item in sUpper)
            {
                if (vowels.IndexOf(item) < 0)
                    consonantCount++;
                else
                    vowelCount++;
            }
            return;
        }
    }
}
