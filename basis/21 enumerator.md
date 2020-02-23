# enumerator 枚举器

- 枚举器是一个只读的，作用于一序列值的、只能向前的游标。
- 枚举器是一个实现了下列任意一个接口的对象：
    - `System.Collections.IEnumerator`
    - `System.Collections.Generic.IEnumerator<T> `
- 技术上来说，任何一个含有名为MoveNext方法和名为Current的属性的对象，都会被当作枚举器来对待。
- foreach语句会迭代可枚举的对象（enumerable object）。可枚举的对象是一序列值的逻辑表示。它本身不是游标，它是一个可以基于本身产生游标的对象。

## enumerable object 可枚举对象

- 一个可枚举对象可以是(下列任意一个)：
    - 实现了`IEnumerable`或者`IEnumerable<T>`的对象。
    - 有一个名为`GetEnumerator`的方法，并且该方法返回一个枚举器(enumerator)
- `IEnumerator` 和 `IEnumerable` 是定义在 `System.Collections` 命名空间下的。
- `IEnumerator<T>` 和 `IEnumerable<T>` 是定义在 `System.Collections.Generic` 命名空间下的。

## enumeration pattern 枚举模式

```c#
class Enumerator    // Typically implements IEnumerator or IEnumerator<T>
{
    public IteratorVariableType Current { get{ } }
    public bool MoveNext() { }
}

class Enumerable    // Typically implements IEnumerator or IEnumerator<T>
{
    public Enumerator GetEnumerator() { }
}
```

### 例子

```c#
foreach (char c in "beer:)
    Console.WriteLine(c);

static void Main(string[] args)
{
    using(var enumerator = "beer".GetEnumerator())
        while(enumerator.MoveNext())
        {
            var element = enumerator.Current;
            System.Console.WriteLine(element);
        }
}
```

注意：如果枚举器（enumerator）实现了IDisposable接口，那么foreach语句就会像using语句那样，隐式的dispose掉这个enumerator对象。

## 集合初始化器

你可以只用一步就把可枚举对象进行实例化并且填充里面的元素：

```c#
using System.Collections.Generic;

List<int> list = new List<int>{1, 2, 3};
```

但是编译器会把它翻译成：

```c#
using System.Collections.Generic;

List<int> list = new List<int>();
List.Add(1);
List.Add(2);
List.Add(3);
```

上例中，要求可枚举对象实现了System.Collections.IEnumerable接口，并且他还有一个可接受适当参数的Add方法。

### 例子

```c#
var dict = new Dictionary<int, string>()
{
    { 5, "five" },
    { 10, "ten"}
};

var dict = new Dictionary<int, string>()
{
    [3] = "three";
    [10] = "ten";
};
```
