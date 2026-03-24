using RoR2.ContentManagement;
using UnityEngine;
using RoR2;
using System.Collections;
using static JohnNightreign.Content.Assets;
using RoR2.Skills;
using JohnNightreign.Entitystates;

namespace JohnNightreign.Content
{
    public class JohnNightreignContent : IContentPackProvider
    {
        public string identifier => JohnNightreignMain.GUID;

        public static ReadOnlyContentPack readOnlyContentPack => new ReadOnlyContentPack(JohnNightreignContentPack);
        internal static ContentPack JohnNightreignContentPack { get; } = new ContentPack();

        private static AssetBundle _bundle;
        private static SurvivorDef _wylderSurvivorDef;
        private static GameObject _wylderBody;

        public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
        {
            var asyncOperation = AssetBundle.LoadFromFileAsync(JohnNightreignMain.assetBundleDir);
            while(!asyncOperation.isDone)
            {
                args.ReportProgress(asyncOperation.progress);
                yield return null;
            }

            _bundle = asyncOperation.assetBundle;
            _wylderSurvivorDef = _bundle.LoadAsset<SurvivorDef>("Wylder");
            _wylderBody = _bundle.LoadAsset<GameObject>("WylderBody");

            bdWylderStamp = _bundle.LoadAsset<BuffDef>("WylderStamp");
            bdWylderFrosted = _bundle.LoadAsset<BuffDef>("WylderFrosted");
            bdWylderIgnited = _bundle.LoadAsset<BuffDef>("WylderIgnited");
            bdWylderSurvive = _bundle.LoadAsset<BuffDef>("WylderSurvive");

            idWylderPassive = _bundle.LoadAsset<ItemDef>("idWylderPassive");
            idWylderPassiveSpent = _bundle.LoadAsset<ItemDef>("idWylderPassiveSpent");

            sfWylderPrimary = _bundle.LoadAsset<SkillFamily>("sfWylderPrimary");
            sfWylderSecondary = _bundle.LoadAsset<SkillFamily>("sfWylderSecondary");
            sfWylderUtility = _bundle.LoadAsset<SkillFamily>("sfWylderUtility");
            sfWylderSpecial = _bundle.LoadAsset<SkillFamily>("sfWylderSpecial");
            sfWylderSword = _bundle.LoadAsset<SkillFamily>("sfWylderSword");

            sdWylderPrimary = _bundle.LoadAsset<SkillDef>("sdWylderPrimary");
            sdWylderSecondary = _bundle.LoadAsset<SkillDef>("sdWylderSecondary");
            sdWylderSecondaryGS = _bundle.LoadAsset<SkillDef>("sdWylderSecondaryGS");
            sdWylderSecondaryDM = _bundle.LoadAsset<SkillDef>("sdWylderSecondaryDM");
            sdWylderUtility = _bundle.LoadAsset<SkillDef>("sdWylderUtility");
            sdWylderSpecial = _bundle.LoadAsset<SkillDef>("sdWylderSpecial");
            sdWylderSwordDefault = _bundle.LoadAsset<SkillDef>("sdWylderSwordDefault");
            sdWylderSwordDarkmoon = _bundle.LoadAsset<SkillDef>("sdWylderSwordDarkmoon");

            JohnNightreignContentPack.survivorDefs.Add(new SurvivorDef[] { _wylderSurvivorDef });
            JohnNightreignContentPack.bodyPrefabs.Add(new GameObject[] { _wylderBody });
            JohnNightreignContentPack.skillFamilies.Add(new SkillFamily[] { sfWylderPrimary, sfWylderSecondary, sfWylderUtility, sfWylderSpecial, sfWylderSword } );
            JohnNightreignContentPack.skillDefs.Add(new SkillDef[] { sdWylderPrimary, sdWylderSecondary, sdWylderSecondaryGS, sdWylderSecondaryDM, sdWylderUtility, sdWylderSpecial, sdWylderSwordDefault, sdWylderSwordDarkmoon } );
            JohnNightreignContentPack.buffDefs.Add(new BuffDef[] { bdWylderStamp, bdWylderFrosted, bdWylderIgnited, bdWylderSurvive });
            JohnNightreignContentPack.itemDefs.Add(new ItemDef[] { idWylderPassive, idWylderPassiveSpent });
            JohnNightreignContentPack.entityStateTypes.Add(new System.Type[] { typeof(PreSwing), typeof(LightSwing), typeof(HeavySwing), typeof(PreStamp), typeof(Stamp), typeof(Darkmoon) });
        }
        public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
        {
            ContentPack.Copy(JohnNightreignContentPack, args.output);
            args.ReportProgress(1f);
            yield break;
        }
        public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
        {
            args.ReportProgress(1f);
            yield break;
        }
        private void AddSelf(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
        {
            addContentPackProvider(this);
        }
        internal JohnNightreignContent()
        {
            ContentManager.collectContentPackProviders += AddSelf;
        }
    }
}
