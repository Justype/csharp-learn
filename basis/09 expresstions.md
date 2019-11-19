# 表达式和操作符

- 表达式其本质就是表示了一个值
- 最简单的表达式就是常量和变量
- 表达式可以通过操作符来进行转换和合并
- 一个操作符需要一个或多个输入操作数来产出新的表达式

## 表达式

示例：

- `12`
- `12 * 13`
- `1 + (12 * 30)`

### 类型

- 包含元素的个数：一元、二元、三元
- 二元操作符采用中缀表示法：`12*13`

### Primary Expressions

Primary Expression 就是表达式，但是它里面包括了使用操作符组成的表达式

例如：`Math.Log(1)`

- 由两个表达式组成
- 第一个表达式使用 `.` 操作符来执行成员的查找
- 第二个表达式使用 `()`操作符来实现了方法的调用

### void 表达式

- 就是没有值的表达式（调用void方法）
- 无法用于构建更复杂的表达式

### 赋值表达式

- 使用 `=` 把另一个表达式结果赋给一个变量
- 赋值表达式不是void 表达式，它有值，就是被赋的值，所以可以与其他表达式进行组合
- `=`为右结合，`a = b = c = d = 0`
- 复合赋值操作符就是使用另一个操作符来组合赋值的句法快捷方式
  - `x *= 2` 相当于`x = x * 2`
  - `X <<= 1` 相当于 `X = X << 1`

### 操作符的优先级和结合性

- `1 + 2 * 3`  相当于 `1 + (2 * 3) `
- `*`的优先级比 `+` 高

### 左结合的操作符

- 二元操作符（除了赋值，lambda，null合并操作符）是左结合的
- 也就是从左到右的进行估算
  - `8 / 4 / 2`  相当于 `( 8 / 4 ) / 2` == 1
  - 而 `8 / ( 4 / 2 )` == 4

### 右结合的操作符

- 赋值、lambda、null合并和条件操作符是右结合的
- 从右向左估算
- x = y = 3

## 各种操作符

| Category            | Operator Symbol | Operator Name               | Example                                  | User-overloadable |
| ------------------- | --------------- | --------------------------- | ---------------------------------------- | ----------------- |
| Primary             | `.`             | Member access               | `x.y`                                    | No                |
|                     | `->` (unsafe)   | Pointer to struct           | `x->y`                                   | No                |
|                     | `()`            | Function call               | `x()`                                    | No                |
|                     | `[]`            | Array / Index               | `a[x]`                                   | Via indexer       |
|                     | `++`            | Post-increment              | `x++`                                    | Yes               |
|                     | `--`            | Post-decrement              | `x--`                                    | Yes               |
|                     | `new`           | Create instance             | `new Foo()`                              | No                |
|                     | `stackalloc`    | Unsafe stack allocation     | `stackalloc(10)`                         | No                |
|                     | `typeof`        | Get type from identifier    | `typeof(int)`                            | No                |
|                     | `nameof`        | Get name from identifier    | `nameof(x)`                              | No                |
|                     | `checked`       | Integral overflow check on  | `checked(x)`                             | No                |
|                     | `unchecked`     | Integral overflow check off | `unchecked(x)`                           | No                |
|                     | `default`       | Default value               | `default(char)`                          | No                |
| Unary               | `await`         | Await                       | `await myTask`                           | No                |
|                     | `sizeof`        | Get the size of struct      | `sizeof(int)`                            | No                |
|                     | `?.`            | Null-conditional            | `x?.y`                                   | No                |
|                     | `+`             | Positive value of           | `+x`                                     | Yes               |
|                     | `-`             | Negative value of           | `-x`                                     | Yes               |
|                     | `!`             | Not                         | `!x`                                     | Yes               |
|                     | `~`             | Bitwise complement          | `~x`                                     | Yes               |
|                     | `++`            | Pre-increment               | `++x`                                    | Yes               |
|                     | `--`            | Pre-decrement               | `--x`                                    | Yes               |
|                     | `()`            | Cast                        | `(int)x`                                 | No                |
|                     | `*`(unsafe)     | Value at address            | `*x`                                     | No                |
|                     | `&`(unsafe)     | Address of value            | `&x`                                     | No                |
| Multiplicative      | `*`             | Multiply                    | `x * y`                                  | Yes               |
|                     | `/`             | Divide                      | `x / y`                                  | Yes               |
|                     | `%`             | Remainder                   | `x % y`                                  | Yes               |
| Additive            | `+`             | Add                         | `x + y`                                  | Yes               |
|                     | `-`             | Subtract                    | `x - y`                                  | Yes               |
| Shift               | `<<`            | Shift Left                  | `x << 1`                                 | Yes               |
|                     | `>>`            | Shift Right                 | `x >> 1`                                 | Yes               |
| Relational          | `<`             | Less than                   | `x < y`                                  | Yes               |
|                     | `>`             | More than                   | `x > y`                                  | Yes               |
|                     | `<=`            | Less than or equal to       | `x <= y`                                 | Yes               |
|                     | `>=`            | More than or equal to       | `x >= y`                                 | Yes               |
|                     | `is`            | Type is or subclass of      | `x is y`                                 | No                |
|                     | `as`            | Type conversion             | `x as y`                                 | No                |
| Equality            | `==`            | Equals                      | `x == y`                                 | Yes               |
|                     | `!=`            | Not Equals                  | `x != y`                                 | No                |
| Logical And         | `&`             | And                         | `x & y`                                  | Yes               |
| Logical Xor         | `^`             | Exclusive Or                | `x ^ y`                                  | Yes               |
| Logical Or          | `|`             | Or                          | `x | y`                                  | Yes               |
| Conditional And     | `&&`            | Conditional And             | `x && y`                                 | Via `&`           |
| Conditional Or      | `||`            | Conditional Or              | `x || y`                                 | Via `|`           |
| Null coalescing     | `??`            | Null coalescing             | `x ?? y`                                 | No                |
| Conditional         | `?:`            | Conditional                 | `isTrue ? thenThisValue : elsethisValue` | No                |
| Assignment & Lambda | `=`             | Assign                      | `x = y`                                  | No                |
|                     | `*=`            | Multiply self by            | `x *= 2`                                 | Via `*`           |
|                     | `/=`            | Divide self by              | `x /= 2`                                 | Via `/`           |
|                     | `+=`            | Add self by                 | `x += 2`                                 | Via `+`           |
|                     | `-=`            | Substract self by           | `x -= 2`                                 | Via `-`           |
|                     | `<<=`           | Shift self left by          | `x <<= 2`                                | Via `<<`          |
|                     | `>>=`           | Shift self right by         | `x >>= 2`                                | Via `>>`          |
|                     | `&=`            | And self by                 | `x &= 2`                                 | Via `&`           |
|                     | `^=`            | Exclusive-or self by        | `x ^= 2`                                 | Via `^`           |
|                     | `|=`            | Or self by                  | `x = 2`                                  | Via `!`           |
|                     | `=>`            | Lambda                      | `x => x + 2`                             | No                |

