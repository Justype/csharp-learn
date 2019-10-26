using System;

namespace Type
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 12345;
            long y = x;         // 隐式转换 int类型的范围小于long类型的范围
            short z = (short)y; // 显式转换
            Console.WriteLine(x);
            Console.WriteLine(y);
            Console.WriteLine(z);

            Point p1 = new Point();
            p1.x = 6;
            p1.y = 0;
            Point p2 = p1;  // 值类型的复制，复制的是值本身
            Console.WriteLine($"p1.x = {p1.x}");  // 6
            Console.WriteLine($"p2.x = {p2.x}");  // 6
            p2.x = 9;
            Console.WriteLine($"p1.x = {p1.x}");  // 6
            Console.WriteLine($"p2.x = {p2.x}");  // 9

            ClassPoint cp1 = new ClassPoint();
            cp1.x = 6;
            ClassPoint cp2 = cp1;  // 引用类型的复制，复制的是引用
            Console.WriteLine($"cp1.x = {cp1.x}");  // 6
            Console.WriteLine($"cp2.x = {cp2.x}");  // 6
            cp2.x = 9;
            Console.WriteLine($"cp1.x = {cp1.x}");  // 6
            Console.WriteLine($"cp2.x = {cp2.x}");  // 6
        }

        public struct Point
        {
            public int x;
            public int y;
        }
    }

    class ClassPoint
    {
        public int x { set; get; }
        public int y { set; get; }
    }
}
