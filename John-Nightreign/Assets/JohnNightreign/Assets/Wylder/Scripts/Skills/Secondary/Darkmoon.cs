using EntityStates;
using UnityEngine;
using UnityEngine.Networking;
using static JohnNightreign.Content.Assets;

namespace JohnNightreign.Entitystates
{
    public class Darkmoon : BaseSkillState
    {
        bool wasExecuted = false;
        static float baseDuration = 1.5f;
        float duration;
        static float buffDuration = 10f;

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Yay1");
            duration = baseDuration / base.attackSpeedStat;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.fixedAge >= duration && base.isAuthority)
            {
                wasExecuted = true;
                outer.SetNextStateToMain();
            }
        }

        public override void OnExit()
        {
            if (wasExecuted && NetworkServer.active) characterBody.AddTimedBuff(bdWylderFrosted, buffDuration);
            base.OnExit();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}
