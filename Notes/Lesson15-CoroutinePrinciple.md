# Lesson15 - 协同程序原理练习

## 对应视频

- `29.协同程序原理.mp4`
- `30.协同程序原理  练习题.mp4`

## 练习目标

不使用 Unity 自带协程调度器，通过迭代器函数实现“每隔一秒执行函数中的一部分逻辑”。

## 对应文件

- [Lesson15.cs](../Assets/Scripts/Lesson/Lesson15_协同程序原理/Lesson15.cs)
- [CoroutineMgr.cs](../Assets/Scripts/Lesson/Lesson15_协同程序原理/CoroutineMgr.cs)
- [Lesson15answer.cs](../Assets/Scripts/Lesson/Lesson15_协同程序原理/Lesson15answer.cs)
- [Lesson15.unity](../Assets/Scripts/Lesson/Lesson15_协同程序原理/Lesson15.unity)

## 第一版实现

`Lesson15.cs` 保存 `IEnumerator`，在 `Update` 中手动计时并调用 `MoveNext()`。

```csharp
private void Start()
{
    timer = Timer();
}

private void Update()
{
    sec += Time.deltaTime;
    if (sec >= 1)
    {
        isGet = true;
        sec = 0;
    }
    timer.MoveNext();
}
```

这版重点是理解：迭代器函数不会一次性执行到底，而是每次 `MoveNext()` 推进到下一个 `yield`。

## 教程思路版本

后续新增 `CoroutineMgr`，用列表保存每个自定义协程和它下一次允许执行的时间。

```csharp
if (coroutines[i].time > Time.time)
    continue;
```

这句和教程把后续逻辑包在：

```csharp
if (coroutines[i].time <= Time.time)
{
    ...
}
```

里面是等价思路：没到时间就跳过，到时间才推进 `MoveNext()`。

## 理解记录

- `IEnumerator.MoveNext()` 返回 `true` 表示协程还没结束，返回 `false` 表示已经执行完。
- `IEnumerator.Current` 可以拿到本次 `yield return` 返回的内容。
- 当前练习只处理 `yield return int`，把 `int` 当作“等待几秒”。
- 反向遍历 `coroutines` 列表可以避免中途 `RemoveAt(i)` 后漏掉后面的元素。
- `CoroutineMgr.Instance.MyStartCoroutine(TimerEvent())` 是本题自定义的启动方式，不再调用 Unity 的 `StartCoroutine`。

## 检查记录

| 优先级 | 位置 | 问题 | 影响 | 状态 |
| --- | --- | --- | --- | --- |
| P2 | `CoroutineMgr.cs` | 当前只支持 `yield return int` | 符合本题“只做 int 返回值类型”的学习范围，但不是完整 Unity 协程系统 | 已记录边界 |
| P3 | `CoroutineMgr.cs` | `private CoroutineMgr(){}` 只能防止普通 `new CoroutineMgr()`，不能防止场景里挂多个组件 | 当前场景只有一个管理器，不影响本练习 | 作为 Unity 单例边界记录 |
| P3 | `Lesson15answer.cs` | `Update()` 为空 | 不影响运行 | 后续整理时可删除 |
| P3 | `CoroutineMgr.cs` | `using System;` 未使用 | 不影响运行 | 后续整理时可删除 |

## 验证方式

- 磁盘场景中 `Lesson15answer` 对象已挂载 `CoroutineMgr` 和 `Lesson15answer`。
- 原第一版 `Lesson15` 对象在场景中保留但已禁用，便于对照学习。
- 自定义管理器会在 `Start` 时先推进一次协程，随后按 `Time.time + int` 设置下一次执行时间。
- 本次 Codex 未直接启动 Unity 运行，只检查了脚本和场景文件。

## 尚未验证或边界

- 未支持 `WaitForSeconds`、`yield return null`、嵌套 `IEnumerator`、停止协程、异常处理和对象生命周期管理。
- 未验证多个自定义协程同时运行时的输出顺序。
- 该练习目标是理解协程调度原理，不是复刻完整 Unity 协程系统。

## 下一步

阶段 04 收尾。下一阶段进入特殊文件夹、`Resources`、资源卸载和场景异步加载。
