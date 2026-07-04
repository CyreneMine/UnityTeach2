# Lesson05 - 三角函数曲线移动练习复盘

日期：2026-07-04

状态：已完成

## 对应内容

- 视频文件：`5.三角函数  练习题.mp4`
- 练习题：实现一个物体按曲线移动，可以使用正弦或余弦曲线
- 对应代码：`Assets/Scripts/Lesson/Lesson2_三角函数/Lesson2.cs`
- 对应场景：`Assets/Scripts/Lesson/Lesson2_三角函数/Lesson2.unity`
- 场景对象：`Cube`

## 当前实现

当前实现使用教程风格的写法：

```csharp
transform.Translate(Vector3.forward * (Time.deltaTime * speed));
timer += Time.deltaTime * changeSpeed;
transform.Translate(Vector3.right * (Time.deltaTime * changeSize * Mathf.Sin(timer)));
```

含义：

- `speed` 控制物体向前移动的速度；
- `timer` 作为正弦函数输入，随时间推进；
- `changeSpeed` 控制正弦变化快慢；
- `changeSize` 控制横向摆动强度；
- `Mathf.Sin(timer)` 在 `-1` 到 `1` 之间周期变化，让物体左右方向的移动速度不断变化；
- 前进方向和左右方向叠加后，轨迹表现为正弦/余弦风格的曲线移动。

## 本次理解

`Mathf.Sin` 接收的是弧度，不是角度。

上一版自己写法里使用了 `Mathf.Deg2Rad`，是把角度转换成弧度后再传给 `Mathf.Sin`；教程写法则是直接让 `timer` 按时间增长，把它作为弧度值使用。

这两种思路都能成立，但含义不同：

- 角度写法更适合练习“角度和弧度转换”；
- 教程写法更适合表达“随时间推进的周期函数”。

当前版本使用 `Translate` 叠加位移，所以更准确地说，是“用正弦值控制横向速度变化”。因为横向速度周期变化，最终路径会形成曲线。

## 检查记录

- 场景已保存，`Lesson2.unity` 中存在 `Cube`。
- `Cube` 已挂载 `Lesson2` 脚本。
- Inspector 中已序列化 `speed`、`changeSpeed` 和 `changeSize`。
- `changeSpeed` 已使用 `Time.deltaTime`，不再按帧率直接累加。
- 旧的自己写法保留在注释中，作为对比记录。

## 注意点

- 当前 `timer` 名称比 `nowSinDeg` 更准确，因为现在传给 `Mathf.Sin` 的不是角度。
- Inspector 中的 `changeSpeed` 和 `changeSize` 会覆盖脚本里的默认值。
- 如果以后需要更严格、可控的曲线位置，可以记录初始位置，再用 `startX + Mathf.Sin(timer) * amplitude` 直接计算横向坐标。

## 下一步

继续 `6.坐标系.mp4`，进入坐标系学习。
