# XAML

- XAML是一种简单的，创建类实例的方法
- XAML只是特定格式的XML，遵循XML的所有规定

## 例子

```xaml
<!-- XAML -->
<Button x:Name="myButton"
        Name="ClickMeButton"
        Content="Click Me"
        Margin="20,20,0,0"
        HorizontalAlignment="Left"
        VerticalAlignment="Top"
        Click="ClickMeButton_Click"
        Background="Red"
        Width="200"
        Height="100"/>
```

```c#
// c#
Button myButton = new Button() {
    Name = "ClickMeButton",
    Content = "Click Me",
    Margin = new Thickness(20, 20, 0, 0),
    HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left,
    VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top,
    Background = new SolidColorBrush(Windows.UI.Colors.Red),
    Width = 200,
    Height = 100
};
myButton.Click += ClickMeButton_Click;
```

### 类型转换器 Type Converter

如同上例所示：C#代码中`HorizontalAlignment`的值对应的是一个枚举类型。通过类型转换器，XAML能把`Left`，转换为`HorizontalAlignment.Left`

## XAML 语法

```xaml
<Page xxx="XXX">
    <Grid yyy="YYY">
        <Button zzz="ZZZ"/>
    </Grid>
</Page>
```

元素是一种内含关系，树状关系（一个元素，嵌套在另一个元素内）：

- `Page` 的 `Content` 属性被设置为 `Grid` 类型
- `Grid` 有一个叫 `Children` 的Collection，这个就是向`Children`中增加了一个`Button`

### 元素属性语法 Property Element Syntax

由于某些属性过于复杂，无法直接使用`键=值`的方法，所以多重嵌套。（个人理解）

`<Object.Property>`

```xaml
<Button>
    <Button.Background> <!-- 这个就是 -->
        <LinearGradientBrush EndPoint="0.5,1" StartPoint="0.5,0">
            <GradientStop Color="Black" Offset"0"/>
            <GradientStop Color="White" Offset"1"/>
        </LinearGradientBrush>
    </Button.Background>
</Button>
<!-- 这也是省略过的 (　TロT)σ -->

<Button>
    <Button.Background>
        <LinearGradientBrush EndPo5,1" StartPoint="0.5,0">
            <LinearGradientBrush.GradientStops>
                <GradientStopCollection>
                    <GradientStop Color="Black" Offset"0"/>
                    <GradientStop Color="White" Offset"1"/>
                </GradientStopCollection>
            </LinearGradientBrush.GradientStops>
        </LinearGradientBrush>
    </Button.Background>
</Button>
```

由于`GradientStopCollection`只能存放`GradientStop`，所以这个就省略掉了，由编译器自动生成。`LinearGradientBrush.GradientStops` 同理。

## schema

也就是XAML要遵循的条款：

- xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"      // UI Elements
- xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"                 // General Rules
- xmlns:local="using:SDKTemplate"                                        // using namespace
- xmlns:d="http://schemas.microsoft.com/expression/blend/2008"           // designer
- xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" // designer

而后面对象中 `x:Name` 的`x`就是第二个 `xmlns:x` ？？


