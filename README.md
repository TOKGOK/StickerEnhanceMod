# 贴纸增强 Mod

> 《卡牌魔王：只剩个头》贴纸增强 Mod，增强所有数值型贴纸的效果。

## 增强内容

| 贴纸 | 原始效果 | 增强后 |
|------|---------|--------|
| 露露贴纸 (5002) | 灵魂获取 +30% | **灵魂获取 +60%** |
| 金头贴纸 (5003) | 开局金币 +15 | **开局金币 +45** |
| 小白贴纸 (5004) | 20% 自动闪避 | **50% 自动闪避** |
| 黑龙贴纸 (5005) | 对小怪伤害 +1 | **对小怪伤害 +3** |
| 魔神贴纸 (5007) | 每攻击 2 次发射剑气 | **每攻击 1 次发射剑气** |

> 墨菲贴纸 (5001) 和女神贴纸 (5006) 为布尔型效果（true/false），无法进一步数值增强，暂不处理。

## 安装方法

1. 将本 Mod 文件夹复制到游戏本地 Mod 目录：
   ```
   C:\Users\<用户名>\AppData\LocalLow\YuWave\DemonLordJustABlock\LocalMods\
   ```
2. 确认目录结构如下：
   ```
   StickerEnhanceMod/
   ├── mod.json
   └── CodeMods/
       ├── StickerEnhanceMod.dll
       ├── StickerEnhanceMod.cs
       └── codemod.json
   ```
3. 启动游戏，在 Mod 列表中启用「贴纸增强」。

## 原理

Mod 加载后首帧通过反射直接修改 `SkillConfig` 中的参数值（`param1`、`cooldownNum`）。贴纸装备时，游戏的 `SkillPassiveModifier` 会读取修改后的配置并应用到 `BattleObject`，无需事件钩子。

## 源码编译

```bash
cd CodeMods
dotnet build -p:GameManagedPath="<游戏Managed目录绝对路径>"
```

例如：
```bash
dotnet build -p:GameManagedPath="D:\Program Files (x86)\Steam\steamapps\common\DemonLordJustABlock\DemonLordJustABlock_Data\Managed"
```

## 版本

当前版本：**V0.1.1**
