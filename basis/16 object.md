# object类型

- object（System.Object）是所有类型的终极父类。
- 所有类型都可以向上转换为object。

```c#
public class Stack
{
    int position;
    object[] data = new object[10];
    public void Push(object obj) { data[position] = obj; }
    public void Pop() { return data[--position]; }
}

Stack stack = new stack();
stack.Push("sausage");
string s = (string) stack.Pop();    // Downcast, so explicit cast is needed
Console.WriteLine(s);       // sausage
```

- object是引用类型
- 但值类型可以转化为object，反之亦然。（类型统一）
  - `stack.Push(3);   int three = (int)stack.Pop();`
- 在值类型和object之间转化的时候，CLR必须执行一些特殊的工作，以弥合值类型和引用类型之间语义上的差异，这个过程就叫做装箱和拆箱。

## 装箱 & 拆箱

### 装箱

- 装箱就是把值类型的实例转化为引用类型实例的动作
- 目标引用类型可以是object，也可以是某个接口

```c#
int x = 9;
object obj = x;     // Box the int
```

### 拆箱

- 拆箱正好相反，把那个对象转化为原来的值类型
- 拆箱需要显式的转换。

```c#
int y = (int)obj;   // Unbox the int
```

- 运行时会检查这个值类型和object对象的真实类型是否匹配
- 如果不匹配就抛出InvalidCastException

```c#
object obj = 9;         // 9 is inferred to be of type of int
long x = (long)obj;     // InvalidCastException

long x2 = (int)obj;     // OK 转成int32，再隐式转换成int64

object obj2 = 3.5;
int x3 = (int)(double)obj; // x3 = 3
```

### 注意

- 装箱对于类型统一是非常重要的。但是系统不够完美
- 数组和泛型只支持引用转换，不支持装箱

```c#
object[] a1 = new string[3];    // Legal
object[] a2 = new int[3];       // Error
```

### 装箱拆箱的复制

- 装箱会把值类型的实例复制到一个新的对象
- 拆箱会把这个对象的内容再复制给一个值类型的实例

```c#
int i = 3;
object boxed = i;
i = 5;
Console.WriteLine(boxed);   // 3
```

## 静态和运行时类型检查

- C#的程序既会做静态的类型检查（编译时），也会做运行时的类型检查（CLR）
- 静态检查：不运行程序的情况下，让编译器保证你程序的正确性
- 运行时的类型检查由CLR执行，发生在向下的引用转换或拆箱的时候。
- 运行时检查之所以可行是因为：每个在heap上的对象内部都存储了一个类型token。这个token可以通过调用object的GetType()方法来获取

```c#
int x = "5";        // 

object y = "5";
int z = (int)y;     // Runtime error, downcast failed
```

## GetType &typeof

- 所有C#的类型在运行时都是以System.Type的实例来展现的
- 两种方式可以获得System.Type对象：
  - 在**实例**上调用GetType()方法
  - 在**类型名**上使用typeof操作符。
- GetType是在运行时被算出的
- typeof 是在编译时被算出（静态）（当涉及到泛型类型参数时，它是由JIT编译器来解析的）

### System.Type

System.Type的属性有：类型的名称，Assembly，基类等等。

### .ToString()


