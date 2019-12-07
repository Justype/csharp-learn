# 泛型

泛型会声明类型参数 – 泛型的消费者需要提供类型参数（argument）来把占位符类型填充上。

```c#
public class Stack<T>   // T is type parameter
{
    int position;
    T[] data = new T[100];
    public void Push(T obj) => data[position++] = obj;
    public T Pop()          => data[--position];
}

var stack = new Stack<int>();
stack.Push(5);
stack.Push(10);
int x = stack.Pop();    // x is 10
int y = stack.Pop();    // y is 5

// 上面的定义
public class ###
{
    int position;
    int[] data = new int[100];
    public void Push(int obj) => data[position++] = obj;
    public int Pop()          => data[--position];
}
```

## 范型的作用

- 跨类型可复用的代码：继承 和 泛型。
- 继承 → 基类
- 泛型 → 带有“（类型）占位符” 的“模板”

```c#
public class ObjectStack
{
    int position;
    object[] data = new object[10];
    public void Push(object obj) => data[position++] = obj;
    public object Pop()          => data[--position];
}

// 需要装箱和向下转换，这种转换在编译时无法进行检查

stack.Push ("s");           // Wrong type, but no error !!
int i = (int)stack.Pop();   // Downcast - runtime error
```

如果不使用范型，只能根据每种类型写一个方法，或者使用object类型，但是object无法在编译时检查。

### Open Type & Closed Type

- `Stack<T>` Open Type（开放类型）
- `Stack<int>` Closed Type（封闭类型）
- 在运行时，所有的泛型类型实例都是封闭的（占位符类型已被填充了）

```c#
var stack = new Stack<T>;   // 不合法的，占位符没有被填充

public class Stack<T>
{
    ...
    public Stack<T> Clone()
    {
        Stack<T> clone = new Stack<T>();    // legal，范型内可使用
    }
}
```

## 泛型方法

泛型方法在方法的签名内也可以声明类型参数

```c#
static void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}
int x = 5;
int y = 10;
Swap(ref x, ref y); // 隐式推断出类型
// Swap<int>(ref x, ref y);
```

- 在泛型类型里面的方法，除非也引入了类型参数（type parameters），否则是不会归为泛型方法的。
- 只有类型和方法可以引入类型参数，属性、索引器、事件、字段、构造函数、操作符等都不可以声明类型参数。但是他们可以使用他们所在的泛型类型的类型参数。
    - `public T this[int index] => data[index];`
    - `public Stack<T>() { }  // Illegal`

## 声明类型参数

- 在声明class、struct、interface、delegate的时候可以引入类型参数（Type parameters）。
- 其它的例如属性，就**不可以引入**类型参数，但是*可以使用*类型参数。

```c#
public struct Nullable<T>
{
    public T Value { get; }
}
```

泛型类型/泛型方法可以有多个类型参数：

```c#
class Dictionary<TKey, TValue> { }

var myDic = new Dictionary<int,string>();
// Dictionary<int,string> myDic = new Dictionary<int,string>();
```

## 声明泛型类型

- 泛型类型/泛型方法的名称可以被重载，条件是参数类型的个数不同：
    - `class A        { }`
    - `class A<T>     { }`
    - `class A<T1,T2> { }`
- 按约定，泛型类型/泛型方法如果只有一个类型参数，那么就叫T。
- 当使用多个类型参数的时候，每个类型参数都使用T作为前缀，随后跟着具有描述性的一个名字（例：TKey, TValue）。

### Typeof 与 未绑定的泛型类型

- 开放的泛型类型在编译后就变成了封闭的泛型类型。
- 但是如果作为Type对象，那么未绑定的泛型类型在运行时是可以存在的。（只能通过typeof操作符来实现）

```c#
class A<T>     { }
class A<T1,T2> { }

Type a1 = typeof(A<>);  // Unbound type (notice no type arguments).
Type a2 = typeof(A<,>); // Use commas to indicate multiple type args.

Type a3 = typeof(A<int,int>);
class B<T> { void X(){ Type t = typeof(T); } }
```

### 泛型的默认值

使用default关键字来获取泛型类型参数的默认值

```c#
static void Zap<T>(T[] array)
{
    for (int i = 0; i <array.Length; i++)
        array[i] = default(T);
}
```

- 引用类型：默认null
- 值类型：按位归零的值

## 泛型的约束

- 默认情况下，泛型的类型参数（parameter）可以是任何类型的。
- 如果只允许使用特定的类型参数（argument），就可以指定约束。

```c#
where T : base-class    // Base-class constraint
where T : interface     // Interface constraint
where T : class         // Reference-type constraint
where T : struct        // Value-type constraint (excludes Nullable types)
where T : new()         // Parameterless constructor constraint
where U : T             // Naked type constraint
```

### 泛型约束 例子

```c#
class SomeClass      { }
interface Interface1 { }

class GenericClass<T,U> where T : SomeClass, Interface1
                        where U : new()
{ }
// T 继承于 SomeClass，并实现 Interface1接口
// U 必须有无参的构造函数
```

### 泛型约束作用

泛型的约束可以作用于**类型**或**方法**的定义。

#### 方法

```c#
public interface Icomparable<T>
{
    int CompareTo(T other);
}

static T Max<T>(T a, T b) where T : Icomparable<T>
{
    return a.CompareTo(b) > 0 ? a : b;
}

int z = Max(5, 10);             // 10
string last = Max("ant", "zoo") // zoo
```

#### struct

```c#
struct Nullable<T> where T : struct { }
```

#### new()

有无参的构造函数

```c#
static void Initializa<T>(T[] array) where T : new()
{
    for (int i = 0; i < array.Length; i++)
        array[i] = new T(); // 由于 T:new()，所以不用担心会报错
}
```

#### 裸类型约束

```c#
class Stack<T>
{
    Stack<U> FilteredStack<U>() where U : T { }
}
```

## 泛型类型的子类

- 泛型class可以有子类，在子类里，可以继续让父类的类型参数保持开放
    - `class Stack<T> { }`
    - `class SpecialStack<T> : Stack<T> { }`
- 在子类里，也可以使用具体的类型来关闭（封闭）父类的类型参数
    - `class IntStack : Stack<int> { }`
- 子类型也可以引入新的类型参数
    - `class List<T> { }`
    - `class KeyedList<T, TKey> : List<T> { }`

技术上来讲，所有子类的类型参数都是新鲜的。你可以认为子类先把父类的类型参数（argument）给关闭了，然后又打开了。为这个先关闭后打开的类型参数（argument）带来新的名称或含义。

```c#
class List<T> { }
class KeyedList<TElement, TKey> : List<TElement> { }
// 相当于把 T 改名为 TElement
```

## 自引用的泛型声明

在封闭类型参数（argument）的时候，该类型可以把它自己作为具体的类型。

```c#
public interface IEquatable<T> { bool Equals (T obj); }

public class Balloon : IEquatable<Balloon>
{
    public string Color { get; set; }
    public int CC { get; set; }

    public bool Equals(Balloon b)
    {
        if (b == null)
            return false;
        return b.Color == Color && b.CC == CC;
    }
}

// 其它例子，均合法
class Foo<T> where T : IComparable<T> { }
class Bar<T> where T : Bar<T> { }
```

## 静态数据

针对每一个封闭类型，静态数据是唯一的

```c#
class Bob<T> { public static int Count; }

class Test
{
    static void Main(string[] args)
    {
        Console.WriteLine(++Bob<int>.Count);    // 1
        Console.WriteLine(++Bob<int>.Count);    // 2
        Console.WriteLine(++Bob<string>.Count); // 1
        Console.WriteLine(++Bob<object>.Count); // 1
    }
}
```

## 类型参数和转换

- C#的转换操作符支持下列转换：
    - 数值转换
    - 引用转换
    - 装箱拆箱转换
    - 自定义转换
- 决定采用的是哪种转换，发生在编译时，根据已知类型的操作数来决定。

```c#
StringBuilder Foo<T>(T arg)
{
    if (arg is StringBuilder)
        return (StringBuilder) args;    // Will not compile
    // 编译不通过：不知道T的具体类型
    // 认为你是要自定义类型转换
}

// 解决：使用 as 操作符

StringBuilder Foo<T>(T arg)
{
    StringBuilder sb = arg as StringBuilder;
    if (sb != null) return sb;
}

// 或
// return (StringBuilder)(object) arg; // 引用转换
```

转换问题：先转成`object`，再转成想要的类型：
- `int Foo<T>(T x) => (int)(object) x;`

## 协变，逆变，不变

- Covariance 协变，当值作为返回值/out 输出
- Contravariance 逆变，当值作为输入 input
- Invariance 不变，当值既是输入又是输出

- `public interface IEnumerable<out T>`
- `public delegate void Action<in T>`
- `public interface IList<T>`

```c#
/*
IEnumerable<T>  只能输出            Covariance      子类=>父类
IList<T>        能输入，又能输出    Invariance      不能变
Action<T>       只能输入            Contravariance  父类=>子类
*/

// 合法
IEnumerable<string> strings = new List<string> { "a", "b", "c" }
IEnumerable<object> objects = strings;

// 不合法
IList<string> strings = new List<string> { "a", "b", "c" }
IList<object> objects = strings;
objects.Add(new object());
string element = strings[3];

// 合法
Action<object> objectAction = obj => Console.WriteLine(obj);
Action<string> stringAction = objectAction; // string为object子类，OK
stringAction("Print Me");

```

## variance

variance 只能出现在接口和委托里。

### Variance 转换

- 涉及到variance的转换就是variance转换。
- Variance转换是引用转换的一个例子。引用转换就是指，你无法改变其底层的值，只能改变编译时类型。
- identity conversion，对CLR而言从一个类型转化到相同的类型。

#### 合理的转换

如果从A到B的转换是**本体转换**或者**隐式引用转换**，那么从`IEnumerable<A>`到`IEnumerable<B>`的转换就是合理的：
- `IEnumerable<string> to IEnumerable<object>`
- `IEnumerable<string> to IEnumerable<IConvertible>`
- `IEnumerable<IDisposable> to IEnumerable<object>`

#### 不合理的转换

- `IEnumerable<object> to IEnumerable<string>`
- `IEnumerable<string> to IEnumerable<Stream>`
- `IEnumerable<int> to IEnumerable<IConvertible>`
- `IEnumerable<int> to IEnumerable<long>`隐式转换，不是隐式引用转换

## 其它

C#的泛型，生产类型（例如`List<T>`）可以被编译到dll里。这是因为这种在生产者和产制封闭类型的消费者之间的合成是发生在运行时的。
