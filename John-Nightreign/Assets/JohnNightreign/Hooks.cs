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
                //Calculate what the damage will be after armor
                float effectiveDamage = damageInfo.damage * (1 - self.body.armor / (100 + Mathf.Abs(self.body.armor)));
                //combinedhealth is the health value made up of all barrier, health, shields at once, we could add a check against osp too but we can worry about that later
                if (effectiveDamage > self.combinedHealth)
                {
                    //Change the damage to 0, add an invincibility buff, remove all DOT effects, and replace the buff that flags the passive as active
                    damageInfo.damage = 0;
                    self.body.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 3f);
                    self.body.RemoveBuff(Content.Assets.surviveBuffDef);
                    DotController.RemoveAllDots(self.gameObject);
                    self.body.AddBuff(Content.Assets.surviveGoneBuffDef);
                }
            }
            orig(self, damageInfo);
        }

    }
}
