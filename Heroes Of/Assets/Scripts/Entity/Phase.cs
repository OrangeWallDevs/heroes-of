using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections.Generic;

public class Phase : Entity {

    private int numPhase;
    private int codPart;
    private string namPhase;
    private int valIniPlayerMoney;
    private int valIniIAMoney;
    private string idtPhaseType;

    public int NumPhase { get => numPhase; set => numPhase = value; }
    public int CodPart { get => codPart; set => codPart = value; }
    public string NamPhase { get => namPhase; set => namPhase = value; }
    public int ValIniPlayerMoney { get => valIniPlayerMoney; set => valIniPlayerMoney = value; }
    public int ValIniIAMoney { get => valIniIAMoney; set => valIniIAMoney = value; }
    public string IdtPhaseType { get => idtPhaseType; set => idtPhaseType = value; }

    // Runtime members:
    public Part Part { get; set; }
    public Score UserScore { get; set; }
    public Grid TilemapsGrid {
        get => TilemapsGrid;
        set {
            TilemapsGrid = value;
            Tilemaps = TilemapsGrid.GetComponentsInChildren<Tilemap>();
        }
    }    
    public Tilemap[] Tilemaps { get; private set; }

    public Phase() {
    }

    public Phase(int numPhase, int codPart, string namPhase, int valIniPlayerMoney,
            int valIniIAMoney, string idtPhaseType) {
        this.numPhase = numPhase;
        this.codPart = codPart;
        this.namPhase = namPhase;
        this.valIniPlayerMoney = valIniPlayerMoney;
        this.valIniIAMoney = valIniIAMoney;
        this.idtPhaseType = idtPhaseType;
    }

}