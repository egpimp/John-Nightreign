using RoR2;
using UnityEngine;

namespace JohnNightreign.Skills
{
    [RequireComponent(typeof(CharacterBody))]
    public class WylderPassiveComponent : MonoBehaviour
    {
        private static readonly float originalMultiplier = 1.45f;
        private static readonly float sprintMultiplier = 1.70f;
        private static readonly float sprintTimeRequired = 1.5f;
        private float sprintStopwatch = sprintTimeRequired;
        private float previousMultiplier = 1.45f;
        public CharacterBody characterBody;
        static bool hasActivated;

        public void OnEnable()
        {
            if (characterBody && !characterBody.HasBuff(Content.Assets.surviveBuffDef) && !characterBody.HasBuff(Content.Assets.surviveGoneBuffDef)) characterBody.AddBuff(Content.Assets.surviveBuffDef);
        }

        public void FixedUpdate()
        {
            if (characterBody.isSprinting)
            {
                if (sprintStopwatch > 0) sprintStopwatch -= Time.fixedDeltaTime;
                else characterBody.sprintingSpeedMultiplier = sprintMultiplier;
            }
            else 
            {
                sprintStopwatch = sprintTimeRequired;
                characterBody.sprintingSpeedMultiplier = originalMultiplier;
            }

            if (previousMultiplier != characterBody.sprintingSpeedMultiplier) characterBody.RecalculateStats();
            previousMultiplier = characterBody.sprintingSpeedMultiplier;
        }
    }
}
