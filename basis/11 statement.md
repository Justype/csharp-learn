# 语句

- 函数由语句组成，这些语句按其出现的文本顺序执行
- 语句块就是 { } 之间的一些语句

## 声明语句

- 声明一个新的变量，也可使用表达式对该变量进行初始化
- 声明语句以分号 ；结束

### 例子

```c#
string someWord = "rosebud";
int someNumber = 42;

bool rich = true, famous = false;   // 可以多个赋值
const double c = 2.99792458E08;     // 常量初始化

```

### 本地变量

- 本地变量/常量的作用范围是当前的块
- 同一个块里面（包括里面嵌套的块）不可以声明重名的变量/常量

## 表达式语句

- 表达式语句是表达式，同样也是合理的语句
- 表达式语句要么改变了状态，要么调用了可以改变状态的东西
  - 改变状态其实就是指改变变量的值

### 类型

- 赋值表达式（包括自增和自减表达式）
- 方法调用表达式（void和非void）
- 对象实例化表达式

## 控制程序的走向

- 选择语句（`if`，`switch`）
- 条件操作符（`?:`）
- 循环语句（`while`, `do…while`, `for`, `foreach`）

### 选择语句

#### if 语句

- 如果if后边的bool表达式的值为true，那么if语句的内容就会被执行
- 使用 { } 增加可读性

#### else 子句

- if 语句可以有else子句
- else 子句还可以嵌套if 语句

```c#
int x = 0;
if (x < 0)
    Console.WriteLine("x < 0");
else if (x == 0)
    Console.WriteLine("x = 0");
else
    Console.WriteLine("x > 0");
```

#### switch 语句

- switch语句允许你根据变量可能有的值来选择分支程序
- 可能比 if 语句更简洁一些

##### 特点

- 当指定常量的时候，只能使用内置的整数类型、`bool`、`char`、`enum`和`string`类型
- 每个case子句的结尾，**必须**使用跳转语句来表明下一步往哪里执行：
  - `break`（跳到switch语句的结尾）
  - `goto case x`（跳转到其它case）
  - `goto default`（跳转到default子句）
  - 其它的跳转语句 `return`, `throw`, `continue`, `goto label`

#### switch with patterns (C# 7)

选择的是值类型

- object类型允许任何类型的变量
- 每个case子句指定一个类型，如果变量的类型与该类型一样，那么就匹配成功
- 可以使用when来断言一个case
- case子句的顺序是有关系的
- 可以case null

```c#
// var x = "Justype";
// var x = true;
var x = false;

switch ((object)x)
{
    case int y:
        Console.WriteLine("x is int");
        break;
    case bool y when (y == true):
        Console.WriteLine("x is true");
        break;
    case bool y:        // bool 比 bool when 的范围更大，所以要放在bool的后面
        Console.WriteLine("x is false");
        break;
    case string y:
        Console.WriteLine("x is string");
        break;
    default:
        Console.WriteLine("x is not in the list");
        break;
}
```



### 迭代语句

- C#允许一串语句可重复的执行
- `while`, `do…while`, `for, foreach` 语句

#### while

- while一个bool表达式值为true的时候，会循环重复的执行代码体
- 这个bool表达式在代码体循环执行之前被检验

#### do…while

和while差不多，只不过bool表达式在代码体执行完之后才被检验

#### for

- 和 while 循环差不多，但有特殊的子句用来初始化和迭代变量
- 结构：`for(初始化子句; 条件子句; 迭代子句) 语句或语句块 `
  - 初始化子句：在循环开始前执行，用来初始化一个或多个迭代变量
  - 条件子句：bool表达式，为true时，会执行代码体
  - 迭代子句：每次语句块迭代之后执行；通常用来更新迭代变量
  - 无限循环 `for(;;)`

```c#
whileDemo:
    int x = 0;
    while (x < 10)
    {
        Console.WriteLine($"x = {x}");
        x++;
    }
doWhileDemo:
    int y = 10;
    do
    {
        Console.WriteLine($"y = {y}");
        y++;
    } while (y < 10);
forDemo:
    for (int z = 0; z < 10; z++)
        Console.WriteLine($"z = {z}");

/* Output:
x = 0
x = 1
x = 2
x = 3
x = 4
x = 5
x = 6
x = 7
x = 8
x = 9

y = 10

z = 0
z = 1
z = 2
z = 3
z = 4
z = 5
z = 6
z = 7
z = 8
z = 9
*/
```

#### foreach

- 迭代enumerable对象里的元素
- C#里大部分集合类型都是enumerable的

### 跳转语句

- break
- continue
- goto
- return
- throw

#### break

break语句可以结束迭代或switch语句的代码体

#### continue

continue语句会放弃当前迭代中剩余语句的执行，直接从下一次迭代开始

#### goto

- goto语句把执行跳转到另一个label的语句块
- goto 语句label;
- 当用于swtich语句内时，goto case case常量; （只能用于常量）
- Label相当于是一个代码块的占位符，放在语句前边，使用冒号 ：做后缀

#### return

- return 语句会退出方法，并返回一个表达式，该表达式的类型和方法的返回类型一致（方法不是void的）
- return语句可以放在方法的任何一个地方，除了finally块里

#### throw

- 抛出异常，表示发生了错误

跳转语句 -遵循try语句的可靠性原则

- 跳转出try块时，在到达跳转目标之前，总会先执行try的finally块
- 不可以从finally块里面跳转到外边，除了throw

## 其它语句

- using
- lock

