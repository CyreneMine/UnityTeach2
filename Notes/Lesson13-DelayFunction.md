# Lesson13 - 延迟函数练习

## 对应视频

- `24.延迟函数.mp4`
- `25.延迟函数  练习题.mp4`

## 练习目标

1. 使用延时函数实现一个计秒器。
2. 用两种方式延时销毁一个指定对象。

## 对应文件

- [Lesson13.cs](../Assets/Scripts/Lesson/Lesson13_延迟函数/Lesson13.cs)
- [Lesson13.unity](../Assets/Scripts/Lesson/Lesson13_延迟函数/Lesson13.unity)

## 当前实现

```csharp
private void Start()
{
    InvokeRepeating("DelayTime", 0, 1);
    Destroy(gameObject, 2f);
    Invoke("DelayDestroy",2f);
}

public void DelayTime()
{
    print(count++);
}

private void DelayDestroy()
{
    Destroy(gameObject);
}
```

## 理解记录

- `InvokeRepeating("DelayTime", 0, 1)` 表示从第 `0` 秒开始调用 `DelayTime`，之后每隔 `1` 秒调用一次。
- `Destroy(gameObject, 2f)` 是 Unity 提供的延时销毁重载，会在 `2` 秒后销毁当前对象。
- `Invoke("DelayDestroy", 2f)` 是先延时调用方法，再在方法里执行 `Destroy(gameObject)`。
- 当前写法把“计秒器”和“被销毁对象”放在同一个 `gameObject` 上，所以对象销毁后计秒器也会一起停止。

## 检查记录

| 优先级 | 位置 | 问题 | 影响 | 状态 |
| --- | --- | --- | --- | --- |
| P2 | `Lesson13.cs` | `Destroy(gameObject, 2f)` 和 `Invoke("DelayDestroy", 2f)` 同时销毁同一个对象 | 能展示两种 API，但验证结果会重叠 | 作为学习边界保留 |
| P2 | `Lesson13.cs` | 计秒器脚本和销毁目标是同一个对象 | 2 秒后对象销毁，计秒器也会停止 | 符合本次练习，但后续若要长期计时应拆到独立对象 |
| P3 | `Lesson13.cs` | `using System;` 未使用 | 不影响运行 | 后续整理时可删除 |

## 验证方式

- 磁盘场景中已能找到 `Lesson13` 脚本序列化记录。
- 运行时应在 Console 中看到计数打印，并在约 `2` 秒后因对象销毁而停止。
- 本次 Codex 未直接启动 Unity 运行，只检查了脚本和场景文件。

## 尚未验证或边界

- 未单独验证 `CancelInvoke`。
- 未验证禁用组件、禁用对象、切换场景时延迟调用的行为。

## 下一步

进入协同程序，比较 `Invoke` 延迟调用和协程分段执行的区别。
