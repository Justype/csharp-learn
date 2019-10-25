using System;

namespace intro // 命名空间为 intro
{
    class Program   // 类名为 Program
    {
        static void Main(string[] args)   // 程序的入口
        {
            // Console.WriteLine("Hello World!");  // 输出 Hello World!
            Console.WriteLine(FeetToInches(30));
            Console.WriteLine(FeetToInches(100));

        }

        static int FeetToInches(int feet)
        {
            int inches = feet * 12;
            return inches;
        }
    }
}
