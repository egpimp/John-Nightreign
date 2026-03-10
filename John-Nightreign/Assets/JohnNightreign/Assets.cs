using RoR2;
using RoR2.Skills;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace JohnNightreign.Content
{
    public static class Assets
    {
        internal static BuffDef stampBuffDef;
        internal static BuffDef revengeBuffDef;
        internal static BuffDef surviveBuffDef;
        internal static BuffDef surviveGoneBuffDef;

        internal static SkillFamily sfWylderPrimary;
        internal static SkillFamily sfWylderSecondary;
        internal static SkillFamily sfWylderUtility;
        internal static SkillFamily sfWylderSpecial;
        internal static SkillFamily sfWylderSword;

        internal static SkillDef sdWylderPrimary;
        internal static SkillDef sdWylderSecondary;
        internal static SkillDef sdWylderSecondaryGS;
        internal static SkillDef sdWylderSecondaryDM;
        internal static SkillDef sdWylderUtility;
        internal static SkillDef sdWylderSpecial;
        internal static SkillDef sdWylderSwordDefault;
        internal static SkillDef sdWylderSwordDarkmoon;

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
