# UnityTeach2 当前状态

更新时间：2026-07-05

## 项目快照

- Unity 版本：`6000.3.10f1`
- 当前 Unity 工程目录：`G:\Unity\UnityProject\UnityTeach2`
- 课程视频目录：`F:\1aUnity教程\6.［唐老狮]【Unity四部曲_基础】`
- 远端仓库：`https://github.com/CyreneMine/UnityTeach2.git`

## 已完成

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

## 当前项目内容

- `Assets/Scenes/SampleScene.unity`
- `Assets/InputSystem_Actions.inputactions`
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
- `Assets/Scripts/Lesson/Lesson12_四元数计算/Bullet.prefab`
- `Assets/Scripts/Lesson/Lesson12_四元数计算/Lesson12.unity`
- `Assets/Materials/Gray.mat`
- `Packages/manifest.json`
- `ProjectSettings/`

当前已开始课程脚本和场景练习。后续每一课的代码、场景、Prefab 或资源变更都应在 `LearningProgress.md` 或 `Notes/` 中记录。

## 下一步

1. 根据教程写法改造 `22.四元数计算  练习题1.mp4`，对比自写核心逻辑和封装写法。
2. 继续后续 `23.四元数计算 练习题2.mp4`。
3. 每次 push 前先检查学习文档是否跟上代码和场景进度。
