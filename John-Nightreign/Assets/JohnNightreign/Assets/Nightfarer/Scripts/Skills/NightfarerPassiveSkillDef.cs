using JetBrains.Annotations;
using RoR2;
using RoR2.Skills;
using UnityEngine;

namespace JohnNightreign.Skills
{
    [CreateAssetMenu(menuName = "RoR2/SkillDef/NightfarerPassive")]
    public class NightfarerPassiveSkillDef : SkillDef
    {
        private bool sprintPassive = false;
        private static readonly float originalMultiplier = 1.45f;
        private static readonly float sprintMultiplier = 1.70f;
        private static readonly float sprintTimeRequired = 1.5f;
        private float sprintStopwatch = sprintTimeRequired;
        private float previousMultiplier = 1.45f;

        public override BaseSkillInstanceData OnAssigned([NotNull] GenericSkill skillSlot)
        {
            Debug.LogError(skillName);
            if (skillName == "sdNightfarerPassiveSprint") sprintPassive = true;
            else skillSlot.characterBody.AddBuff(Content.Assets.surviveBuffDef);
            return base.OnAssigned(skillSlot);
        }

        public override void OnFixedUpdate([NotNull] GenericSkill skillSlot, float deltaTime)
        {
            base.OnFixedUpdate(skillSlot, deltaTime);
            if (!sprintPassive) return;
            if (skillSlot.characterBody.isSprinting)
            {
                if (sprintStopwatch > 0) sprintStopwatch -= Time.fixedDeltaTime;
                else skillSlot.characterBody.sprintingSpeedMultiplier = sprintMultiplier;
            }
            else 
            {
                sprintStopwatch = sprintTimeRequired;
                skillSlot.characterBody.sprintingSpeedMultiplier = originalMultiplier;
            }

            if (previousMultiplier != skillSlot.characterBody.sprintingSpeedMultiplier) skillSlot.characterBody.RecalculateStats();
            previousMultiplier = skillSlot.characterBody.sprintingSpeedMultiplier;
        }
    }
}
