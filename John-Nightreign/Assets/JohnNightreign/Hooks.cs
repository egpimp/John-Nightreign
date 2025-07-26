using JohnNightreign.Content;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoR2;

namespace JohnNightreign
{
    public static class Hooks
    {
        internal static void Init()
        {
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
        }

        internal static void Remove()
        {
            On.RoR2.HealthComponent.TakeDamage -= HealthComponent_TakeDamage;
        }

        private static void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            //Verify body should even be checked for if this should trigger, not nonlethal damage and has appropriate buff
            if (self && self.body && self.body.HasBuff(Content.Assets.surviveBuffDef) && !((damageInfo.damageType.damageType & DamageType.NonLethal) == DamageType.NonLethal))
            {
                float effectiveDamage = damageInfo.damage * (1 - self.body.armor / (100 + Mathf.Abs(self.body.armor)));
                if (effectiveDamage > self.combinedHealth)
                {
                    damageInfo.damage = 0;
                    self.body.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 3f);
                    self.body.RemoveBuff(Content.Assets.surviveBuffDef);
                    DotController.RemoveAllDots(self.gameObject);
                }
            }
            orig(self, damageInfo);
        }

    }
}
