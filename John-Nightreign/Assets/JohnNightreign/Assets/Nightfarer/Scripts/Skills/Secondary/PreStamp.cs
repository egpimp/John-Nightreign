using EntityStates;
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
            if (NetworkServer.active) base.characterBody.AddBuff(Content.Assets.stampBuffDef);
        }

        public override void OnExit()
        {
            base.OnExit();
            On.RoR2.HealthComponent.TakeDamage -= HealthComponent_TakeDamage;
            if (NetworkServer.active && base.characterBody.HasBuff(Content.Assets.stampBuffDef)) base.characterBody.RemoveBuff(Content.Assets.stampBuffDef);
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Death;
        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, RoR2.HealthComponent self, RoR2.DamageInfo damageInfo)
        {
            if (self && self.alive && self.body && self.body.HasBuff(Content.Assets.stampBuffDef)) damageTaken += damageInfo.damage;
            orig(self, damageInfo);
        }
    }
}
