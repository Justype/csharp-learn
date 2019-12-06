# 枚举

枚举是一个特殊的值类型，它可以让你指定一组命名的数值常量。

本质上是数值常量，但有名称。

```c#
public enum BorderSide { Left, Right, Top, Bottom }

BorderSide topSide = BorderSide.Top;
bool isTop = (topSide == BorderSide.Top);   // true
```

## 枚举的底层原理

- 每个枚举都对应一个底层的整形数值(Enum.GetUnderlyingType())。默认：
    - 是int类型
    - 0，1，2…会按照枚举成员的声明顺序自动赋值
- 也可以指定其他的类型作为枚举的整数类型，例如byte：
    - `public enum BorderSide : byte { Left, Right, Top, Bottom }`
- 也可以单独指定枚举成员对应的整数值
    - `public enum BorderSide : byte { Left=1, Right=2, Top=10, Bottom=11 }`
- 也可以只指定其中某些成员的数值，未被赋值的成员将接着它前面已赋值成员的值递增

## 枚举的转换

枚举可以显式的和其底层的数值相互转换

```c#
int i = (int)BorderSide.Left;
BorderSide side = (BorderSide)i;
bool LeftOrRight = (int)side <= 2;

HorizontalAlignment h = (HorizontalAlignment)BorderSide.Right;
// same as:
// HorizontalAlignment h = (HorizontalAlignment)(int)BorderSide.Right;
```

## 0

- 在枚举表达式里，0数值会被编译器特殊对待，它不需要显式的转换：
    - BorderSide b = 0; if (b == 0) { }
- 因为枚举的第一个成员通常被当作“默认值”，它的值默认就是0
- 组合枚举里，0表示没有标志（flags）

## Flags Enum

可以对枚举的成员进行组合
为了避免歧义，枚举成员的需要显式的赋值。典型的就是使用2的乘幂

```c#
[Flags]
public enum BorderSides { None=0, Left=1, Right=2, Top=4, Bottom=8 }

/*
内存中：
Top:    0000_0001
Right:  0000_0010
Bottom: 0000_0100
Left:   0000_1000
*/
```

flags enum，可以使用位操作符，例如 | 和 &。

```c#
var leftRight = BorderSides.Top | BorderSides.Right;

if((leftRight & BorderSides. Left) != 0)
    Console.WriteLine("Includes Left");

string formatted = leftRight.ToString();    // "Left, Right"

BorderSides s = BorderSides.Left;
s |= BorderSides.Right;
Console.WriteLine(s == leftRight);  // true

s ^= BorderSides.Right;     // Toggles BorderSides.Right
Console.WriteLine(s);       // Left
```

### 底层原理

```c#
// 
var b = BorderSides.Top | BorderSides.Right | BorderSides.Bottom;
/*
Top:    0000_0001
Right:  0000_0010
Bottom: 0000_0100
|------------------
b:      0000_0111
*/

var c = b & BorderSide.Right;
/*
b:      0000_0111
Right:  0000_0010
&------------------
c:      0000_0010
*/
```

### Flags 属性

- 按约定，如果枚举成员可组合的话，flags 属性就应该应用在枚举类型上。
    - 如果声明了这样的枚举却没有使用flags属性，你仍然可以组合枚举的成员，但是调用枚举实例的ToString()方法时，输出的将是一个数值而不是一组名称。
- 按约定，可组合枚举的名称应该是*复数*的。

在声明可组合枚举的时候，可以直接使用组合的枚举成员作为成员：

```c#
[Flags]
public enum BorderSides
{
    None = 0,
    Left = 1, Right = 2, Top = 4, Bottom = 8,
    LeftRight = Left | Right,
    TopBottom = Top | Bottom,
    All       = LeftRight | TopBottom
}
```

## 枚举支持的操作符

- `= == != < > <= >= + - ^ & | ~ += -= ++ -- sizeof`
- 其中按位的、比较的、算术的操作符返回的都是处理底层值后得到的结果
- 加法操作符只允许一个枚举和一个整形数值相加，两个枚举相加是不可以的

## 类型安全的问题

```c#
public enum BorderSide { Left, Right, Top, Bottom }
BorderSide b = (BorderSide) 12345;
Console.WriteLine(b);       // 12345

BorderSide b = BorderSide.Bottom;
b++;        // No errors
```

- 检查枚举值的合理性：Enum.IsDefined()静态方法。
- 据说不支持flags enum

```c#
BorderSide side = （BorderSide) 12345;
Console.WriteLine(Enum.IsDefined(typeof(BorderSide), side));    // False
```

