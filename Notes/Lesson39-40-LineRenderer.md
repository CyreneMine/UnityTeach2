# Lesson39-40 - LineRenderer 练习

## 对应视频

- `39.LineRenderer.mp4`
- `40.LineRenderer  练习题.mp4`

## 练习目标

1. 写一个方法，传入中心点和半径，用 `LineRenderer` 画一个圆。
2. 在 `Game` 窗口长按鼠标时，用 `LineRenderer` 画出鼠标移动轨迹。

## 对应文件

- [Lesson21.cs](../Assets/Scripts/Lesson/Lesson21_LineRender/Lesson21.cs)
- [Lesson21.unity](../Assets/Scripts/Lesson/Lesson21_LineRender/Lesson21.unity)

## 当前实现

画圆方法：

```csharp
public void DrawSphere(Vector3 center, float radius)
{
    if (GetComponent<LineRenderer>() == null)
    {
        lineRenderer = this.AddComponent<LineRenderer>();
    }
    lineRenderer.loop = true;
    lineRenderer.positionCount = 360;
    for (int i = 0; i < 360; i++)
    {
        lineRenderer.SetPosition(i, center + Quaternion.AngleAxis(i, Vector3.up) * Vector3.forward * radius);
    }
}
```

鼠标轨迹：

```csharp
line2 = this.AddComponent<LineRenderer>();
line2.loop = false;
line2.positionCount = 0;
```

```csharp
if (flag)
{
    line2.positionCount += 1;
    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10);
    line2.SetPosition(line2.positionCount - 1, pos);
}
```

## 理解记录

- `LineRenderer.positionCount` 决定线段点的数量。
- `SetPosition(index, position)` 用来设置每个点的位置。
- 画圆时可以把一个方向向量绕 `Vector3.up` 旋转一圈，得到圆周上的点。
- `LineRenderer.loop = true` 可以让最后一个点和第一个点闭合。
- 鼠标坐标需要从屏幕坐标转换到世界坐标，当前使用 `Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10)`，让轨迹落在距离相机 10 个单位的世界平面附近。

## ScreenToWorldPoint 理解补充

本节额外复盘了 `ScreenToWorldPoint` 和 `Input.mousePosition.z` 的区别。

一开始容易把下面两种写法看成等价：

```csharp
Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
```

```csharp
Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10);
```

它们实际含义不同。

`Input.mousePosition` 是屏幕坐标，其中 `x`、`y` 是鼠标屏幕像素位置，`z` 不是世界坐标的 z，而是这个屏幕点距离摄像机多远。默认 `Input.mousePosition.z == 0`，所以：

```csharp
Camera.main.ScreenToWorldPoint(Input.mousePosition)
```

等价于把鼠标屏幕点转换到距离摄像机 `0` 单位的位置，结果通常会落在摄像机位置附近，而不是鼠标在场景中真正想要的世界点。

如果之后再写：

```csharp
+ Vector3.forward * 10
```

这只是把“摄像机附近的世界点”沿世界 Z 轴正方向移动 10 个单位。`Vector3.forward` 是世界方向 `(0, 0, 1)`，不是屏幕深度。

而当前代码：

```csharp
Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10)
```

本质是先把鼠标屏幕点改成：

```csharp
new Vector3(mouseX, mouseY, 10)
```

再转换成世界坐标，表示取“鼠标屏幕位置在摄像机前方 10 单位处”的世界点。

更清晰的推荐写法是：

```csharp
Vector3 mousePos = Input.mousePosition;
mousePos.z = 10;
Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
```

一句话记忆：

```text
ScreenToWorldPoint 里的 z，不是世界 z，而是离摄像机多远。
```

## 检查记录

| 优先级 | 位置 | 问题 | 影响 | 状态 |
| --- | --- | --- | --- | --- |
| 通过 | `Lesson21.cs` | `DrawSphere(center, radius)` 使用 `LineRenderer` 设置 360 个圆周点，并开启 `loop` | 满足“写一个画圆方法”的核心要求 | 已实现 |
| 通过 | `Lesson21.cs` | 鼠标按下时持续增加点并记录鼠标世界坐标 | 满足“长按鼠标画轨迹”的核心要求 | 已实现 |
| 通过 | `Lesson21.unity` | 场景中 `Lesson21` 对象已挂载脚本，`center = (3, 0, 0)`、`radius = 10` | 画圆参数已保存 | 已检查 |
| 通过 | `Lesson21.unity` | `Main Camera` 带有 `MainCamera` 标签 | `Camera.main` 有基础条件 | 已检查 |
| P2 | `Lesson21.cs` | `DrawSphere(center, radius)` 在 `Start()` 中被注释掉 | 运行场景时不会自动显示第一题的圆，第一题只处于“方法已写好”状态 | 若要验证第一题，需要临时调用该方法 |
| P2 | `Lesson21.cs` | `DrawSphere` 只在没有 `LineRenderer` 时给 `lineRenderer` 赋值 | 如果对象已经有 `LineRenderer`，再调用会让 `lineRenderer` 保持空引用 | 后续可改为先获取组件，再按需添加 |
| P3 | `Lesson21.cs` | 鼠标每次重新按下不会清空旧轨迹 | 多次绘制时会把上一段和下一段连起来 | 学习阶段可接受，后续可在按下时重置或新建一条线 |
| P3 | `Lesson21.cs` | 按住鼠标时每帧 `print(Input.mousePosition)` | 便于验证屏幕坐标，但长按时 Console 输出会很多 | 调试结束后可注释 |
| P3 | `Lesson21.cs` | `using System;`、`using Unity.VisualScripting;` 当前未使用 | 不影响运行；`VisualScripting` 包已存在，不会因此编译失败 | 后续整理时可删除 |

## 验证方式

- 静态检查 `Lesson21.cs`：确认画圆方法设置了 `loop`、`positionCount` 和每个圆周点。
- 静态检查 `Lesson21.cs`：确认鼠标长按时会持续向第二个 `LineRenderer` 追加点。
- 静态检查 `Lesson21.unity`：确认 `Lesson21` 脚本已挂载，画圆参数已保存。
- 静态检查 `Lesson21.unity`：确认 `Main Camera` 带有 `MainCamera` 标签。
- 根据外部讨论复盘，确认 `Input.mousePosition + Vector3.forward * 10` 是在转换前设置屏幕点深度，和转换后沿世界 Z 轴偏移不同。
- 本次 Codex 没有直接启动 Unity 运行场景，圆形和鼠标轨迹的实际显示效果以用户在 Unity 中验证为准。

## 尚未验证或边界

- 未验证 `LineRenderer` 的材质、宽度、排序和颜色在 Game 视图中的实际显示效果。
- 未验证多次按下鼠标时是否需要清空轨迹或拆分成多条线。
- 当前画圆方法名为 `DrawSphere`，实际画的是绕 `Y` 轴的一圈圆；如果后续要画真正球体，需要多圈圆或网格。

## 下一步

进入 `41.范围检测.mp4`。
