# Grid

```xml
<Grid>
    <Button><!-- myGrid.Children.Add(new Button()); -->
        <TextBlock>Hi</TextBlock>
        <!-- Content 只能有一个 -->
    </Button>
<Grid>
```

## 属性

- Grid 默认一行一列
- RowDefinitions
- ColumnDefinitions

```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="*" />
    </Grid.RowDefinitions>
    
    <Rectangle Grid.Row="0" Height="100" Fill="Beige" />
    <Rectangle Grid.Row="1" Fill="SteelBlue" />
<Grid>
```

### Height

- `Auto`: 与行内控件相关
- `*`:占据剩余可用的高度，按比例分配

```xml
<Grid.RowDefinitions>
    <RowDefinition Height="1*" />
    <RowDefinition Height="2*" />
    <RowDefinition Height="3*" />
</Grid.RowDefinitions>

<!-- 把屏幕高度分为六份，第一行 1份， 第二行 2份... -->
```

## 设置元素的位置

- 元素的位置
    - Grid.Row="1" : 从 0 开始，默认为 0
    - Grid.Column="1"
- 元素的跨行
    - Grid.ColumnSpan="3" ： 从 1 开始，默认为 1
    - Grid.RowSpan

## 位置相关

- HorizontalAlignment
- VerticalAlignment
- Margin "左, 上, 右, 下"

注意！！：这里的Margin与CSS中的不同，**CSS**为 **上右下左**

Alignment决定的是margin作用的边：
- 如果设置了HorizontalAlignment="Right"，则看MarginRight

## 练习 UWP-11

```xml
<Page
    x:Class="UWP_11.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:UWP_11"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">

    <Grid Margin="20,20,20,20">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <!-- 不知道高度，可以设置Auto-->
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition/>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="2*"/>
            <ColumnDefinition Width="3*"/>
            <ColumnDefinition Width="1*"/>
        </Grid.ColumnDefinitions>
        <TextBlock Text="ACME Sales Corp" Grid.ColumnSpan="3" FontSize="42"/>
        <TextBlock Text="First Name:" Grid.Row="1" VerticalAlignment="Center" HorizontalAlignment="Right"/>
        <TextBox Grid.Row="1" Grid.Column="1" Margin="10,10,10,10"/>
        <TextBlock Text="Last Name:" Grid.Row="2" VerticalAlignment="Center" HorizontalAlignment="Right"/>
        <TextBox Grid.Row="2" Grid.Column="1" Margin="10,10,10,10"/>
        <TextBlock Text="Email:" Grid.Row="3" VerticalAlignment="Center" HorizontalAlignment="Right"/>
        <TextBox Grid.Row="3" Grid.Column="1" Margin="10,10,10,10"/>
        <Button Content="Save" Grid.Row="4" Grid.Column="1" Margin="10,10,10,10"/>
    </Grid>
</Page>
```