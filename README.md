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

- 已完成 `Mathf`、三角函数、坐标系、向量、点乘、叉乘、插值、四元数、延迟函数、协同程序、协程原理、特殊文件夹、`Resources` 加载、场景异步加载、`LineRenderer`、范围检测和射线检测等基础知识主线。
- 基础内容 1-44p 已按当前学习节奏收尾；`45` 是知识点总结，不作为新的主线练习节点。
- 原教程的小实践采用 `NGUI + XML`；本仓库没有照搬这套技术路线，而是使用 Unity 内置的 `UGUI + JSON` 完成对应 UI 与数据功能，以贴近当前项目的实际使用方式。
- 已进入 `46.需求分析.mp4` 到 `68.实践总结.mp4` 的综合实践项目；开始、设置、排行榜和选角面板的主流程已实现，并可以从选角进入 `GameScene`。
- 排行榜 UI 已记录一次重要排错：动态实例设置父节点时需要使用 `SetParent(content, false)`，避免保留世界变换造成异常缩放。
- 当前仍需修正选角右箭头边界并补充仓库内的初始角色 JSON；之后继续制作游戏场景，并接入声音控制和游戏结果写入排行榜等跨场景功能。

## 协作原则

运行时代码、场景、Prefab、`ProjectSettings` 和 `Packages` 默认受保护。除非明确说“直接修改”“帮我改”“可以动代码”“直接修复”，否则 Codex 只做读取、分析、解释、建议和文档整理。

每完成一节课或一个阶段，都应更新 `LearningProgress.md`，必要时在 `Notes/` 下追加复盘。没有经过代码、Unity 场景、运行时和对应主题检查的内容，不记录为“已完成”。

## GitHub 工作流

常规提交信息默认使用中文。提交前先检查 `git status`，避免把 `Library/`、`Logs/`、`UserSettings/`、本地视频、压缩包或无关生成文件提交进仓库。

课程完成或阶段总结时，可以补充 GitHub Release，但必须先确认最终内容、验证方式和发布说明编码。
