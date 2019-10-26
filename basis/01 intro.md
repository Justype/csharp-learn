# C# 简介

## C# 简单介绍

- 特点
  - 通用性语言
  - 类型安全
  - 面向对象：封装、继承、多态
- 目标：生产力
  - 简洁性
  - 表达力
  - 高性能
- 创作者：Anders Hejlsberg
  - Turbo Pascal的创作者
  - Delphi的主设计师
  - Typescript的创作者
- C#是平台中立的，与平台无关

## C# 面对对象特性

- 类和接口
  - `Class` 类
  - `Interface` 接口，可用于多继承
- 属性(`Property`)、方法(`Method`)、事件(`Event`)
  - 唯一一种函数成员（`Function Member`）：方法（`Method`）
  - 方法还包括：属性（`Property`）和事件（`Event`）,还有其它的
  - 属性（`Property`）
  - 事件（`Event`）
- C# 主要是一种面向对象的语言，但是也借用了不少函数式编程的特性
  - 函数可以当作值来对待
    - 委托 Delegate
  - 支持纯（purity）模式
    - 避免使用值可变的变量

### 类型安全

C#主要来说是类型安全的

- 静态类型 Static Typing

- 动态类型 dynamic （总体来说是静态类型的）

- 强类型 Strongly Typed language

### 内存管理

- 依赖于运行时来执行自动内存管理
- CLR：Common Language Runtime（公共语言运行时）
  - GC：Garbage Collector（垃圾收集器）
- C#没有消灭指针
  - 通常情况下不需要使用指针
  - `unsafe` 标记

### 平台支持

- 原来C#主要是在Windows上面运行
- 现在可以在所有的平台上运行 (Windows, Mac, Linux, iOS, Android …)
- .NET Core

### CLR

- .NET/.NET Core的核心就是CLR：Common Language Runtime
- CLR和语言无关
- C#是一种托管语言
  - 会被编译成托管代码（IL：Intermediate Language）
  - CRL把IL转化为机器（x64，x86）的原生代码
  - JIT（Just-In-Time）编译    (刚刚在执行之前)
  - Ahead-of-Time编译       (可以提升启动速度)
- 托管代码的容器：Assembly 或 Portable Executable
  - .exe 或 dll
  - 包含 IL 和 类型信息（metadata）
- Ildasm (微软上的工具，可查看IL)

### 支持C# 的框架

- .NET Framework
- .NET Core
- Unity
- Xamarin
- UWP
- WinRT
- Windows Phone
- XNA
- Silverlight
- .NET Micro Framework
- Mono
- Sql Server

## 安装开发环境

- .NET Core SDK
- 一个IDE (Visual Studio)



 C# 源代码通过 .NET Core (CLR + FCL) 翻译成本地计算机可以理解的语言

- CLR(Common Language Runtime)
- FCL(Framework Class Library, 包含了一些基本库，减少底层代码的编写)



### 简单的新建项目

在 intro 下创建新的命令行项目

```
$ dotnet new console -n intro
```

可以通过`dotnet --help` 查看帮助

最简单的C#程序

```c#
using System;

namespace intro // 命名空间为 intro
{
    class Program   // 类名为 Program
    {
        static void Main(string[] args)   // 程序的入口
        {
            Console.WriteLine("Hello World!");  // 输出 Hello World!
        }
    }
}
```

#### 编译运行

```
$ dotnet run
```

### 了解类

```c#
using System; // 引用了 System 命名空间
// 命名空间可以嵌套

namespace intro // 命名空间为 intro， 可以有多个类，减少类名重名
{
    class Program   // 类名为 Program
    {
        static void Main(string[] args)   // 程序的入口 没有返回值
        {
            Console.WriteLine(FeetToInches(30));
            Console.WriteLine(FeetToInches(100));
        }

        static int FeetToInches(int feet)  // 自定义的静态方法，返回 int
        {
            int inches = feet * 12;
            return inches;
        }
    }
}
```

### 编译

```
$ dotnet run     生成并运行 .NET 项目输出。
```

相当于

```
$ dotnet restore 还原 .NET 项目中指定的依赖项。
$ dotnet build   生成 .NET 项目。
$ dotnet run     运行 .NET 项目。
```



- C#编译器把.cs结尾的源码文件编译成Assembly。
- Assembly是.NET Core里的包装和部署的单元。
- Assembly可以是应用程序 .exe，也可以是库.dll。

在 `./bin/Debug/netcoreappX.X/` 下有 `项目文件名.dll`，运行

```
dotnet 项目文件名.dll
```

