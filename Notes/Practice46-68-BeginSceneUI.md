# 综合实践：开始场景 UI 基础逻辑

更新时间：2026-07-11

## 当前进度

- 状态：学习中
- 阶段：46-68p 综合实践项目
- 当前完成：开始、设置、排行榜、选角面板的主流程，以及进入 `GameScene`
- 待后续完成：选角边界修正、初始角色 JSON、gameplay、游戏结果写入排行榜、完整音乐与音效联动

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

## 当前边界

- 设置数据已经可以读写，但音效系统和完整场景生命周期仍待后续联动验证。
- 游戏结果尚未接入排行榜数据。
- 右箭头当前使用 `nowSelHeroIndex >= roleData.list.Count - 1` 判断回绕；索引到达最后一个元素时会直接归零，因此最后一架飞机无法通过右箭头选中。正确边界应在索引达到 `Count` 时才回绕，留待下一步由用户修正。
- 当前仓库中没有 `role.json`。`JsonMgr` 会先读取 `StreamingAssets/role.json`，不存在时再读取本机 `persistentDataPath/role.json`；因此当前运行依赖本机已有数据，换电脑或清除持久化数据后 `roleData.list` 为空，选角时会越界。
- `RoleInfo.resName` 当前尚未参与资源加载，飞机资源仍按照角色索引拼接 `Airplane1` 到 `Airplane5`；实践后续可对照教程决定是否改为数据驱动路径。

## 下一步

- 修正选角右箭头的回绕条件。
- 补充初始 `role.json`，验证新环境首次运行也能正常选角。
- 继续制作 `GameScene` gameplay。
- 在游戏结束时保存排行榜数据，并复核排行榜刷新行为。
