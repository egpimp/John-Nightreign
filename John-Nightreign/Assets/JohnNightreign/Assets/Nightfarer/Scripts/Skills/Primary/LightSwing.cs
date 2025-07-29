using EntityStates;
using RoR2.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JohnNightreign.Entitystates
{
    public class LightSwing : BaseSkillState
    {
        public int combo;
        public float duration;
        public float recastGraceWindow = 0.5f;
        private bool recast;
        public static readonly float[] earlyExits = new float[] { 1.9f, 1.7f, 1.7f, 2f };
        public float earlyExit;
        public static readonly float[] damageCoefficients = new float[] { 3.5f, 3.5f, 4f, 4.5f };
        public float damageCoefficient;
        public static readonly string[] hitboxGroups = new string[] { "SwingCmb1", "SwingCmb2", "SwingCmb3", "SwingCmb4" };
        public string hitboxGroup;
        public static readonly string[] animNames = new string[] { "p1", "p2", "p3", "p4" };
        public string animName;
        public static readonly float procCoefficient = 1f;

        public override void OnEnter()
        {
            base.OnEnter();
            earlyExit = earlyExits[combo];
            damageCoefficient = damageCoefficients[combo];
            hitboxGroup = hitboxGroups[combo];
            animName = animNames[combo];
            recast = false;
        }

        public override void Update()
        {
            base.Update();
            if (base.inputBank.skill1.down && base.fixedAge + 0.5f >= earlyExit) recast = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(base.isAuthority)
            {
                if (recast && base.fixedAge >= earlyExit) outer.SetNextState(new PreSwing());
                else if (base.fixedAge > duration) outer.SetNextStateToMain();
            }

        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            if (base.fixedAge < earlyExit) return InterruptPriority.Skill;
            else return InterruptPriority.Any;
        }
    }
}
