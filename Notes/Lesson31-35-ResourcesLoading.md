# Lesson31-35 - 特殊文件夹与 Resources 加载

## 对应视频

- `31.特殊文件夹.mp4`
- `32.Resources资源同步加载.mp4`
- `33.Resources资源同步加载  练习题.mp4`
- `34.Resources资源异步加载.mp4`
- `35.Resources资源异步加载  练习题.mp4`

## 练习目标

1. 创建课程要求的 Unity 特殊文件夹。
2. 把之前四元数练习中的子弹发射逻辑改为通过 `Resources` 同步加载预制体后创建。
3. 写一个简单资源管理器，提供统一方法给外部做 `Resources` 异步加载，并在资源加载结束后通过回调使用资源。

## 对应文件

- [Lesson12.cs](../Assets/Scripts/Lesson/Lesson12_四元数计算/Lesson12.cs)
- [Bullet.prefab](../Assets/Scripts/Lesson/Lesson12_四元数计算/Resources/Bullet.prefab)
- [ResourcesMgr.cs](../Assets/Scripts/Lesson/Lesson18_异步加载/ResourcesMgr.cs)
- [Lesson18.cs](../Assets/Scripts/Lesson/Lesson18_异步加载/Lesson18.cs)
- [Lesson18.unity](../Assets/Scripts/Lesson/Lesson18_异步加载/Lesson18.unity)
- [BeLovedCyrene.jpg](../Assets/Scripts/Lesson/Lesson18_异步加载/Resources/BeLovedCyrene.jpg)

## 特殊文件夹

当前按课程练习创建了：

- `Assets/Editor`
- `Assets/Plugins`
- `Assets/Resources`
- `Assets/StreamingAssets`

一开始出现过 `Resource` 单数目录，后续已修正为 Unity 识别的 `Resources` 复数目录。

这道题没有要求创建所有 Unity 特殊文件夹，所以没有额外补 `Gizmos` 等目录。部分目录当前只是为了完成本题创建，后续不一定实际使用。

## Resources 同步加载

`Lesson12` 原本通过 Inspector 引用子弹预制体，当前改为在 `Start()` 中加载：

```csharp
private void Start()
{
    bullet = Resources.Load<GameObject>("Bullet");
}
```

随后仍然用 `Instantiate(bullet, ...)` 创建单发、双发、扇形和环形子弹。

关键规则：

- `Resources.Load` 的路径相对任意 `Resources` 文件夹。
- 路径不写扩展名，所以是 `"Bullet"`，不是 `"Bullet.prefab"`。
- `Resources` 文件夹可以放在 `Assets` 下的子目录里，不一定必须是 `Assets/Resources` 顶层。
- 当前 `Bullet.prefab` 位于 `Assets/Scripts/Lesson/Lesson12_四元数计算/Resources/Bullet.prefab`，因此 `Resources.Load<GameObject>("Bullet")` 可以加载。

## Resources 异步加载

`ResourcesMgr` 当前提供两种异步加载方式。

回调版：

```csharp
public void LoadRes<T>(string resName, UnityAction<T> callback) where T : Object
{
    ResourceRequest request = Resources.LoadAsync<T>(resName);
    request.completed += (a) =>
    {
        callback((a as ResourceRequest).asset as T);
    };
}
```

协程版：

```csharp
public IEnumerator LoadResToIEnumerator<T>(string resName, UnityAction<T> callback) where T : Object
{
    ResourceRequest request = Resources.LoadAsync<T>(resName);
    yield return request;
    callback(request.asset as T);
}
```

`Lesson18` 通过两种方式加载同一张图片：

```csharp
ResourcesMgr.Instance.LoadRes<Sprite>("BeLovedCyrene", (arg0 =>
{
    img.sprite = arg0;
    img.SetNativeSize();
}));

StartCoroutine(ResourcesMgr.Instance.LoadResToIEnumerator<Sprite>("BeLovedCyrene", (arg0 =>
{
    img.sprite = arg0;
    img.SetNativeSize();
})));
```

## 检查记录

| 优先级 | 位置 | 问题 | 影响 | 状态 |
| --- | --- | --- | --- | --- |
| 通过 | `Lesson12.cs` | 使用 `Resources.Load<GameObject>("Bullet")` 加载预制体 | 符合同步加载练习目标 | 已完成 |
| 通过 | `Lesson12_四元数计算/Resources/Bullet.prefab` | 预制体位于 Unity 可识别的 `Resources` 目录下 | 加载路径 `"Bullet"` 有效 | 已完成 |
| 通过 | `ResourcesMgr.cs` | 封装了 `Resources.LoadAsync<T>`，并通过回调返回资源 | 符合异步加载练习目标 | 已完成 |
| 通过 | `BeLovedCyrene.jpg.meta` | 图片导入类型为 `Sprite` | `Resources.LoadAsync<Sprite>("BeLovedCyrene")` 可以按类型加载 | 已完成 |
| 通过 | `Lesson18.unity` | `Lesson18.img` 已绑定到场景中的 `Image` 组件 | 回调赋值可以显示到 UI 上 | 已完成 |
| P3 | `Lesson18.cs` | 同时调用回调版和协程版加载同一张图 | 图片会被赋值两次；适合对比写法，不适合正式只做一次加载的流程 | 学习阶段保留 |
| P3 | `ResourcesMgr.cs` | 未判断 `callback` 或加载结果是否为空 | 路径错误或未传回调时不够稳 | 后续写正式工具类再补 |

## 验证方式

- 静态检查确认 `Bullet.prefab` 和 `BeLovedCyrene.jpg` 都在 `Resources` 文件夹下。
- 静态检查确认 `Resources.Load` 和 `Resources.LoadAsync` 路径均不带扩展名。
- 静态检查确认 `BeLovedCyrene.jpg` 导入类型为 `Sprite`。
- 静态检查确认 `Lesson18.unity` 中 `Image` 引用已经绑定。
- 本次 Codex 没有直接启动 Unity 运行场景，运行时显示效果以用户在 Unity 中验证为准。

## 尚未验证或边界

- 还未学习 `Resources.UnloadAsset`、`Resources.UnloadUnusedAssets` 等资源卸载逻辑。
- 还未验证多个 `Resources` 文件夹中出现同名相对路径资源时的冲突风险。
- 当前 `ResourcesMgr` 只是学习用简单封装，不包含缓存、引用计数、取消加载、错误处理或统一卸载。

## 下一步

继续 `36..Resources资源卸载.mp4`，再进入场景异步加载。
