# Navigation

在不同的页面中切换

- UWP里，页面在Frame 对象里
- 当应用启动时，会有 Application 对象
- 在里面是Frame 用于存放页面(Pages)
- App - Window - rootFrame - mainpage

```c#
// App.xaml.cs

/// <summary>
/// 在应用程序由最终用户正常启动时进行调用。
/// 将在启动应用程序以打开特定文件等情况下使用。
/// </summary>
/// <param name="e">有关启动请求和过程的详细信息。</param>
protected override void OnLaunched(LaunchActivatedEventArgs e)
{
    Frame rootFrame = Window.Current.Content as Frame;

    // 不要在窗口已包含内容时重复应用程序初始化，
    // 只需确保窗口处于活动状态
    if (rootFrame == null)
    {
        // 创建要充当导航上下文的框架，并导航到第一页
        rootFrame = new Frame();

        rootFrame.NavigationFailed += OnNavigationFailed;

        if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
        {
            //TODO: 从之前挂起的应用程序加载状态
        }

        // 将框架放在当前窗口中
        Window.Current.Content = rootFrame;
    }

    if (e.PrelaunchActivated == false)
    {
        if (rootFrame.Content == null)
        {
            // 当导航堆栈尚未还原时，导航到第一页，
            // 并通过将所需信息作为导航参数传入来配置
            // 参数
            rootFrame.Navigate(typeof(MainPage), e.Arguments);
        }
        // 确保当前窗口处于活动状态
        Window.Current.Activate();
    }
}
```

## Frame.Navigate()

```c#
bool Frame.Navigate(Type sourcePageType);
bool Frame.Navigate(Type sourcePageType, object parameter);
bool Frame.Navigate(Type sourcePageType, object parameter, Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo infoOverride);
```

## 获得参数

```c#
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    var value = (传的类型)e.Parameter;
}
```

## 注意

重新导航到页面，也就是使用 Frame.Navigate() 会保留原参数

例如：
1. 页面1 => 页面2 没有传参数
2. 页面2 写了东西
3. 页面3 返回 页面2，由于页面1 => 页面2 没有传参数，页面2 写的东西没有了

保存导航状态: http://stackoverflow.com/questions/35944277/uwp-page-state-manage

