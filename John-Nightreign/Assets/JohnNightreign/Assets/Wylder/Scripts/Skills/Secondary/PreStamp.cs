using EntityStates;
using UnityEngine;
using UnityEngine.Networking;

namespace JohnNightreign.Entitystates
{
    public class PreStamp : BaseSkillState
    {
        private static readonly float minDuration = 0.25f;
        private static readonly float maxDuration = 1f;
        private float damageTaken = 0;

        public override void OnEnter()
        {
            base.OnEnter();
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
            if (NetworkServer.active) base.characterBody.AddBuff(Content.Assets.bdWylderStamp);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void OnExit()
        {
            base.OnExit();
            On.RoR2.HealthComponent.TakeDamage -= HealthComponent_TakeDamage;
            if (NetworkServer.active && base.characterBody.HasBuff(Content.Assets.bdWylderStamp)) base.characterBody.RemoveBuff(Content.Assets.bdWylderStamp);
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Death;
        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, RoR2.HealthComponent self, RoR2.DamageInfo damageInfo)
        {
            if (self && self.alive && self.body && self.body.HasBuff(Content.Assets.bdWylderStamp))
            {
                damageTaken += damageInfo.damage;
                damageInfo.force = Vector3.zero;
                float healthFraction = 0.2f * characterBody.healthComponent.fullCombinedHealth;
                float armorMult = 1f - characterBody.armor / (100f + Mathf.Abs(characterBody.armor));
                float netDamage = armorMult * damageInfo.damage;
                if (netDamage > healthFraction) damageInfo.damage = healthFraction / armorMult;
            }
            orig(self, damageInfo);
        }
    }
}
