# struct

## struct 与 class

- struct和class差不多，但是有一些不同：
- struct是值类型，class是引用类型
- struct不支持继承（除了隐式的继承了object，具体点就是System.ValueType）

## struct的成员

class能有的成员，struct也可以有，但是以下几个**不行**：

- 无参构造函数
- 字段初始化器
- 终结器
- virtual或protected成员

## struct的构建

- struct有一个无参的构造函数，但是你不能对其进行重写。它会对字段进行按位归零操作。
- 当你定义struct构造函数的时候，必须显式的为**每个字段赋值**。
不可以有字段初始化器。

## struct例子

```c#
public struct Point
{
    int x, y;
    public Point(int x, int y) { this.x = x; this.y = y; }
}

Point p1 = new Point();     // p1.x and p1.y will be 0
Point p2 = new Point(1, 1); // p2.x and p2.y will be 0
```

### 错误例子

```c#
public struct Point
{
    int x = 1;                          // Illegal: field initializer
    int y;
    public Point() { }                  // Illeagal: parameterless construstor
    public Point(int x) { this.x = x; } // Inllegal: must assign field y
}
```
