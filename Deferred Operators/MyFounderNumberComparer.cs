using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deferred_Operators
{
    public class MyFounderNumberComparer<T> : IEqualityComparer<T>
    {
        
        public bool Equals(T x, T y)
        {
            dynamic temp1 = (dynamic)x;
            dynamic temp2 = (dynamic)y;
            return (isFounder(temp1) == isFounder(temp2));
        }
        public string FounderName
        {

            set { FounderNames.Add(value); }
        }
        List<string> FounderNames = new List<string>{"Edward", "Joe","Steve","Stive"};
        public bool isFounder(string  firstname)
        {
            
            foreach (string name in FounderNames)
            {
                if(firstname == name)
                return firstname == name;
            }

            return false;
        }
        public bool isFounder(int id)
        {
            return id < 100;


        }

        public int GetHashCode(T i)
        {
            dynamic temp = (dynamic)i;
            
            int f = 1;
            int nf = 100;
            return (isFounder(temp) ? f.GetHashCode() : nf.GetHashCode());

        }

        
    }
}
