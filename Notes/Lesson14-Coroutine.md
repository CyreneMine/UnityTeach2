# Lesson14 - 协同程序练习

## 对应视频

- `26.协同程序1.mp4`
- `27.协同程序2.mp4`
- `28.协同程序  练习题.mp4`

## 练习目标

1. 使用协程制作一个计秒器。
2. 在场景中创建 `100000` 个随机位置的立方体，并让生成过程不会明显卡顿。

## 对应文件

- [Lesson14.cs](../Assets/Scripts/Lesson/Lesson14_协同程序/Lesson14.cs)
- [Lesson14.unity](../Assets/Scripts/Lesson/Lesson14_协同程序/Lesson14.unity)

## 当前实现

```csharp
private void Start()
{
    Coroutine timerSec = StartCoroutine(Timer());
    Coroutine randomCube = StartCoroutine(RandomCube(100000));
}
```

计秒器：

```csharp
IEnumerator Timer()
{
    while (true)
    {
        yield return new WaitForSeconds(1f);
        print(++sec);
    }
}
```

分帧生成方块：

```csharp
IEnumerator RandomCube(int num)
{
    for (int i = 0; i < num; i++)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position += new Vector3(
            Random.Range(-100, 101),
            Random.Range(-100, 101),
            Random.Range(-100, 101));

        if (i % 1000 == 0)
            yield return null;
    }
}
```

## 理解记录

- 协程不是新线程，它仍然运行在 Unity 主线程里，只是可以通过 `yield return` 把一段逻辑拆到不同帧或不同时间点继续执行。
- `yield return new WaitForSeconds(1f)` 会等待约 1 秒后再继续执行后面的语句。
- `yield return null` 表示把后续逻辑让到下一帧继续执行。
- 生成 `100000` 个方块如果全挤在同一帧里做，容易造成明显卡顿；按批次 `yield return null` 可以把压力分摊到多帧。

## 检查记录

| 优先级 | 位置 | 问题 | 影响 | 状态 |
| --- | --- | --- | --- | --- |
| P3 | `Lesson14.cs` | `i == 0` 时也会触发一次 `yield return null` | 第一帧只生成 1 个后就让出一帧，影响很小 | 保留 |
| P3 | `Lesson14.cs` | `timerSec` 和 `randomCube` 变量目前没有用于停止协程 | 不影响启动协程；如果后续要 `StopCoroutine` 才需要保留引用 | 作为后续停止协程学习点 |
| P3 | `Lesson14.cs` | `using System;` 未使用 | 不影响运行 | 后续整理时可删除 |

## 验证方式

- 磁盘场景中已能找到 `Lesson14` 对象和 `Lesson14` 脚本序列化记录。
- 脚本明确启动了计时协程和随机方块生成协程。
- 本次 Codex 未直接启动 Unity 运行，只检查了脚本和场景文件。

## 尚未验证或边界

- 未验证 `Time.timeScale` 改变后 `WaitForSeconds` 的表现。
- 未验证停止协程、禁用对象和销毁对象时协程的生命周期。
- 未验证大量方块生成后的内存占用和场景操作流畅度。

## 下一步

进入协程原理，理解 `IEnumerator`、`MoveNext()` 和 `Current` 如何支撑协程分段执行。
