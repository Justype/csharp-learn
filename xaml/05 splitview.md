# SplitView

有动画，实现抽屉菜单

- Pane **隐藏**在展示部分旁边的部分
- Content 被覆盖或被推开用于展示的部分

```c#
// 取反，减小代码量
MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
```

## 模式 DisplayMode

| DisplayMode    | 变化                         |
| -------------- | -----------------------------|
| Inline         | Pane 把 Content 往外顶        |
| Overlay        | Pane 把 Content 完全盖住      |
| CompactInline  | 能使用 CompactPaneLength 属性 |
| CompactOverlay | 能使用 CompactPaneLength 属性 |

- CompactXxx 要设置 `CompactPaneLength` 一开始显示的长度
- `OpenPaneLength` 展开后的长度



### 示例 UWP-18

MainPage.xaml

```xaml
<StackPanel>
    <SplitView x:Name="MySplitView" CompactPaneLength="20" OpenPaneLength="50" DisplayMode="CompactOverlay">
        <SplitView.Pane>
            <!-- 是Content，只能有一个 -->
            <StackPanel>
                <TextBlock Text="Pane 1"/>
                <TextBlock Text="Pane 2"/>
                <TextBlock Text="Pane 3"/>
            </StackPanel>
        </SplitView.Pane>
        
        <SplitView.Content>
            <StackPanel>
                <TextBlock Text="Content 1"/>
                <TextBlock Text="Content 2"/>
                <TextBlock Text="Content 3"/>
            </StackPanel>
        </SplitView.Content>
    </SplitView>
    <Button x:Name="Switch" Content="Pane = Content" Click="Switch_Click"/>
</StackPanel>
```

MainPage.xaml.cs

```c#
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWP_18
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Switch_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }
    }
}

```

