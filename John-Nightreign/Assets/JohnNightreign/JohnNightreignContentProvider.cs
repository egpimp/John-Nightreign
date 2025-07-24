using RoR2.ContentManagement;
using UnityEngine;
using RoR2;
using System.Collections;
using RoR2.Skills;

namespace JohnNightreign.Content
{
    public class JohnNightreignContent : IContentPackProvider
    {
        public string identifier => JohnNightreignMain.GUID;

        public static ReadOnlyContentPack readOnlyContentPack => new ReadOnlyContentPack(JohnNightreignContentPack);
        internal static ContentPack JohnNightreignContentPack { get; } = new ContentPack();

        private static AssetBundle _bundle;
        private static SurvivorDef _nightfarerSurvivorDef;
        private static GameObject _nightfarerBody;
        private static SkillFamily[] _nightfarerFamilies;

        public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
        {
            var asyncOperation = AssetBundle.LoadFromFileAsync(JohnNightreignMain.assetBundleDir);
            while(!asyncOperation.isDone)
            {
                args.ReportProgress(asyncOperation.progress);
                yield return null;
            }

            _bundle = asyncOperation.assetBundle;
            _nightfarerSurvivorDef = _bundle.LoadAsset<SurvivorDef>("Nightfarer");
            _nightfarerBody = _bundle.LoadAsset<GameObject>("NightfarerBody");
            JohnNightreignContentPack.survivorDefs.Add(new SurvivorDef[] { _nightfarerSurvivorDef });
            JohnNightreignContentPack.bodyPrefabs.Add(new GameObject[] { _nightfarerBody });
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
