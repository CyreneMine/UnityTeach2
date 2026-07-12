# UnityTeach2 当前状态

更新时间：2026-07-12

## 项目快照

- Unity 版本：`6000.3.10f1`
- 当前 Unity 工程目录：`G:\Unity\UnityProject\UnityTeach2`
- 课程视频目录：`F:\1aUnity教程\6.［唐老狮]【Unity四部曲_基础】`
- 远端仓库：`https://github.com/CyreneMine/UnityTeach2.git`

## 已完成

- 确认综合实践技术路线：原教程使用 `NGUI + XML`，本项目改用 `UGUI + JSON`，保留教程的功能目标，但不照搬旧 UI 框架和 XML 数据实现。
- 46-68p 综合实践已经完成，基础课程与小实践主线全部收尾。
- 新增 `BeginScene.unity`、通用 `BasePanel<T>` 面板基类、开始场景 UI 脚本、设置与排行榜数据结构，以及对应 UI Prefab 和字体资源。
- 设置面板已能读取和保存音乐/音效开关与音量数据；排行榜面板已能读取数据并动态生成排行榜条目。
- 修复排行榜条目实例化后的异常缩放：将 `SetParent(svList.content)` 改为 `SetParent(svList.content, false)`，避免保留世界变换造成局部缩放和位置异常。
- 排行榜新条目已加入复用列表，重复打开时可以更新已有条目，不再重复实例化同一批内容。
- 新增选角面板、角色数据结构和五架飞机 Resources Prefab，实现左右切换、属性显示、鼠标拖动旋转、关闭返回和进入 `GameScene`。
- `BeginScene` 与 `GameScene` 已加入 Build Settings；开始、设置、排行榜、选角、游戏、退出确认、游戏结束和返回开始场景的流程已经串联。
- `GameScene` 已实现角色数据生成玩家、移动和屏幕边界、生命与计时、八方向开火、随机发射模式、五类子弹轨迹、鼠标消弹、碰撞受伤、死亡特效和音效。
- 游戏结束会把姓名和存活时间写入排行榜；音乐和音效设置已接入实际播放逻辑。
- `role.json`、`bullet.json`、`fire.json` 已迁移到 `Assets/StreamingAssets/`；`music.json`、`rank.json` 继续保存在 `persistentDataPath`。
- 五架飞机 Prefab 当前不自带 `PlayerObject`，由 `Main` 根据选角结果实例化后动态添加组件、设置 `Player` Tag 并应用角色属性。
- 选角右箭头边界已修正为索引达到 `roleData.list.Count` 时回绕，最后一架飞机可以正常选中。
- 已归档 JSON 字段大小写、UI 父节点缩放、Quaternion 方向转换、旋转轴、Translate 坐标空间、重复碰撞回调和音源生命周期等实践排错过程。

- 确认 Unity 工程存在。
- 确认当前项目未包含已有 `AGENTS.md`、`README.md` 或 `LearningProgress.md`。
- 确认课程目录存在，并识别到 67 个 `.mp4` 视频。
- 建立基础仓库文档和协作规则。
- 完成 `3.Mathf  练习题.mp4` 的 Lerp 跟随练习。
- 归档 `Mathf.Lerp` 的理解过程和两种写法区别。
- 在 `AGENTS.md` 中补充 push 前必须先检查学习文档是否跟上进度的规则。
- 完成 `5.三角函数  练习题.mp4` 的曲线移动练习。
- 将课程脚本目录整理到 `Assets/Scripts/Lesson/`。
- 在 `AGENTS.md` 中补充检查问题默认用表格展示的规则。
- 完成 `8.向量模长和单位向量  练习题.mp4` 的理论计算练习。
- 归档两点距离、向量模长、单位向量的手算过程。
- 完成 `10.向量加减乘除  练习题.mp4` 的摄像机跟随练习。
- 归档目标局部前方、局部上方与摄像机偏移公式的理解。
- 完成 `12.向量点乘  练习题.mp4` 的入侵者角度和距离判断练习。
- 完成 `14.向量叉乘  练习题.mp4` 的左右方位和非对称扇形判断练习。
- 归档当前手动拖动物体调试时，不把保存瞬间摆位作为主要判错依据；后续进入输入系统或正式运行逻辑后再恢复检查场景初始位置。
- 完成 `16.向量插值运算  练习题.mp4` 的摄像机插值跟随和太阳球形插值升降练习。
- 阶段 02 - 坐标系、向量、点乘、叉乘、插值 已收尾。
- 完成 `20.四元数的常用方法  练习题.mp4` 的 `LookRotation`、扩展方法和 `Slerp` 练习。
- 记录 `Lesson11` 按教程写法封装 `Tools.MyLookAt` 扩展方法，并通过 `cubeA.MyLookAt(target)` 实现 `LookAt` 效果。
- 记录 `Lesson5` 被复用于四元数常用方法第二题：摄像机用 `Vector3.Lerp` 平滑移动到目标后方上方位置，并用 `Quaternion.Slerp` 缓慢转向目标。
- 记录用户手动更新 Rider 包依赖到 `com.unity.ide.rider` `3.0.40`。
- 完成 `22.四元数计算  练习题1.mp4` 的自写版检查点。
- 记录 `Lesson12` 先直接写核心逻辑，实现单发、双发、扇形和环形发射；后续再根据教程改造成开火方法和发射模式枚举。
- 记录 `CameraFollow` 实现摄像机斜后方跟随、滚轮限制距离、看向目标头顶附近，并使用 `Vector3.Lerp` 和 `Quaternion.Slerp` 过渡。
- 完成四元数阶段收尾复盘：明确方向向量、偏移量、世界坐标点和 `LookRotation` 参数之间的区别。
- 归档摄像机跟随公式：目标观察点 `target.position + target.up * cameraHeight`，摄像机方向 `Quaternion.AngleAxis(tiltDeg, target.right) * -target.forward`，摄像机目标点 `观察点 + 方向 * 距离`，看回目标使用 `Quaternion.LookRotation(-方向)`。
- 阶段 03 - 四元数 已收尾。
- 完成 `24.延迟函数.mp4` 和 `25.延迟函数  练习题.mp4`，新增 `Lesson13` 延迟函数练习。
- 完成 `26.协同程序1.mp4`、`27.协同程序2.mp4` 和 `28.协同程序  练习题.mp4`，新增 `Lesson14` 计秒器和分帧生成 `100000` 个随机方块练习。
- 完成 `29.协同程序原理.mp4` 和 `30.协同程序原理  练习题.mp4`，新增 `Lesson15` 手动迭代器版和 `CoroutineMgr` 自定义调度器版。
- 阶段 04 - 延迟函数、协同程序、协程原理 已收尾。
- 完成 `31.特殊文件夹.mp4`，当前已创建 `Assets/Editor`、`Assets/Plugins`、`Assets/Resources` 和 `Assets/StreamingAssets`。
- 完成 `32.Resources资源同步加载.mp4` 和 `33.Resources资源同步加载  练习题.mp4`，`Lesson12` 的子弹预制体已移入 `Resources` 文件夹，并改为 `Resources.Load<GameObject>("Bullet")` 加载后实例化。
- 完成 `34.Resources资源异步加载.mp4` 和 `35.Resources资源异步加载  练习题.mp4`，新增 `Lesson18_异步加载` 场景、测试脚本、`ResourcesMgr` 和测试图片资源。
- 完成 `37.场景异步加载.mp4` 和 `38.场景异步加载  练习题.mp4`，新增 `Lesson20_场景异步加载` 场景、测试脚本和 `SceneMgr`。
- `SceneMgr` 当前封装 `SceneManager.LoadSceneAsync`，并在 `AsyncOperation.completed` 中调用外部传入的回调。
- `ProjectSettings/EditorBuildSettings.asset` 已加入 `Lesson20_1.unity` 和 `Lesson20_2.unity`，用于验证场景异步加载。
- 阶段 05 - 特殊文件夹、`Resources`、场景异步加载 已完成；资源卸载的深入边界保留为后续扩展复盘项。
- 完成 `39.LineRenderer.mp4` 和 `40.LineRenderer  练习题.mp4`，新增 `Lesson21_LineRender` 场景和脚本。
- `Lesson21` 当前包含 `DrawSphere(center, radius)` 画圆方法，以及按住鼠标时用 `LineRenderer` 绘制鼠标移动轨迹的逻辑。
- 已归档 `ScreenToWorldPoint` 理解补充：鼠标屏幕坐标转世界坐标前需要给 `Input.mousePosition.z` 设置“离摄像机多远”的深度。
- 完成 `41.范围检测.mp4` 和 `42.范围检测  练习题.mp4`，新增 `Lesson22_范围检测` 场景和脚本。
- `Lesson22` 当前使用 `WASD` 控制物体移动和旋转，并通过 `J/K/L` 分别练习盒形、胶囊和球形范围检测。
- 完成 `43.射线检测.mp4` 和 `44.射线检测  练习题.mp4`，新增 `Lesson23_射线检测` 场景和脚本。
- `Lesson23` 当前用鼠标射线实现点击墙面生成命中特效和弹孔，以及选中物体后在平面上拖动。
- `ProjectSettings/TagManager.asset` 已新增 `Monster` 和 `Player` 层，用于范围检测和射线检测过滤。
- 新增命中特效资源：`Assets/Resources/Effect/HitEff.prefab`、`Assets/Resources/Effect/DDD.prefab`，以及 `Assets/ArtRes/BulletEff/` 下的特效资源。
- 阶段 06 - `LineRenderer`、范围检测、射线检测 已收尾。
- 基础知识主线 1-44p 已结束；`45` 是知识点总结，不作为新的主线练习节点。
- 基础知识主线结束后已进入并完成 46-68p 小项目实践。

## 当前项目内容

- [Assets/Scenes/BeginScene.unity](../Assets/Scenes/BeginScene.unity)
- [Assets/Scripts/UI/BasePanel.cs](../Assets/Scripts/UI/BasePanel.cs)
- [Assets/Scripts/UI/BeginScene/BeginPanel.cs](../Assets/Scripts/UI/BeginScene/BeginPanel.cs)
- [Assets/Scripts/UI/BeginScene/SettingPanel.cs](../Assets/Scripts/UI/BeginScene/SettingPanel.cs)
- [Assets/Scripts/UI/BeginScene/RankPanel.cs](../Assets/Scripts/UI/BeginScene/RankPanel.cs)
- [Assets/Scripts/UI/BeginScene/RankItem.cs](../Assets/Scripts/UI/BeginScene/RankItem.cs)
- [Assets/Scripts/UI/BeginScene/ChoosePanel.cs](../Assets/Scripts/UI/BeginScene/ChoosePanel.cs)
- [Assets/Scripts/Data/GameDataMgr.cs](../Assets/Scripts/Data/GameDataMgr.cs)
- [Assets/Scripts/Data/RoleData.cs](../Assets/Scripts/Data/RoleData.cs)
- [Assets/Resources/UI/RankItem.prefab](../Assets/Resources/UI/RankItem.prefab)
- [Assets/Resources/Airplane](../Assets/Resources/Airplane)
- [Assets/Scenes/GameScene.unity](../Assets/Scenes/GameScene.unity)
- `Assets/InputSystem_Actions.inputactions`
- `Assets/Editor`
- `Assets/Plugins`
- `Assets/Resources`
- `Assets/StreamingAssets`
- `Assets/Scripts/Lesson/Lesson1_Mathf/Lesson1.cs`
- `Assets/Scripts/Lesson/Lesson1_Mathf/Lesson1.unity`
- `Assets/Scripts/Lesson/Lesson2_三角函数/Lesson2.cs`
- `Assets/Scripts/Lesson/Lesson2_三角函数/Lesson2.unity`
- `Assets/Scripts/Lesson/Lesson5_向量加减乘除/Lesson5.cs`
- `Assets/Scripts/Lesson/Lesson5_向量加减乘除/Lesson5.unity`
- `Assets/Scripts/Lesson/Lesson6_向量点乘/Lesson6.cs`
- `Assets/Scripts/Lesson/Lesson6_向量点乘/Lesson6.unity`
- `Assets/Scripts/Lesson/Lesson7_向量叉乘/Lesson7.cs`
- `Assets/Scripts/Lesson/Lesson7_向量叉乘/Lesson7.unity`
- `Assets/Scripts/Lesson/Lesson8_向量插值/Lesson8.cs`
- `Assets/Scripts/Lesson/Lesson8_向量插值/Lesson8.unity`
- `Assets/Scripts/Lesson/Lesson11_四元数常用方法/Lesson11.cs`
- `Assets/Scripts/Lesson/Lesson11_四元数常用方法/Tools.cs`
- `Assets/Scripts/Lesson/Lesson11_四元数常用方法/Lesson11.unity`
- `Assets/Scripts/Lesson/Lesson12_四元数计算/Lesson12.cs`
- `Assets/Scripts/Lesson/Lesson12_四元数计算/CameraFollow.cs`
- `Assets/Scripts/Lesson/Lesson12_四元数计算/BulletObj.cs`
- `Assets/Scripts/Lesson/Lesson12_四元数计算/Resources/Bullet.prefab`
- `Assets/Scripts/Lesson/Lesson12_四元数计算/Lesson12.unity`
- [Assets/Scripts/Lesson/Lesson13_延迟函数/Lesson13.cs](../Assets/Scripts/Lesson/Lesson13_延迟函数/Lesson13.cs)
- [Assets/Scripts/Lesson/Lesson13_延迟函数/Lesson13.unity](../Assets/Scripts/Lesson/Lesson13_延迟函数/Lesson13.unity)
- [Assets/Scripts/Lesson/Lesson14_协同程序/Lesson14.cs](../Assets/Scripts/Lesson/Lesson14_协同程序/Lesson14.cs)
- [Assets/Scripts/Lesson/Lesson14_协同程序/Lesson14.unity](../Assets/Scripts/Lesson/Lesson14_协同程序/Lesson14.unity)
- [Assets/Scripts/Lesson/Lesson15_协同程序原理/Lesson15.cs](../Assets/Scripts/Lesson/Lesson15_协同程序原理/Lesson15.cs)
- [Assets/Scripts/Lesson/Lesson15_协同程序原理/CoroutineMgr.cs](../Assets/Scripts/Lesson/Lesson15_协同程序原理/CoroutineMgr.cs)
- [Assets/Scripts/Lesson/Lesson15_协同程序原理/Lesson15answer.cs](../Assets/Scripts/Lesson/Lesson15_协同程序原理/Lesson15answer.cs)
- [Assets/Scripts/Lesson/Lesson15_协同程序原理/Lesson15.unity](../Assets/Scripts/Lesson/Lesson15_协同程序原理/Lesson15.unity)
- [Assets/Scripts/Lesson/Lesson18_异步加载/ResourcesMgr.cs](../Assets/Scripts/Lesson/Lesson18_异步加载/ResourcesMgr.cs)
- [Assets/Scripts/Lesson/Lesson18_异步加载/Lesson18.cs](../Assets/Scripts/Lesson/Lesson18_异步加载/Lesson18.cs)
- [Assets/Scripts/Lesson/Lesson18_异步加载/Lesson18.unity](../Assets/Scripts/Lesson/Lesson18_异步加载/Lesson18.unity)
- [Assets/Scripts/Lesson/Lesson18_异步加载/Resources/BeLovedCyrene.jpg](../Assets/Scripts/Lesson/Lesson18_异步加载/Resources/BeLovedCyrene.jpg)
- [Assets/Scripts/Lesson/Lesson20_场景异步加载/SceneMgr.cs](../Assets/Scripts/Lesson/Lesson20_场景异步加载/SceneMgr.cs)
- [Assets/Scripts/Lesson/Lesson20_场景异步加载/Lesson20.cs](../Assets/Scripts/Lesson/Lesson20_场景异步加载/Lesson20.cs)
- [Assets/Scripts/Lesson/Lesson20_场景异步加载/Lesson20_1.unity](../Assets/Scripts/Lesson/Lesson20_场景异步加载/Lesson20_1.unity)
- [Assets/Scripts/Lesson/Lesson20_场景异步加载/Lesson20_2.unity](../Assets/Scripts/Lesson/Lesson20_场景异步加载/Lesson20_2.unity)
- [Assets/Scripts/Lesson/Lesson21_LineRender/Lesson21.cs](../Assets/Scripts/Lesson/Lesson21_LineRender/Lesson21.cs)
- [Assets/Scripts/Lesson/Lesson21_LineRender/Lesson21.unity](../Assets/Scripts/Lesson/Lesson21_LineRender/Lesson21.unity)
- [Assets/Scripts/Lesson/Lesson22_范围检测/Lesson22.cs](../Assets/Scripts/Lesson/Lesson22_范围检测/Lesson22.cs)
- [Assets/Scripts/Lesson/Lesson22_范围检测/Lesson22.unity](../Assets/Scripts/Lesson/Lesson22_范围检测/Lesson22.unity)
- [Assets/Scripts/Lesson/Lesson23_射线检测/Lesson23.cs](../Assets/Scripts/Lesson/Lesson23_射线检测/Lesson23.cs)
- [Assets/Scripts/Lesson/Lesson23_射线检测/Lesson23.unity](../Assets/Scripts/Lesson/Lesson23_射线检测/Lesson23.unity)
- [Assets/Resources/Effect/HitEff.prefab](../Assets/Resources/Effect/HitEff.prefab)
- [Assets/Resources/Effect/DDD.prefab](../Assets/Resources/Effect/DDD.prefab)
- [Assets/ArtRes/BulletEff](../Assets/ArtRes/BulletEff)
- `Assets/Materials/Gray.mat`
- `Packages/manifest.json`
- `ProjectSettings/`

当前基础知识主线和综合实践均已结束。后续变更应作为维护或扩展内容单独记录，不再计入本课程主线进度。

## 下一步

1. 本系列项目已完成，后续可选择创建 GitHub Release 作为阶段成果。
2. 如继续扩展，可优先考虑对象池、统一音效管理和更明确的场景生命周期。
3. 每次 push 前继续检查学习文档是否跟上代码、场景和验证状态。
