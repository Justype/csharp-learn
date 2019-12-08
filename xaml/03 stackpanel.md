# StackPanel

```xaml
<StackPanel>
    <TextBlock>Top</TextBlock>
    <TextBlock>Bottom</TextBlock>
</StackPanel>
```

- Vertical Orientation by default.
- Left-to-right flow by default when Horizontal orientation.
- Most layouts will combine multiple layout controls.
- Grid will overlap controls. StackPanel will stack them.

## 练习 UWP-13

```xaml
<Page
    x:Class="UWP_13.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:UWP_13"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">

    <StackPanel Margin="10" Height="auto">
        <TextBlock Text="Lorem Ipsum" FontSize="48"/>
        <StackPanel Orientation="Horizontal">
            <StackPanel Width="250" Margin="10">
                <TextBlock  Margin="0,0,0,10" Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut luctus, mi nec fermentum congue, turpis nibh porttitor ex, sed commodo neque eros non libero. Donec mauris felis, malesuada nec consectetur et, pharetra in ante. Praesent nec hendrerit purus. Ut vel nunc id nisl volutpat porta. Cras in convallis ipsum. Integer id sollicitudin ex. Ut sed iaculis nisl." TextWrapping="Wrap"/>
                <TextBlock Text="Vestibulum sit amet neque nec eros interdum laoreet nec vitae justo. Maecenas scelerisque finibus nisl, a iaculis mauris fermentum sed. Pellentesque faucibus, turpis quis ultrices facilisis, metus eros vulputate augue, sit amet sodales nisl lacus ut massa. Pellentesque vitae dolor nulla. Nullam ultricies imperdiet nisl, interdum placerat dolor posuere a. Nunc tempor sed sapien nec dictum. Morbi eleifend tellus vitae nunc tincidunt viverra. Duis vitae justo eget purus bibendum dictum quis feugiat turpis. Curabitur ultrices purus ut ipsum ullamcorper, sit amet faucibus nisl commodo. Nulla aliquet ante eu leo tincidunt, a tempor nibh maximus." TextWrapping="Wrap"/>
            </StackPanel>
            <StackPanel Width="500" Margin="10">
                <TextBlock Margin="0,0,0,10" TextWrapping="Wrap" Text="Vestibulum varius bibendum metus. Aliquam erat volutpat. Vestibulum dignissim quam at ligula cursus iaculis. Nam posuere enim ut pellentesque congue. Aliquam malesuada nulla lacus, auctor interdum magna gravida molestie. Sed mollis non velit a maximus. Phasellus scelerisque, nibh at maximus ultrices, ex eros condimentum leo, vel aliquam dolor nisl in mauris. "/>
                <TextBlock Margin="0,0,0,10" TextWrapping="Wrap" Text="Praesent volutpat odio sed ante posuere lacinia. Suspendisse eget ullamcorper dui. Curabitur interdum velit congue erat elementum eleifend. Donec tincidunt arcu vitae est condimentum lacinia. Nullam at sem eu purus congue porta eget vel lorem. Aenean gravida diam tincidunt consequat tincidunt. Sed vel vestibulum eros, quis lacinia tortor. Suspendisse luctus, arcu quis venenatis dapibus, dolor neque suscipit ipsum, id cursus nibh nisi eu diam. Suspendisse vestibulum ut orci ut malesuada. Aliquam gravida leo et massa commodo, lobortis ultrices mauris pulvinar. Cras vulputate, tellus quis consectetur fringilla, enim leo pharetra est, et varius nibh eros in sapien. Nunc tortor tortor, mattis at ullamcorper et, tincidunt ac ante. Nam sed orci felis. Duis non turpis eu mauris blandit mollis. Ut vestibulum libero sed ex viverra, quis faucibus mauris tristique."/>
                <TextBlock TextWrapping="Wrap" Text="Nunc a vehicula velit. Vivamus rutrum elementum massa in consectetur. Nunc placerat leo sit amet est accumsan, et rhoncus leo tincidunt. Ut suscipit orci sed ultrices commodo. Vestibulum sed purus semper, ullamcorper metus in, ultrices nulla. Aliquam erat volutpat. Mauris non tellus pretium, accumsan velit eu, suscipit ipsum. Integer risus dui, feugiat id rutrum ut, aliquet et arcu. Duis ac lacus ac magna pretium semper ut ut metus. Phasellus nec elit quis erat condimentum tincidunt. Vestibulum porttitor tincidunt dui, eu ornare massa fringilla sit amet. Cras et maximus justo. Aenean non enim vel massa pulvinar congue. Phasellus quis nisi vel mauris gravida tempor. Mauris sed fringilla lacus, interdum fermentum massa. Quisque pretium sollicitudin sapien vitae cursus."/>
            </StackPanel>
            <StackPanel Orientation="Horizontal" Width="300" Margin="10" VerticalAlignment="Top">
                <Rectangle Fill="Blue" Height="200" Width="200"/>
                <StackPanel>
                    <Rectangle Fill="Red" Height="100" Width="100"/>
                    <Rectangle Fill="Yellow" Height="100" Width="100"/>
                </StackPanel>
            </StackPanel>
        </StackPanel>
    </StackPanel>
</Page>

```

## Grid+StackPanel 练习 UWP-15

```xaml
<Page
    x:Class="UWP_15.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:UWP_15"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">

    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition/>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="150"/>
            <ColumnDefinition Width="350"/>
            <ColumnDefinition/>
        </Grid.ColumnDefinitions>

        <TextBlock Text="Title 1" Margin="10" FontSize="24"/>
        <TextBlock Grid.Row="1" Text="Title 2" Margin="10" FontSize="24"/>
        <TextBlock Grid.Row="2" Text="Title 3" Margin="10" FontSize="24"/>

        <StackPanel Grid.Column="1">
            <TextBlock Margin="10,10,10,0" TextWrapping="Wrap" Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut luctus, mi nec fermentum congue, turpis nibh porttitor ex, sed commodo neque eros non libero. Donec mauris felis, malesuada nec consectetur et, pharetra in ante. Praesent nec hendrerit purus. Ut vel nunc id nisl volutpat porta. Cras in convallis ipsum. Integer id sollicitudin ex. Ut sed iaculis nisl. "/>
            <StackPanel Orientation="Horizontal" Margin="10,0,0,0">
                <Rectangle Fill="Green" Width="100" Height="30"/>
                <Rectangle Fill="Black" Width="100" Height="30"/>
                <Rectangle Fill="Red" Width="100" Height="30"/>
            </StackPanel>
        </StackPanel>
        <StackPanel Grid.Row="1" Grid.Column="1">
            <TextBlock Margin="10,10,10,0" TextWrapping="Wrap" Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut luctus, mi nec fermentum congue, turpis nibh porttitor ex, sed commodo neque eros non libero. Donec mauris felis, malesuada nec consectetur et, pharetra in ante. Praesent nec hendrerit purus. Ut vel nunc id nisl volutpat porta. Cras in convallis ipsum. Integer id sollicitudin ex. Ut sed iaculis nisl. "/>
            <StackPanel Orientation="Horizontal" Margin="10,0,0,0">
                <Rectangle Fill="Red" Width="100" Height="30"/>
                <Rectangle Fill="Black" Width="100" Height="30"/>
                <Rectangle Fill="Blue" Width="100" Height="30"/>
            </StackPanel>
        </StackPanel>
        <StackPanel Grid.Row="2" Grid.Column="1">
            <TextBlock Margin="10,10,10,0" TextWrapping="Wrap" Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut luctus, mi nec fermentum congue, turpis nibh porttitor ex, sed commodo neque eros non libero. Donec mauris felis, malesuada nec consectetur et, pharetra in ante. Praesent nec hendrerit purus. Ut vel nunc id nisl volutpat porta. Cras in convallis ipsum. Integer id sollicitudin ex. Ut sed iaculis nisl. "/>
            <StackPanel Orientation="Horizontal" Margin="10,0,0,0">
                <Rectangle Fill="Blue" Width="100" Height="30"/>
                <Rectangle Fill="Black" Width="100" Height="30"/>
                <Rectangle Fill="Yellow" Width="100" Height="30"/>
            </StackPanel>
        </StackPanel>
    </Grid>
</Page>

```
