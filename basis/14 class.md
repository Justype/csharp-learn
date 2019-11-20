# class

- 最常见的一种引用类型
- `class ClassName { }`

### Field 字段

- 是Class或Struct的成员，它是一个变量

```c#
class Octopus
{
    string name;
    public int age = 10;
}
// 可同时声明多个字段
```

#### 字段初始化

- 字段在声明的时候可以进行初始化
- 未初始化的字段有一个默认
- 字段的初始化在构造函数**之前**运行

#### readonly

- `readonly` 修饰符防止字段在构造之后被改变
- `readonly` 字段只能在声明的时候被赋值，或在构造函数里被赋值

### 方法

- 通常包含一些语句，会执行某个动作
- 可以传入参数
- 可以返回数据，返回类型
  Void，不返回数据
- ref/out 参数

#### 方法的签名

- 一个类型内，其每个方法的签名必须是唯一的
- 签名：方法名、参数类型（含顺序，但与参数名称和返回类型无关）

#### 方法的重载 overload

类型里的方法可以进行重载（允许多个同名的方法同时存在），只要这些方法的签名不同就行

```c#
void Foo() { }
void Foo(int x) { }
void Foo(double x) { }
void Foo(float x, int y) { }
void Foo(int x, float y) { }
```

#### 按值传递 vs 按引用传递

参数是按值传递的还是按引用传递的，也是方法签名的一部分

```c#
void Foo(int x) { }
void Foo(ref int x) { } // OK so far
void Foo(out int x) { } // Compile-time error
// 二三互斥，按引用互斥
```



#### Expression-bodied 方法

适用于单表达式方法

```c#
// 例子
void Foo(int x) => Console.WriteLine(x);

int Foo(int x) => x * 2;
// 相当于 int Foo（int x) { return x * 2; }

```

#### 本地方法

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







