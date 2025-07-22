using EntityStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavySwing : BaseSkillState
{
    public float charge;
    public float baseDamageCoefficient = 6f;
    public float maxChargeBonus = 4f;
    public float damageCoefficient;

    public override void OnEnter()
    {
        base.OnEnter();
        damageCoefficient = baseDamageCoefficient + maxChargeBonus * charge;
    }
}
