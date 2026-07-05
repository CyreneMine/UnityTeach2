# Lesson20 - 四元数常用方法练习

## 对应视频

- `17.为何使用四元数.mp4`
- `18.四元数是什么.mp4`
- `19.四元数的常用方法.mp4`
- `20.四元数的常用方法  练习题.mp4`

## 练习目标

1. 使用 `Quaternion.LookRotation` 实现 `LookAt` 的效果。
2. 将之前摄像机移动练习中的 `LookAt` 改成 `LookRotation`，并通过 `Quaternion.Slerp` 缓慢看向玩家。

## 对应文件

- `Assets/Scripts/Lesson/Lesson11_四元数常用方法/Lesson11.cs`
- `Assets/Scripts/Lesson/Lesson11_四元数常用方法/Tools.cs`
- `Assets/Scripts/Lesson/Lesson11_四元数常用方法/Lesson11.unity`
- `Assets/Scripts/Lesson/Lesson5_向量加减乘除/Lesson5.cs`
- `Assets/Scripts/Lesson/Lesson5_向量加减乘除/Lesson5.unity`
- `Packages/manifest.json`
- `Packages/packages-lock.json`

## 当前实现

第一题：

```csharp
cubeA.MyLookAt(target);
```

扩展方法实现：

```csharp
public static void MyLookAt(this Transform obj, Transform target)
{
    Vector3 targetDic = target.position - obj.position;
    Quaternion lookRotation = Quaternion.LookRotation(targetDic);
    obj.rotation = lookRotation;
}
```

第二题：

```csharp
if (nowPos != target.position || nowRot != target.rotation)
{
    nowPos = target.position;
    time = 0;
    nowRot = target.rotation;
}

transform.position = target.position - target.forward * zOffset + target.up * yOffset;
time += Time.deltaTime;
transform.position = Vector3.Lerp(
    startPos,
    target.position - target.forward * zOffset + target.up * yOffset,
    time);
transform.rotation = Quaternion.Slerp(
    transform.rotation,
    Quaternion.LookRotation(target.position - transform.position),
    time * 0.1f);
```

场景序列化检查：

| 场景 | 对象 | 组件/字段 | 当前值 |
| --- | --- | --- | --- |
| `Lesson11.unity` | `Main Camera` | `Lesson11` | 已挂载 |
| `Lesson11.unity` | `Lesson11.target` | 引用 | 指向 `Target` |
| `Lesson11.unity` | `Lesson11.cubeA` | 引用 | 指向 `CubeA` |
| `Lesson11` | `Tools.MyLookAt` | 扩展方法 | 使用 `Quaternion.LookRotation` 实现看向目标 |
| `Lesson5.unity` | `Main Camera` | `Lesson5` | 已挂载 |
| `Lesson5.unity` | `Lesson5.target` | 引用 | 指向 `Cube` |
| `Lesson5.unity` | `zOffset` / `yOffset` | 偏移 | `4` / `7` |

## 理解记录

- `Quaternion.LookRotation(direction)` 会生成一个旋转，让物体的正前方朝向 `direction`。
- `target.position - obj.position` 是从当前物体指向目标的方向，所以可以替代 `obj.LookAt(target)` 的核心效果。
- `MyLookAt(this Transform obj, Transform target)` 是扩展方法写法，调用时可以写成 `cubeA.MyLookAt(target)`。
- `Quaternion.Slerp(currentRotation, targetRotation, t)` 用于在两个旋转之间做球形插值，适合做“缓慢转向”。
- 第二题先用 `Vector3.Lerp` 平滑移动到摄像机目标位置，再计算从摄像机指向目标的旋转，并用 `Slerp` 平滑转向目标。
- 用户手动更新了 Rider 包：`com.unity.ide.rider` 更新到 `3.0.40`，这不是 Unity 自动误改。

## 自查结果

| 优先级 | 位置 | 问题 | 影响 | 建议 |
| --- | --- | --- | --- | --- |
| P3 | `Tools.cs` | `targetDic` 变量名像是 `targetDir` 的笔误 | 不影响运行，只是命名不够准确 | 后续整理时可以改名 |
| P3 | `Lesson5.cs` | 当前 `Slerp` 使用 `transform.rotation` 作为每帧起点 | 这会形成持续平滑追踪效果，不是固定起点到终点的一次性插值 | 符合摄像机缓慢看向目标的练习目标 |
| P3 | `Lesson5.cs` | `time` 会持续累加，目标变化时重置 | `Lerp/Slerp` 参数最终会变大并趋近或夹紧到目标值 | 学习阶段可接受；后续可用 `Time.deltaTime * speed` 表达持续平滑 |
| P3 | `Lesson5.cs`、`Lesson11.cs`、`Tools.cs` | 没有空引用保护 | 当前场景引用已绑定，不影响本练习；复制到其他场景时可能报错 | 后续学习引用检查时再补 |
| P3 | `Lesson5.cs` | `using System;` 当前未使用 | 不影响运行 | 后续整理脚本时可以删除 |

## 验证方式

已经做过的静态检查：

- `Lesson11` 中通过 `cubeA.MyLookAt(target)` 调用扩展方法，扩展方法内部使用 `LookRotation` 实现看向目标。
- `Lesson11.unity` 中 `target` 和 `cubeA` 已绑定。
- `Lesson5` 中使用 `Vector3.Lerp` 平滑移动到摄像机目标位置，并使用 `Quaternion.Slerp` 平滑转向目标。
- `Lesson5.unity` 中 `target`、`zOffset`、`yOffset` 已绑定。
- Rider 包更新来自用户手动操作，后续提交时可以保留。

建议在 Unity 中继续确认：

- 停止 Play Mode 后保存场景。
- 运行 `Lesson11.unity`，观察 `CubeA` 是否朝向 `Target`。
- 运行 `Lesson5.unity`，移动或旋转目标，观察摄像机是否仍保持后方上方位置，并缓慢看向目标。
- Console 没有 `NullReferenceException`。

## 尚未验证或边界

- Codex 没有直接启动 Unity 运行场景，只检查了磁盘上的脚本和场景序列化内容。
- 四元数阶段尚未全部结束，后续还有 `21.四元数计算.mp4`、`22.四元数计算  练习题1.mp4`、`23.四元数计算 练习题2.mp4`。
- 本次只更新文档，不 commit、不 push；等四元数阶段全部结束后统一提交和推送。

## 下一步

继续 `21.四元数计算.mp4`。
