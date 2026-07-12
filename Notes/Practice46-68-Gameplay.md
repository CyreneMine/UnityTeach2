# 综合实践：Gameplay 与项目收尾

更新时间：2026-07-12

## 完成状态

- 状态：已完成
- 课程范围：46-68p 综合实践
- 实际技术路线：UGUI + JSON
- 对应场景：`Assets/Scenes/BeginScene.unity`、`Assets/Scenes/GameScene.unity`

## 最终流程

1. 在开始场景调整音乐和音效、查看排行榜并选择飞机。
2. `Main` 根据 `RoleInfo.resName` 加载飞机 Prefab，动态添加 `PlayerObject`，并应用速度和生命属性。
3. 八个 `FireObject` 根据 `FireData` 随机切换发射模式，再从 `BulletData` 的 ID 区间选择子弹配置。
4. 玩家在屏幕范围内移动和倾斜，躲避不同轨迹的子弹，也可以用鼠标射线消除 `Monster` 层子弹。
5. 子弹碰撞玩家后扣除生命并生成死亡特效；死亡特效对象负责播放音效。
6. 生命归零后暂停游戏并显示结束面板，输入姓名后保存存活时间并返回开始场景。

## 数据划分

| 数据 | 文件 | 位置 | 原因 |
| --- | --- | --- | --- |
| 角色配置 | `role.json` | `StreamingAssets` | 随构建发布，只读加载 |
| 子弹配置 | `bullet.json` | `StreamingAssets` | 随构建发布，只读加载 |
| 开火配置 | `fire.json` | `StreamingAssets` | 随构建发布，只读加载 |
| 音乐/音效设置 | `music.json` | `persistentDataPath` | 玩家运行时可修改 |
| 排行榜 | `rank.json` | `persistentDataPath` | 游戏结束时持续写入 |

`JsonMgr.LoadData` 当前优先读取 `StreamingAssets`，找不到时再读取 `persistentDataPath`。因此只读配置不能在两个位置同时保留不同版本，否则随包配置会优先于本地文件。

## 子弹和开火系统

- `FireInfo.type == 1`：按 CD 连续生成并朝玩家当前位置发射。
- `FireInfo.type == 2`：根据 `cd` 区分同一帧扇形齐射和按间隔逐颗展开。
- 扇形方向由 `Quaternion.AngleAxis(angle, Vector3.up) * initDir` 计算，再通过 `Quaternion.LookRotation` 转为子弹旋转。
- `BulletInfo.type` 覆盖直线、正弦横移、左右转向和追踪等轨迹。
- 子弹移动使用本地 `Vector3.forward` 和 `Vector3.right`，与 `Transform.Translate` 默认的 `Space.Self` 保持一致。

## 关键排错记录

| 现象 | 根因 | 结论 |
| --- | --- | --- |
| `FireData` 列表为空 | JSON 使用 `fireInfoList`，C# 曾写成 `fireinfoList` | LitJson 成员名需要与 JSON 大小写一致 |
| 子弹数量结束后仍可能发射 | 只检查 CD，没有把 `nowNum` 纳入停止条件 | 发射状态必须同时考虑时间和剩余数量 |
| 扇形子弹重叠 | 旋转了发射器 `transform`，没有旋转新生成的 `bullet.transform` | 方向必须赋给实际移动对象 |
| 部分子弹越来越大 | XZ 游戏平面错误地绕 `Vector3.forward` 旋转，子弹向透视摄像机靠近 | XZ 平面扇形应绕 `Vector3.up` 展开 |
| 子弹位移方向怪异 | `transform.forward/right` 已是世界方向，却交给默认 `Space.Self` 的 `Translate` 再转换一次 | 向量来源必须和 API 的坐标空间匹配 |
| 一颗子弹可能扣多次血 | 玩家曾同时存在多个重叠碰撞体，单颗子弹会收到多组 Trigger 回调 | 当前飞机 Prefab 只保留所需碰撞体并设为 Trigger |
| 替换玩家后旧引用报错 | 场景旧玩家被销毁时，其他对象仍读取旧 `PlayerObject.Instance` | 最终改为由 `Main` 统一创建当前玩家 |
| `AudioSource.Play()` 没声音 | 播放后立即销毁了音源所在的子弹对象 | 音效改由独立死亡特效对象播放 |

## 验证记录

- 五架飞机 Prefab 当前不自带 `PlayerObject`；`Main` 动态添加组件并设置 `Player` Tag。
- 子弹和开火配置可以从 `StreamingAssets` 读取，配置列表字段名已经统一。
- 子弹 Prefab 已配置 Trigger、`Monster` 层和所需显示资源。
- 开始场景和游戏场景当前各保留一个启用的 `AudioListener`。
- 最近一次 Editor 日志检查未发现新的 C# 编译异常或运行时异常。
- 用户已在 Unity Editor 中完成最终运行调试，并确认课程项目结束。

## 已知边界

- 当前是课程综合实践，不包含对象池；子弹和特效仍通过 `Instantiate/Destroy` 管理。
- `Resources.Load`、普通 C# 单例和现有输入方式沿用基础课程方案，没有进一步引入 Addressables、依赖注入或新输入系统。
- `LifeDead` 到期时直接清理子弹；碰撞或鼠标消除时才生成死亡特效并播放声音。
- GitHub Release 尚未创建，保留为用户后续可选的展示步骤。
