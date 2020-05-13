# 创建ASP.NET Web 项目

1. 打开 Visual Studio 2019，创建新的ASP.Net Core Web项目
2. 空模板，

# 观察文件

## 项目文件（.csproj）

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp3.1</TargetFramework>
  </PropertyGroup>

</Project>
```

## Program.cs

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            // 本质上是控制台应用
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                // 进行默认配置
                {
                    webBuilder.UseStartup<Startup>();
                    // 使用Startup类
                    // 配置较动态
                });
    }
}
```

## Startup.cs

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        // 先调用，负责依赖输入
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}

```

# 相关知识

依赖注入 Dependency Injection

IoC容器 （Inversion of Control）
- 注册：启动的时候，就有一些类型到容器里注册。这些类型就是服务 services
- 请求实例：运行时，就可以请求这些服务的实例
- 实例的生命周期：可设置，由IoC容器控制
    - Transient：每次请求生成一次实例
    - Scoped：一次Web请求产生一次，Web请求处理完，周期终止
    - Singleton：就创建一次

## 例子

### 注册

#### 已经存在的服务

MVC服务，在Startup.cs内添加

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();
    // 使用传统 MVC
}
```

#### 自定义的服务

1. 定义一个接口
2. 实现接口
3. 注册相关服务，在Startup.cs内添加

```c#
public interface IClock { }
public class ChinaClock : IClock { }
public class UtcClock : IClock { }

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IClock, ChinaClock>();
        // 只要请求了 实现IClock对象，就返回ChinaClock

        // 如果想要使用UtcClock，不需要再修改 Controller的代码，直接
        // services.AddSingleton<IClock, UtcClock>();
    }
}
```

## DI 依赖注入的优点

- 解耦，没有强依赖。
    - 利于单元测试
- 也不需要了解具体的服务类
- 也不需要管理服务类的生命周期