using EntityStates;
using UnityEngine.Networking;

namespace JohnNightreign.Entitystates
{
    public class PreStamp : BaseSkillState
    {
        private static readonly float minDuration = 0.25f;
        private static readonly float maxDuration = 1f;

        public override void OnEnter()
        {
            base.OnEnter();
            if (NetworkServer.active) base.characterBody.AddBuff(Content.Assets.stampBuffDef);
        }

        public override void OnExit()
        {
            base.OnExit();
            if (NetworkServer.active && base.characterBody.HasBuff(Content.Assets.stampBuffDef)) base.characterBody.RemoveBuff(Content.Assets.stampBuffDef);
        }
    }
}
