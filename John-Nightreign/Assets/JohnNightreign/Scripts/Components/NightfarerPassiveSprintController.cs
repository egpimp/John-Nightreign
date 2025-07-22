using RoR2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace JohnNightreign.Components
{
    public class NightfarerPassiveSurviveController : MonoBehaviour
    {
        public static float buffDuration = 10f;
        public bool canSurvive = true;
        public CharacterBody body;

        private void Start()
        {
            body = GetComponent<CharacterBody>();
            Stage.onServerStageBegin += ResetPassive;
        }

        [Server]
        void ResetPassive(Stage stage)
        {
            canSurvive = true;
        }

        public void LethalDamageTaken()
        {
            if (!NetworkServer.active) return;
            if (!canSurvive) return;
            body.AddTimedBuff(RoR2Content.Buffs.Immune, buffDuration);
            canSurvive = false;
        }
    }
}
