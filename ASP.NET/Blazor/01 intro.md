# Blazor

Blazor 就是一个写Web应用的引擎，可以使用C#代替JS。

我主要看中了Blazor Desktop才来学习的，主要是MAUI不知道好不好，但Web跨平台是一定可行的。

## Razor

类似HTML，有`.cshtml`和`.razor`两种类型，使用`HTML`和`C#`编写页面

## Pages & Components

Page 就是展示整个页面，里面可以包含多个组件

Component 组件，可以反复利用的，一般用组件

## Blazor Server or WebAssembly

Server：使用SignalR与服务器进行连接，更新页面在服务器上完成，文件下载量小，加载快

WebAssembly：与Angular和Vue相似，下载好代码后本地更新，下载文件很大（有C#垃圾回收器）

[详细对比](https://docs.microsoft.com/learn/modules/blazor-introduction/3-when-to-use-blazor)

# First Blazor App

## Create

构建一个新的Blazor Server 项目，并输出到 BlazorApp文件夹

```
dotnet new blazorserver -o BlazorApp --no-https
```

### 查看文件目录

- `Program.cs` 程序的入口，设置应用服务和中间件
- `App.razor` 应用的根组件(root component)
- `Pages` 存放页面
- `BlazorApp.csproj` 定义项目，声明依赖项
- `launchSettings.json` 为本地环境定义了了多个配置(？profile settings)，端口号在创建项目的时候自动生成(5000-5300).

## Run

```
dotnet watch
```

可以按`CTRL`+`C`停止

`Pages/Index.razor`: 默认显示的页面，包含了一个`Survey Prompt`组件

```razor
@page "/"

<PageTitle>Index</PageTitle>

<h1>Hello, world!</h1>

This is my first Blazor App.

<SurveyPrompt Title="How is Blazor working for you?" />
```

## Counter

`Pages/Counter.razor`

```razor
@page "/counter"

<PageTitle>Counter</PageTitle>

<h1>Counter</h1>

<p role="status">Current count: @currentCount</p>

<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>
@*
每次点击时触发 onclick 事件 
调用 IncrementCount 方法
currentCount 增加
更新计数
*@

@code {
    private int currentCount = 10;

    private void IncrementCount()
    {
        currentCount += 2;
    }
}
```

## 添加组件

在`Page/Index.razor`内添加`Counter`组件

```
@page "/"

<PageTitle>Index</PageTitle>

<h1>Hello, world!</h1>

This is my first Blazor App.

<SurveyPrompt Title="How is Blazor working for you?" />

<hr>

<p>新增的Counter 组件：</p>

<Counter/>
```

## 增加 参数 Parameter

```razor
@page "/counter" 

<PageTitle>Counter</PageTitle>

<h1>Counter</h1>

<p role="status">Current count: @currentCount</p>

<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>

@code {
    private int currentCount = 10;

    [Parameter] // 将其设置为属性后，就可以在使用该组件时传入
    public int IncrementAmount { get; set; } = 1;

    private void IncrementCount()
    {
        currentCount += IncrementAmount;
    }
}
```

```razor
@page "/"

<PageTitle>Index</PageTitle>

<h1>Hello, world!</h1>

This is my first Blazor App.

<SurveyPrompt Title="How is Blazor working for you?" />

<hr>

<p>新增的Counter 组件：</p>

<Counter IncrementAmount="10"/> <!-- 这里传入了参数 -->
```

## DataBinding & Event

### 代码隐藏 code-behind

在 Blazor 中，可以将 C# 文件直接添加到应用项目，就像其他 .NET 项目一样。 此方法通常称为“代码隐藏”，它使用单独的代码文件来存储应用逻辑。 当业务逻辑较复杂、较长或有多个类时，单独的文件是一个非常好的策略。

对于简单的逻辑，并不总是需要新建 `.cs` 文件。

### 组件中的 C# 内联 (C# inline in components)

常见做法是在一个 Razor 组件文件中混用 HTML 和 C#。 对于具有较轻代码要求的简单组件，此方法非常有效。 可使用指令将代码添加到 Razor 文件。

### Razor 指令 (directives)

Razor 指令是用于使用 HTML 添加 C# 内联的组件标记。 使用指令，开发人员可以定义单个语句、方法或更大的代码块。

### 代码指令

可使用 @expression() 来添加一个与 HTML 内联的 C# 语句。 如果需要更多代码，请使用 @code 指令添加多个语句（用括号括起来）。

还可将 @functions 部分添加到方法和属性的模板。 它们将添加到生成的类顶部，文档可在其中引用它们。

### Page 指令

@Page 指令是用于将组件标识为页面的特殊标记。 可使用此指令指定路由。 该路由映射到 Blazor 引擎识别的属性路由，以注册和访问页面。

### Razor 数据绑定

在 Razor 组件中，可以将 HTML 元素数据绑定到 C# 字段、属性和 Razor 表达式值。 数据绑定支持在 HTML 和 Microsoft .NET 之间进行双向同步。

呈现组件时，数据从 HTML 推送到 .NET。 组件在事件处理程序代码执行后呈现自身。 因此在触发事件处理程序后，属性更新会立即反映在 UI 中。

使用 @bind 标记将 C# 变量绑定到 HTML 对象。 按名称将 C# 变量定义为 HTML 中的字符串。

