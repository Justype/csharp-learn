# Bool

## 特点

- System.Boolean 的别名
- true、false（Literal）
- 只需要1 bit存储空间，但运行时会使用1 byte 内存（运行时和处理器可以高效操作的最小的块）
- 针对数组，Framework提供了BitArray类（System.Collections），在这里每个bool值只占用 1 bit 内存。



## 操作符

### == 相等 != 不等

- == 和 != 操作符在用来比较相等性的时候，通常都会返回bool类型
- 对于值类型（原始类型）来说，== 和 != 就是比较它们的值
- 对于引用类型，默认情况下，是比较它们的引用

```c#
Person p1 = new Person("Ziyue", 20);
Person p2 = new Person("Justype", 0);
Person p3 = new Person("Ziyue", 20);
var p4 = p1;

Console.WriteLine(p1 == p2);    // False
Console.WriteLine(p1 == p3);    // False
Console.WriteLine(p1 == p4);    // True

Console.WriteLine(p1.name == p2.name);  // False
Console.WriteLine(p1.name == p3.name);  // True
```

### 数值类型

- 数值类型都可以使用这些操作符：==，!=，<，>，<=，>=。（实数还是需要注意一下）
- 枚举类型（enum）也可以使用这些操作符，就是比较它们底层对应的整型数值

### && 和 || 或

- && 和 || 可以用来判断 “与” 和 “或” 的条件
- && 和 || 有短路机制
  - &&  前一个为 false 后面一个不判断
  - ||  前一个为 true 后面一个不判断
- && 和 || 可避免NullReferenceException

### & |

- & 和 | 也可以用来判断 “与” 和 “或” 的条件
- & 和 | 很少用来条件判断上
- & 和 | 没有短路机制
- 当使用于数值的时候，& 和 | 执行的是按位操作

### 三元操作符

- q ? a : b
- 有三个操作数
- 如果 q 为 true，那么就计算 a，否则计算 b。

```c#
int x = 3;
int y = 2;

int z = x > y ? x : y;
Console.WriteLine(z);   // 3
```



