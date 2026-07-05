# Lesson22 - 四元数计算练习题1自写版

## 对应视频

- `21.四元数计算.mp4`
- `22.四元数计算  练习题1.mp4`

## 练习目标

1. 用当前所学知识模拟飞机发射不同类型子弹：单发、双发、扇形、环形。
2. 用 3D 数学知识实现摄像机跟随效果：
   - 摄像机位于人物斜后方；
   - 通过角度控制倾斜率；
   - 鼠标滚轮控制摄像机和人物之间的距离，并有最大最小限制；
   - 摄像机看向人物头顶上方一个可调位置；
   - 用 `Vector3.Lerp` 实现相机位置跟随；
   - 用 `Quaternion.Slerp` 实现相机朝向过渡。

## 对应文件

- `Assets/Scripts/Lesson/Lesson12_四元数计算/Lesson12.cs`
- `Assets/Scripts/Lesson/Lesson12_四元数计算/CameraFollow.cs`
- `Assets/Scripts/Lesson/Lesson12_四元数计算/BulletObj.cs`
- `Assets/Scripts/Lesson/Lesson12_四元数计算/Bullet.prefab`
- `Assets/Scripts/Lesson/Lesson12_四元数计算/Lesson12.unity`
- `ProjectSettings/ProjectSettings.asset`

## 当前实现

第一题没有先照教程做面向对象封装，而是为了追进度先写核心逻辑：

- `D`：单发，按飞机当前旋转发射一颗子弹。
- `S`：双发，从飞机左右两侧各生成一颗子弹。
- `A`：扇形，围绕飞机当前旋转叠加 `Quaternion.AngleAxis` 生成多颗子弹。
- `Q`：环形，围绕飞机当前旋转生成一圈子弹。

第二题新增 `CameraFollow`，并在后续复盘后改为更贴近 3D 数学含义的写法：

- 目标观察点：`target.position + target.up * cameraHeight`。
- 摄像机方向：`Quaternion.AngleAxis(tiltDeg, target.right) * -target.forward`。
- 摄像机目标点：`目标观察点 + 摄像机方向 * zOffset`。
- 相机位置：`Vector3.Lerp(startPos, lookTargetPos, followTime * followSpeed)`。
- 相机朝向：`Quaternion.Slerp(startRot, Quaternion.LookRotation(-lookTargetDir), roundTime * roundSpeed)`。
- 滚轮距离：通过 `Input.GetAxis("Mouse ScrollWheel")` 修改 `zOffset`，并用 `Mathf.Clamp` 限制在 `3~6`。

## 深入复盘后的理解修正

这次最关键的修正是把“方向、偏移、位置点”拆开看。

```csharp
Vector3 nowTargetPos = target.position + target.up * cameraHeight;
Vector3 lookTargetDir = Quaternion.AngleAxis(tiltDeg, target.right) * -target.forward;
Vector3 lookTargetPos = nowTargetPos + lookTargetDir * zOffset;
```

- `target.up` 是目标自身上方的方向，不是目标上方的位置。
- `target.up * cameraHeight` 是从目标位置出发的高度偏移量。
- `target.position + target.up * cameraHeight` 才是目标上方的世界坐标观察点。
- `Quaternion.AngleAxis(tiltDeg, target.right) * -target.forward` 得到的是一个旋转后的方向向量。
- `lookTargetDir * zOffset` 是沿这个方向走出的偏移量。
- `nowTargetPos + lookTargetDir * zOffset` 才是摄像机应该移动到的世界坐标点。

`Quaternion.LookRotation` 的参数不是“目标点”，而是“朝向方向”。如果已经算出的 `lookTargetDir` 表示“目标观察点到摄像机”的方向，那么摄像机看回目标时要反过来：

```csharp
Quaternion.LookRotation(-lookTargetDir)
```

因此这道题最终可以浓缩成：

```csharp
方向 = Quaternion.AngleAxis(角度, 旋转轴) * 原始方向;
偏移量 = 方向 * 距离;
目标位置点 = 基准点 + 偏移量;
看向方向 = 目标点 - 自己位置;
```

在当前摄像机跟随案例里，因为 `lookTargetDir` 已经是“目标到摄像机”的方向，所以看向方向可以直接用 `-lookTargetDir`。

## 检查记录

| 优先级 | 位置 | 结果 | 影响 | 状态 |
| --- | --- | --- | --- | --- |
| P2 | `CameraFollow.cs` | 目标移动或旋转时，现在会同时重置 `followTime` 和 `roundTime` | 位置和朝向都能重新进入过渡过程 | 已修正 |
| P2 | `CameraFollow.cs` | `nowTargetPos` 同时用于检测目标位置变化，又在后面被改写成目标观察点 | 当 `cameraHeight` 不为 0 时，下一帧 `nowTargetPos != target.position` 会持续成立，当前表现更接近每帧平滑追踪，而不是严格的一次性固定起点插值 | 作为学习边界记录 |
| P3 | `Lesson12.cs` | 环形发射从 `-180` 到 `180` 包含两端 | 两端是同一方向，会多生成一颗重叠子弹 | 保留，教程改造时再处理 |
| P3 | `Lesson12.cs`、`CameraFollow.cs`、`BulletObj.cs` | `using System;` 未使用 | 不影响运行 | 后续整理 |

## 自己的实现和教程写法差异

本次是自写版检查点。用户已确认教程使用更面向对象的方式，例如封装开火方法、发射模式枚举等；当前版本先保留为“核心逻辑版”，用于保留自己从迷糊到理清公式的过程。

## 下一步

四元数阶段按当前学习节奏收尾。后续进入延迟函数、协同程序相关课程。
