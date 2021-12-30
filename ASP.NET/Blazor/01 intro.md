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

