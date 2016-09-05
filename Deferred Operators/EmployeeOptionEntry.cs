using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deferred_Operators
{
    public class EmployeeOptionEntry
    {
        public int Id;
        public long optionCount;
        public DateTime dateAwarded;

        public static EmployeeOptionEntry[] GetEmployeeOptionEntries()
        {
            EmployeeOptionEntry[] empOption = new EmployeeOptionEntry[]
            {
                new EmployeeOptionEntry{Id=1,optionCount=2,dateAwarded=DateTime.Parse("1999/12/31")},
                new EmployeeOptionEntry{Id=2,optionCount=10000,dateAwarded=DateTime.Parse("1992/06/30")},
                new EmployeeOptionEntry{Id=2,optionCount=10000,dateAwarded=DateTime.Parse("1994/01/01")},
                new EmployeeOptionEntry{Id=3,optionCount=5000,dateAwarded=DateTime.Parse("1997/09/30")},
                new EmployeeOptionEntry{Id=2,optionCount=10000,dateAwarded=DateTime.Parse("2003/04/01")},
                new EmployeeOptionEntry{Id=3,optionCount=7500,dateAwarded=DateTime.Parse("1998/09/30")},
                new EmployeeOptionEntry{Id=3,optionCount=7500,dateAwarded=DateTime.Parse("1998/09/30")},
                new EmployeeOptionEntry{Id=4,optionCount=1500,dateAwarded=DateTime.Parse("1997/12/31")},
                new EmployeeOptionEntry{Id=101,optionCount=2,dateAwarded=DateTime.Parse("1998/12/31")},
            };
            return empOption;
        }

    }
}
