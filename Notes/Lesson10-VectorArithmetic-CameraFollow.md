# Lesson10 - 向量加减乘除摄像机跟随练习

## 对应视频

- `10.向量加减乘除  练习题.mp4`

## 练习目标

用向量相关知识实现摄像机跟随目标物体，并且摄像机不能作为目标物体的子物体。

要求位置：

- 摄像机在物体后方 `4` 米；
- 摄像机在物体上方 `7` 米；
- 摄像机持续看向目标物体。

## 对应文件

- `Assets/Scripts/Lesson/Lesson5_向量加减乘除/Lesson5.cs`
- `Assets/Scripts/Lesson/Lesson5_向量加减乘除/Lesson5.unity`

## 当前实现

```csharp
transform.position = target.position - target.forward * zOffset + target.up * yOffset;
transform.LookAt(target);
```

场景序列化检查：

| 对象 | 组件/字段 | 当前值 |
| --- | --- | --- |
| `Main Camera` | `Lesson5` | 已挂载 |
| `Main Camera` | `zOffset` | `4` |
| `Main Camera` | `yOffset` | `7` |
| `Main Camera` | `target` | 指向 `Cube` |
| `Cube` | Transform | 独立对象，不是摄像机父子层级 |

## 理解记录

这节真正要练的是“用目标自己的方向向量描述相对位置”，而不是写死世界坐标。

- `target.position` 是目标当前位置。
- `target.forward` 是目标自身的前方方向。
- `-target.forward * zOffset` 表示沿目标自身后方移动 `zOffset` 米。
- `target.up * yOffset` 表示沿目标自身上方移动 `yOffset` 米。
- 三段向量相加后，得到摄像机应该在的世界坐标。

一开始写的思路：

```csharp
transform.position = new Vector3(target.position.x, target.position.y + 7, target.position.z - 4);
```

这个写法只是在世界坐标里固定让 `z` 减 `4`、`y` 加 `7`。如果目标永远不旋转，看起来可以满足需求；但目标一旦转向，世界坐标的 `z - 4` 就不一定是目标的“身后”。所以它没有真正使用目标自身方向。

## 自查结果

| 优先级 | 位置 | 问题 | 影响 | 建议 |
| --- | --- | --- | --- | --- |
| P3 | `Lesson5.cs` | `using System;` 当前没有被使用 | 不影响运行，只是多余引用 | 后续整理脚本时可以删除 |
| P3 | `Lesson5.cs` | 没有对 `target` 为空做保护 | 当前场景已绑定，不影响本练习；复制到别的场景时可能空引用 | 学到组件引用检查后，可以补 `if (target == null) return;` |

## 验证方式

已经做过的静态检查：

- 脚本公式符合“目标后方 4 米、上方 7 米”的要求。
- 场景中 `Main Camera` 已挂载脚本。
- `target` 已绑定到 `Cube`。
- 摄像机和 `Cube` 不是父子关系。

建议在 Unity 中继续确认：

- 停止 Play Mode 后保存场景。
- 运行场景，观察摄像机是否跟随 `Cube`。
- 手动旋转 `Cube` 的 Y 轴，确认摄像机跟随的是目标自身后方，而不是世界坐标负 Z 方向。
- Console 没有 `NullReferenceException`。

## 尚未验证或边界

- Codex 没有直接启动 Unity 运行场景，只检查了磁盘上的脚本和场景序列化内容。
- 如果后续目标快速移动或旋转，这个写法是硬跟随，没有平滑过渡。
- 这节课重点是向量加减乘除，不需要提前引入平滑相机系统。

## 下一步

进入 `11.向量点乘.mp4`，重点理解点乘和夹角、朝向判断之间的关系。
