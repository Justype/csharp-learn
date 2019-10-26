# 句法和类型

## 句法

### 标识符 identifier

标识符就是程序员给他们所写的类、方法、变量等起的名字。

标识符必须是一个完整的单词，它由Unicode字符组成，并且由字母或者下划线开头。

### 关键字

关键字就是对编译器有特殊意义的一些名字。

- `using`, `class`, `static`, `void`, `int`

大部分关键字都是保留的，意思就是你不可以把它们当作标识符来用。

- 如果你非要用：前面加一个@

#### 上下文关键字 

上下文关键字 (Contextual Keywords) 用于在代码中提供特定含义，但不是 C# 中的保留字。 一些上下文关键字（如 `partial` 和 `where`）在两个或多个上下文中有特殊含义。

### 其它

Literal (12 等值？)，标点符号，操作符

### 注释

```c#
// 单行注释

/*
多行注释，范围内的全部被注释掉了
*/
```

## 类型

- 类型定义了一个值的蓝本。
- 变量是一个存储位置，它在不同的时期可能是包含不同的值。
  - 常量永远表示相同的值。
- C#里所有的值都是类型的实例。值的含义，变量可能拥有的值是什么，都由它的类型决定。
- 预定义的类型
  - `int`, `string`, `bool`, ...

### 自定义类型

- 可以使用原始类型来构建复杂类型。
- 类型的成员
  - 数据成员 Data
  - 函数成员 Function

### 构造函数和实例化

- 数据是通过实例化一个类型来创建的。
  - 预定义的类型直接写Literal就可以被实例化了。
  - 而自定义类型则通过new操作符来创建实例。

```c#
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
```

### 实例成员 vs 静态成员

- 操作于类型实例的数据成员和函数成员都叫做实例成员。
- 操作于类型而不是类型实例的数据成员和函数成员叫做静态成员。
  - `static`
- 静态类 `static class`的所有成员都是静态的。
  - 静态类不可以创建实例。例如`Console`，它在整个程序里就一个。

### 读取权限

`public`：可以把成员暴露给其它的类

`protected`

`private`