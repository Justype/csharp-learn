# 可空值类型 和 字符串

## 可空值类型

- 可空值类型是`System.Nullable<T>` 这个struct的实例
- 可空值类型除了可以正确的表示其底层数据类型的范围，还可以表示null

### 例子

- `Nullable<bool>` 可以存放三种值：`true`, `false`, `null`

### 作用

可以接受空类型的返回，提高了程序的灵活性？

### 简写

- `int?`
- `char?`
- `DateTime?`

## Null 和 空， 空白string

```c#
string name1 = "Nick";    // 普通字符串
string name2 = null;      // null
string name3 = "";        // 空 string
string name4 = "      ";  // 空白 string
```

### 判断

```c#
// null
if (name == null) { }

// 空字符串
if (string.IsNullOrEmpty(name)) { }

// 空白字符串
if (string.IsNullOrWhiteSpace(name)) { }
```



## Nullable<> 常用属性和方法

- `.HasValue`
- `.Value`：如果为null，直接调用会报错
- `.GetValueOrDefault()`
- `.GetValueOrDefault(指定的默认值)`

### 转换

- 到Nullable 隐式转换
- 转换为别的 显式转换

### 操作符

- `?:`
- `??`
- `?.`, `?[]`