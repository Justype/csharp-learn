# 简单的汉堡菜单

1. 点击弹出，使用的是`SplitView`
2. 按住选中，使用的是`ListBox`
3. 页面使用`Grid`布局

## 增强版网址



## 使用Grid整体布局

两行两列，第一行，用作按钮栏，二行一列为汉堡菜单，二行二列用作页面显示。

```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="auto" />
        <RowDefinition />
    </Grid.RowDefinitions>

    <!-- 菜单展示按钮 -->
    <Button x:Name="GlobalNavigationButton" 
            FontFamily="Segoe MDL2 Assets" Content="&#xE700;" FontSize="36"/>

        <!-- 汉堡菜单 -->
        <SplitView Grid.Row="1">
            <SplitView.Pane>
                <ListBox>
                    <!-- 汉堡菜单的按钮 -->
                    <ListBoxItem></ListBoxItem>
                    <ListBoxItem></ListBoxItem>
                    <ListBoxItem></ListBoxItem>
                </ListBox>
            </SplitView.Pane>
            
            <SplitView.Content>
                <!-- 显示不同页面 -->
                <Frame Grid.Column="1" Grid.Row="1"/>
            </SplitView.Content>
        </SplitView>
</Grid>
```

## 细化

```xml
<Page
    x:Class="UWP_21.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:UWP_21"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">

    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="auto" />
            <RowDefinition />
        </Grid.RowDefinitions>

        <RelativePanel>
            <!-- 菜单展示按钮 -->
            <Button x:Name="GlobalNavigationButton" 
                    FontFamily="Segoe MDL2 Assets" Content="&#xE700;" FontSize="36"
                    Click="GlobalNavigationButton_Click"
                    RelativePanel.AlignLeftWithPanel="True"/>
            <Button x:Name="BackButton"
                    FontFamily="Segoe MDL2 Assets" Content="&#xE72B;" FontSize="36"
                    Click="BackButton_Click"
                    RelativePanel.RightOf="GlobalNavigationButton"
                    Visibility="Collapsed"/>
            <TextBlock x:Name="MainTextBlock" Text="Home"
                       FontSize="36"
                       RelativePanel.RightOf="BackButton"/>

            <TextBox x:Name="SearchTextBox"
                     PlaceholderText="Search" FontSize="27" Width="300"
                     RelativePanel.LeftOf="SearchButton"/>
            <Button x:Name="SearchButton"
                    FontFamily="Segoe MDL2 Assets" Content="&#xE721;" FontSize="36"
                    Click="SearchButton_Click"
                    RelativePanel.AlignRightWithPanel="True"/>
            
            
        </RelativePanel>

        <!-- 汉堡菜单 -->
        <SplitView x:Name="MainSplitView"
                   Grid.Row="1"
                   DisplayMode="CompactOverlay" CompactPaneLength="56" 
                   HorizontalAlignment="left">
            <SplitView.Content>
                <!-- 显示不同页面 -->
                <Frame x:Name="MainFrame"/>
            </SplitView.Content>
            
            
            <SplitView.Pane>
                <!-- 汉堡菜单的按钮 -->
                <ListBox x:Name="LeftMenu" SelectionMode="Single"
                         SelectionChanged="LeftMenu_SelectionChanged">

                    <!-- Home -->
                    <ListBoxItem x:Name="HomeListBoxItem" IsSelected="True">
                        <StackPanel Orientation="Horizontal">
                            <TextBlock FontFamily="Segoe MDL2 Assets" Text="&#xE80F;" FontSize="36"
                                   VerticalAlignment="Center" />
                            <TextBlock Text="Home" FontSize="36" Margin="10,0,0,0"/>
                        </StackPanel>
                    </ListBoxItem>

                    <!-- Quick Note -->
                    <ListBoxItem x:Name="QuickNoteListBoxItem">
                        <StackPanel Orientation="Horizontal">
                            <TextBlock FontFamily="Segoe MDL2 Assets" Text="&#xE70B;" FontSize="36"
                                   VerticalAlignment="Center"/>
                            <TextBlock Text="Quick Note" FontSize="36" Margin="10,0,0,0"/>
                        </StackPanel>
                    </ListBoxItem>
                    <ListBoxItem></ListBoxItem>
                </ListBox>
            </SplitView.Pane>
            
        </SplitView>
    </Grid>
</Page>

```

```c#
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWP_21
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //HomeListBoxItem.IsSelected = true;
            // 一开始，HomeListBoxItem 没有被选择
            //MainFrame.Navigate(typeof(Pages.HomePage));
            Debug.WriteLine($"HomeListBoxItem.IsSelected = {HomeListBoxItem.IsSelected}");
            /* Debug 输出的竟然是 true
             * 渲染的问题？！
             * 但如果直接在 XAML 里面写 IsSelected = true 又会报错。
             * 对啊！如果把Content 写在 Pane 前面呢？
             * 
             * 结果就是真的可以！
             * 运行一点问题也没有，不需要Navigate，直接就有HomePage
             */
        }

        private void GlobalNavigationButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void LeftMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HomeListBoxItem.IsSelected)
            {
                MainFrame.Navigate(typeof(Pages.HomePage));
                MainTextBlock.Text = "Home";
                BackButton.Visibility = Visibility.Collapsed;
            }
            if (QuickNoteListBoxItem.IsSelected)
            {
                MainFrame.Navigate(typeof(Pages.QuickNotePage));
                MainTextBlock.Text = "Quick Note";
                BackButton.Visibility = Visibility.Visible;

            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
                HomeListBoxItem.IsSelected = true;
                /* 这里好像有个问题：
                 * 如果用代码改变IsSelected属性，“好像” 会触发SelectionChanged事件
                 *      如果注释掉 MainFrame.GoBack(); 也能回到HomePage
                 * 这样就会有多占点内存？
                 * */
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
```

## WIndows 的 Segoe MDL2 Assets 符号集

有各种win10程序用到的字符。

- 可以直接在 win10 搜 "character map" 的 "char" 就能弹出字符映射表
- https://docs.microsoft.com/en-us/windows/uwp/design/style/segoe-ui-symbol-font
- 也可以在 这里 http://modernicons.io/segoe-mdl2/cheatsheet/ 查找

使用方法：

例如：我想使用 十六进制为 E700 的字符

```xml
<Button FontFamily="Segoe MDL2 Assets" Content="&#xE700;"/>
```
