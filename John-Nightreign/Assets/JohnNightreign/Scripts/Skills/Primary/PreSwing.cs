using EntityStates;
using RoR2.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JohnNightreign.Entitystates
{
    public class PreSwing : BaseSkillState, SteppedSkillDef.IStepSetter
    {
        public int step;
        public float chargeThreshhold = 0.2f;
        public float maxCharge = 1.2f;

        void SteppedSkillDef.IStepSetter.SetStep(int i)
        {
            step = i;
        }
        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.isAuthority && (!base.IsKeyDownAuthority() || base.fixedAge >= maxCharge))
            {
                if (base.fixedAge >= chargeThreshhold) outer.SetNextState(new HeavySwing()
                {
                    charge = maxCharge - base.fixedAge
                });
                else outer.SetNextState(new LightSwing()
                {
                    combo = step
                });
            }
        }
    }
}
