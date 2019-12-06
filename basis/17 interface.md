# interface

## 接口

- 接口与class类似，但是它只为其成员提供了规格，而没有提供具体的实现
- 接口的成员都是隐式抽象的
- 一个class或者struct可以实现多个接口

```c#
public interface IEnumerator
{
    bool MoveNext();
    object Current { get; }
    void Reset();
}
```

### 接口的实现

- 接口的成员都是隐式public的，不可以声明访问修饰符
- 实现接口对它的所有成员进行**public**的实现：

```c#
internal class Countdown : IEnumerator
{
    int count = 11;
    public bool MoveNext() => count-- > 0;
    public object Current => count;
    public void Reset() { throw new NotSupportedException(); }
}
```

### 对象与接口的转换

可以隐式的把一个对象转化成它实现的接口：

```c#
IEnumerator e = new Countdown();
while(e.MoveNext())
    Console.Write(e.Current);   // 109876543210
```

虽然Countdown是一个internal的class，但是可以通过把它的实例转化成IEnumerator接口来公共的访问它的成员。

```c#
public interface IFoo { void Do(); }
public class Parent : IFoo
{
    // void IFoo.Do() => Console.WriteLine("P");
    public void Do() => Console.WriteLine("P");
}

class Program
{
    static void Main(string[] args)
    {
        IFoo f = new Parent();
        f.Do();     // P
    }
}
```

### 接口的扩展

接口可以继承其它接口

```c#
public interface IUndoable             { void Undo(); }
public interface IRedoable : IUndoable { void Redo(); }
// IRedoable 继承了 IUndoable 的所有成员
```

### 显式的接口实现

实现多个接口的时候可能会造成成员签名的冲突。通过显式实现接口成员可以解决这个问题。

```c#
interface I1 { void Foo(); }
interface I2 { int  Foo(); }

public class Widget : I1, I2
{
    public void Foo()
    {
        Console.WriteLine("Widget's implementation of I1.Foo");
    }
    int I2.Foo()    // 显式的接口实现
    {
        Console.WriteLine("Widget's implementation of I2.Foo");
        return 42;
    }
}
```

本例中，想要调用相应实现的接口方法，只能把其实例转化成相应的接口才行：

```c#
Widget w = new Widget();
w.Foo();        // Widget's implementation of I1.Foo
                // I1的实现为 public，I2的实现不是public
((I1)w).Foo();  // Widget's implementation of I1.Foo
((I2)w).Foo();  //Widget's implementation of I2.Foo
```

另一个显式实现接口成员的理由是故意隐藏那些对于类型来说不常用的成员。

```c#
public interface IFoo { void Do(); }
public class Parent : IFoo { public void Do() => Console.WriteLine("Parent"); }
public class Child : Parent { public void Do() => Console.WriteLine("Child"); }

class Program
{
    static void Main(string[] args)
    {
        Child c = new Child();
        c.Do();             // Child
        ((Parent)c).Do();   // Parent
        ((IFoo)c).Do();     // Parent
        // Parent类 是对 IFoo接口的直接实现
    }
}
```

如果签名和返回值完全一样，可以只实现一次：

```c#
public interface IFoo { void Foo(); }
public interface IBar { void Foo(); }
public class Parent : IFoo, IBar
{
    public void Foo() => Console.WriteLine("Parent");
}

class Program
{
    static void Main(string[] args)
    {
        Parent p = new Parent();
        IFoo f = p;
        IBar b = p;
        f.Foo();        // Parent
        b.Foo();        // Parent
    }
}
```

## virtual的实现接口成员

- 隐式实现的接口成员默认是sealed的。
- 如果想要进行重写的话，必须在基类中把成员标记为virtual或者abstract。

```c#
public interface IUndoable { void Undo(); }

public class TextBox : IUndoable
{
    public virtual void Undo() => Console.WriteLine("TextBox.Undo");
}
public class RichTextBox : TextBox
{
    public override void Undo() => Console.WriteLine("RichTextBox.Undo");
}
```

无论是转化为基类还是转化为接口来调用接口的成员，调用的都是子类的实现:

```c#
RichTextBox r = new RichTextBox();
r.Undo();               // RichTextBox.Undo
((IUndoable)r).Undo();  // RichTextBox.Undo
((TextBox)r).Undo();    // RichTextBox.Undo
```

显示实现的接口成员不可以被标记为virtual，也不可以通过寻常的方式来重写，但是可以对其进行重新实现。

## 在子类中重新实现接口

- 子类可以重新实现父类已经实现的接口成员
- 重新实现会“劫持”成员的实现（通过转化为接口然后调用），无论在基类中该成员是否是virtual的。无论该成员是显式的还是隐式的实现（但最好还是显式实现的）。

```c#
public interface IFoo { void Do(); }
public class Parent : IFoo
{
    void IFoo.Do() => Console.WriteLine("P");
}
public class Child : Parent, IFoo
{
    public void Do() => Console.WriteLine("C");
}

class Program
{
    static void Main(string[] args)
    {
        Child c = new Child();
        c.Do();
        ((IFoo)c).Do();
        // 子类直接实现了 IFoo接口
    }
}
```

```c#
public interface IUndoable { void Undo(); }

public class TextBox : IUndoable
{
    void IUndoable.Undo() => Console.WriteLine("TextBox.Undo");
}
public class RichTextBox : TextBox， IUndoable
{
    public void Undo() => Console.WriteLine("RichTextBox.Undo");
}

// 转化为接口后调用重新实现的成员，就是调用子类的实现

RichTextBox r = new RichTextBox();
r.Undo();               // RichTextBox.Undo     Case 1
((IUndoable)r).Undo();  // RichTextBox.Undo     Case 2
```

如果Textbox是隐式实现的Undo

```c#
public interface IUndoable { void Undo(); }

public class TextBox : IUndoable
{
    public void Undo() => Console.WriteLine("TextBox.Undo");
}
public class RichTextBox : TextBox， IUndoable
{
    public void Undo() => Console.WriteLine("RichTextBox.Undo");
}

RichTextBox r = new RichTextBox();
r.Undo();               // RichTextBox.Undo     Case 1
((IUndoable)r).Undo();  // RichTextBox.Undo     Case 2
((TextBox)r).Undo();    // TextBox.Undo         Case 3
```

说明重新实现接口这种劫持只对转化为接口后的调用起作用，对转化为基类后的调用不起作用。

重新实现适用于重写显式实现的接口成员。

### 重新实现接口的替代方案

- 即使是显式实现的接口，接口的重新实现也可能有一些问题：
    - 子类无法调用基类的方法
    - 基类的开发人员没有预见到方法会被重新实现，并且可能不允许潜在的后果

- 最好的办法是设计一个无需重新实现的基类：
    - 隐式实现成员的时候，按需标记virtual
    - 显式实现成员的时候，可以这样做：

```c#
public interface IUndoable { void Undo(); }

public class TextBox : IUndoable
{
    void IUndoable.Undo()         => Undo();    // Calls method below
    protected virtual void Undo() => Console.WriteLine("TextBox.Undo");
}
public class RichTextBox : TextBox， IUndoable
{
    // 重写了Undo 也就重写了接口的Undo
    public override void Undo() => Console.WriteLine("RichTextBox.Undo");
}
```

如果不想有子类，那么直接把class 给 sealed。

## 接口与装箱

- 把struct转化为接口会导致装箱
- 调用struct上隐式实现的成员不会导致装箱

```c#
interface I { void Foo(); }
struct S : I { public void Foo() { } }

S s = new S();
s.Foo();        // No boxing

I i = s;        // Box occurs when casting to interface
i.Foo();
```
