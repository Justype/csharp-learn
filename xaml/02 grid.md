# Grid

```xaml
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

```xaml
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

```xaml
<Grid.RowDefinitions>
    <RowDefinition Height="1*" />
    <RowDefinition Height="2*" />
    <RowDefinition Height="3*" />
</Grid.RowDefinitions>

<!-- 把屏幕高度分为六份，第一行 1份， 第二行 2份... -->


```

## 位置相关

- HorizontalAlignment
- VerticalAlignment
- Margin "左, 上, 右, 下"

注意！！：这里的Margin与CSS中的不同，**CSS**为 **上右下左**

Alignment决定的是margin作用的边：
- 如果设置了HorizontalAlignment="Right"，则看MarginRight
