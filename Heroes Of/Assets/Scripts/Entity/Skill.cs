using UnityEngine;

public class Skill : Entity {

    private int codSkill;
    private int codHero;
    private string namSkill;
    private string desSkill;
    private string txtAssetIdentifier;
    private int valDamage;
    private int numEffectArea;
    private int numCooldown;
    private bool idtAttributeBuff;

    // Runtime members:
    public SkillAction Action { get; set; }
    
    public Skill() {
    }

    public int CodSkill { get => codSkill; set => codSkill = value; }
    public int CodHero { get => codHero; set => codHero = value; }
    public string NamSkill { get => namSkill; set => namSkill = value; }
    public string DesSkill { get => desSkill; set => desSkill = value; }
    public string TxtAssetIdentifier { get => txtAssetIdentifier; set => txtAssetIdentifier = value; }
    public int ValDamage { get => valDamage; set => valDamage = value; }
    public int NumEffectArea { get => numEffectArea; set => numEffectArea = value; }
    public int NumCooldown { get => numCooldown; set => numCooldown = value; }
    public bool IdtAttributeBuff { get => idtAttributeBuff; set => idtAttributeBuff = value; }

    public Skill(int codSkill, int codHero, string namSkill, string desSkill, int valDamage,
        int numEffectArea, int numCooldown, bool idtAttributeBuff) {
        this.codSkill = codSkill;
        this.codHero = codHero;
        this.namSkill = namSkill;
        this.desSkill = desSkill;
        this.valDamage = valDamage;
        this.numEffectArea = numEffectArea;
        this.numCooldown = numCooldown;
        this.idtAttributeBuff = idtAttributeBuff;
    }

    public void PerformAction(Hero hero) {
        if (Action) {
            Action.PerformAction(hero);
        }
    }

}