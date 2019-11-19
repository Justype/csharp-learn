# 命名空间

- 命名空间是类型的域，类型通常被组织到层次化的命名空间中，使它们更容易查找且避免了冲突
  - `System.Security.Cryptography `
- 命名空间是类型名称的组成部分
  - `System.Security.Cryptography.RSA rsa = System.Security.Cryptography.RSA.Create(); `
- 命名空间独立于Assembly，对成员的可见性也没有影响

## namespace

- `namespace` 关键字定义了块 { } 里面类型的命名空间
- 命名空间中的点表示嵌套命名空间的层次结构
- 未在任何命名空间里定义的类型被称为驻留在全局名称空间中

```c#
namespace Outer.Inner
{
    class InnerClass{ }
}

/* 相当于
namespace Outer
{
    namespace Inner
    {
        class InnerClass { }
    }
}
*/
```

## using

- `using`指令可导入一个命名空间，允许你不需要类型的全名就可以使用该类型
- 可以在不同的命名空间中定义相同名称的类型

### using static

- 从C# 6开始，你不仅仅可以引入命名空间，还可以引入具体的类型，这就需要使用using static
- 被引入类型的所有**静态成员**可被直接使用，无需使用类名
- 所有可访问的静态成员都会被引入，字段、属性、嵌套类型
- 也可用于enum，这样的话它的成员就被引入了
- 如果多个static引入存在歧义的话，将会发生错误

## 命名空间里的规则

- 在外层命名空间声明的名称可以在内部的命名空间里直接使用，无需全名
- 如果想要引用命名空间层次结构下不同分支的类型，可以使用部分名称
- **名称隐藏**：如果同一个类型名同时出现在外层和内层的命名空间里，那么，直接使用类型名的时候，使用的是内层的。
- **重复的命名空间**：可以重复声明命名空间，只要它们下面没有冲突的类型名就可以。

### 嵌套using指令

- 可在一个命名空间内嵌套using指令
- 可以让using的东西的作用范围限制在这个命名空间内

```c#
namespace N1
{
    class Class1 { }
}

namespace N2
{
    using N1;
    class Class2 : Class1 { }
}

namespace N2    // 重复的命名空间
{
    class Class3 : Class1 { } // 报错
}
```



### 为命名空间/类型起别名

- 引入命名空间之后，可能会导致类型名冲突
- 可以不引入完整的命名空间，只引入你需要的那个类型，然后给这个类型起一个别名
- `using xxx = XXX.YYY`

### 命名空间的高级特性

#### Extern

`Extern`别名允许你的程序引用两个全名相同的类型名，通常这两个类型来自不同的Assembly（程序集 dll）

```c#
Library 1:
    // csc target:library /out:Widgets1.dll widgetsv1.cs
    namespace Widgets
    {
        public class Widget { }
    }

Library 2:
    // csc target:library /out:Widgets2.dll widgetsv2.cs
    namespace Widgets
    {
        public class Widget { }
    }


// csc /r:Widgets1.dll /r:Widgets2.dll application.cs
using Widget; // 报错
class Test
{
    static void Main()
    {
        Widget w = new Widget();
    }
}

// 注意：编译器配置不同
// csc /r:W1=Widgets1.dll /r:W2=Widgets2.dll application.cs

extern alias W1;
extern alias W2;

class Test
{
    static void Main()
    {
        W1.Widgets.Widget w1 = new W1.Widgets.Widget();
        W2.Widgets.Widget w2 = new W2.Widgets.Widget();
    }
}
```

#### 命名空间别名限定符 Namespace alias qualifiers

内层命名空间的类型名会把外层命名空间下的类型名隐藏，有时即使使用全名也无法解决冲突

- 使用global命名空间，:: 
- extern alias

```c#
namespace N
{
    class A
    {
        public class B { }
        static void Main()
        {
            new A.B(); // 调用的是 namespace N { class A { class B {} } }
            new global::A.B(); // 调用的是 namespace A { class B {} }
        }
    }
}
namespace A
{
    class B { }
}
```





