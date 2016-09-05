using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace cSharpLanguageEnchancementsForLINQ
{
    class Program
    {
        public static bool IsOdd(int i)
        {
            return ((i & 1) == 0);
        }
        static void Main(string[] args)
        {
            #region named and anonymous methos and lambda methods
            //Массив интов с которым мы будем работать 
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //С начала напишем метод с названием
            //int[] odNums = Common.FilterArrayOfInts(nums,IsOdd);

            //Метод с ананимным методом
            int[] odNums = Common.FilterArrayOfInts(nums,
                delegate(int i) { return ((i & 1) == 1); });

            //Тоже самое что Method(x){return (x&1)==1};, нужно помнить что лямбда выражение
            //должно совпадать с делегатом который был прописан в качестве параметра
            //в данном случае делегат принимает один int (x) и отдает оидн bool ((x&1)==1)
            int[] odNumByLambda = Common.FilterArrayOfInts(nums, x => (x & 1) == 1);

            //Выводим наш масси не четных
            foreach (var o in odNums)
            {
                Console.WriteLine(o);
            }
            Console.WriteLine("Using lamba expression");
            foreach (var o in odNumByLambda)
            {
                Console.WriteLine(o);
            } 
            #endregion
            Console.WriteLine("---------Next part of a programm----");
            #region KeyWord var, Object initialization and anonymous Types
            
            //AАнонимный тип
            var adress = new {
            _street="Kuncevshina",
            _city="Minsk",
            _buildingNumber=4,
            _country="Belarus"
            };
            Console.WriteLine("Aress:{0}-{1} \t{2}-{3} street.",adress._country,adress._city,
            adress._street,adress._buildingNumber);

            //Расширенный метод
            //Создаем экземпляр класса А
            var a = new A {Id=3,b=4,c=0 };
            Console.WriteLine(a);
            
            //Применяем к этому экземпляру метод из другого класса
            a.SetToFiveAClassFields(5);
            
            Console.WriteLine(a);


            #endregion
            //Список состоящий из обьектов класса А.
            List<A> ClassA = new List<A>
            { 
                new A{b=3,c=6},
                new A{b=13,c=3},
                new A{b=15,c=4},
                new A{b=4,c=5},
                new A{b=4,c=0},
                new A{b=6,c=6},
                new A{b=15,c=15},
            };
            ClassA.SetTheId();
            var collection = ClassA.Where<A>(o => o.b==o.c||(o.b<5 && o.c<5)).OrderBy<A, int>(o => o.Id);
            //int index = 0;
            
            foreach (var item in collection)
            {
                Console.WriteLine("A[{0}]:({1},{2}) ", item.Id, item.b, item.c);
            }

            Console.WriteLine("-----------Partial nethod part of a programm---------");

            #region Partial method
            //Неполный метод
            PartClassExampl prt = new PartClassExampl();

            #endregion
            #region Query Expressions

            string[] names = {"Adam","James","Tim","Ludvig","Bush","Snake","Solid","Liquid","Ford"
                             ,"Adam","Pet","Harry","Truman"};
            //Staandart dot notation Syntax
            IEnumerable<string> sequence = names.Where(n => n.Length < 5)
                .Select(n=>n);
            foreach (var item in sequence)
            {
                Console.WriteLine(item);
            }
            //Query Express Syntax
            IEnumerable<string> queryExpression = from n in names
                                                  where n.Length<5
                                                  select n;
            foreach (var item in queryExpression)
            {
                Console.WriteLine(item);
            }
            #endregion
            

        }
        
    }
    public partial class PartClassExampl
    {
        partial void MyWidgetStart(int count);
        partial void MyWidgetEnd(int count);

        public PartClassExampl()
        {
            int count = 0;
            MyWidgetStart(++count);
            Console.WriteLine("In the constructor of PartClassExampl");
            MyWidgetEnd(++count);
            Console.WriteLine("count: "+count);
        }
    }
    public partial class PartClassExampl
    {
        partial void MyWidgetStart(int count)
        {
            Console.WriteLine("In MyWidgetStart (count is {0})",count);
        }
        partial void MyWidgetEnd(int count)
        {
            Console.WriteLine("In MyWidgetEnd (count is {0})",count);
        }
    }
    /// <summary>
    /// метод расширения обязательно должен быть обьявлен  статическом классе
    /// и сам должен являтся статическим
    /// </summary>
    public static class ExtMethoClass
    {
        /// <summary>
        ///Этот метод можно вызвать у экземпляра класса А, даже не смотря на то что этот метод
        ///не является частью класса А, и никак к нему не привязан. Он прописан абсолютно в другом
        ///статическом классе. Метод обязан быть статичным и находится в статическом классе.
        ///И ОБЯЗАТЕЛЬНО первый параметр этого метода должен принимать экземпляр интересующего нас метода
        ///и перед этим должно быть зарегестрироанное слово this
        /// </summary>
        /// <param name="omj"></param>
        /// <param name="x"></param>
        public static void SetToFiveAClassFields(this A omj,int x)
        {
            omj.Id = x;
            omj.b = x;
            omj.c = x;
        }
        public static void SetTheId(this List<A> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Id = i;
            }
        }
        
    }

    public class A 
    {
        public override string ToString()
        {
            return String.Format("Id:{0}({1},{2})",Id,b,c);
            //return base.ToString();
        }
        public int Id { get; set; }
        public int b { get; set; }
        public int c { get; set; }
    }
    
    //Класс написанный для примера Lamba Expressions
    public static class Common
    {
        public delegate bool IntFilter(int i);
        public static int[] FilterArrayOfInts(int[] ints, IntFilter filter)
        {
            ArrayList aList = new ArrayList();
            foreach (int i in ints)
            {
                if (filter(i))
                    aList.Add(i);
            }
            return (int[])aList.ToArray(typeof(int));
        }
    }
}
