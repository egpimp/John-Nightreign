using RoR2;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace JohnNightreign.Content
{
    public static class Assets
    {
        internal static BuffDef stampBuffDef;
        internal static BuffDef surviveBuffDef;

        internal const string LangFolder = "jnr_language";
        internal static string RootLangFolderPath => System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), LangFolder);

        internal static void RegisterLanguage()
        {
            if (Directory.Exists(RootLangFolderPath)) Language.collectLanguageRootFolders += RegisterTokensFolder;
        }

        internal static void RegisterTokensFolder(List<string> list)
        {
            list.Add(RootLangFolderPath);
        }
    }
}
