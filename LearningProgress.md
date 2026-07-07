# UnityTeach2 学习进度

更新时间：2026-07-07

## 总览

- 课程名称：「[唐老狮] Unity 四部曲 - 基础」
- 本地视频目录：`F:\1aUnity教程\6.［唐老狮]【Unity四部曲_基础】`
- 已识别视频：67 个
- 当前进度：阶段 06 - `LineRenderer` 练习已完成
- 当前阶段：阶段 06 - `LineRenderer`、范围检测、射线检测
- 下一步：进入 `41.范围检测.mp4`

备注：课程编号 `45` 是知识点总结，当前不作为主线练习进度的关键节点。

## 阶段划分

| 阶段 | 范围 | 状态 | 验证要求 |
| --- | --- | --- | --- |
| 01 | 概述、`Mathf`、三角函数 | 已完成 | 能说明常用 API、完成练习、在 Unity 中验证输出 |
| 02 | 坐标系、向量、点乘、叉乘、插值 | 已完成 | 能解释方向/长度/角度含义，并用场景或调试线验证 |
| 03 | 四元数 | 已完成 | 能说明欧拉角和四元数差异，验证旋转结果 |
| 04 | 延迟函数、协同程序、协程原理 | 已完成 | 能说明生命周期、停止条件和时间缩放影响 |
| 05 | 特殊文件夹、`Resources`、场景异步加载 | 待复盘 | 能验证路径、异步状态、卸载和 Build Settings 边界 |
| 06 | `LineRenderer`、范围检测、射线检测 | 学习中 | 能验证坐标、层级、碰撞体和 Debug 可视化 |
| 07 | 综合实践 UI 与数据准备 | 未开始 | 能验证面板引用、数据读写、交互状态和分辨率适配 |
| 08 | 综合实践 gameplay 逻辑 | 未开始 | 能验证玩家、子弹、开火点、状态切换和重复进入 |
| 09 | 实践总结、仓库复盘、可展示成果 | 未开始 | 完成最终复盘、GitHub 文档和可选 Release |

## 每课记录模板

复制下面模板到本文件底部或 `Notes/` 中使用。

```md
### LessonXX - 标题

- 状态：学习中 / 待复盘 / 已完成
- 视频文件：
- 对应代码/场景/资源：
- 学到的知识：
- 我一开始理解错的点：
- Codex/自查发现的问题：
- 如何验证：
- 尚未验证或不确定：
- 下一步：
```

## 历史记录

### 2026-07-07 - LineRenderer 练习

- 完成 `39.LineRenderer.mp4` 和 `40.LineRenderer  练习题.mp4`：用 `LineRenderer` 写一个画圆方法，并实现按住鼠标时绘制鼠标移动轨迹。
- 新增练习文件：`Assets/Scripts/Lesson/Lesson21_LineRender/Lesson21.cs`、`Assets/Scripts/Lesson/Lesson21_LineRender/Lesson21.unity`。
- `DrawSphere(center, radius)` 当前通过 `Quaternion.AngleAxis(i, Vector3.up) * Vector3.forward * radius` 计算圆上一圈点，并设置 `LineRenderer.loop = true`、`positionCount = 360`。
- 鼠标轨迹部分在 `Start()` 中动态添加第二个 `LineRenderer`，按下鼠标后每帧增加一个点，并使用 `Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10)` 将鼠标屏幕坐标转成世界坐标。
- 根据 `ScreenToWorldPoint_MousePosition_理解总结.md` 补充复盘：`Input.mousePosition.z` 表示离摄像机的深度，不是世界 z；应在调用 `ScreenToWorldPoint` 前设置深度，转换后再加 `Vector3.forward * 10` 只是沿世界 Z 轴偏移。
- 场景中 `Lesson21` 对象已挂载 `Lesson21` 脚本，`center = (3, 0, 0)`、`radius = 10`；`Main Camera` 带有 `MainCamera` 标签。
- 检查发现：画圆方法目前在 `Start()` 中被注释，没有自动运行；如果要验证第一题，需要临时调用 `DrawSphere(center, radius)` 或通过其他测试入口调用。
- 检查发现：`DrawSphere` 在对象已有 `LineRenderer` 时没有把组件赋值给 `lineRenderer` 字段，后续如果在已存在组件的对象上调用，可能出现空引用；当前第二题轨迹逻辑不受影响。
- 新增笔记：`Notes/Lesson39-40-LineRenderer.md`。
- 阶段 06 已进入，下一步是范围检测。

### 2026-07-07 - 场景异步加载练习

- 完成 `37.场景异步加载.mp4` 和 `38.场景异步加载  练习题.mp4`：写一个简单场景管理器，对外提供统一方法用于场景异步切换，并在异步切换结束后执行外部传入的委托逻辑。
- 新增练习文件：`Assets/Scripts/Lesson/Lesson20_场景异步加载/SceneMgr.cs`、`Assets/Scripts/Lesson/Lesson20_场景异步加载/Lesson20.cs`、`Assets/Scripts/Lesson/Lesson20_场景异步加载/Lesson20_1.unity`、`Assets/Scripts/Lesson/Lesson20_场景异步加载/Lesson20_2.unity`。
- `SceneMgr` 使用普通 C# 单例封装 `SceneManager.LoadSceneAsync(sceneName)`，并在 `AsyncOperation.completed` 中调用外部传入的 `UnityAction`。
- `Lesson20` 在 `Start()` 中调用 `SceneMgr.Instance.LoadScene("Lesson20_2", callback)`，用于从 `Lesson20_1` 异步切换到 `Lesson20_2`，并在完成时打印提示。
- `ProjectSettings/EditorBuildSettings.asset` 已加入 `Lesson20_1.unity` 和 `Lesson20_2.unity`，满足场景异步加载需要进入 Build Settings 的要求。
- 检查发现：当前 `SceneMgr.cs` 的 Git 暂存区里还是早期空类版本，工作区里才是最终实现；后续提交时必须确认暂存的是最终版，避免只提交空类。
- 记录边界：`SceneMgr.LoadScene` 当前没有空回调保护、没有进度回调、没有 `allowSceneActivation` 控制；本题目标是理解异步切换和完成回调，当前实现可接受。
- `36..Resources资源卸载.mp4` 本次没有看到单独代码验证文件，先记录为资源卸载边界待后续复盘，不强行标记为完整验证。
- 新增笔记：`Notes/Lesson37-38-SceneAsyncLoading.md`。
- 阶段 05 的特殊文件夹、`Resources` 加载和场景异步切换主线已推进到场景加载；下一步进入 `LineRenderer`。

### 2026-07-06 - 特殊文件夹与 Resources 同步/异步加载

- 完成 `31.特殊文件夹.mp4`，在工程中创建本节需要用到的特殊文件夹：`Assets/Editor`、`Assets/Plugins`、`Assets/Resources`、`Assets/StreamingAssets`。
- 检查并修正 `Assets/Resource` 单数命名问题，最终改为 Unity 可识别的 `Assets/Resources`。
- 完成 `32.Resources资源同步加载.mp4` 和 `33.Resources资源同步加载  练习题.mp4`，复用 `Lesson12_四元数计算` 的发射子弹练习，把 `Bullet.prefab` 移入 `Assets/Scripts/Lesson/Lesson12_四元数计算/Resources/`，并改用 `Resources.Load<GameObject>("Bullet")` 动态加载后实例化。
- 明确 `Resources.Load` 路径规则：路径相对任意 `Resources` 文件夹，不带扩展名；`Resources` 文件夹可以嵌套在 `Assets` 下的子目录里。
- 完成 `34.Resources资源异步加载.mp4` 和 `35.Resources资源异步加载  练习题.mp4`，新增 `Assets/Scripts/Lesson/Lesson18_异步加载/ResourcesMgr.cs`、`Lesson18.cs`、`Lesson18.unity` 和 `Resources/BeLovedCyrene.jpg`。
- `ResourcesMgr` 当前提供两种异步加载方式：基于 `ResourceRequest.completed` 的回调版，以及 `IEnumerator` 协程等待版。
- 检查确认 `BeLovedCyrene.jpg` 位于 `Resources` 文件夹下，并在 `.meta` 中按 `Sprite` 类型导入；`Lesson18.unity` 中 `Lesson18.img` 已绑定到场景 `Image` 组件。
- 记录边界：`Lesson18.Start()` 当前同时调用了两种异步加载方式，会对同一张图赋值两次；这是为了对比两种写法，后续正式使用时保留一种即可。
- 新增笔记：`Notes/Lesson31-35-ResourcesLoading.md`。
- 阶段 05 仍在学习中，下一步进入资源卸载和场景异步加载。

### 2026-07-05 - 延迟函数、协同程序和协程原理阶段收尾

- 完成 `24.延迟函数.mp4`、`25.延迟函数  练习题.mp4`、`26.协同程序1.mp4`、`27.协同程序2.mp4`、`28.协同程序  练习题.mp4`、`29.协同程序原理.mp4` 和 `30.协同程序原理  练习题.mp4`。
- 新增练习文件：`Assets/Scripts/Lesson/Lesson13_延迟函数/Lesson13.cs`、`Assets/Scripts/Lesson/Lesson13_延迟函数/Lesson13.unity`、`Assets/Scripts/Lesson/Lesson14_协同程序/Lesson14.cs`、`Assets/Scripts/Lesson/Lesson14_协同程序/Lesson14.unity`、`Assets/Scripts/Lesson/Lesson15_协同程序原理/Lesson15.cs`、`Assets/Scripts/Lesson/Lesson15_协同程序原理/Lesson15.unity`、`Assets/Scripts/Lesson/Lesson15_协同程序原理/CoroutineMgr.cs`、`Assets/Scripts/Lesson/Lesson15_协同程序原理/Lesson15answer.cs`。
- `Lesson13` 练习了 `InvokeRepeating` 每秒计数、`Destroy(gameObject, 2f)` 延迟销毁、`Invoke("DelayDestroy", 2f)` 延迟调用方法；当前保留了两种销毁写法并记录其重叠验证边界。
- `Lesson14` 练习了 `StartCoroutine`、`WaitForSeconds` 和分帧生成大量方块；当前按教程思路每生成约 1000 个方块让出一帧，避免一次性生成 100000 个方块造成明显卡顿。
- `Lesson15` 先手写 `IEnumerator` + `MoveNext()` 模拟“一秒执行一次部分逻辑”，再按教程思路新增 `CoroutineMgr`，用列表保存协程和下一次执行时间。
- 复盘确认：`if (coroutines[i].time > Time.time) continue;` 与教程的 `if (coroutines[i].time <= Time.time) { ... }` 是同一判断的反向写法，当前不会造成时间判断 bug。
- 记录边界：当前 `CoroutineMgr` 只支持本题要求的 `int` 秒数返回值；不支持 `WaitForSeconds`、`yield return null`、嵌套协程、停止协程等完整 Unity 协程能力。
- 新增笔记：`Notes/Lesson13-DelayFunction.md`、`Notes/Lesson14-Coroutine.md`、`Notes/Lesson15-CoroutinePrinciple.md`。
- 阶段 04 按当前学习节奏收尾。明天计划尽量完成 31-44p 后续基础知识点；后天进入 46-68p 小项目实践。

### 2026-07-05 - 四元数阶段收尾复盘

- 基于 `21.四元数计算.mp4` 和 `22.四元数计算  练习题1.mp4` 完成四元数阶段收尾。
- 复盘了摄像机跟随中的核心公式：`Quaternion.AngleAxis(tiltDeg, target.right) * -target.forward` 得到的是旋转后的方向向量，不是位置点。
- 修正理解：`target.up`、`target.forward`、`target.right` 都是方向；`target.position + target.up * cameraHeight` 才是目标上方的观察点。
- 修正理解：`Quaternion.LookRotation` 的参数是方向，不是目标点；如果 `nowDir` 表示“目标观察点到摄像机”的方向，摄像机看回目标时应使用 `Quaternion.LookRotation(-nowDir)`。
- `CameraFollow.cs` 已改为先算目标观察点、再算摄像机相对目标的方向、再用方向乘距离得到摄像机目标位置，并用 `Vector3.Lerp` 和 `Quaternion.Slerp` 做过渡。
- 当前仍记录一个学习边界：`nowTargetPos` 同时承担“目标位置变化检测”和“目标观察点”含义，会让非零 `cameraHeight` 时更接近每帧平滑追踪，而不是严格的一次性固定起点插值。
- 阶段 03 四元数内容按当前学习节奏收尾；后续进入阶段 04 延迟函数、协同程序。

### 2026-07-05 - 完成四元数计算练习题1自写版检查点

- 完成 `21.四元数计算.mp4` 和 `22.四元数计算  练习题1.mp4` 的自写版练习。
- 新增脚本、场景和 Prefab：`Assets/Scripts/Lesson/Lesson12_四元数计算/Lesson12.cs`、`Assets/Scripts/Lesson/Lesson12_四元数计算/CameraFollow.cs`、`Assets/Scripts/Lesson/Lesson12_四元数计算/BulletObj.cs`、`Assets/Scripts/Lesson/Lesson12_四元数计算/Bullet.prefab`、`Assets/Scripts/Lesson/Lesson12_四元数计算/Lesson12.unity`。
- 第一题先按自己的节奏写核心逻辑，没有照教程封装开火方法和发射模式枚举；当前实现了单发、双发、扇形和环形四种发射方式。
- 第二题新增 `CameraFollow`，实现独立摄像机跟随目标斜后方、鼠标滚轮控制距离、看向目标头顶附近，并用 `Vector3.Lerp` 和 `Quaternion.Slerp` 做位置与朝向过渡。
- 检查发现并修正：目标移动或旋转时，现在会同时重置位置插值和旋转插值，避免只移动不转向或只旋转不平滑跟随。
- 当前保留的学习边界：环形发射 `-180` 到 `180` 会包含同一方向的两端；`using System;` 仍有未使用项；后续准备根据教程写法进行面向对象封装。
- ProjectSettings 已切到 `activeInputHandler: 2`，保证旧版 `Input.GetAxis("Mouse ScrollWheel")` 可以在当前项目输入设置下工作。
- 新增笔记：`Notes/Lesson22-QuaternionCalculationExercise1.md`。
- 下一步根据教程改造本题，重点对比“直接写核心逻辑”和“封装发射模式/开火方法”的差异。

### 2026-07-05 - 完成四元数常用方法练习

- 完成 `17.为何使用四元数.mp4`、`18.四元数是什么.mp4`、`19.四元数的常用方法.mp4` 和 `20.四元数的常用方法  练习题.mp4`。
- 新增脚本和场景：`Assets/Scripts/Lesson/Lesson11_四元数常用方法/Lesson11.cs`、`Assets/Scripts/Lesson/Lesson11_四元数常用方法/Tools.cs`、`Assets/Scripts/Lesson/Lesson11_四元数常用方法/Lesson11.unity`。
- 第一题按教程写法封装 `MyLookAt` 扩展方法，内部使用 `Quaternion.LookRotation(target.position - obj.position)` 实现 `LookAt` 的效果，并在 `Lesson11` 中调用 `cubeA.MyLookAt(target)`。
- 第二题直接在之前摄像机移动练习 `Assets/Scripts/Lesson/Lesson5_向量加减乘除/Lesson5.cs` 中，将摄像机位置改为 `Vector3.Lerp` 平滑移动，并用 `Quaternion.Slerp` 缓慢看向目标。
- 用户手动更新 Rider 包依赖：`Packages/manifest.json` 中 `com.unity.ide.rider` 更新到 `3.0.40`，`Packages/packages-lock.json` 同步更新。
- 检查发现：`Lesson5` 已将目标变化检测移动到插值前，避免使用旧 `time` 先插值一帧；当前写法属于“平滑移动并持续平滑看向目标”的学习实现。
- 新增笔记：`Notes/Lesson20-QuaternionCommonMethods.md`。
- 本次先只更新文档，不 push；等四元数阶段全部结束后再统一提交和推送。
- 下一步进入 `21.四元数计算.mp4`。

### 2026-07-04 - 完成向量插值运算练习，阶段 02 收尾

- 完成 `15.向量插值运算.mp4` 和 `16.向量插值运算  练习题.mp4`：用插值实现摄像机平滑跟随，并用球形插值模拟太阳升降变化。
- 新增脚本、场景和材质：`Assets/Scripts/Lesson/Lesson8_向量插值/Lesson8.cs`、`Assets/Scripts/Lesson/Lesson8_向量插值/Lesson8.unity`、`Assets/Materials/Gray.mat`。
- 第一题沿用摄像机目标点：`target.position - target.forward * zOffset + target.up * yOffset`，再用 `Vector3.Lerp` 从起点插值到目标相机位。
- 第二题使用 `Vector3.Slerp(Vector3.right * 10, Vector3.left * 10 + Vector3.up, sunTime * 0.3f)` 模拟太阳沿弧线升降。
- 场景中 `Main Camera` 已挂载 `Lesson8`，`target` 指向 `Cube`，`sun` 指向 `Sphere`，摄像机、目标和太阳均为独立根对象。
- 记录边界：当前太阳升降是一次性从右侧插值到左侧并停住；如果后续要循环昼夜，可再引入 `Mathf.PingPong` 或重置 `sunTime`。
- 新增笔记：`Notes/Lesson16-VectorInterpolation-CameraAndSun.md`。
- 阶段 02 的坐标系、向量、点乘、叉乘和插值内容已收尾；下一步进入四元数阶段 `17.为何使用四元数.mp4`。

### 2026-07-04 - 完成向量叉乘练习

- 完成 `13.向量叉乘.mp4` 和 `14.向量叉乘  练习题.mp4`：用叉乘判断目标在 A 的左侧或右侧，并结合点乘角度、距离判断入侵范围。
- 新增脚本和场景：`Assets/Scripts/Lesson/Lesson7_向量叉乘/Lesson7.cs`、`Assets/Scripts/Lesson/Lesson7_向量叉乘/Lesson7.unity`。
- 核心逻辑：`Vector3.Cross(transform.forward, targetDir).y` 的正负用于区分左右，`Vector3.Dot` 和 `Mathf.Acos` 用于计算夹角。
- 本节的场景摆位主要用于手动拖动物体调试边界；当前没有输入系统或自动移动逻辑时，不把保存瞬间的 A/B 坐标是否触发作为主要判错依据。
- 注意点：当前脚本用完整 3D 向量参与角度和距离计算，后续如果题目明确是俯视平面方向判断，可以将方向向量的 `y` 分量归零后再算。
- 新增笔记：`Notes/Lesson14-CrossProduct-DirectionDetection.md`。
- 下一步进入 `15.向量插值运算.mp4`。

### 2026-07-04 - 完成向量点乘练习

- 完成 `11.向量点乘.mp4` 和 `12.向量点乘  练习题.mp4`：判断目标是否处于 A 正前方指定角度范围内，并且距离不超过指定范围。
- 新增脚本和场景：`Assets/Scripts/Lesson/Lesson6_向量点乘/Lesson6.cs`、`Assets/Scripts/Lesson/Lesson6_向量点乘/Lesson6.unity`。
- 核心逻辑：先计算 A 到 B 的单位方向，再用 `Vector3.Dot` 和 `Mathf.Acos` 得到夹角，同时用 `Vector3.Distance` 判断距离。
- 手写 `Dot + Acos` 是为了加深理解；实际项目里也可以直接使用 `Vector3.Angle`。
- 本节的场景摆位主要用于手动拖动物体调试边界；当前没有输入系统或自动移动逻辑时，不把保存瞬间的 A/B 坐标是否触发作为主要判错依据。
- 新增笔记：`Notes/Lesson12-DotProduct-IntruderDetection.md`。
- 下一步进入 `13.向量叉乘.mp4`。

### 2026-07-04 - 完成向量加减乘除摄像机跟随练习

- 完成 `10.向量加减乘除  练习题.mp4`：用向量加减乘除实现摄像机跟随目标物体。
- 新增脚本和场景：`Assets/Scripts/Lesson/Lesson5_向量加减乘除/Lesson5.cs`、`Assets/Scripts/Lesson/Lesson5_向量加减乘除/Lesson5.unity`。
- 核心公式：`target.position - target.forward * zOffset + target.up * yOffset`，表示摄像机在目标自身后方 `4` 米、上方 `7` 米。
- 场景中 `Main Camera` 已挂载 `Lesson5`，`zOffset = 4`、`yOffset = 7`，`target` 指向 `Cube`。
- 复盘了自己一开始的世界坐标写法：`new Vector3(target.position.x, target.position.y + 7, target.position.z - 4)` 只适用于目标朝向固定的情况，不能表达“目标自身后方”。
- 新增笔记：`Notes/Lesson10-VectorArithmetic-CameraFollow.md`。
- 下一步进入 `11.向量点乘.mp4`。

### 2026-07-04 - 完成向量模长和单位向量练习

- 完成 `8.向量模长和单位向量  练习题.mp4`，本节为理论计算练习，不需要写 Unity 代码。
- 记录两点距离的两种常用方式：`Vector3.Distance(a, b)` 和 `(a - b).magnitude`。
- 手算向量 `(3, 4, 5)` 的模长：`sqrt(50)`，约等于 `7.07`。
- 手算向量 `(3, -4)` 的单位向量：`(0.6, -0.8)`。
- 新增笔记：`Notes/Lesson08-VectorMagnitude-And-UnitVector.md`。
- 下一步进入 `9.向量加减乘除.mp4`。

### 2026-07-04 - 完成三角函数曲线移动练习

- 完成 `5.三角函数  练习题.mp4`：实现一个物体按正弦/余弦风格曲线移动。
- 新增脚本和场景：`Assets/Scripts/Lesson/Lesson2_三角函数/Lesson2.cs`、`Assets/Scripts/Lesson/Lesson2_三角函数/Lesson2.unity`。
- 将课程脚本目录整理到 `Assets/Scripts/Lesson/` 下，并保留 Unity `.meta` GUID。
- 记录教程写法：向前移动叠加 `Mathf.Sin(timer)` 控制的横向位移。
- 确认场景中 `Cube` 已挂载 `Lesson2`，并序列化 `speed`、`changeSpeed`、`changeSize`。
- 新增笔记：`Notes/Lesson05-Trigonometry-CurveMovement.md`。
- 下一步进入 `6.坐标系.mp4`。

### 2026-07-04 - 归档 Mathf Lerp 练习理解

- 记录 `Mathf.Lerp(start, end, t)` 中 `t` 是插值进度，不是自动意义上的秒数。
- 区分两种写法：每帧用当前位置当开始值会形成先快后慢的缓动追踪；固定开始点和目标点并让 `t` 从 `0` 累加到 `1`，才是一次完整插值移动。
- 练习中用方块作为跟随者、球作为目标点，方便观察移动关系。
- 新增笔记：`Notes/Lesson03-Mathf-Lerp.md`。
- 用户确认练习已完成；Codex 静态检查脚本和场景引用正常。
- 检查发现的注意点：当前写法是“固定时间移动”而不是严格匀速；注释里的曲线移动示例有 `z` 重复、`y` 缺失的问题，但不影响当前启用逻辑。
- 当时阶段 01 仍保持“学习中”，因为后续还有三角函数等内容；后续已完成三角函数练习，以上方新记录为准。

### 2026-07-03 - 初始化学习仓库

- 创建基础 GitHub 仓库环境。
- 建立 `AGENTS.md`、`README.md`、`LearningProgress.md`、`Notes/`、`Docs/CurrentStatus.md` 和 `Screenshots/`。
- 确认 Unity 版本为 `6000.3.10f1`。
- 确认课程目录存在，并识别到 67 个视频文件。
