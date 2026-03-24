using EntityStates;
using RoR2;

namespace JohnNightreign.Entitystates
{
    public class HeavySwing : BaseSkillState
    {
        public float charge;
        public float baseDamageCoefficient = 6f;
        public float maxChargeBonus = 4f;
        public float damageCoefficient;
        public DamageTypeCombo damageType;

        public override void OnEnter()
        {
            base.OnEnter();
            damageCoefficient = baseDamageCoefficient + maxChargeBonus * charge;
        }
    }
}
