using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.Linq;

namespace Deferred_Operators
{
    class MainLayer
    {
        static void Main(string[] args)
        {
           

            ArrayList MyList = Employee.GetEmplyeesArrayList();

            #region Where
            
            //"Where" example by first name and by intex of our ArrayList
            //Where(this IEnumerable<T>, Func<T,bool>)
            //Where(this IEnumerable<T>, Func<T,bool>, int)
            IEnumerable<Employee> TakeNamesShorterThan6 = MyList.Cast<Employee>().Where(p => p.firstName.Length < 6);
            
            foreach (var item in TakeNamesShorterThan6)
            {
                Console.WriteLine(item.firstName);
            }
            
            //using cast since ArrayList is legacy list, and it doesnt suport Linq operators
            IEnumerable<Employee> takeOddEmployee = MyList.Cast<Employee>().Where((e, i) => (i & 1) == 1);
            
            foreach (var item in takeOddEmployee)
            {
                Console.WriteLine("Id {0}: {1} - {2}", (item.Id - 1), item.firstName, item.secondName);
            } 
            
            #endregion

            #region Select
            
            //Select(this IEnumerable<T>, Func<T,AnyType>)
            //"Select" examples where output is first time int, and second anonnymus type.
            //Here we will have an output of int, which represent lenght of names of our employees
            IEnumerable<int> selectLengthOfNamesInEmployee = MyList.Cast<Employee>().Select(p=>p.firstName.Length);

            foreach (var item in selectLengthOfNamesInEmployee)
            {
                Console.WriteLine("Length:{0}",item);
            }

            //Now example where we create anonnymous type which contain Names and length of that names
            var nameObjects = MyList.Cast<Employee>().Select(p => new {p.firstName,p.firstName.Length});
            foreach (var item in nameObjects)
            {

                Console.WriteLine(item);
            }

            //Same variant but we assign names to our fields in anonymous type we create while creating sequance
            var nameObjectsWithNames = MyList.Cast<Employee>().Select(p => new {FirstName= p.firstName,Length= p.firstName.Length });
            foreach (var item in nameObjectsWithNames)
            {

                Console.WriteLine("Name: {0} is {1} characters long.",item.FirstName,item.Length);
            }
            
            //Working with second parametr in Select method which is index
            //This time we can take the index of every item we put in our sequance it is represented by i
            //needed if we need the initial position of item in a list.
            var nameObjectWithIndex = MyList.Cast<Employee>().Select((e, i) => new {Index=i,FirstName=e.firstName});

            foreach (var item in nameObjectWithIndex)
            {
                Console.WriteLine("{0}. {1}",(item.Index+1),item.FirstName);
            }

            #endregion

            #region SelectMany
            //SelectMany(this IEnumerable<T>, Furk<T,IEnumerable<AnyTime>>)
            //Examples of selectMany
            IEnumerable<char> charsFromEmployeeNames = MyList.Cast<Employee>().SelectMany(e=>e.firstName.ToArray());

            foreach (char item in charsFromEmployeeNames)
            {
                Console.WriteLine(item);
            }

            //Second Example with employee and employeeOption (complicated)
            Employee[] employeeArray = Employee.GetEmploeesArray();

            EmployeeOptionEntry[] employeeOptionArray = EmployeeOptionEntry.GetEmployeeOptionEntries();
            
            //для каждого элемента из employeeArray вабрать несколько анонимных сущньстей по принципу:
            //Мы находим все элементы employeeOption у которых совпадает id с данным элементом employeeArray
            //и для каждого из таких employeeOption создаем анонимную сущьность, в которую помещаем поля
            //имя сущбности employeeArray для которой мы выбираем эту сущьность, id и optionCount из employeeOption
            //SelectMany(T=F(a)) выбирает все обьекты типа T которые удовлетворяют функции F(a).
            var SelectManyExample = employeeArray.
                SelectMany(e => //здесь входящий параметр employeeArray[i], выходящий сущбность анонимного класса
                    employeeOptionArray.Where(eo => eo.Id == e.Id).Select(eo => 
                        new { firstname=e.firstName, id = eo.Id, OptionCount = eo.optionCount }
                        ));

            foreach (var item in SelectManyExample)
            {
                Console.WriteLine(item);
            }

            //Example with SelectMany using index
            IEnumerable<char> EmployeeNamesByChars =MyList.Cast<Employee>().SelectMany((e,i)=>(i<3)? e.firstName.ToArray():new char[]{'H','e','l','l'});

            foreach (var item in EmployeeNamesByChars)
            {
                Console.WriteLine(item);
            }
            #endregion

            #region Take

            //Take examples Take(this IEnumerable<T> input, int num )
            IEnumerable<string> FirstThreeEmployeNames = MyList.Cast<Employee>().Take(3).
                Select(e => e.firstName);

            foreach (var item in FirstThreeEmployeNames)
            {
                Console.WriteLine(item);
            }
            //Выбираем 3 эелемента последовательности mylist и выбираем от туда массивы символов которые
            //получаются из строк firstname.
            IEnumerable<char> NamesOfEmployeeByLetters = MyList.Cast<Employee>().Take(3).
                SelectMany(e=>e.firstName.ToArray());

            foreach (var item in NamesOfEmployeeByLetters)
            {
                Console.WriteLine(item);
            }


            #endregion

            #region TakeWhile

            //Examples of TakeWhile
            //TakeWhile will take only till moment when predicate give true, when it will give false
            //the sequence will stop. Thats the difference betwen Where and TakeWhile.
            IEnumerable<string> FirstNamesOfEmployeeShorterThanFour = MyList.Cast<Employee>()
                .TakeWhile(e => e.firstName.Length < 7).Select(e=>e.firstName);

            foreach (var item in FirstNamesOfEmployeeShorterThanFour)
            {
                Console.WriteLine(item);
            }

            //Secont prototype of TakeWhile Looks like that TakeWhile(this IEnumerable ,Func<T,bool>,int) 
            IEnumerable<string> NamesShorterThanFiveAndNotFertherThanFifthName = MyList.Cast<Employee>()
                .TakeWhile((e, i) => (e.firstName.Length < 10 && i < 5)).Select(e=>e.firstName);

            foreach (var item in NamesShorterThanFiveAndNotFertherThanFifthName)
            {
                Console.WriteLine(item);
                
            }
            #endregion

            #region Skip
            //Example of Skip, Skip(this IEnumerable<t>,int) пропускает int колличество элементов
            //и забераем остальные.
            Console.WriteLine();
            IEnumerable<string> SecondNamesOfEmployeeStartingFromFourth = MyList.Cast<Employee>()
                .Skip(4).Select(e => e.secondName);

            foreach (var item in SecondNamesOfEmployeeStartingFromFourth)
            {
                Console.WriteLine(item);
            }
            #endregion

            #region SkipWhile
            //Example of SkipWhile. 
            //SkipWhile(this IEnumerable<T>, Func(T,bool))
            //SkipWhile(this IEnumerable<T>, Func(T,bool), int)
            Console.WriteLine();
            IEnumerable<string> SecondNamesStartingFromLongerThanFiveChar = MyList.Cast<Employee>()
                .SkipWhile(e => e.secondName.Length < 7).Select(e => e.secondName);

            foreach (var item in SecondNamesStartingFromLongerThanFiveChar)
            {
                Console.WriteLine(item);
            }

            //Second prototype of SkipWhile
            Console.WriteLine();
            IEnumerable<string> SecondNamesStartingFromLongerThanSevenTillFourth = MyList.Cast<Employee>()
                .SkipWhile((e, i) => e.secondName.Length < 7 && i < 4).Select(e => e.secondName);

            foreach (var item in SecondNamesOfEmployeeStartingFromFourth)
            {
                Console.WriteLine(item);
            }

            #endregion

            #region Concat
            //Examples with Concat. Allow ot take two sequances and combine it to one.
            //Concat(this IEnumerable, IEnumerable)
            Console.WriteLine();
            IEnumerable<string> SequanceFirstThreeNamesStartingFromthirdSecondNames = MyList.Cast<Employee>()
                .Take(3).Select(e => e.firstName)
                .Concat(MyList.Cast<Employee>().Skip(3).Take(3).Select(e => e.secondName));

            foreach (var item in SequanceFirstThreeNamesStartingFromthirdSecondNames)
            {
                Console.WriteLine(item);
            }
            
            //Second Example of how we could concat that shit...
            Console.WriteLine();
            IEnumerable<string> concatExample2 = new[] {
                MyList.Cast<Employee>().Take(4).Select(e=>e.firstName),
                MyList.Cast<Employee>().Skip(4).Take(4).Select(e=>e.secondName)
            }.SelectMany(s=>s);

            foreach (var item in concatExample2)
            {
                Console.WriteLine(item);
            }
            #endregion

            #region OrderBy
            //Examples of OrderBy
            //OrderBy(this IEnumerable<T>, Func<T,IComparable> )
            //OrderBy(this IEnumerable<T>, Func<T,IComparable>, IComparer<IComparable>)
            Console.WriteLine();
            var OrderByExample = MyList.Cast<Employee>().Select((e, i) => new {Index=i,FirstName=e.firstName }).OrderBy(fn => fn.FirstName);
            
            foreach (var item in OrderByExample)
            {
                Console.WriteLine(item);
            }

            //Example with output sequence selection of strings
            Console.WriteLine();
            IEnumerable<string> OrderByExample1 = MyList.Cast<Employee>().Select(e => e.firstName).OrderBy(fn => fn);

            foreach (string item in OrderByExample1)
            {
                Console.WriteLine(item);
            }

            //Example of second prototype of OrderBy (Complicated)
            //Для этого примера мы написали класс котоырй реализует интерфейс  IComparer<T> в котором всего
            //один метод, Compare(T,T) этот метод возвращает инт значение отрицательное, положительное
            // значение или ноль, в зависимости от характера нашего сравнения. Скажем в данном случае, 
            //метод считает отношение гласных к согласным в входящих строках, и если это соотношение,
            //больше у второй строки (больше гласных по отношению к согласным) то он выдает -1, 0 при одинаковом
            //соотношении и 1 при соотношении больше в первой строке. Далее создается сущьность этого класса
            //и отправляется в качаестве параметра myComp в метод OrderBy.
            MyVowelToConsonantRatioComparer myComp = new MyVowelToConsonantRatioComparer();
            Console.WriteLine();
            IEnumerable<string> namesByVToCRAtio = MyList.Cast<Employee>().OrderBy(e => e.firstName, myComp).Select(e => e.firstName);

            foreach (string item in namesByVToCRAtio)
            {
                int vCount = 0;
                int cCount = 0;
                myComp.GetVowelConsonantCount(item, ref vCount, ref cCount);
                double dRatio = (double)vCount/(double)cCount;
                Console.WriteLine(item+" - "+dRatio+" - "+vCount+":"+cCount);
            }

            #endregion

            #region ThenBy
            //Example of ThenBy 
            //can be called only if OrerBy or OrerByDeccending was calle before that.
            //ThenBy(this IEnumerable<T>, Func<T,K> ) K - IComparable
            //ThenBy(this IEnumerable<T>, Func<T,K>, IComparer<K> )

            IEnumerable<Employee> ThenByExample = MyList.Cast<Employee>().OrderBy(e => e.firstName.Length).ThenBy(e => e.firstName);
            Console.WriteLine();
            IEnumerable<string> ThenByExample2 = MyList.Cast<Employee>().Select(e => e.firstName).OrderBy(s => s).ThenBy(s => s.Length);

            foreach (var item in ThenByExample)
            {
                Console.WriteLine(item.firstName);
            }
            Console.WriteLine();
            foreach (var item in ThenByExample2)
            {
                Console.WriteLine(item);
            }
            
            //Secon example using secon protorype of ThenBy
            IEnumerable<string> ThenByExample3 = MyList.Cast<Employee>().Select(e => e.firstName)
                .OrderBy(s => s.Length).ThenBy(s => s, myComp);

            foreach (var item in ThenByExample3)
            {
                int vCount = 0;
                int cCount = 0;

                myComp.GetVowelConsonantCount(item, ref vCount, ref cCount);
                double dRatio = (double)vCount / (double)cCount;
                Console.WriteLine(item+ " - "+dRatio+" - "+vCount+":"+cCount);
            }
            #endregion

            #region Reverse

            //Example of Reverse operator that outputs the incoming sequence but in reversed orer.
            //Reverse(this IEnumerable<T>)

            IEnumerable<string> ReverseExample = MyList.Cast<Employee>().Select(e => e.firstName)
                .Reverse();

            for (int i = 0; i < MyList.Count; i++)
            {
                Console.WriteLine("{0}\t{1}",ReverseExample.ElementAt(i),MyList.Cast<Employee>().ElementAt(i).firstName);
            }
            #endregion

            #region Join
            //Example of join operator.
            //Join(this IEnumerable<T>, IEnumerable<U>, Func<T,K>, Func<U,K>, Func<T,U,V>)

            var JoinExample = employeeOptionArray.Join
                (
                employeeArray,
                o => o.Id,
                e => e.Id,
                (o, e) => new
                {
                    id = e.Id,
                    name = string.Format("{0} {1}", e.firstName, e.secondName),
                    options = o.optionCount
                }
                ).OrderByDescending(e=>e.name);

            foreach (var item in JoinExample)
            {
                Console.WriteLine(item);
            }
            #endregion
            //Hello here is some code
            //Are u sure there is Noany desiase about that  
            #region GroupJoin
            //Example of a GroupJoin() operator.
            //GroupJoin(this Ienumerable<T>, IEnumerable<U>, Func<T,K>, Func<U,K>, Func<T, IEnumerable<U>, V>)
            var GroupJoinExample = employeeArray.
                GroupJoin(employeeOptionArray,
                e => e.Id,
                o => o.Id,
                (e, os) => new
                {
                    id = e.Id,
                    name = string.Format("{0} {1}", e.firstName, e.secondName),
                    option = os.Sum(o => o.optionCount)
                }).OrderBy(e => e.id);
            Console.WriteLine();
            foreach (var item in GroupJoinExample)
            {
                Console.WriteLine(item);
            }


            #endregion

            #region GroupBy
            // The examples of GroupBy operator.
            // first IGrouping intarface 
            // public interface Igrouping<K,T>:IEnumeratble<T> {K Key{get;} }
            // GroupBy<T,K>(this IEnumerable<T>, Func<T,K> )
            // GroupBy<T,K>(this IEnumerable<T>, Func<T,K>, IEqualityComparer<K> )
            // GroupBy<T,K>(this IEnumerable<T>, Func<T,K>, Func<T,E> elementSelector, IEqualityComparer<K> )

            IEnumerable<IGrouping<int, EmployeeOptionEntry>> GroupByExample = employeeOptionArray
                .GroupBy(o => o.Id);
            //First enumerate through the outer sequence of IGroupings
            foreach (IGrouping<int, EmployeeOptionEntry> item in GroupByExample)
            {
                Console.WriteLine("Option records for employee:"+item.Key);
                //now enumerate through the grouping's sequence of EmployeeOptionEntry
                foreach (EmployeeOptionEntry element in item)
                {
                    Console.WriteLine("\tid={0}\t : optionCount={1}\t : dateAwarded={2:d}",
                        element.Id,element.optionCount,element.dateAwarded);
                }
            }

            //Example of second prototype of GroupBy
            Console.WriteLine();
            //We are gonna use our comparerClass in that case
            MyFounderNumberComparer InstanceForGroupByExample = new MyFounderNumberComparer();

            IEnumerable<IGrouping<int, EmployeeOptionEntry>> GroupByExample2 = employeeOptionArray
                .GroupBy(o=>o.Id,InstanceForGroupByExample);

            foreach (IGrouping<int,EmployeeOptionEntry> item in GroupByExample2)
            {
                Console.WriteLine("Option records for: "+ 
                    (InstanceForGroupByExample.isFounder(item.Key)? "founder":"non-founder"));
                foreach (EmployeeOptionEntry subItem in item)
                {
                    Console.WriteLine("id={0} : optionCounts={1} : dateAwarded={2:d}",
                        subItem.Id,subItem.optionCount,subItem.dateAwarded);
                }
            }

            #endregion

        }
    }
    //OrderBy class for example.
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
