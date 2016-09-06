using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToObjectsIntroduction
{

    class MainConsoleClass
    {

        static void Main(string[] args)
        {

            string[] presidents = {"Garfield","Carter","Buchanan","Bush","Obama",
                                      "Cleveland","Clinton","Coolidge","Eisenhower","Fillmore",
                                      "Ford","Putin","Grant","Lincoln" };

            #region Sort presidents: String[]
            
            //Make from IEnumerable<string> an array of string. ToArray<string>
            Console.WriteLine("Unsorted-------------");
            
            foreach (var item in presidents)
            {
                Console.WriteLine(item);
            }
           
            string[] sortedPresidents = presidents.OrderBy(p => p).ToArray<string>();
           
            presidents = sortedPresidents;


            Console.WriteLine("Sorted--------------");
            foreach (var item in presidents)
            {
                Console.WriteLine(item);

            }

            #endregion
            string president = presidents.Where(p => p.StartsWith("Lin")).First();
            Console.WriteLine(president);
            
            IEnumerable<string> oddPesedents = presidents.Where((p, i) => (i & 1) == 1);
            foreach (var item in presidents)
            {
                Console.Write(item+" ");
            }
            //wtf is going on
            Console.WriteLine();
            Console.WriteLine("------------------Returning all odd presidents---------------------------");
            foreach (var item in oddPesedents)
            {

                Console.WriteLine(item);
            }
            


            Console.WriteLine("----------------Next part of a program------------------");

            #region Enumirating not in moment Queuering is executed
            //where исполняется не во время запуска построения очереди, а в момент вызова каждого
            // элемента очереди
            // ЭТОТ КОД ВЫЗОВЕТ ОШИБКУ

            //Здесь вроде как начинается запонлнение items по предикату s => Char.IsLower(s[4])
            IEnumerable<string> items = presidents.Where(s => Char.IsLower(s[4]));
            Console.WriteLine("After the query");

            //Ошибка будет вызвана во время исполнения кода только после 4ого элемента, так как
            //длинна четвертой строки короче 4 символов, а наш предикат как раз проверяет
            //является ли 4ый элемент заглавным или прописным.
            //foreach (var item in items)
            //{
            //    Console.WriteLine(item);
            //} 
            #endregion

            #region Пример демонстрирующий изменения результатов очередей во время перечисления
           //Создаем массив Интов
            int[] intArray = {1,2,3};
            IEnumerable<int> ints = intArray.Select(i=>i);

            //Выводим результаты
            foreach (var i in ints)
            {
                Console.WriteLine(i);
            }
            
            //Меняем элемент в исходном ресурсе
            intArray[0] = 5;
            Console.WriteLine("--------");
            
            //Выводим результат
            foreach (var i in ints)
            {
                Console.WriteLine(i);
            }
            //Для ясности, мы пихаем нашу выборку из intArray в ints и после этого распечатываем ints.
            //после этого мы меняем значение одного из элементов intArray НО не пихаем выборку из intArray
            //в ints и еще раз выводим ints, и значение в нем поменялось. Хотя в коде мы заполняем ints 
            //элементами из inArray до того как меняем элемент в intArray. Это должно показать, что заполнение
            // ints происходит  в момент вывода ints но не в момент создания выборки. т.е когда мы присваеваем
            //очереди ints выборку из intArray мы помещаем туда алгаритм по которому нужно делать выборку
            //но сама выборка произведется при первом запросе ints.
            #endregion
            Console.WriteLine("----------------------Next part of a program-------------------------");
            #region Returning a List so the Query is Executed Immediately and the Results are Cached
            List<int> intsAsList = intArray.Select(i=>i).ToList();
            foreach (var item in intsAsList)
            {
                Console.WriteLine(item);
            }
            intArray[1] = 5;//this change will not affect 
            foreach (var item in intsAsList)
            {
                Console.WriteLine(item);
            }
            #endregion
            Console.WriteLine("----------------------Using two styles together-------------------------");
            
            #region Query Expression Syntax and Dot Notation Syntax mixed
            IEnumerable<int> oddNumbers = (from n in intArray
                                           where n % 2 == 1
                                           select n).Reverse();
            IEnumerable<int> evenNumbers = (from n in intArray
                                            where n % 2 == 0

                                            select n).Reverse();
            string str = "";
            foreach (var item in intArray)
            {
                str += item.ToString();
                str += " ";
            }
            Console.WriteLine("Odd numbers" + str);
            foreach (var item in oddNumbers)
            {
                Console.WriteLine(item);
            }



            Console.WriteLine("Even numbers" + str);
            foreach (var item in evenNumbers)
            {
                Console.WriteLine(item);
            } 
            #endregion

        }
    }


}
