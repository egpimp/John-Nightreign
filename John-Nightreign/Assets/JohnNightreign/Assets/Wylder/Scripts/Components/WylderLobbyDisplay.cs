using RoR2;
using RoR2.Skills;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Events;

public class WylderLobbyDisplay : CharacterSelectSurvivorPreviewDisplayController
{
    private BodyIndex bodyIndex;
    public SkillFamily wylderSword;
    public SkillFamily wylderSecondary;
    public SkillDef[] replacementSkills;
    public SkillDef[] swordSkills;

    public Loadout tempLoadout;

    bool _shouldUpdate;

    private void Awake()
    {
        bodyIndex = BodyCatalog.FindBodyIndex("WylderBody");
    }

    private new void OnEnable()
    {
        currentLoadout = Loadout.RequestInstance();
        NetworkUser.onLoadoutChangedGlobal += OnLoadoutChangedGlobal;
        RoR2Application.onNextUpdate += Refresh;
        QueueUpdate();
    }

    private new void OnDisable()
    {
        NetworkUser.onLoadoutChangedGlobal -= OnLoadoutChangedGlobal;
        currentLoadout = Loadout.ReturnInstance(currentLoadout);
    }

    public new void OnLoadoutChangedGlobal(NetworkUser changed)
    {
        if (changed != this.networkUser) return;
        tempLoadout = Loadout.RequestInstance();

        changed.networkLoadout.CopyLoadout(tempLoadout);
        if (bodyIndex == BodyIndex.None) return;

        foreach (SkillChangeResponse response in skillChangeResponses)
        {
            if(!HasSkillVariantEnabled(currentLoadout, bodyIndex, response.triggerSkillFamily, response.triggerSkill) && HasSkillVariantEnabled(tempLoadout, bodyIndex, response.triggerSkillFamily, response.triggerSkill))
            {
                Debug.Log(response.triggerSkill);
                Debug.Log(response.triggerSkillFamily);
                UnityEvent responseEvent = response.response;
                if (responseEvent != null) responseEvent.Invoke();
            }
        }

        foreach (SkinChangeResponse response in skinChangeResponses)
        {
            uint responseSkindex = (uint)SkinCatalog.FindLocalSkinIndexForBody(bodyIndex, response.triggerSkin);
            uint oldSkindex = this.currentLoadout.bodyLoadoutManager.GetSkinIndex(bodyIndex);
        }

        tempLoadout.Copy(currentLoadout);
        Loadout.ReturnInstance(tempLoadout);
        QueueUpdate();
    }

    public void FixedUpdate()
    {
        if (!_shouldUpdate) return;
        LoadoutPanelController loadoutPanelInstance = FindObjectOfType<LoadoutPanelController>();
        if (loadoutPanelInstance == null) return;
        loadoutPanelInstance.Rebuild();
        _shouldUpdate = false;
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

                GenericSkill[] prefabSkillSlots = bodyPrefab.GetComponents<GenericSkill>();
                for (int j = 0; i < prefabSkillSlots.Length; i++)
                {
                    if (prefabSkillSlots[j].skillFamily == wylderSecondary)
                    {
                        prefabSkillSlots[j].skillFamily.variants[0].skillDef = replacement;
                        prefabSkillSlots[j].skillDef = replacement;
                        Loadout.BodyLoadoutManager.allBodyInfos[(int)bodyIndex].prefabSkillSlots[j].skillFamily.variants[0].skillDef = replacement;
                        Loadout.BodyLoadoutManager.allBodyInfos[(int)bodyIndex].prefabSkillSlots[j].skillDef = replacement;
                    }
                }
            }
        }

        Debug.Log("GGGS");
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

                GenericSkill[] prefabSkillSlots = bodyPrefab.GetComponents<GenericSkill>();
                for (int j = 0; i < prefabSkillSlots.Length; i++)
                {
                    if (prefabSkillSlots[j].skillFamily == wylderSecondary)
                    {
                        prefabSkillSlots[j].skillFamily.variants[0].skillDef = replacement;
                        prefabSkillSlots[j].skillDef = replacement;
                        Loadout.BodyLoadoutManager.allBodyInfos[(int)bodyIndex].prefabSkillSlots[j].skillFamily.variants[0].skillDef = replacement;
                        Loadout.BodyLoadoutManager.allBodyInfos[(int)bodyIndex].prefabSkillSlots[j].skillDef = replacement;
                    }
                }
            }
        }
        Debug.Log("DDM");
    }

    void QueueUpdate()
    {
        _shouldUpdate = true;
    }
}
