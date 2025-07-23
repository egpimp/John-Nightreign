using EntityStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JohnNightreign.Entitystates
{
    public class LightSwing : BaseSkillState
    {
        public int combo;
        public static float[] durations = new float[] { 2.4f, 1.7f, 1.7f, 2f };
        public float duration;
        public static float[] earlyExits = new float[] { 1.9f, 1.7f, 1.7f, 2f };
        public float earlyExit;
        public static float[] damageCoefficients = new float[] { 3.5f, 3.5f, 4f, 4.5f };
        public float damageCoefficient;
        public static string[] hitboxGroups = new string[] { "SwingCmb1", "SwingCmb2", "SwingCmb3", "SwingCmb4" };
        public string hitboxGroup;
        public static string[] animNames = new string[] { "p    1", "p2", "p3", "p4" };
        public static float procCoefficient = 1f;

        public override void OnEnter()
        {
            base.OnEnter();
            duration = durations[combo];
            earlyExit = earlyExits[combo];
            damageCoefficient = damageCoefficients[combo];
            hitboxGroup = hitboxGroups[combo];
        }
    }
}
