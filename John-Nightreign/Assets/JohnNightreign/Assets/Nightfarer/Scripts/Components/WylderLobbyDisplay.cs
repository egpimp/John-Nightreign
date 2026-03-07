using RoR2;
using RoR2.Skills;
using UnityEngine;

public class WylderLobbyDisplay : CharacterSelectSurvivorPreviewDisplayController
{
    private SkillLocator skillLocator;
    private BodyIndex bodyIndex;
    public SkillFamily wylderSword;
    public SkillFamily wylderSecondary;
    public SkillDef[] replacementSkills;
    public SkillDef[] swordSkills;

    private void Awake()
    {
        this.currentLoadout = Loadout.RequestInstance();
        this.skillLocator = bodyPrefab.GetComponent<SkillLocator>();
        this.bodyIndex = BodyCatalog.FindBodyIndex("NightfarerBody");
        if (HasSkillVariantEnabled(currentLoadout, bodyIndex, wylderSword, swordSkills[0])) SetDefaultSword();
        else if (HasSkillVariantEnabled(currentLoadout, bodyIndex, wylderSword, swordSkills[1])) SetDarkmoonSword();
    }

    private new void OnEnable()
    {
        NetworkUser.onLoadoutChangedGlobal += OnLoadoutChangedGlobal;
    }

    private new void OnDisable()
    {
        NetworkUser.onLoadoutChangedGlobal -= OnLoadoutChangedGlobal;
    }

    private void OnDestroy()
    {
        currentLoadout = Loadout.ReturnInstance(currentLoadout);
    }

    public new void OnLoadoutChangedGlobal(NetworkUser changed)
    {
        base.OnLoadoutChangedGlobal(changed);
    }

    public void SetDefaultSword()
    {
        for (int i = 0; i < BodyCatalog.skillSlots[(int)bodyIndex].Length; i++)
        {
            GenericSkill skill = BodyCatalog.skillSlots[(int)(bodyIndex)][i];
            if (skill.skillFamily && skill.skillFamily == wylderSecondary)
            {
                SkillDef replacement = replacementSkills[0];
                skill.skillFamily.variants[0].skillDef = replacement;
                skill.skillDef = replacement;
                BodyCatalog.skillSlots[(int)(bodyIndex)][i] = skill;
            }
        }
        Loadout.Init();
    }

    public void SetDarkmoonSword()
    {
        for (int i = 0; i < BodyCatalog.skillSlots[(int)bodyIndex].Length; i++)
        {
            GenericSkill skill = BodyCatalog.skillSlots[(int)(bodyIndex)][i];
            if (skill.skillFamily && skill.skillFamily == wylderSecondary)
            {
                SkillDef replacement = replacementSkills[1];
                skill.skillFamily.variants[0].skillDef = replacement;
                skill.skillDef = replacement;
                BodyCatalog.skillSlots[(int)(bodyIndex)][i] = skill;
            }
        }
        Loadout.Init();
    }
}
