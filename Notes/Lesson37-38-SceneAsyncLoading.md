# Lesson37-38 - 场景异步加载练习

## 对应视频

- `37.场景异步加载.mp4`
- `38.场景异步加载  练习题.mp4`

## 练习目标

写一个简单的场景管理器，提供统一方法给外部用于场景异步切换；外部可以传入委托，在异步切换结束时执行某些逻辑。

## 对应文件

- [SceneMgr.cs](../Assets/Scripts/Lesson/Lesson20_场景异步加载/SceneMgr.cs)
- [Lesson20.cs](../Assets/Scripts/Lesson/Lesson20_场景异步加载/Lesson20.cs)
- [Lesson20_1.unity](../Assets/Scripts/Lesson/Lesson20_场景异步加载/Lesson20_1.unity)
- [Lesson20_2.unity](../Assets/Scripts/Lesson/Lesson20_场景异步加载/Lesson20_2.unity)
- [EditorBuildSettings.asset](../ProjectSettings/EditorBuildSettings.asset)

## 当前实现

`SceneMgr` 使用普通 C# 单例封装场景异步加载：

```csharp
public void LoadScene(string resName, UnityAction callback)
{
    AsyncOperation ao = SceneManager.LoadSceneAsync(resName);
    ao.completed += (a) =>
    {
        callback();
    };
}
```

`Lesson20` 中调用：

```csharp
SceneMgr.Instance.LoadScene("Lesson20_2", (() =>
{
    print("加载完成，开始跳转");
}));
```

`ProjectSettings/EditorBuildSettings.asset` 中已包含：

- `Assets/Scripts/Lesson/Lesson20_场景异步加载/Lesson20_1.unity`
- `Assets/Scripts/Lesson/Lesson20_场景异步加载/Lesson20_2.unity`

## 理解记录

- `SceneManager.LoadSceneAsync(sceneName)` 会返回 `AsyncOperation`。
- 可以监听 `AsyncOperation.completed`，在异步操作完成后执行回调。
- 场景异步加载需要目标场景进入 Build Settings，否则按场景名加载时可能失败。
- 当前封装把“怎么加载场景”和“加载完成后做什么”拆开：管理器负责加载，外部通过委托补充完成后的逻辑。

## 检查记录

| 优先级 | 位置 | 问题 | 影响 | 状态 |
| --- | --- | --- | --- | --- |
| 通过 | `SceneMgr.cs` | 使用 `SceneManager.LoadSceneAsync` 并监听 `completed` | 符合异步切换和完成回调目标 | 已完成 |
| 通过 | `Lesson20.cs` | 从 `Lesson20_1` 调用 `SceneMgr.Instance.LoadScene("Lesson20_2", callback)` | 可以触发切换到第二个场景 | 已完成 |
| 通过 | `EditorBuildSettings.asset` | `Lesson20_1.unity` 和 `Lesson20_2.unity` 已加入 Build Settings | 满足场景加载配置要求 | 已完成 |
| 已修正 | Git 提交 | 之前检查时 `SceneMgr.cs` 暂存区还是早期空类版本 | 如果只提交暂存区，会漏掉真正实现 | 后续提交 `c7bfe1f` 已包含最终版 `SceneMgr.cs` |
| P3 | `SceneMgr.cs` | 参数名 `resName` 更像资源名，不像场景名 | 不影响运行，但读代码时容易和 `Resources` 加载混淆 | 后续可改成 `sceneName` |
| P3 | `SceneMgr.cs` | 没有判断 `callback` 是否为空 | 外部传 `null` 时会报错 | 学习阶段可接受，正式工具类再补 |
| P3 | `SceneMgr.cs` | 没有暴露加载进度、`allowSceneActivation` 或加载模式 | 只能做最基础的异步切换完成回调 | 符合当前练习范围 |
| P3 | `Lesson20.cs` | `using System;` 未使用 | 不影响运行 | 后续整理可删除 |

## 验证方式

- 静态检查 `SceneMgr.cs`：确认调用 `SceneManager.LoadSceneAsync`，并在 `completed` 中执行外部回调。
- 静态检查 `Lesson20.cs`：确认调用 `SceneMgr.Instance.LoadScene("Lesson20_2", callback)`。
- 静态检查 `Lesson20_1.unity`：确认场景中存在 `Lesson20` 对象并挂载 `Lesson20` 脚本。
- 静态检查 `EditorBuildSettings.asset`：确认两个练习场景都已启用并写入 Build Settings。
- 本次 Codex 没有直接启动 Unity 运行场景，运行时切换效果以用户在 Unity 中验证为准。

## 尚未验证或边界

- `36..Resources资源卸载.mp4` 本次没有看到单独代码验证文件，先保留为资源卸载边界待复盘。
- 当前未实现加载进度条、延迟激活场景、叠加加载、多场景管理或切换失败处理。
- 当前回调逻辑只是打印提示；如果后续回调访问旧场景对象，要注意旧场景在切换完成后可能已经被卸载。

## 下一步

进入 `39.LineRenderer.mp4`。
