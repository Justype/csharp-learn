# class

- 最常见的一种引用类型
- `class ClassName { }`

## Field 字段

- 是Class或Struct的成员，它是一个变量

```c#
class Octopus
{
    string name;
    public int age = 10;
}
// 可同时声明多个字段
```

### 字段初始化

- 字段在声明的时候可以进行初始化
- 未初始化的字段有一个默认
- 字段的初始化在构造函数**之前**运行

### readonly

- `readonly` 修饰符防止字段在构造之后被改变
- `readonly` 字段只能在声明的时候被赋值，或在构造函数里被赋值

## 方法

- 通常包含一些语句，会执行某个动作
- 可以传入参数
- 可以返回数据，返回类型
  Void，不返回数据
- ref/out 参数

### 方法的签名

- 一个类型内，其每个方法的签名必须是唯一的
- 签名：方法名、参数类型（含顺序，但与参数名称和返回类型无关）

### 方法的重载 overload

类型里的方法可以进行重载（允许多个同名的方法同时存在），只要这些方法的签名不同就行

```c#
void Foo() { }
void Foo(int x) { }
void Foo(double x) { }
void Foo(float x, int y) { }
void Foo(int x, float y) { }
```

### 按值传递 vs 按引用传递

参数是按值传递的还是按引用传递的，也是方法签名的一部分

```c#
void Foo(int x) { }
void Foo(ref int x) { } // OK so far
void Foo(out int x) { } // Compile-time error
// 二三互斥，按引用互斥
```

### Expression-bodied 方法

适用于单表达式方法

```c#
// 例子
void Foo(int x) => Console.WriteLine(x);

int Foo(int x) => x * 2;
// 相当于 int Foo（int x) { return x * 2; }

```

### 本地方法

- 位于方法内的方法
- 不可以使用`static`修饰符

```c#
void WriteCubes()
{
    Console.WriteLine(Cube(3));
    Console.WriteLine(Cube(4));
    Console.WriteLine(Cube(5));

    int Cube(int value) => value * value * value; // 这个就是本地方法
}
```

## 构造函数

- 在class或struct上运行初始化代码
- 和定义方法差不多，但构造函数的名和类型名一致，返回类型也和类型一致，并且返回类型就省略不写了
- C#7，允许单语句的构造函数写成expression-bodied成员的形式

### 构造函数重载

- class和struct可以重载构造函数
- 调用重载构造函数时使用this
- 当同一个类型下的构造函数A调用构造函数B的时候，B先执行
- 可以把表达式传递给另一个构造函数，但表达式本身不能使用this引用，因为这时候对象还没有被初始化，所以对象上任何方法的调用都会失败。但是可以使用static方法

```c#
public class Wine
{
    public decimal Price;
    public int Year;
    public Wine(decimal Price) { this.Price = Price; }
    public Wine(decimal Price, int Year) : this(Price) { this.Year = Year; }
    // 这个就是重载，:this() 调用构造函数
}
```

### 无参构造函数

- 对于class，如果你没有定义任何构造函数的话，那么C#编译器会自动生成一个无参的public构造函数。
- 但是如果你定义了构造函数，那么这个无参的构造函数就不会被生成了

### 构造函数和字段的初始化顺序

- 字段的初始化发生在构造函数执行之前
- 字段按照声明的先后顺序进行初始化

### 非public的构造函数

- 构造函数可以不是public的
- 静态方法调用非public构造函数，实现单例

```c#
public class Wine
{
    Wine() { }

    public static Wine CreateInstance()
    {
        return new Wine();
    }
}
```

### Deconstructor（C#7）解构函数？

- C#7 引入了*de*constructor 模式
- 作用基本和构造函数相反，它会把字段反赋给一堆变量
- 方法名必须是Deconstruct， 有一个或多个out参数
- Deconstructor可以被重载
- Deconstruct这个方法可以是扩展方法

```c#
class Program
{
    static void Main(string[] args)
    {
        Rectangle rect = new Rectangle(3, 4);
        (float width, float height) = rect;  // Deconstruction
        // 相当于
        // rect.Deconstruct(out float width, out float height);
        Console.WriteLine($"width = {width}, height = {height}");
    }
}

class Rectangle
{
    public readonly float Width, Height;
    public Rectangle(float Width, float Height)
    {
        this.Width = Width;
        this.Height = Height;
    }
    public void Deconstruct(out float Width, out float Height)
    {
        Width = this.Width;
        Height = this.Height;
    }
}
```

```c#
// 扩展方法
class Program
{
    static void Main(string[] args)
    {
        Rectangle rect = new Rectangle(3, 4);
        // Extensions.Deconstruct(rect, out float width, out float height);
        // rect.Deconstruct(out float width, out float height);
        (float width, float height) = rect;
        Console.WriteLine($"width = {width}, height = {height}");
    }
}

public class Rectangle
{
    public readonly float Width, Height;
    public Rectangle(float Width, float Height)
    {
        this.Width = Width;
        this.Height = Height;
    }
}

// 扩展类
public static class Extensions
{
    public static void Deconstruct(this Rectangle rectangle, out float width, out float height)
    {
        width = rectangle.Width;
        height = rectangle.Height;
    }
}
```

## 对象初始化器

对象任何**可访问**的字段/属性在构建之后，可通过对象初始化器直接为其进行设定值

```c#
public class Bunny
{
    public string Name;
    public bool LikesCarrots;
    public bool LikesHumans;
    public Bunny() { }
    public Bunny(string n) { Name = n; }
}

static void Main(string[] args)
{
    Bunny b1 = new Bunny { Name = "Bo", LikesCarrots = true, LikesHumans = false };
    Bunny b2 = new Bunny("Bo") { LikesCarrots = true, LikesHumans = false };
}
```

### 编译器生成的代码

临时变量可以保证：如果在初始化的过程中出现了异常，那么不会以一个初始化到一半的对象来结尾。

```c#
Bunny temp1 = new Bunny();  // 使用临时变量
temp1.Name = "Bo";
temp1.LikesCarrots = true;
temp1.LikesHumans = false;
Bunny b1 = temp1;

Bunny temp2 = new Bunny("bo");
temp2.LikesCarrots = true;
temp2.LikesHumans = false;
Bunny b2 = temp2;
```

### 对象初始化器 vs 可选参数

- 可选参数方式
- 优点：可以让Bunny类的字段/属性只读
- 缺点：每个可选参数的值都被嵌入到了calling site，C#会把构造函数的调用翻译成：
  - `Bunny b1 = new Bunny ("bo", true, false);`
  - 从另一个程序集实例化Bunny类，再为Bunny添加一个可选参数（共4个），除非程序集被编译，仍会调用三个可选参数的构造函数，报错。
  - 如果改变可选参数的值，除非程序集被编译，其它程序集仍会调用原来参数的构造函数。

### this 引用

this引用指的是实例的本身

```c#
public class Panda
{
    public Panda Mate;

    public void Marry (Panda partner)
    {
        Mate = partner;
        partner.Mate = this;
    }
}
```

- `this`引用可以让你把字段与本地变量或参数区分开
- 只有`class`/`struct`的非静态成员才可以使用`this`

```c#
public class Test
{
  string name;
  public Test (string name) { this.name = name; }
}
```

## 属性 Properties

从外边来看，属性和字段很像。但从内部看，属性含有逻辑，就像方法一样

```c#
Stock msft = new Stock();
msft.CurrentPrice = 30;
msft.CurrentPrice -= 3;
Console.WriteLine(msft.CurrentPrice);
```

### 属性的声明

属性的声明和字段的声明很像，但多了一个 `get` `set` 块。

```c#
public class Stock
{
  decimal currentPrice;
  
  public decimal CurrentPrice
  {
    get { return currentPrice; }
    set { currentPrice = value; }
  }
}
```

#### CLR的属性实现

- C#的属性访问器内部会编译成 get_XXX和set_XXX
  - `public decimal get_CurrentPrice { }`
  - `public void set_CurrentPrice { }`
- 简单的非virtual属性访问器会被JIT编译器进行内联（inline）操作，这会消除访问属性与访问字段之间的性能差异。内联是一种优化技术，它会把方法调用换成直接使用方法体。

#### get/set

- get/set 代表属性的访问器
- get访问器会在属性被读取的时候运行，必须返回一个该属性类型的值
- set访问器会在属性被赋值的时候运行，有一个隐式的该类型的参数 value，通常你会把 value 赋给一个私有字段

#### get 和 set的访问性

- get和set访问器可以拥有不同的访问级别
- 典型用法：`public get`，`internal/private set`
- 注意，属性的访问级别更“宽松”一些，访问器的访问级别更“严”一些

```c#
public class Foo
{
  private decimal x;
  public decimal X
  {
    get         { return x; }
    private set { x = Math.Round(value, 2)}
  }
}
```

### 属性与字段的区别

尽管属性的访问方式与字段的访问方式相同，但不同之处在于，**属性赋予了实现者对获取和赋值的完全控制权**。这种控制允许实现者选择任意所需的内部表示，不向属性的使用者公开其内部实现细节。

#### 只读和计算的属性

- 如果属性只有get访问器，那么它是只读的
- 如果只有set访问器，那么它就是只写的（很少这样用）
- 属性通常拥有一个专用的“幕后”字段（backing field），这个幕后字段用来存储数据

```c#
decimal currentPrice, sharesOwned;

public decimal Worth
{
  get { return currentPrice * sharesOwned; }
}
```

#### Expression-bodied 属性

- 从C# 6开始，你可以使用Expression-bodied形式来表示只读属性
- `public decimal Worth => currentPrice * sharesOwned;`
- C# 7，允许set访问器也可以使用该形式

```c#
public decimal Worth
{
  get => currentPrice * sharesOwned;
  set => sharesOwned = value / currentPrice;
}
```

#### 自动属性

- 属性最常见的一种实践就是：getter和setter只是对private field进行简单直接的读写
- 自动属性声明就告诉编译器来提供这种实现
- 编译器会自动生成一个私有的幕后字段，其名称不可引用（由编译器生成）
- set 访问器也可以是private 或 protected

```c#
public class Stock
{
  public decimal CurrentPrice { get; set; }
}
```

### 属性初始化器

- 从C# 6 开始，你可以为自动属性添加属性初始化器
- `public decimal CurrentPrice { get; set; } = 123;`
- 只读的自动属性也可以使用（只读自动属性也可以在构造函数里被赋值）
- `public int Maximum { get; } = 999;`

## 索引器

- 索引器提供了一种可以访问封装了列表值或字典值的class/struct的元素的一种自然的语法。
- 语法很像使用数组时用的语法，但是这里的索引参数可以是任何类型的

```c#
string s = "hello";
Console.WriteLine(s[0]);  // 'h'
Console.WriteLine(s[3]);  // 'l'
```

### 实现索引器

需要定义一个`this`属性，并通过中括号指定参数。

```c#
class Sentence
{
    string[] words = "The quick brown fox".Split();

    public string this[int wordNum]
    {
        get { return words[wordNum]; }
        set { words[wordNum] = value; }
    }
}
```

### 使用索引器

```c#
Sentence s = new Sentence();
Console.WriteLine(s[3]);    // fox
s[3] = "kangaroo";
Console.WriteLine(s[3]);    // kangaroo
```

### 多个索引器

- 一个类型可以声明多个索引器，它们的参数类型可以不同
- 一个索引器可以有多个参数

```c#
public string this [int arg1, string arg2]
{
  get { } set { }
}
```

### 只读索引器

- 如果不写set访问器，那么这个索引器就是只读的
- 在C# 6以后，也可以使用 expression-bodied 语法
  - `public string this[int wordNum] => words[wordNum]`

### CLR的索引器实现

索引器在内部会编译成get_Item和set_Item方法

```c#
public string get_Item (int wordNum) { }
public void set_Item (int wordNum, string value) { }
```

## 常量

- 一个值不可以改变的静态字段
- 在编译时值就已经定下来了
- 任何使用常量的地方，编译器都会把这个常量替换为它的值
- 常量的类型可以是内置的数值类型、`bool`、`char`、`string`或`enum`
- 使用`const`关键字声明，声明的同时必须使用具体的值来对其初始化

```c#
public class Test
{
  public const string Message = "HelloWorld";
}
```

### 常量与静态只读字段

- 常量比静态只读字段更严格：
  - 可使用的类型
  - 字段初始化的语义上
- 常量是在编译时进行值的估算的

### 注意

当值有可能改变，并且需要暴露给其它Assembly的时候，静态只读字段是相对较好的选择

```c#
public const decimal ProgramVersion = 2.3;

// 如果 Y Assembly引用了 X Assembly并且使用了这个常量，那么在编译的时候，2.3这个值就会被固化于Y Assembly里。这意味着，如果后来X重编译了，这个常量变成了2.4，如果Y不重新编译的话，Y将仍然使用2.3这个值，直到Y被重新编译，它的值才会变成2.4。静态只读字段就会避免这个问题的发生
```

#### 本地常量

方法里可以有本地的常量

`const double twoPI = 2 * System.Math.PI;`

## 静态构造函数

- 静态构造函数，每个类型执行一次
- 非静态构造函数，每个实例执行一次
- 一个类型只能定义一个静态构造函数
  - 必须无参
  - 方法名与类型一致

```c#
class Test
{
  static Test() { Console.WriteLine("Type Intialized"); }
}
```

如果静态构造函数抛出了未处理的异常，那么这个类型在该程序的剩余生命周期内将无法使用了

### 初始化顺序

- 静态字段的初始化器在静态构造函数被调用之前的一瞬间运行
- 如果类型没有静态构造函数，那么静态字段初始化器在类型被使用之前的一瞬间执行，或者更早
- 静态字段的初始化顺序与它们的声明顺序一致

```c#
class Program
{
    static void Main() { Console.WriteLine($"Foo.X = {Foo.X}"); }  // 3
}

class Foo
{
    public static Foo Instance = new Foo();
    public static int X = 3;

    Foo() { Console.WriteLine($"Foo() {X}"); } // 0
}
```

#### 静态类

- 类也可以是静态的
- 其成员必须全是静态的
- 不可以有子类，例如
  - System.Console
  - System.Math

## Finalizer 析构函数？

- Finalizer是class专有的一种方法
- 在GC回收未引用对象的内存之前运行
- 其实就是对object的Finalize()方法重写的一种语法

```c#
class Class1
{
  ~Class1() { }
}

// 编译生成
protected override void Finalize() { base.Finalize(); }

// C# 7 写法
~Class1() => Console.WriteLine("Finalizing");
```

## Partial 局部xx

### Partial Type 局部类型

- 允许一个类型的定义分布在多个地方（文件）
- 典型应用：一个类的一部分是自动生成的，另一部分需要手动写代码

```c#
// PaymentFormGen.cs - auto-generated
partial class PaymentForm { }

// PaymentForm.cs - hand-authored
partial class PaymentForm { }
```

- 每个分布的类都必须使用partial来声明
- 下面这个例子就会报错：
  - `partical class PaymentForm { } class PaymentForm { } // 报错`
- 每个分布类的成员不能冲突，不能有同样参数的构造函数
- 各分布类完全靠编译器来进行解析：每个分布类在编译时必须可用，且在同一个Assembly里



- 如果有父类，可以在一个或多个分布类上指明，但必须一致
- 每个分布类可以独立的实现不同的接口
- 编译器无法保证各分布类的字段的初始化顺序

### 局部方法

- partial 类型可以有partial method
- 自动生成的分布类里可以有partial method，通常作为“钩子”使用，在另一部分的partial method里，我们可以对这个方法进行自定义。

```c#
partial class PaymentForm       // In auto-generated file
{
  partial void ValidatePayment(decimal amount);
}

partial class PaymentForm       // In hand-authored file
{
  partial void ValidatePayment(decimal amount)
  {
    if(amount > 100)
  }
}
```

- partial method由两部分组成：定义 和 实现。
- 定义部分 通常是生成的
- 实现部分 通常是手动编写的
- 如果partial method只有定义，没有实现，那么编译的时候该方法定义就没有了，调用该方法的代码也没有了。这就允许自动生成的代码可以自由的提供钩子，不用担心代码膨胀
- partial method必须是void，并且隐式private的

## nameof C# 6

- nameof 操作符会返回任何符号（类型、成员、变量…）的名字（string）
- 利于重构

```c#
int count = 123;
string name = nameof(count);  // name is "count"

string name2 = nameof(StringBuilder.Length);  // name2 is "Length"
```

## 5个访问修饰符

- public，完全可访问。enum和interface的成员默认都是这个级别
- internal，当前assembly或朋友assembly可访问，非嵌套类型的默认访问级别
- private，本类可访问。class和struct的成员的默认访问级别。
- protected，本类或其子类可以访问。
- protected internal，联合了protected和internal的访问级别。

### 例子

```c#
class Class1 { }        // Class1 is internal (default)
public class Class2 { }

class ClassA { int x; } // x is private (dafault)
class ClassB { internal int x; }

class BaseClass
{
  void Foo() { }       // Foo is private (default)
  protected void Bar() { }
}
class SubClass : BaseClass
{
  void Test1() { Foo(); } // Error - cannot access Foo
  void Test2() { Bar(); } // OK
}
```

### 朋友assembly

- 通过添加System.Runtime.CompilerServices.InternalsVisibleTo 这个Assembly的属性，并指定朋友Assembly的名字，就可以把internal的成员暴露给朋友Assembly。
- `[assembly: InternalsVisibleTo ("Friend")]`
- 如果朋友Assembly有Strong name，那么就必须指定其完整的160字节的public key。
- `[assembly: InternalsVisibleTo ("StrongFriend, PublicKey=0024f000048c...")]`

### 类型限制成员的访问级别会

```c#
class C { public void Foo() { } }
```

### 访问修饰符的限制

- 当重写父类的函数时，重写后的函数和被重写的函数的访问级别必须一致
- 有一个例外：当在其它Assembly重写protected internal的方法时，重写后的方法必须是protected。

```c#
class BaseClass             { protected virtual  void Foo() { } }
class SubClass1 : BaseClass { protected override void Foo() { } } // OK
class SubClass2 : BaseClass { public    override void Foo() { } } // Error
```
