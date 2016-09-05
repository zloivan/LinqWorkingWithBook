using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.Linq;

namespace Deferred_Operators
{
    public class Employee
    {
        public int Id;
        public string firstName;
        public string secondName;
        //Статический метод который инициализирует набор сотрудников помещает их в Лист и возвращает
        public static ArrayList GetEmplyeesArrayList()
        {
            ArrayList al = new ArrayList();
            al.Add(new Employee { Id = 1, firstName = "Joe", secondName = "Rattz" });
            al.Add(new Employee { Id = 2, firstName = "Edward", secondName = "Elrick" });
            al.Add(new Employee { Id = 3, firstName = "Stive", secondName = "Gates" });
            al.Add(new Employee { Id = 4, firstName = "Anderts", secondName = "Hejlsberg" });
            al.Add(new Employee { Id = 5, firstName = "David", secondName = "Lightman" });
            al.Add(new Employee { Id = 6, firstName = "Kevin", secondName = "Flynn" });
            al.Add(new Employee { Id = 7, firstName = "Williams", secondName = "Daniels" });
            al.Add(new Employee { Id = 8, firstName = "Stewi", secondName = "Stivenson" });
            al.Add(new Employee { Id = 9, firstName = "Carl", secondName = "Lamar" });
            return al;
        }
        //Метод который возвращает массив наших сотрудников
        public static Employee[] GetEmploeesArray()
        {
            return (GetEmplyeesArrayList().Cast<Employee>().ToArray());
        }


        
    }
}
