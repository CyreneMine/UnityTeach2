# UnityTeach2

UnityTeach2 是一个 Unity 基础教程学习仓库，用来跟随「[唐老狮] Unity 四部曲 - 基础」课程进行练习、复盘和阶段性整理。

这个仓库的目标不是尽快堆出一个完整项目，而是把 Unity/C# 基础、调试习惯、课程练习、问题复盘和 GitHub 记录沉淀成可以长期回看的学习资料。

## 项目信息

- Unity 版本：`6000.3.10f1`

## 学习范围

本课程目前识别到的主题包括：

- `Mathf`、三角函数、坐标系、向量模长、单位向量、向量运算、点乘、叉乘、插值；
- 四元数基础、常用方法、旋转计算；
- 延迟函数、协同程序和协程原理；
- Unity 特殊文件夹、`Resources` 同步/异步加载、资源卸载、场景异步加载；
- `LineRenderer`、范围检测、射线检测；
- 一个综合实践项目：开始界面、设置面板、排行榜、选角、游戏面板、退出/结束面板、主玩家、子弹和开火点逻辑。

## 仓库结构

- [Assets/](Assets/)：Unity 资产、脚本、场景和课程练习内容。
- [Packages/](Packages/)：Unity 包依赖配置。
- [ProjectSettings/](ProjectSettings/)：Unity 项目设置。
- [AGENTS.md](AGENTS.md)：Codex 在本仓库内协作时必须遵守的规则。
- [LearningProgress.md](LearningProgress.md)：课程学习进度、验证结果和下一步。
- [Notes/](Notes/)：每节课或每个主题的复盘笔记。
- [Docs/CurrentStatus.md](Docs/CurrentStatus.md)：当前项目状态快照。
- [Screenshots/](Screenshots/)：必要时保存学习截图或验证截图。

## 当前状态

- 已完成 `Mathf`、三角函数、坐标系、向量、点乘、叉乘、插值、四元数、延迟函数、协同程序和协程原理阶段。
- 当前正在阶段 05：已完成特殊文件夹创建、`Resources` 同步加载练习和 `Resources` 异步加载练习。
- 最新练习包含 `Lesson12` 子弹预制体通过 `Resources.Load` 动态加载，以及 `Lesson18_异步加载` 中的 `ResourcesMgr` 回调/协程两种异步加载写法。
- 下一步继续资源卸载和场景异步加载，再进入 `LineRenderer`、范围检测、射线检测和 46-68p 小项目实践。

## 协作原则

运行时代码、场景、Prefab、`ProjectSettings` 和 `Packages` 默认受保护。除非明确说“直接修改”“帮我改”“可以动代码”“直接修复”，否则 Codex 只做读取、分析、解释、建议和文档整理。

每完成一节课或一个阶段，都应更新 `LearningProgress.md`，必要时在 `Notes/` 下追加复盘。没有经过代码、Unity 场景、运行时和对应主题检查的内容，不记录为“已完成”。

## GitHub 工作流

常规提交信息默认使用中文。提交前先检查 `git status`，避免把 `Library/`、`Logs/`、`UserSettings/`、本地视频、压缩包或无关生成文件提交进仓库。

课程完成或阶段总结时，可以补充 GitHub Release，但必须先确认最终内容、验证方式和发布说明编码。
