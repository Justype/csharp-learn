using System;

namespace Type
{
    class Program
    {
        static void Main(string[] args)
        {
            // 比较类型
            Person p1 = new Person("Ziyue", 20);
            Person p2 = new Person("Justype", 0);
            Person p3 = new Person("Ziyue", 20);
            var p4 = p1;

            Console.WriteLine(p1 == p2);    // False
            Console.WriteLine(p1 == p3);    // False
            Console.WriteLine(p1 == p4);    // True

            Console.WriteLine(p1.Name == p2.Name);  // False
            Console.WriteLine(p1.Name == p3.Name);  // True

            // 三元运算符
            Console.WriteLine("三元运算符");
            int x = 3;
            int y = 2;

            int z = x > y ? x : y;
            Console.WriteLine(z);   // 3
        }
    }
    class Person
    {
        public string Name;
        public int Age;
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
