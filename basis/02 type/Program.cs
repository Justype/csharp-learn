using System;

namespace Type
{
    class Program
    {
        static void Main(string[] args)
        {
            // 对象的实例通过new来实现
            UnitConverter feetToInchesConv = new UnitConverter(12);
            UnitConverter milesToFeetConv = new UnitConverter(5280);

            Console.WriteLine(feetToInchesConv.Convert(30));
            Console.WriteLine(milesToFeetConv.Convert(100));

            Console.WriteLine(milesToFeetConv.Convert(feetToInchesConv.Convert(1)));
        }
    }

    public class UnitConverter
    {
        int ratio; // Field 字段

        public UnitConverter(int unitRatio)  // Constructor 构造函数
        {
            ratio = unitRatio;
        }

        public int Convert(int unit)  // Method 方法
        {
            return unit * ratio;
        }
    }
}
