using EntityStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JohnNightreign.Entitystates
{
    public class Stamp : BaseSkillState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Fuck.");
            outer.SetNextStateToMain();
        }
    }
}
