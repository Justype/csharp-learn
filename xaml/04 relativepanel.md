# RelativePanel

## quarrel

应用的顶部和左侧，用于提供应用内的导航和服务。这种导航模式称为 Hamburger Navigation 抽屉导航。

典型的Win10设置
- 收拢模式：只显示图标
- 点击后会展开

| 图标 | 工具栏 |
| ---  | ----- |
| 工   | 内容  |
| 具   | 内容  |
| 栏   | 内容  |

## Attach Property 附加属性

1. Panel Alignment Relationship 面板对齐关系：将控件与面板对齐（顶端，左侧...）
2. Sibling Alignment Relationship 同级对齐关系
3. Sibling Postion Relationship 同级位置关系

### 各种属性

可以多个RelativePanel属性

- Panel Alignment Relationship
    - AlignXXXWithPanel
    - `RelativePanel.AlignRightWithPanel = "true"`
    - `RelativePanel.AlignHorizontalCenterWithPanel = "true"`
- Sibling Alignment Relationship
    - AlignXXXWith
    - `RelativePanel.AlignVerticalCenterWith = "object"`
    - `RelativePanel.AlignTopWith = "object"`
- Sibling Postion Relationship
    - `RelativePanel.Leftof = "object"`
    -  `RelativePanel.Below = "object"`

## Order of Alignment Priority

1. Alignment to Panel
    - AlignTopWithPanel
    -  AlignLeftWithPanel
2. Sibling Alignment
    - AlignTopWith
    - AlignLeftWith
3. Sibling Postional
    - Above
    - Below