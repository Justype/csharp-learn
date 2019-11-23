## 继承

- 一个类可以继承另一个类，从而对原有类进行扩展和自定义
- 可以叫做子类和父类
- 继承的类让你可以重用被继承类的功能
- C#里，一个类只能继承于一个类，但是这个类却可以被多个类继承

```C#
public class Asset
{
    public string Name;
}

public class Stock : Asset
{
    public long SharesOwned;
}

public class House : Asset
{
    public decimal Mortgage;
}


Stock msft = new Stock{Name = "MSFT", SharesOwned = 1000};
Console.WriteLine(msft.Name);         // MSFT
Console.WriteLine(msft.SharesOwned);  // 1000

House mansion = new House{Name = "Masion", Mortgage = 250000};
Console.WriteLine(mansion.Name);      // Masion
Console.WriteLine(mansion.Mortgage);  // 250000
```

## 多态

- 引用是多态的，类型为x的变量可以引用其子类的对象
- 因为子类具有父类的全部功能特性，所以参数可以是子类

```c#
public static void Display(Asset asset)
{
    System.Console.WriteLine(asset.Name);
}

Display(msft);
Display(mansion);
```

反过来不行

```c#
public static void Display(House house)
{
    System.Console.WriteLine(house.Mortgage);
}

Display(new Asset());  // 报错
```

## 引用转换

- 一个对象的引用可以隐式的转换到其父类的引用（向上转换）
- 想转换到子类的引用则需要显式转换（向下转换）
- 引用转换：创建了一个新的引用，它也指向同一个对象

### 向上转换

从子类的引用创建父类的引用

```c#
Stock msft = new Stock();
Asset a = msft;             // Upcast
```

变量a依然指向同一个Stock对象（msft也指向它）

```c#
Console.WriteLine (a == msft);  // True
```

尽管变量a和msft指向同一个对象，但是a的可视范围更小一些

```c#
Console.WriteLine (a.Name);         // OK
Console.WriteLine (a.SharesOwned);  // Error: SharesOwned undefined
```

### 向下转换

从父类的引用创建出子类的引用

```c#
Stock msft = new Stock();
Asset a = msft;                     // Upcast
Stock s = (Stock)a;                 // Downcast
Console.WriteLine(s.SharesOwned);   // <No error>
Console.WriteLine(s == a);          // True
Console.WriteLine(s == msft);       // True
```

- 和向上转换一样，只涉及到引用，底层的对象不会受影响
- 需要显式转换，因为可能会失败

```c#
House h = new House();
Asset a = h;            // Upcast always succeeds
Stock s = (Stock)a;     // Downcast fails: a is not a stock
```

如果向下转换失败，那么会抛出 InvalidCastException（属于运行时类型检查）

### as 操作符

as 操作符会执行向下转换，如果转换失败，不会抛出异常，值会变为null

```c#
Asset a = new Asset();
Stock s = a as Stock;   // s is null; no exception throw
```

as 操作符无法做自定义转换

```c#
long x = 3 as long;     // Compile-time error
```

### is 操作符

- is 操作符会检验引用的转换是否成功。换句话说，判断对象是否派生于某个类（或者实现了某个接口）
- 通常用于向下转换前的验证：

```c#
if(a is Stock)
    Console.WriteLine(((Stock)a).SharesOwned);
```

如果拆箱转换可以成功的话，那么使用is操作符的结果会是true

#### is 操作符和模式变量

C# 7里，在使用 is 操作符的时候，可以引入一个变量

```c#
Stock s;
if(a is Stock)
{
    s = (Stock)a;
    Console.WriteLine(s.SharesOwned);
}
```

引入的变量可以立即“消费”

```c#
if(a is Stock s && s.SharesOwned > 100000)
    Console.WriteLine("Wealthy");
else
    s = new Stock();    // s is in scope

Console.WriteLine(s.SharesOwned)    // Still in scope
```

## virtual函数成员

标记为virtual的函数可以被子类重写，包括方法、属性、索引器、事件

```c#
public class Asset
{
    public string Name;
    public virtual decimal Liabiligy => 0;
}
```

### override 重写

使用override修饰符，子类可以重写父类的函数

```c#
public class Asset
{
    public string Name;
    public virtual decimal Liabiligy => 0;
}
public class House : Asset
{
    public decimal Mortgage;
    public override decimal Liability => Mortgage;
}

House mansion = new House { Name = "McMansion", Mortgage = 250000 };
Asset a = mansion;
Console.WriteLine(mansion.Liability);   // 250000
Console.WriteLine(a.Liability);         // 250000
```

- virtual方法和重写方法的签名、返回类型、可访问程度必须是一样的
- 重写方法里使用base关键字可以调用父类的实现

#### 注意

- 在构造函数里调用virtual方法可能比较危险，因为编写子类的开发人员可能不知道他们在重写方法的时候，面对的是一个未完全初始化的对象。
- 换句话说，重写的方法可能会访问依赖于还未被构造函数初始化的字段的属性或方法。

## 抽象类和抽象成员

- 使用abstract声明的类是抽象类
- 抽象类不可以被实例化，只有其具体的子类才可以实例化
- 抽象类可以定义抽象成员
- 抽象成员和virtual成员很像，但是不提供具体的实现。子类必须提供实现，除非子类也是抽象的

```c#
public abstract class Asset
{
    // Note empty implementation
    public abstract decimal NetValue { get; }
}
public class Stock : Asset
{
    public long SharesOwned;
    public decimal CurrentPrice;

    // Override like a virtual method.
    public override decimal NetValue => CurrentPrice * SharesOwned;
}
```

## 隐藏被继承的成员

父类和子类可以定义相同的成员：

```c#
public class A      { public int Counter = 1;}
public class B : A  { public int Counter = 2;}
```

- class B中的 Counter字段就隐藏了A里面的Counter字段（通常是偶然发生的）。例如子类添加某个字段之后，父类也添加了相同的一个字段。
- 编译器会发出警告

- 按照如下规则进行解析：
  - 编译时对A的引用会绑定到A.Counter
  - 编译时对B的引用会绑定到B.Counter

### new

- 如果像故意隐藏父类的成员，可以在子类的成员前面加上 new 修饰符
- 这里的 new 修饰符仅仅会抑制编译器的警告而已

```c#
public class A      { public     int Counter = 1;}
public class B : A  { public new int Counter = 2;}
```

### new vs override

```c#
public class BaseClass
{
    public virtual void Foo() { Console.WriteLine("BaseClass.Foo"); }
}

public class Overrider : BaseClass
{
    public override void Foo() { Console.WriteLine("Override.Foo"); }
}

public class Hider : BaseClass
{
    public new void Foo() { Console.WriteLine("Hider.Foo"); }
}

Overrider over = new Overrider();
BaseClass b1 = over;
over.Foo();             // Overrider.Foo
b1.Foo();               // Overrider.Foo
Hider h = new Hider();
BaseClass b2 = h;
h.Foo();                // Hider.Foo
b2.Foo();               // BaseClass.Foo
```

### sealed

- 针对重写的成员，可以使用sealed关键字把它“密封”起来，防止它被其子类重写
- 也可以sealed 类本身，就隐式的sealed所有的virtual函数了
```c#
public sealed override decimal Liability { get { return Mortgage; } }
```

## base

- base 和 this 略像，base主要用于：
- 从子类访问父类里被重写的函数
- 调用父类的构造函数
- 这种写法可保证，访问的一定是Asset的Liability属性，无论该属性是被重写还是被隐藏了

```c#
public class House : Asset
{
    ...
    public override decimal Liability => base.Liability + Mortgage;
}
```

### 构造函数和继承

- 子类必须声明自己的构造函数
- 从子类可访问父类的构造函数，但不是自动继承的
- 子类必须重新定义它想要暴露的构造函数

```c#
public class BaseClass
{
    public int x;
    public BaseClass() { }
    public BaseClass(int x) { this.x = x; }
}

public class SubClass : BaseClass { }

class Program
{
    static void Main(string[] args)
    {
        Subclass s = new SubClass(123);     // 报错
    }
}
```

- 调用父类的构造函数需要使用 base 关键字
- 父类的构造函数肯定会先执行

```c#
public class BaseClass
{
    public int x;
    public BaseClass() { }
    public BaseClass(int x) { this.x = x; }
}

public class SubClass : BaseClass
{
    public SubClass(int x) : base(x) { }
}

class Program
{
    static void Main(string[] args)
    {
        Subclass s = new SubClass(123);
    }
}
```

### 隐式调用无参的父类构造函数

- 如果子类的构造函数里没有使用base关键字，那么父类的无参构造函数会被隐式的调用
- 如果父类没有无参构造函数，那么子类就必须在构造函数里使用base关键字

```c#
public class BaseClass
{
    public int x;
    public BaseClass() { x = 1; }
}

public class SubClass : BaseClass
{
    public SubClass(int x){ Console.WriteLine(x); } // 1
}
```

### 构造函数和字段初始化顺序

- 对象被实例化时，初始化动作按照如下顺序进行：
  - 从子类到父类：
    - 字段被初始化
    - 父类构造函数的参数值被算出
  - 从父类到子类
    - 构造函数体被执行

```c#
public class B
{
    int x = 1;              // Execute 3rd
    public B(int x) { }     // Execute 4th
}
public class D : B
{
    int y = 1;              // Execute 1st
    public D(int x) 
        : base (x + 1)      // Execute 2nd
    {
                            // Execute 5th
    }
}
```

### 重载和解析

```c#
static void Foo(Asset a) { }
static void Foo(House h) { }

// 重载方法被调用时，更具体的类型拥有更高的优先级
House h = new House();
Foo(h);                 // Calls Foo(House)

// 调用哪个重载方法是在编译时就确定下来的
Asset a = new House();
Foo(a);                 // Calls Foo(Asset)
```
