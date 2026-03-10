using RoR2;
using UnityEngine;
using static JohnNightreign.Content.Assets;

namespace JohnNightreign.Skills
{
    [RequireComponent(typeof(CharacterBody))]
    public class WylderPassiveComponent : MonoBehaviour
    {
        private static readonly float originalMultiplier = 1.45f;
        private static readonly float sprintMultiplier = 1.70f;
        private static readonly float sprintTimeRequired = 2f;
        private float sprintStopwatch = sprintTimeRequired;
        private float previousMultiplier = 1.45f;
        public CharacterBody characterBody;
        static bool hasActivated;

        public void OnEnable()
        {
            CheckSenseState();
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

            CheckSenseState();
        }

        void CheckSenseState()
        {
            Inventory inventory = characterBody.inventory;
            if (!inventory || inventory.GetItemCountEffective(idWylderPassiveSpent) > 0) return;
            if (inventory.GetItemCountEffective(idWylderPassive) > 0)
            {
                if (!characterBody.HasBuff(surviveBuffDef)) characterBody.AddBuff(surviveBuffDef);
            }
            else inventory.GiveItemPermanent(ItemCatalog.FindItemIndex("idWylderPassive"));
        }
    }
}
