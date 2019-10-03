using UnityEngine;
public class Skill : Entity {

    private int codSkill;
    private int codHero;
    private string namSkill;
    private string desSkill;
    private int vlrDamage;
    private int numEffectArea;
    private int numCooldown;
    private bool idtAttributeBuff;
    
    public Skill(GameObject gameObject) : base(gameObject) {

    }

    public Skill(int codSkill, int codHero, string namSkill, string desSkill
    , int vlrDamage, int numEffectArea, int numCooldown, bool idtAttributeBuff)
    : base(null) {
        this.codSkill = codSkill;
        this.codHero = codHero;
        this.namSkill = namSkill;
        this.desSkill = desSkill;
        this.vlrDamage = vlrDamage;
        this.numEffectArea = numEffectArea;
        this.numCooldown = numCooldown;
        this.idtAttributeBuff = idtAttributeBuff;
    }

    public int CodSkill { get => codSkill; set => codSkill = value; }
    public int CodHero { get => codHero; set => codHero = value; }
    public string NamSkill { get => namSkill; set => namSkill = value; }
    public string DesSkill { get => desSkill; set => desSkill = value; }
    public int VlrDamage { get => vlrDamage; set => vlrDamage = value; }
    public int NumEffectArea { get => numEffectArea; 
    set => numEffectArea = value; }
    public int NumCooldown { get => numCooldown; set => numCooldown = value; }
    public bool IdtAttributeBuff { get => idtAttributeBuff; 
    set => idtAttributeBuff = value; }
}