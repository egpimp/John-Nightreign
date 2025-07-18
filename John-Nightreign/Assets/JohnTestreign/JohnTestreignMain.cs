using BepInEx;
using RoR2.Skills;
using System.IO;
using UnityEngine;
namespace JohnTestreign
{
    #region Dependencies
    [BepInDependency("com.bepis.r2api")]
    #endregion
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class JohnTestreignMain : BaseUnityPlugin
    {
        public const string GUID = "com.egpimp.JohnTestreign";
        public const string MODNAME = "John Testreign";
        public const string VERSION = "0.0.1";

        public static PluginInfo pluginInfo { get; private set; }
        public static JohnTestreignMain instance { get; private set; }
        internal static AssetBundle assetBundle { get; private set; }
        internal static string assetBundleDir => Path.Combine(Path.GetDirectoryName(pluginInfo.Location), "JohnTestreignAssets");

        private void Awake()
        {
            instance = this;
            pluginInfo = Info;
            new JohnTestreignContent();
        }

        internal static void LogFatal(object data)
        {
            instance.Logger.LogFatal(data);
        }
        internal static void LogError(object data)
        {
            instance.Logger.LogError(data);
        }
        internal static void LogWarning(object data)
        {
            instance.Logger.LogWarning(data);
        }
        internal static void LogMessage(object data)
        {
            instance.Logger.LogMessage(data);
        }
        internal static void LogInfo(object data)
        {
            instance.Logger.LogInfo(data);
        }
        internal static void LogDebug(object data)
        {
            instance.Logger.LogDebug(data);
        }
    }
}
