# 建立新的项目

创建的是 Blazor WebAssembly 项目，有 `wwwroot` 文件夹

# Blazor WebAssembly 加载顺序

1. `index.html`
2. `blazor.webassembly.js`
3. .NET WebAssembly 运行时 和 依赖
4. 将界面加载到 `div#app`

# 重要的文件

- `index.html`
- `Program.cs`

## index.html

只有WebAssembly 才有 `index.html`

```html
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>BlazingTrails.Client</title>
    <base href="/" />
    <!-- blazor 使用 这个base标签 来处理路由 -->
    <link href="css/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="css/app.css" rel="stylesheet" />
    <link href="BlazingTrails.Client.styles.css" rel="stylesheet" />
</head>

<body>
    <div id="app">Loading...</div>
    <!-- 使用这个标签来展示Blazor应用 -->

    <div id="blazor-error-ui">
        <!-- 当错误出现时，展示这个界面 -->
        An unhandled error has occurred.
        <a href="" class="reload">Reload</a>
        <a class="dismiss">🗙</a>
    </div>
    <script src="_framework/blazor.webassembly.js"></script>
    <!-- 下载运行时、依赖项，初始化程序 -->
</body>

</html>
```

这个就是出现异常后，blazor-error-ui展示的画面

![](assets/02.01%20error.png)

## Program.cs

```cs
using BlazingTrails.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args); // 构建一个WebAssemblyHostBuilder实例
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after"); // 为程序设置根组件

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }); // 注册服务

await builder.Build().RunAsync(); // 利用已有设置，创建WebAssemblyHost实例
```

C#9 新特性：**top-level statements** Main方法不在需要包括在 命名空间和类内。详细内容看[官方文档](https://docs.microsoft.com/dotnet/csharp/whats-new/tutorials/top-level-statements)

### 关键部分

1. 根组件 root components
2. 添加到 `IServiceCollection` 的服务 services

默认情况下注册两个组件：`App`和`HeadOutlet`。

- `App` 作为程序入口
- `HeadOutlet` 是.NET 6 新加入的，可以修改`<head>`标签内的元素，更新`<meta>`标签



