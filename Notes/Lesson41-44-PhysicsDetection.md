# Lesson41-44 - 范围检测和射线检测

## 对应视频

- `41.范围检测.mp4`
- `42.范围检测  练习题.mp4`
- `43.射线检测.mp4`
- `44.射线检测  练习题.mp4`

## 对应文件

- `Assets/Scripts/Lesson/Lesson22_范围检测/Lesson22.cs`
- `Assets/Scripts/Lesson/Lesson22_范围检测/Lesson22.unity`
- `Assets/Scripts/Lesson/Lesson23_射线检测/Lesson23.cs`
- `Assets/Scripts/Lesson/Lesson23_射线检测/Lesson23.unity`
- `Assets/Resources/Effect/HitEff.prefab`
- `Assets/Resources/Effect/DDD.prefab`
- `ProjectSettings/TagManager.asset`

## 学到的知识

- `Physics.OverlapBox` 使用中心点、半尺寸、旋转和 LayerMask 做盒形范围检测。
- `Physics.OverlapCapsule` 的两个端点必须是世界坐标点，不是单纯方向向量。
- `Physics.OverlapSphere` 适合做以某个中心点为圆心的球形范围检测。
- `Camera.main.ScreenPointToRay(Input.mousePosition)` 可以把鼠标屏幕坐标转换成从摄像机发出的射线。
- `Physics.Raycast` 配合 LayerMask 可以把不同交互目标拆开，例如墙面、可选中角色和平面。
- `Resources.Load` / 已封装的 `ResourcesMgr.LoadRes` 路径相对 `Resources` 文件夹，不带扩展名。

## 练习实现

### 范围检测

`Lesson22` 中通过 `W/S` 控制物体沿自身前后移动，通过 `A/D` 控制物体绕 Y 轴旋转。

- `J`：在物体前方 1 米位置做盒形检测，使用 `Vector3.one * 0.5f` 作为半尺寸，并传入 `transform.rotation` 让盒子跟随物体朝向。
- `K`：从 `transform.position` 到 `transform.position + transform.forward * 5` 做胶囊检测。
- `L`：以 `transform.position` 为中心做半径 10 的球形检测。
- 三种检测都通过 `1 << LayerMask.NameToLayer("Monster")` 过滤怪物层，避免把玩家自己算进去。

### 射线检测

`Lesson23` 中用鼠标射线实现两类练习：

- 点击 `Monster` 层墙面时，在命中点附近实例化 `HitEff` 和 `DDD` 两个特效资源，并延迟销毁。
- 点击 `Player` 层物体时记录当前选中对象；按住左键时继续用射线命中 `Default` 层平面，把选中对象移动到命中点上方 `offsetY` 的位置；右键取消选中。

## 检查记录

- 场景中 `Wall` 使用 `Monster` 层，`Player` 使用 `Player` 层，`Plane` 使用 `Default` 层，脚本中的 LayerMask 与场景配置能对应。
- 胶囊检测最初如果只使用 `transform.forward` 作为端点，会把方向向量误当成世界点，导致玩家移动后检测范围脱离玩家；当前版本已改为基于 `transform.position` 计算端点。
- 射线练习中左键同时承担“打墙特效”和“选中物体”两套逻辑。当前是课程练习，两个逻辑在同一脚本中并行可接受；实际项目中应拆分交互状态或命中优先级。
- 命中特效当前使用 `Quaternion.identity` 实例化，没有按 `hit.normal` 对齐命中面。当前资源效果可用于练习，正式项目中再处理贴花或特效朝向。

## 如何验证

- 静态检查 `Lesson22.cs`、`Lesson23.cs`、场景层级、碰撞体和 `TagManager` 层配置。
- 在 Unity 中运行场景后，通过 `J/K/L` 和 Console 输出确认范围检测命中 `Monster` 层对象。
- 在 Unity 中运行场景后，通过鼠标点击墙面观察特效生成，通过点击并按住左键拖动对象确认射线命中平面。

## 尚未验证或边界

- Codex 没有直接运行 Unity Play Mode，只读取了保存到磁盘的脚本、场景和配置。
- 当前练习没有把两套鼠标左键逻辑做项目化拆分。
- 当前特效资源没有做空资源保护，也没有统一同步/异步加载方式。

## 下一步

基础知识主线 1-44p 到这里收尾。`45` 是知识点总结，后续进入 `46.需求分析.mp4` 到 `68.实践总结.mp4` 的综合实践项目。
