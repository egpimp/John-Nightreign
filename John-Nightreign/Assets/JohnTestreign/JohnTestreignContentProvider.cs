using RoR2.ContentManagement;
using UnityEngine;
using RoR2;
using System.Collections;
using RoR2.Skills;
using UnityEngine.AddressableAssets;
using System;
namespace JohnTestreign
{
    public class JohnTestreignContent : IContentPackProvider
    {
        public string identifier => JohnTestreignMain.GUID;

        public static ReadOnlyContentPack readOnlyContentPack => new ReadOnlyContentPack(JohnTestreignContentPack);
        internal static ContentPack JohnTestreignContentPack { get; } = new ContentPack();
        private static SkillDef _testDef;
        private static AssetBundle _bundle;

        public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
        {
            var asyncOperation = AssetBundle.LoadFromFileAsync(JohnTestreignMain.assetBundleDir);
            while (!asyncOperation.isDone)
            {
                args.ReportProgress(asyncOperation.progress);
                yield return null;
            }

            //Write code here to initialize your mod post assetbundle load
            _bundle = asyncOperation.assetBundle;
            Debug.LogError(_bundle);
            _testDef = _bundle.LoadAsset<SkillDef>("TestreignSkillDef");
            Debug.LogError(_testDef);
            JohnTestreignContentPack.skillDefs.Add(new SkillDef[] { _testDef });
        }
        public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
        {
            ContentPack.Copy(JohnTestreignContentPack, args.output);
            args.ReportProgress(1f);
            yield break;
        }
        public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
        {
            SkillFamily mercPrimary = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Merc/MercBody.prefab").WaitForCompletion().GetComponent<SkillLocator>().primary.skillFamily;
            Array.Resize(ref mercPrimary.variants, mercPrimary.variants.Length + 1);
            mercPrimary.variants[mercPrimary.variants.Length - 1] = new SkillFamily.Variant
            {
                skillDef = _testDef,
                viewableNode = new ViewablesCatalog.Node(_testDef.skillNameToken, false, null),
                unlockableDef = null
            };


            args.ReportProgress(1f);
            yield break;
        }
        private void AddSelf(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
        {
            addContentPackProvider(this);
        }
        internal JohnTestreignContent()
        {
            ContentManager.collectContentPackProviders += AddSelf;
        }
    }
}
