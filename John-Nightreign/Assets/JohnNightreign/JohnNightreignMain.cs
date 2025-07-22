using BepInEx;
using System.IO;
using UnityEngine;

namespace JohnNightreign
{
    #region Dependencies
    [BepInDependency("___riskofthunder.RoR2BepInExPack")]
    #endregion
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class JohnNightreignMain : BaseUnityPlugin
    {
        public const string GUID = "com.egpimp.JohnNightreign";
        public const string MODNAME = "John Nightreign";
        public const string VERSION = "0.0.1";

        public static PluginInfo pluginInfo { get; private set; }
        public static JohnNightreignMain instance { get; private set; }
        internal static AssetBundle assetBundle { get; private set; }
        internal static string assetBundleDir => Path.Combine(Path.GetDirectoryName(pluginInfo.Location), "JohnNightreignAssets");

        private void Awake()
        {
            instance = this;
            pluginInfo = Info;
            new JohnNightreignContent();
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
