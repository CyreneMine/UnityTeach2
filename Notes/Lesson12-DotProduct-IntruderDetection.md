# Lesson12 - 向量点乘入侵者检测练习

## 对应视频

- `11.向量点乘.mp4`
- `12.向量点乘  练习题.mp4`

## 练习目标

当目标 B 位于物体 A 前方 `45` 度角范围内，并且距离 A 不超过 `5` 米时，在控制台打印“检测到敌人入侵！”。

## 对应文件

- `Assets/Scripts/Lesson/Lesson6_向量点乘/Lesson6.cs`
- `Assets/Scripts/Lesson/Lesson6_向量点乘/Lesson6.unity`

## 当前实现

```csharp
float dotResult = Vector3.Dot(transform.forward, (target.transform.position - transform.position).normalized);
float angle = Mathf.Acos(dotResult) * Mathf.Rad2Deg;
float distance = Vector3.Distance(transform.position, target.transform.position);

if (angle < detectionAngle / 2 && distance < detectionDistance)
{
    print("检测到敌人入侵！");
}
```

场景序列化检查：

| 对象 | 组件/字段 | 当前值 |
| --- | --- | --- |
| `A` | `Lesson6` | 已挂载 |
| `A` | `detectionAngle` | `45` |
| `A` | `detectionDistance` | `5` |
| `A` | `target` | 指向 `B` |

## 理解记录

- `target.position - transform.position` 得到 A 指向 B 的方向向量。
- `.normalized` 只保留方向，避免距离影响点乘结果。
- `Vector3.Dot(transform.forward, targetDir)` 可以表示两个方向的接近程度。
- `Mathf.Acos(dotResult) * Mathf.Rad2Deg` 将点乘结果转换成夹角角度。
- 距离条件需要单独判断，角度成立不代表目标一定在 5 米内。

手写 `Dot + Acos` 是为了理解点乘和夹角的关系。实际项目中，如果只是想要夹角，也可以直接使用：

```csharp
Vector3.Angle(transform.forward, target.transform.position - transform.position);
```

## 自查结果

| 优先级 | 位置 | 问题 | 影响 | 建议 |
| --- | --- | --- | --- | --- |
| P3 | `Lesson6.cs` | 使用 `<`，边界值正好等于半角或距离时不会触发 | 当前练习不一定要求边界包含，但题目写“范围内”时容易产生理解差异 | 如果要包含边界，可以改成 `<=` |
| P3 | `Lesson6.cs` | `target` 没有空引用保护 | 当前场景已绑定，不影响本练习；换场景可能报错 | 后续学习组件引用检查时再补 |
| P3 | `Lesson6.cs` | `Mathf.Acos` 前没有 clamp | 极端浮点误差下可能得到 `NaN`，本练习通常不会遇到 | 后续可以养成 `Mathf.Clamp(dotResult, -1, 1)` 的习惯 |

## 验证方式

已经做过的静态检查：

- 脚本挂在 `A` 上。
- `target` 已绑定到 `B`。
- 角度判断和距离判断同时存在。
- 当前没有输入系统或自动移动逻辑，A/B 摆位主要用于手动拖动调试边界。

建议在 Unity 中继续确认：

- 停止 Play Mode 后保存场景。
- 运行场景，手动拖动 B 到 A 前方范围内，观察 Console 是否打印。
- 将 B 拖出角度范围或 5 米范围，确认不再打印。
- Console 没有 `NullReferenceException`。

## 尚未验证或边界

- Codex 没有直接启动 Unity 运行场景，只检查了磁盘上的脚本和场景序列化内容。
- 当前练习没有输入系统，所以不把保存瞬间的 B 坐标是否触发作为主要判错依据。
- 后续如果 B 的位置由输入系统、自动移动或关卡初始站位决定，就需要重新检查场景物体位置。

## 下一步

进入 `13.向量叉乘.mp4`，重点理解叉乘结果方向和左右判断之间的关系。
