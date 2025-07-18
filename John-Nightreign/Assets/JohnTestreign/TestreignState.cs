using EntityStates;
using EntityStates.Merc;
using EntityStates.Merc.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestreignState : BaseState
{
    private static float baseDuration = 0.8f;
    private static float minCharge = 0.1f;
    private float timer;

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!base.inputBank.skill1.down || base.fixedAge > baseDuration && base.isAuthority)
        {
            if (base.fixedAge < minCharge)
            {
                this.outer.SetNextState(new GroundLight2());
            }
            else
            {
                this.outer.SetNextState(new ThrowEvisProjectile());
            }
        }
    }

    public override InterruptPriority GetMinimumInterruptPriority()
    {
        return InterruptPriority.Skill;
    }
}
