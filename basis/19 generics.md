# 泛型

泛型会声明类型参数 – 泛型的消费者需要提供类型参数（argument）来把占位符类型填充上。

```c#
public class Stack<T>
{
    int position;
    T[] data = new T[100];
    public void Push(T obj) => data[position++] = obj;
    public T Pop()          => data[--position];
}

var stack = new Stack<int>();
stack.Push(5);
stack.Push(10);
int x = stack.Pop();    // x is 10
int y = stack.Pop();    // y is 5


public class ###
{
    int position;
    int[] data = new int[100];
    public void Push(int obj) => data[position++] = obj;
    public int Pop()          => data[--position];
}
```

## 范型的作用

- 跨类型可复用的代码：继承 和 泛型。
- 继承 → 基类
- 泛型 → 带有“（类型）占位符” 的“模板”

```c#
public class ObjectStack
{
    int position;
    object[] data = new object[10];
    public void Push(object obj) => data[position++] = obj;
    public object Pop()          => data[--position];
}

// 需要装箱和向下转换，这种转换在编译时无法进行检查

stack.Push ("s");           // Wrong type, but no error !!
int i = (int)stack.Pop();   // Downcast - runtime error
```

### Open Type & Closed Type

- `Stack<T>` Open Type（开放类型）
- `Stack<int>` Closed Type（封闭类型）
- 在运行时，所有的泛型类型实例都是封闭的（占位符类型已被填充了）

```c#
var stack = new Stack<T>;

public class Stack<T>
{
    ...
    public Stack<T> Clone()
    {
        Stack<T> clone = new Stack<T>();    // legal
    }
}
```

## 泛型方法

泛型方法在方法的签名内也可以声明类型参数

```c#
static void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}
int x = 5;
int y = 10;
Swap(ref x, ref y);
// Swap<int>(ref x, ref y);
```

- 在泛型类型里面的方法，除非也引入了类型参数（type parameters），否则是不会归为泛型方法的。
- 只有类型和方法可以引入类型参数，属性、索引器、事件、字段、构造函数、操作符等都不可以声明类型参数。但是他们可以使用他们所在的泛型类型的类型参数。
    - `public T this[int index] => data[index];`
    - `public Stack<T>() { }  // Illegal`
