# Lesson14 - 向量叉乘方位判断练习

## 对应视频

- `13.向量叉乘.mp4`
- `14.向量叉乘  练习题.mp4`

## 练习目标

1. 判断目标 B 在物体 A 的左上、左下、右上、右下哪个方位。
2. 当目标 B 位于 A 的左前方 `20` 度范围内，或右前方 `30` 度范围内，并且距离 A 不超过 `5` 米时，在控制台打印“检测到敌人入侵！”。

## 对应文件

- `Assets/Scripts/Lesson/Lesson7_向量叉乘/Lesson7.cs`
- `Assets/Scripts/Lesson/Lesson7_向量叉乘/Lesson7.unity`

## 当前实现

```csharp
float dot = Vector3.Dot(transform.forward, (target.position - transform.position).normalized);
Vector3 cross = Vector3.Cross(transform.forward, (target.position - transform.position).normalized);
float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
float distance = Vector3.Distance(transform.position, target.position);
```

方位判断：

```csharp
if (dot <= 0)
{
    print(cross.y > 0 ? "右后方" : "左后方");
}
else if (dot > 0)
{
    print(cross.y > 0 ? "右前方" : "左前方");
}
```

扇形检测：

```csharp
if (cross.y > 0)
{
    if (angle <= 30 && distance <= 5)
    {
        print("检测到敌人入侵！");
    }
}
else
{
    if (angle <= 20 && distance <= 5)
    {
        print("检测到敌人入侵！");
    }
}
```

场景序列化检查：

| 对象 | 组件/字段 | 当前值 |
| --- | --- | --- |
| `A` | `Lesson7` | 已挂载 |
| `A` | `target` | 指向 `B` |
| `A` | Transform | 默认朝向，前方为世界 `+Z` |

## 理解记录

- 点乘 `dot` 可以判断目标大致在前方还是后方：`dot > 0` 为前方，`dot <= 0` 为后方。
- 叉乘 `cross` 可以判断目标在左侧还是右侧。
- 在当前 Unity 坐标约定和 `Vector3.Cross(transform.forward, targetDir)` 顺序下，`cross.y > 0` 对应右侧，`cross.y < 0` 对应左侧。
- 题面里的“上/下”可以映射成场景里的“前/后”：左上就是左前，左下就是左后。
- 第二题是非对称扇形：左前方 `20` 度，右前方 `30` 度，还要同时满足距离不超过 `5` 米。

## 自查结果

| 优先级 | 位置 | 问题 | 影响 | 建议 |
| --- | --- | --- | --- | --- |
| P3 | `Lesson7.cs` | 当前角度和距离使用完整 3D 坐标，包含 `y` 高度差 | 俯视方向题通常只关心 XZ 平面；高度差可能让角度或距离略有偏差 | 如果后续题目明确只看水平面，可将方向向量的 `y` 分量归零后再计算 |
| P3 | `Lesson7.cs` | `target` 没有空引用保护 | 当前场景已绑定，不影响本练习；换场景可能报错 | 后续学习组件引用检查时再补 |
| P3 | `Lesson7.cs` | `Mathf.Acos` 前没有 clamp | 极端浮点误差下可能得到 `NaN`，本练习通常不会遇到 | 后续可以养成 `Mathf.Clamp(dot, -1, 1)` 的习惯 |

## 验证方式

已经做过的静态检查：

- 脚本挂在 `A` 上。
- `target` 已绑定到 `B`。
- 同时使用了点乘、叉乘、夹角和距离判断。
- 当前没有输入系统或自动移动逻辑，A/B 摆位主要用于手动拖动调试边界。

建议在 Unity 中继续确认：

- 停止 Play Mode 后保存场景。
- 运行场景，手动拖动 B 到 A 的四个方位，确认 Console 方位文字符合预期。
- 将 B 拖到左前 20 度内、右前 30 度内、距离 5 米内，确认会打印入侵提示。
- 将 B 拖出角度范围或距离范围，确认不会打印入侵提示。

## 尚未验证或边界

- Codex 没有直接启动 Unity 运行场景，只检查了磁盘上的脚本和场景序列化内容。
- 当前练习没有输入系统，所以不把保存瞬间的 B 坐标是否触发作为主要判错依据。
- 后续如果 B 的位置由输入系统、自动移动或关卡初始站位决定，就需要重新检查场景物体位置。

## 下一步

进入 `15.向量插值运算.mp4`，重点对比 `Lerp`、`Slerp`、`MoveTowards` 等插值方式的含义和适用场景。
