using RoR2.ContentManagement;
using UnityEngine;
using RoR2;
using System.Collections;
using static JohnNightreign.Content.Assets;

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
            stampBuffDef = _bundle.LoadAsset<BuffDef>("NightfarerStamp");
            surviveBuffDef = _bundle.LoadAsset<BuffDef>("NightfarerSurvive");
            JohnNightreignContentPack.survivorDefs.Add(new SurvivorDef[] { _nightfarerSurvivorDef });
            JohnNightreignContentPack.bodyPrefabs.Add(new GameObject[] { _nightfarerBody });
            JohnNightreignContentPack.buffDefs.Add(new BuffDef[] { stampBuffDef, surviveBuffDef });
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
