# try-catch

## Try 语句

- try语句指定了用来进行错误处理或清理的一个代码块。
- try语句块后边必须紧接着一个catch块或者是一个finally块，或者两者都有。
- 当try块里发生错误的时候，catch块就会被执行。
- finally块会在执行完try块之后执行，如果catch也执行了，那就在catch块后边执行。finally块用来执行一些清理代码，无论是否有错误发生。

### catch

- catch块可以访问一个Exception对象，这个Exception对象里含有关于错误的信息。
- catch块通常被用来对错误进行处理/补偿或者重新抛出异常。

### finally

finally块为你的程序增加了确定性：CLR总是尽力去执行它。它通常用来做一些清理任务。

### 样式

```c#
try
{
    // exception may get thrown within execution of this block
}
catch(ExceptionA ex)
{
    // handle exception of type ExceptionA
}
catch(ExceptionB ex)
{
    // handle exception of type ExceptionB
}
finally
{
    // cleanup code
}
```

## 当异常被抛出的时候

CLR会执行一个测试，当前是否执行在能够catch异常的try语句里？
- 如果是：当前执行就会传递给兼容的catch块里面，如果catch块完成了执行，那么执行会移动到try语句后边的语句。如果有finally块存在，会先执行finally块。
- 如果不是：执行会返回到函数的调用者，并重复这个测试过程（在执行完任何包裹这语句的finally块之后）。

### catch子句 (clause)

- `catch`子句指定要捕获的异常的类型。这个异常必须是`System.Exception`或其子类。
- 捕获`System.Exception`这个异常的话就会捕获所有可能的错误。当处理下面几种情况时，这么做是很有用的：
    - 无论是哪种类型的异常，你的程序都可能从错误中恢复。
    - 你计划重新抛出异常（可能在你记录了log之后）。
    - 你的错误处理器是程序终止运行前的最后一招。
- 更典型的情况是，你会catch特定类型的异常。为的是避免处理那些你的处理程序并未针对设计的情况。
- 你可以使用多个catch子句来处理多个异常类型。

```c#
class Test
{
    static void Main(string[] args)
    {
        try
        {
            byte b = byte.Parse(args[0]);
            Console.WriteLine(b);
        }
        catch(IndexOutOfRangeException ex)
        {
            Console.WriteLine("Please provide at least one argument");
        }
        catch(FormatException ex)
        {
            Console.WriteLine("That's not a number");
        }
        catch(OverflowException ex)
        {
            Console.WriteLine("You've given me more than a byte!");
        }
    }
}
```

#### catch子句

- 上例中，针对给定的异常，只有一个catch子句会执行。
- 如果你希望有一个兜底的catch可以捕获任何类型的异常，那么你需要把特定类型的异常捕获放在靠前的位置。

- 如果你不需要访问异常的属性，那么你可以不指定异常变量:
    - `catch(OverflowException) { }`
- 更甚者，你可以把异常类型和变量都拿掉，这也意味着它会捕获所有的异常：
    - `catch { }`

## 异常的过滤

从C#6开始，你可以在catch子句中添加一个when子句来指定一个异常过滤器：

```c#
catch(WebException ex) when(ex.Status == WebExceptionStatus.Timeout) { }
```

此例中，如果WebException被抛出的话，那么when后边的bool表达式就会被执行估算。如果计算的结果是false，那么后边所有的catch子句都会在考虑范围内。

```c#
catch(WebException ex) when(ex.Status == WebExceptionStatus.Timeout) { }
catch(WebException ex) when(ex.Status == WebExceptionStatus.SendFailure) { }
```

## finally块

- finally块永远都会被执行，无论是否抛出异常，无论try块是否跑完，finally块通常用来写**清理代码**。
- finally块会在以下情况被执行：
    - 在一个catch块执行完之后，
    - 因为跳转语句（例如return或goto），程序的执行离开了try块。
    - try块执行完毕后。
- 唯一可以不让finally块执行的东西就是无限循环，或者程序突然结束。

```c#
static void ReadFile()
{
    StreamReader reader = null; // In System.IO namespace
    try
    {
        reader = File.OpenText("file.txt");
        if(reader.EndOfStream) return;
        Console.WriteLine(reader.ReadToEnd());
    }
    finally
    {
        if(reader != null)
            reader.Dispose();
    }
}
```

## using语句

- 很多类都封装了非托管的资源，例如文件处理、图像处理、数据库连接等。这些类都实现了IDisposable接口，这个接口定义了一个无参的Dispose方法用来清理这些资源。
- using语句提供了一个优雅的语法来在finally块里调用实现了IDisposable接口对象上的Dispose方法。

```c#
using(StreamReader reader = File.OpenText("file.txt")) { }

// 相当于

{
    StreamReader reader = File.OpenText("file.txt");
    try { }
    finally
    {
        if(reader != null)
            ((IDisposable)reader).Dispose();
    }
}
```

# throw 抛出异常

异常可以被运行时或者用户抛出。

```c#
class Program
{
    static void Display(string name)
    {
        if (name == null)
            throw new ArgumentNullException(nameof(name));
        Console.WriteLine(name);
    }
    static void Main(string[] args)
    {
        try { Display(null); }
        catch (ArgumentNullException ex)
        {
            System.Console.WriteLine("Caught the exception");
        }
    }
}
```

## C#7 抛异常

- C# 7 之前，throw 肯定是个语句。而现在它可以作为expression-bodied functions 里的一个表达式：
    - `public string Foo() => throw new NotImplementedException();`
- 也可以出现在三元条件表达式里：

```c#
string ProperCase(string value) =>
    value == null ? throw new ArgumentException("value") :
    value == "" ? "" :
    char.ToUpper(value[0]) + value.Substring(1);
```

## 重新抛出异常

```c#
try { }
catch {Exception ex}
{
    // Log error

    throw;  // Rethrow same exception
}
```

如果使用throw ex代替throw的话，程序仍可运行，但是新传递的异常的Stacktrace属性就不会反应原始错误了。

像上述这样重抛异常可以让你记录异常并且不会把它吞没，如果情况超出了你的预期，它允许你放弃在这里处理这个异常：

```c#
string s = null;
using (WebClient wc = new WebClient())
    try { s = wc.DownloadString("https://justype.github.io/"); }
    catch(WebException ex)
    {
        if(ex.Status == WebExceptionStatus.Timeout)
            System.Console.WriteLine("Timeout");
            else
            throw;  // can't handle other sorts of WebException, so rethrow
}
```

使用C#6+，你可以这样简洁的重写上例：

```c#
catch(WebException ex) when (ex.Status == WebExceptionStatus.Timeout)
{
    System.Console.WriteLine("Timeout");
}
```

其它常见的情景是抛出一个更具体的异常类型：

```c#
try
{
    // Parse a DateTime from XML element data
}
catch(FormatException ex)
{
    throw new XmlException("Invalid DateTime", ex);
}
```

- 注意：在组建XmlException的时候，我把原异常ex，作为第二个参数传了进去，这样有助于调试。
- 有时候你会抛出一个更抽象的异常，通常是因为要穿越信任边界，防止信息泄露。

## System.Exception的关键属性

- `StackTrace`：它是一个字符串，展现了从异常发生地到catch块所有的被调用的方法。
- `Message`：关于错误的描述信息
- `InnerException`：引起外层异常的内层异常（如果存在的话）。而且InnerException本身还有可能含有InnerException

## 常见异常类型

```c#
System.ArgumentException 
System.ArgumentNullException 
System.ArgumentOutOfRangeException 
System.InvalidOperationException 
System.NotSupportedException 
System.NotImplementedException 
System.ObjectDisposedException 
NullReferenceException 
// 你也可以直接throw null，来抛出此类型的异常。
```

## TryXXX模式

```c#
public int  Parse   (string input);
public bool TryParse(string input, out int returnValue);

// 如果解析失败，Parse方法会抛出异常，而TryParse方法会返回false。
public return-type XXX(input-type input)
{
    return-type returnValue;
    if(!TryXXX(input, out returnValue))
        throw new YYYException(...);
    return returnValue;
}
```
