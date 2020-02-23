# **Delegates** 委托

委托是一个对象，它知道如何调用一个方法。

## 委托类型和委托实例

委托类型定义了委托实例可以调用的那类方法，委托类型定义了方法的返回类型和参数。

返回类型和参数一样，委托类型就可以调用那类方法

### 例子

```c#
delegate int Transformer(int x); // 定义了委托类型
    //  返回类型          类型参数
static int Square(int x) { return x * x; }
static int Square(int x) => x * x;

// 把方法赋值给委托变量的时候就创建了委托实例。
Transformer t = Square;
// 调用：
int answer = t(3);	// answer is 9
```

### 完整的例子

```c#
class Program
{
    delegate int Transformer(int x);
    
    static int Square(int x) => x * x;
    
    static void Main()
    {
        Transformer t = Square;	// 创建委托实例
        int result = t(3);		// 委托实例调用委托方法
        System.Console.WriteLine(result);
    }
}
```

## 委托实例

- 委托的实例其实就是调用者的委托：调用者调用委托，然后委托调用目标方法.
- 间接的把调用者和目标方法解耦合了。

```c#
Transformer t = Square;
// 相当于
Transformer t = new Transformer(Square);

t(3);
// 相当于
t.Invoke(3);
```

### 编写插件式的方法

方法是在运行时才赋值给委托变量的

```c#
public delegate int Transformer(int x);

class Util
{
    // 也就是只要是 参数为int，返回值为int的函数都可以传到这个方法
    public static void Transform(int[] values, Transformer t)
    {
        for (int i = 0; i < values.Length, i++)
            values[i] = t(values[i]);
    }
}
public class Program
{
    static int Square(int x) => x * x;
    
    static void Main()
    {
        int[] values = { 1, 2, 3 };
        Util.Transform(values, Square);
        foreach (int i in values)
            System.Console.Write(i + " ");
    }
}
```

## 多播委托

所有的委托实例都具有多播的能力。一个委托实例可以引用一组目标方法。

- `+` 和 `+=` 操作符可以**合并委托实例**
    - `SomeDelegate d = SomeMethod1;`
    - `d += SomeMethod2;`
    - 或者`d = d + SomeMethod2`
- 调用d就会调用SomeMethod1和SomeMethod2
- 委托的调用顺序与它们的定义顺序一致
- `-`和 `-=` 会把右边的委托从左边的委托里**移除**
    - `d -= SomeMethod1;`
- 委托变量使用+或+=操作符时，其操作数可以是null。就相当于把一个新的值赋给了委托变量。
    - `SomeDelegate d = null; d += SomeMethod1;`
    - 相当于`d = SomeMethod1;`
- 对单个目标方法的委托变量使用-=操作符时，就相当于把null值赋给了委托变量。
- 委托是不可变的
    - 使用+=或-=操作符时，实际上是创建了新的委托实例，并把它赋给当前的委托变量。
- 如果多播委托的返回类型不是void，那么调用者从**最后一个被调用的方法来接收返回值**。前面的方法仍然会被调用，但是其返回值就被弃用了。

```c#
namespace console_sharp
{
    public delegate int TransFormer(int inputNum);

    class Program
    {
        static void Main(string[] args)
        {
            TransFormer trans = Square;
            trans += Cube;

            var result = trans(3);
            System.Console.WriteLine(result);
            /* 输出
            9
            9
            27
            */
        }

        static int Square(int num)
        {
            int result = num * num;
            System.Console.WriteLine(result);
            return result;
        }

        static int Cube(int num)
        {
            int result = num * num * num;
            System.Console.WriteLine(result);
            return result;
        }
    }
}

```

## 类

- 所有的委托类型都派生于 `System.MulticastDelegate`，而它又派生于`System.Delegate`。
- C#会把作用于委托的`+`，`-`，`+=`，`-=`操作编译成使用`System.Delegate`的`Combine`和`Remove`两个静态方法。

## 实例方法目标和静态方法目标

- 当一个实例方法被赋值给委托对象的时候，这个委托对象不仅要保留着对方法的引用，还要保留着方法所属实例的引用。
- `System.Delegate` 的`Target`属性就代表着这个实例。
- 如果引用的是静态方法，那么Target属性的值就是null。

```c#
public delegate void ProgressReporter(int percentComplete);

class Program
{
    static void Main()
    {
        X x = new X();
        ProgressReporter p = x.InstanceProgress;
        p(99);
        System.Console.WriteLine(p.Target == x); // true
        System.Console.WriteLine(p.Method); // Void InstanceProgress(Int32)
    }
}

class X
{
    public void InstanceProgress(int percentComplete) => System.Console.WriteLine(percnetComplete);
}
```



## 泛型委托类型

委托类型可以包含泛型类型参数

```c#
public delegate T Transformer<T>(T arg);

public class Util
{
    public static void Transform<T>(T[] values, Transformer<T> t)
    {
        for (int i = 0; i < values.Length; i++)
            values[i] = t(values[i]);
    }
}

class Test
{
    static void Mian()
    {
        int[] values = { 1, 2, 3 };
        // Util.Transform<int>(values, Square); // Hook in Square
        Util.Transform(values, Square); // 可以不写<int>，自动推断
        foreach(int i in values)
            Console.Write(i+" "); // 1, 4, 9
    }

    static int Square(int x) => x * x;
}
```



## **Func** **和** **Action** 委托

使用泛型委托，就可以写出这样一组委托类型，它们可调用的方法可以拥有任意的返回类型和任意（合理）数量的参数。

```c#
// System 命名空间

delegate TResult Func <out TResult> ();
delegate TResult Func <in T, out TResult> (T args);
delegate
```



