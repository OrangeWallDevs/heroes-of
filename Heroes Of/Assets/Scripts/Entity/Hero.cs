using System.Collections.Generic;
using UnityEngine;

public class Hero : Entity {

    private int codHero;
    private int codPart;
    private string namHero;
    private string desHero;
    private string txtAssetIdentifier;
    private int valHp;
    private int valScore;
    private int valDamageDealt;
    private int valMotionSpeed;
    private int valAttackSpeed;
    private int valDropMoney;

    public int CodHero { get => codHero; set => codHero = value; }
    public int CodPart { get => codPart; set => codPart = value; }
    public string NamHero { get => namHero; set => namHero = value; }
    public string DesHero { get => desHero; set => desHero = value; }
    public string TxtAssetIdentifier { get => txtAssetIdentifier; set => txtAssetIdentifier = value; }
    public int ValHp { get => valHp; set => valHp = value; }
    public int ValScore { get => valScore; set => valScore = value; }
    public int ValDamageDealt { get => valDamageDealt; set => valDamageDealt = value; }
    public int ValMotionSpeed { get => valMotionSpeed; set => valMotionSpeed = value; }
    public int ValAttackSpeed { get => valAttackSpeed; set => valAttackSpeed = value; }
    public int ValDropMoney { get => valDropMoney; set => valDropMoney = value; }

    // Runtime members?
    public Part Part { get; set; }
    public List<Skill> Skills { get; set; }

    public Hero(GameObject gameObject) : base(gameObject) {

    }

    public Hero(int codHero, int codPart, string namHero, string desHero
    , int valHp, int valScore, int valDamageDealt, int valMotionSpeed
    , int valAttackSpeed, int valDropMoney) : base(null) {
        this.codHero = codHero;
        this.codPart = codPart;
        this.namHero = namHero;
        this.desHero = desHero;
        this.valHp = valHp;
        this.valScore = valScore;
        this.valDamageDealt = valDamageDealt;
        this.valMotionSpeed = valMotionSpeed;
        this.valAttackSpeed = valAttackSpeed;
        this.valDropMoney = valDropMoney;
    }
}