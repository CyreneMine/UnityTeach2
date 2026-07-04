# Lesson16 - 向量插值摄像机跟随和太阳升降练习

## 对应视频

- `15.向量插值运算.mp4`
- `16.向量插值运算  练习题.mp4`

## 练习目标

1. 用线性插值相关知识，实现摄像机跟随目标物体，摄像机不能作为目标物体的子物体。
2. 摄像机保持在目标物体后方 `4` 米、上方 `7` 米的位置。
3. 通过球形插值模拟太阳升降变化。

## 对应文件

- `Assets/Scripts/Lesson/Lesson8_向量插值/Lesson8.cs`
- `Assets/Scripts/Lesson/Lesson8_向量插值/Lesson8.unity`
- `Assets/Materials/Gray.mat`

## 当前实现

摄像机插值跟随：

```csharp
if (nowTargetPos != target.position)
{
    nowTargetPos = target.position;
    time = 0;
    startPos = transform.position;
}

time += Time.deltaTime;
finalPos = nowTargetPos - target.forward * zOffset + target.up * yOffset;
transform.position = Vector3.Lerp(startPos, finalPos, time);
transform.LookAt(target);
```

太阳球形插值：

```csharp
sunTime += Time.deltaTime;
sun.position = Vector3.Slerp(Vector3.right * 10, Vector3.left * 10 + Vector3.up, sunTime * 0.3f);
```

场景序列化检查：

| 对象 | 组件/字段 | 当前值 |
| --- | --- | --- |
| `Main Camera` | `Lesson8` | 已挂载 |
| `Main Camera` | `target` | 指向 `Cube` |
| `Main Camera` | `sun` | 指向 `Sphere` |
| `Main Camera` | `zOffset` | `4` |
| `Main Camera` | `yOffset` | `7` |
| `Main Camera` | Transform | 根对象，不是 `Cube` 子物体 |
| `Cube` | Transform | 根对象 |
| `Sphere` | Transform | 根对象，用作太阳演示对象 |
| `Plane` | Material | 使用 `Gray.mat` |

## 理解记录

- 摄像机最终目标点仍然来自目标物体自身方向：`target.position - target.forward * zOffset + target.up * yOffset`。
- `Vector3.Lerp(startPos, finalPos, time)` 表示从记录的起点向目标相机位插值。
- 当前写法在检测到目标位置变化时，重新记录相机当前位置为新的起点，并把 `time` 重置为 `0`。
- `Vector3.Slerp` 用于球形插值，比普通 `Lerp` 更适合表现弧线运动。
- 太阳使用 `Vector3.right * 10` 到 `Vector3.left * 10 + Vector3.up` 的球形插值，表现从右侧向左侧升降的弧线变化。

## 自查结果

| 优先级 | 位置 | 问题 | 影响 | 建议 |
| --- | --- | --- | --- | --- |
| P3 | `Lesson8.cs` | `using System;` 当前未使用 | 不影响运行 | 后续整理脚本时可以删除 |
| P3 | `Lesson8.cs` | `time` 和 `sunTime` 没有手动限制到 `0~1` | `Vector3.Lerp/Slerp` 会夹紧插值参数；太阳到终点后会停住 | 如果要循环太阳升降，可后续使用 `Mathf.PingPong` |
| P3 | `Lesson8.cs` | 目标只旋转但位置不变时，不会重置摄像机插值起点 | 当前题目主要练插值和跟随位置；如果后续测试旋转跟随，可能出现相机直接贴到新的后方位置 | 后续可同时记录目标朝向，或改成每帧从当前位置向目标相机位插值 |
| P3 | `Lesson8.cs` | `target`、`sun` 没有空引用保护 | 当前场景已绑定，不影响本练习；换场景时可能报错 | 后续学习引用检查时再补 |

## 验证方式

已经做过的静态检查：

- `Lesson8` 挂载在 `Main Camera`。
- `target` 已绑定到 `Cube`。
- `sun` 已绑定到 `Sphere`。
- 摄像机、目标物体和太阳演示对象都是根对象，摄像机没有作为目标物体子物体。
- 插值跟随和球形插值代码都存在。

建议在 Unity 中确认：

- 停止 Play Mode 后保存场景。
- 运行场景，移动 `Cube`，观察摄像机是否从当前位置插值到目标后方上方的位置。
- 观察 `Sphere` 是否从右侧沿弧线向左侧移动。
- Console 没有 `NullReferenceException`。

## 尚未验证或边界

- Codex 没有直接启动 Unity 运行场景，只检查了磁盘上的脚本和场景序列化内容。
- 当前太阳升降是一次性插值到终点，不是循环昼夜。
- 当前摄像机跟随是“目标位置变化后重置一次插值”，不是每帧持续平滑追踪目标相机位。

## 阶段收尾

阶段 02 的坐标系、向量、点乘、叉乘和插值练习已完成。下一步进入四元数阶段，从 `17.为何使用四元数.mp4` 开始。
