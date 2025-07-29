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
        public static readonly float[] lightDurations = new float[] { 2.4f, 1.7f, 1.7f, 2f };

        void SteppedSkillDef.IStepSetter.SetStep(int i)
        {
            step = i;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            SteppedSkillDef sd = (SteppedSkillDef)base.activatorSkillSlot.skillDef;
            sd.stepGraceDuration = lightDurations[step] + 1.5f;
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
                    combo = step,
                    duration = lightDurations[step]

                });
            }
        }
    }
}
