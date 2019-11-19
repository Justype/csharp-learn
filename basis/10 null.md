# Null

- C# 提供了两种操作符，它们可以更容易的处理null
- Null 合并操作符
- Null 条件操作符

## Null 合并操作符

- `??`
- 如果操作数不是null，那么把它给我；否则的话，给我一个默认值
- 如果左边的表达式非null，那么??右边的表达式就不会被计算。

```c#
string s1 = null;
string s2 = "something";
Console.WriteLine(s1 ?? "nothing");   // nothing
Console.WriteLine(s2 ?? "nothing");   // something
```



## Null 条件操作符（Elvis）

- C# 6

- 允许你像 . 操作符那样调用方法或访问成员，除非当左边的操作数是null的时候，那么整个表达式就是null，而不会抛出NullReferenceException

- 一旦遇到null，这个操作符就把剩余表达式给短路掉了

```c#
x?.y?.z
// 就是说，如果x为null，整个的值为null，直接短路
x == null ? null : (x.y == null ? null : x.y.z) 
```

- 最终的表达式必须可以接受null
- 可以和Null合并操作符一起使用