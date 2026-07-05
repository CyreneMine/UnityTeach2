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

第二题新增 `CameraFollow`：

- 相机目标点：`target.position - zOffset * target.forward + xOffset * target.right + yOffset * target.up`。
- 相机位置：`Vector3.Lerp(startPos, finalAt, followTime * followSpeed)`。
- 相机朝向：先用 `Quaternion.LookRotation` 看向目标头顶附近，再用 `Quaternion.Slerp` 做过渡。
- 滚轮距离：通过 `Input.GetAxis("Mouse ScrollWheel")` 修改 `zOffset`，并用 `Mathf.Clamp` 限制在 `3~6`。

## 检查记录

| 优先级 | 位置 | 结果 | 影响 | 状态 |
| --- | --- | --- | --- | --- |
| P2 | `CameraFollow.cs` | 目标移动或旋转时，现在会同时重置 `followTime` 和 `roundTime` | 位置和朝向都能重新进入过渡过程 | 已修正 |
| P3 | `Lesson12.cs` | 环形发射从 `-180` 到 `180` 包含两端 | 两端是同一方向，会多生成一颗重叠子弹 | 保留，教程改造时再处理 |
| P3 | `CameraFollow.cs` | 看向头顶使用世界 Y 轴偏移 | 当前目标不倾斜时符合练习观察目标 | 保留 |
| P3 | `Lesson12.cs`、`CameraFollow.cs`、`BulletObj.cs` | `using System;` 未使用 | 不影响运行 | 后续整理 |

## 自己的实现和教程写法差异

本次是自写版检查点。用户已确认教程使用更面向对象的方式，例如封装开火方法、发射模式枚举等；当前版本先保留为“核心逻辑版”，用于在按教程改造前留一个可追溯状态。

## 下一步

根据教程写法改造本题，重点观察：

- 发射模式枚举如何让逻辑更清晰；
- 开火方法封装后能减少哪些重复；
- 自写核心逻辑和教程封装写法各自适合什么阶段。
