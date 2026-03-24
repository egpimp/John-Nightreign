using EntityStates;
using RoR2;

namespace JohnNightreign.Entitystates
{
    public class LightSwing : BaseState
    {
        public int combo;
        public float duration;
        public static float recastGraceWindow = 0.5f;
        public bool recast;
        public static float[] earlyExits = new float[] { 1.9f, 1.7f, 1.7f, 2f };
        public float earlyExit;
        public static float[] damageCoefficients = new float[] { 3.5f, 3.5f, 4f, 4.5f };
        public float damageCoefficient;
        public static string[] hitboxGroups = new string[] { "SwingCmb1", "SwingCmb2", "SwingCmb3", "SwingCmb4" };
        public string hitboxGroup;
        public static string[] animNames = new string[] { "p1", "p2", "p3", "p4" };
        public string animName;
        public static float procCoefficient = 1f;
        public DamageTypeCombo damageType;
        OverlapAttack attack;

        public override void OnEnter()
        {
            base.OnEnter();
            earlyExit = earlyExits[combo];
            damageCoefficient = damageCoefficients[combo];
            hitboxGroup = hitboxGroups[combo];
            animName = animNames[combo];
            recast = false;
            attack = InitMeleeOverlap(damageCoefficient, EntityStates.Merc.GroundLight.comboHitEffectPrefab, GetModelTransform(), hitboxGroup);
            characterBody.SetAimTimer(duration + 2f);
        }

        public override void Update()
        {
            base.Update();
            if (base.inputBank.skill1.down && base.fixedAge + recastGraceWindow >= earlyExit) recast = true;
        }

        public override void OnExit()
        {
            base.OnExit();
            attack.Fire();
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
