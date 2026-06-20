using System.Collections;
using System.Linq;
using UnityEngine;

namespace StickerEnhanceMod
{
    /// <summary>
    /// 贴纸增强 Mod V0.1.1
    /// 直接修改 SkillConfig 原始参数值，增强贴纸效果：
    /// - 露露贴纸（5002）：灵魂加成 param1 从 0.3 提升到 0.6
    /// - 金头贴纸（5003）：开局金币 param1 从 15 提升到 45
    /// - 小白贴纸（5004）：自动闪避 param1 从 0.2 提升到 0.5
    /// - 黑龙贴纸（5005）：对小怪伤害 param1 从 1 提升到 3
    /// - 魔神贴纸（5007）：攻击发射剑气冷却 cooldownNum 从 2 降低到 1
    /// </summary>
    public class Main : SimpleModBehaviour
    {
        private const string ModVersion = "0.1.1";
        private const string LogPrefix = "[StickerEnhanceMod]";

        // 贴纸ID定义
        private const int StickerLulu = 5002;    // 露露贴纸（灵魂加成）
        private const int StickerJin = 5003;     // 金头贴纸（开局金币）
        private const int StickerXiaoBai = 5004; // 小白贴纸（自动闪避）
        private const int StickerHeiLong = 5005; // 黑龙贴纸（对小怪增伤）
        private const int StickerDemon = 5007;   // 魔神贴纸（攻击触发剑气）

        // 增强目标值（直接覆盖 SkillConfig.param1）
        private const float LuluSoulBonusTarget = 0.6f;    // 灵魂加成 → 60%
        private const float JinMoneyBonusTarget = 45f;      // 开局金币 → 45
        private const float XiaoBaiDodgeTarget = 0.5f;     // 闪避概率 → 50%
        private const float HeiLongDmgTarget = 3f;          // 对小怪伤害 → 3
        private const int DemonCooldownTarget = 1;          // 剑气冷却 → 1 次攻击

        private bool _done;

        public override void OnModLoaded()
        {
            Debug.Log(LogPrefix + " V" + ModVersion + " 已加载：增强贴纸效果。");
        }

        public override void OnModUnloaded()
        {
            Debug.Log(LogPrefix + " V" + ModVersion + " 已卸载。");
        }

        /// <summary>
        /// 首帧 Update 执行修改，与 SinNegativeRemoval 同样的模式
        /// </summary>
        private void Update()
        {
            if (_done) return;
            _done = true;

            EnhanceStickerConfig(StickerLulu, "param1", LuluSoulBonusTarget,
                "露露贴纸：灵魂加成 30% → 60%");
            EnhanceStickerConfig(StickerJin, "param1", JinMoneyBonusTarget,
                "金头贴纸：开局金币 15 → 45");
            EnhanceStickerConfig(StickerXiaoBai, "param1", XiaoBaiDodgeTarget,
                "小白贴纸：闪避概率 20% → 50%");
            EnhanceStickerConfig(StickerHeiLong, "param1", HeiLongDmgTarget,
                "黑龙贴纸：对小怪伤害 1 → 3");

            // 魔神贴纸：修改 cooldownNum
            var demonConfig = SkillConfigLoader.GetConfig(StickerDemon);
            if (demonConfig != null)
            {
                int oldCD = demonConfig.cooldownNum;
                demonConfig.cooldownNum = DemonCooldownTarget;
                Debug.Log(LogPrefix + " 魔神贴纸：攻击冷却 " + oldCD + " → " + DemonCooldownTarget);
            }
            else
            {
                Debug.Log(LogPrefix + " 警告：未找到魔神贴纸配置 (ID=" + StickerDemon + ")");
            }

            Debug.Log(LogPrefix + " 所有贴纸增强已完成。");
        }

        /// <summary>
        /// 通过反射修改 SkillConfig 的参数字段值
        /// </summary>
        private void EnhanceStickerConfig(int stickerId, string fieldName, object targetValue, string desc)
        {
            var config = SkillConfigLoader.GetConfig(stickerId);
            if (config == null)
            {
                Debug.Log(LogPrefix + " 警告：未找到贴纸配置 (ID=" + stickerId + ")");
                return;
            }

            var field = config.GetType().GetField(fieldName);
            if (field == null)
            {
                Debug.Log(LogPrefix + " 警告：字段 " + fieldName + " 不存在于 SkillConfig");
                return;
            }

            object oldValue = field.GetValue(config);
            field.SetValue(config, targetValue);
            Debug.Log(LogPrefix + " " + desc + " (" + fieldName + ": " + oldValue + " → " + targetValue + ")");
        }
    }
}
