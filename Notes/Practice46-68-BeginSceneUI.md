# 综合实践：开始场景 UI 基础逻辑

更新时间：2026-07-12

## 当前进度

- 状态：已完成
- 阶段：46-68p 综合实践项目
- 当前完成：开始、设置、排行榜、选角面板主流程，以及进入 `GameScene`
- 后续衔接：gameplay、排行榜写入和音效联动已完成，详见 `Notes/Practice46-68-Gameplay.md`

## 技术路线调整

原教程的小实践使用：

- `NGUI` 构建界面；
- `XML` 保存和读取设置、排行榜等数据。

本项目实际使用：

- Unity 内置 `UGUI` 构建 Canvas、面板、按钮、Slider、Toggle、ScrollRect 和动态列表；
- `JSON` 保存和读取音乐设置、排行榜以及后续角色选择等数据。

调整原因是 `NGUI` 已不是当前 Unity 项目的主流 UI 方案，而本项目也已经建立了 `JsonMgr` 和 JSON 数据流程。继续使用 `UGUI + JSON` 更符合当前工程环境，也能把基础阶段学到的 UI、Resources 和数据管理知识串进综合实践。

这次调整保留教程的业务目标和学习顺序，但实现细节以本仓库代码为准。XML 本身仍适用于配置、交换格式等场景；这里不使用 XML，是针对本次 Unity 学习项目做出的技术选择。

## 对应文件

- `Assets/Scenes/BeginScene.unity`
- `Assets/Scripts/UI/BasePanel.cs`
- `Assets/Scripts/UI/BeginScene/BeginPanel.cs`
- `Assets/Scripts/UI/BeginScene/SettingPanel.cs`
- `Assets/Scripts/UI/BeginScene/RankPanel.cs`
- `Assets/Scripts/UI/BeginScene/RankItem.cs`
- `Assets/Scripts/Data/GameDataMgr.cs`
- `Assets/Scripts/Data/MusicData.cs`
- `Assets/Scripts/Data/RankData.cs`
- `Assets/Resources/UI/RankItem.prefab`

## 已实现内容

- 使用泛型 `BasePanel<T>` 统一面板实例、初始化、显示和隐藏入口。
- 开始面板连接开始、排行榜、设置和退出按钮。
- 设置面板读取并保存音乐、音效的开关和音量数据。
- 排行榜面板读取排行榜数据，通过 `Resources.Load` 和 `Instantiate` 动态创建 `RankItem`。
- `ScrollRect`、Viewport、Content、垂直布局和内容自适应已经搭建。
- 新建排行榜条目时直接指定 Content 父节点并传入 `false`，随后加入 `rankItems` 列表，用于面板再次打开时复用和刷新。
- 选角面板从角色数据读取生命、速度、容量和显示缩放，通过 10 个 Toggle 展示三组属性。
- 左右按钮可以切换飞机，关闭按钮返回开始面板，鼠标拖动展示区域可以旋转飞机。
- 五架飞机 Prefab 位于 `Assets/Resources/Airplane/`，开始按钮使用 `SceneManager.LoadScene("GameScene")` 进入游戏场景。
- `BeginScene` 和 `GameScene` 均已加入 Build Settings。

## 排行榜条目异常缩放复盘

### 现象

- 排行榜数据文字在 Game 窗口中显示得非常大。
- TextMesh Pro 中设置的字号仍然是 `36`，与预期设置一致。
- 排行榜 Content 和滚动条能够滚动，但条目的显示位置和尺寸异常。

### 一开始容易误判的方向

检查场景 YAML 时，Viewport 的序列化初始值看起来是零尺寸，但 Inspector 显示这些值由 `ScrollRect` 驱动。运行时 Viewport 实际被设置为拉伸状态，因此 Viewport 不是这次问题的根因。

### 根因

原写法：

```csharp
obj.transform.SetParent(svList.content);
```

`Transform.SetParent(Transform parent)` 默认等价于保留世界变换。新对象先在 Content/Canvas 层级之外实例化，再设置为 Content 的子对象时，Unity 会为了保持原来的世界位置、旋转和缩放，反向计算新的局部变换。

因此即使 TMP 字号仍是 `36`，父子层级中的异常 `localScale` 仍会把最终画面中的文字放大；异常位置和 Z 值也可能让条目超出预期显示区域。

### 修复

```csharp
obj.transform.SetParent(svList.content, false);
```

传入 `false` 表示不保留原世界变换，让实例采用 Content 下的局部坐标与缩放体系。对于要放进 UI Layout Group 的动态 UI Prefab，这是更符合当前需求的写法。

也可以在实例化时直接指定父对象：

```csharp
GameObject obj = Instantiate(prefab, svList.content, false);
```

### 验证方法

1. 退出 Play Mode并保存场景与 Prefab。
2. 运行后打开排行榜。
3. 在 Hierarchy 中选中动态生成的 `RankItem(Clone)`。
4. 确认 `Local Scale` 为 `(1,1,1)`，局部 Z 坐标正常。
5. 确认字号 `36` 的最终视觉尺寸与 RankItem Prefab 一致。
6. 确认条目能在 Viewport 中显示，并随 Content 正常滚动。

## 阶段收尾

- 设置数据已经接入背景音乐和子弹死亡音效；游戏结束结果也已写入排行榜。
- 右箭头最初使用 `nowSelHeroIndex >= roleData.list.Count - 1` 判断回绕，导致最后一架飞机无法选中；现已修正为索引达到 `roleData.list.Count` 时才归零。
- 角色配置已迁移到 `Assets/StreamingAssets/role.json`，克隆仓库或构建 Windows 程序时可以随项目读取；音乐设置和排行榜仍保留在可写的 `persistentDataPath`。
- `RoleInfo.resName` 已由 `Main` 用于动态加载所选飞机资源。

## 下一步

- 本笔记负责的开始场景阶段已经结束；后续维护以 gameplay 综合复盘和仓库当前状态为准。
