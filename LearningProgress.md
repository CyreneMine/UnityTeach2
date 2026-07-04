# UnityTeach2 学习进度

更新时间：2026-07-04

## 总览

- 课程名称：「[唐老狮] Unity 四部曲 - 基础」
- 本地视频目录：`F:\1aUnity教程\6.［唐老狮]【Unity四部曲_基础】`
- 已识别视频：67 个
- 当前进度：Lesson14 向量叉乘练习已完成，阶段 02 学习中
- 当前阶段：阶段 02 - 坐标系、向量、点乘、叉乘、插值
- 下一步：继续 `15.向量插值运算.mp4`

备注：课程编号 `45` 是知识点总结，当前不作为主线练习进度的关键节点。

## 阶段划分

| 阶段 | 范围 | 状态 | 验证要求 |
| --- | --- | --- | --- |
| 01 | 概述、`Mathf`、三角函数 | 已完成 | 能说明常用 API、完成练习、在 Unity 中验证输出 |
| 02 | 坐标系、向量、点乘、叉乘、插值 | 学习中 | 能解释方向/长度/角度含义，并用场景或调试线验证 |
| 03 | 四元数 | 未开始 | 能说明欧拉角和四元数差异，验证旋转结果 |
| 04 | 延迟函数、协同程序、协程原理 | 未开始 | 能说明生命周期、停止条件和时间缩放影响 |
| 05 | 特殊文件夹、`Resources`、场景异步加载 | 未开始 | 能验证路径、异步状态、卸载和 Build Settings 边界 |
| 06 | `LineRenderer`、范围检测、射线检测 | 未开始 | 能验证坐标、层级、碰撞体和 Debug 可视化 |
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
