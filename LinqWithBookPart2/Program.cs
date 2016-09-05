using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Collections;

namespace LinqWithBookPart2
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Parse an ToArray
            string[] numbers = { "004,2", "010", "9", "27" };

            double[] nums = numbers.Select(s => Double.Parse(s)).ToArray();

            foreach (var num in nums)
                Console.WriteLine(num);
            #endregion

            
            Console.WriteLine("---Next part of a programm---");

            #region Working with different namespaces
            //Создаем массив сотруднико и заполняем его значениями используя метод GetEmployees
            ArrayList alEmployees = LinqDev.HR.Employee.GetEmployees();
            ///создаем масси контактов, заполняем его значениями из массива сотруднико
            ///предварительно преобразуем сотрудников в IEnumerable при помощи cast
            ///далее выбираем из этого массива значения e.id efirstname e.secondname
            ///и присваеваем их значениям эелементов контакты, из которых в последствии
            ///формируем массив.
            LinqDev.Common.Contact[] contacts = alEmployees.
                Cast<LinqDev.HR.Employee>().Select(e => new LinqDev.Common.Contact
                {
                    Id = e.id,
                    Name = string.Format("{0} {1}", e.firstName, e.lastName)
                }).ToArray<LinqDev.Common.Contact>();

            LinqDev.Common.Contact.PublishContacts(contacts);
            #endregion
            Console.WriteLine("---Next part of the programm---");
            #region Working with TypeOf an Cast
            //Это наследуемая коллекция которая не реализует интерфейс IEnumerable
            ArrayList araylist = new ArrayList();
            araylist.Add("Adams");
            araylist.Add("Arthur");
            araylist.Add("Buchanan");
            //для того что бы работать с IEnumerable кастуем наш элементы листа в стринги
            // надо заметить что параметрами  ArrayList являются object 
            IEnumerable<string> names = araylist.Cast<string>().Where(n => n.Length < 7);
            foreach (var name in names)
            {
                Console.WriteLine("Name: {0}",name);
            }

            //то же самое только с TypeOf, разница в том что каст ыдаст исключение если
            //дать ему для каста не подходящий тип, TypeOf просто пропустит этот обьект
            ArrayList araylist1 = new ArrayList();
            araylist1.Add("Adams");
            araylist1.Add("Arthur");
            araylist1.Add(35);
            
            //для того что бы работать с IEnumerable выбираем наши элементы листа которые
            // можно переделать в стринги стринги
            // надо заметить что параметрами  ArrayList являются object 
            IEnumerable<string> names1 = araylist1.OfType<string>().Where(n => n.Length < 7);
            foreach (var name in names)
            {
                Console.WriteLine("Name: {0}", name);
            }


            #endregion
        }
    }
}
namespace LinqDev.Common
{
    public class Contact
    {
        public int Id;
        public string Name;
        public static void PublishContacts(Contact[] contacts)
        {
            foreach(var contact in contacts)
            {
                Console.WriteLine("Contact Id: {0} Contact: {1}",
                    contact.Id,contact.Name);
            }
        }
    }
}

namespace LinqDev.HR
{
    public class Employee
    {
        public int id;
        public string firstName;
        public string lastName;


        public static ArrayList GetEmployees()
        {
            ArrayList al = new ArrayList();
            al.Add(new Employee { id = 1, firstName = "Joe", lastName = "Rattz" });
            al.Add(new Employee { id = 2, firstName = "William", lastName = "Gates" });
            al.Add(new Employee {id=3,firstName="Anders",lastName="Hejlsberg" });
            return al;
        }
    }
}
