# 综合实践：开始场景 UI 基础逻辑

更新时间：2026-07-11

## 当前进度

- 状态：学习中
- 阶段：46-68p 综合实践项目
- 当前完成：开始面板、设置面板、排行榜面板的基础逻辑
- 待后续完成：游戏场景切换、游戏结果写入排行榜、完整音乐与音效联动

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

- 开始按钮当前只有基础面板行为，正式场景切换待游戏场景建立后接入。
- 设置数据已经可以读写，但音效系统和完整场景生命周期仍待后续联动验证。
- 游戏结果尚未接入排行榜数据。
- 新实例化的 `RankItem` 当前尚未加入 `rankItems` 列表，多次打开排行榜可能重复创建条目；后续需要补充条目复用或清理逻辑并验证。

## 下一步

- 继续跟随综合实践课程制作后续场景。
- 将开始场景中的基础 UI 与游戏流程连接。
- 在游戏结束时保存排行榜数据，并复核排行榜重复打开和数据刷新行为。
