# 输入控件

三大点：Display, Event, Retrieve

## 各种控件



| 控件        | 特点                      | 属性                 | 事件    |
| ----------- | ------------------------- | -------------------- | ------- |
| CheckBox    | 复选                      | IsChecked            | Tapped  |
| RadioButton | 单选                      | GroupName, IsChecked | Checked |
| ComboBox    | 下拉菜单选择               | IsSelected           |         |
| **ListBox** | 列表选择                  | SelectionMode, SelectionChanged |         |
| Image       | 图片                      | Source, Stetch        |         |
| ToggleButton | 开关按钮（可以有三种状态） | IsThreeState          | |
| ToggleSwitch | 开关                     | .OffContent, .OnContent | |

### ComboBox 获得所选的值

```c#
private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    if(ComboBoxResultTextBlock == null) return;
    
    String Text = ((ComboBoxItem)((ComboBox)sender)).Content.ToString();
}
```

### ToggleSwitch

```xml
<ToggleSwitch>
    <ToggleSwitch.OffContent>
        <TextBlock Text="Off" />
    </ToggleSwitch.OffContent>
    <ToggleSwitch.OnContent>
        <TextBlock Text="On" />
    </ToggleSwitch.OnContent>
    <!-- 显示在开关的后面 -->
</ToggleSwitch>
```

### ListBox

获得选中的项目

```c#
var selectedItmes = MyListBox.Items.Cast<ListBoxItem>()
                        .Where(p => p.IsSelected)
                            .Select(t => t.Content.ToString())
                                .ToArray();
```
