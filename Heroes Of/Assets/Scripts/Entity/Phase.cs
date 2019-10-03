using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections.Generic;

public class Phase : Entity {

    private int numPhase;
    private int codPart;
    private string namPhase;
    private int vlrIniPlayerMoney;
    private int vlrIniIAMoney;
    private string idtPhaseType;

    public Grid TilemapsGrid { get; private set; }
    
    public Tilemap[] Tilemaps { get; private set; }

    public Phase(Grid tilemapsGrid) : base(null) {
        TilemapsGrid = tilemapsGrid;
        Tilemaps = tilemapsGrid.GetComponentsInChildren<Tilemap>();
    }

    public Phase(int numPhase, int codPart, string namPhase,
     int vlrIniPlayerMoney, int vlrIniIAMoney, string idtPhaseType) : base(null) {
        this.numPhase = numPhase;
        this.codPart = codPart;
        this.namPhase = namPhase;
        this.vlrIniPlayerMoney = vlrIniPlayerMoney;
        this.vlrIniIAMoney = vlrIniIAMoney;
        this.idtPhaseType = idtPhaseType;
    }

    public int NumPhase { get => numPhase; set => numPhase = value; }
    public int CodPart { get => codPart; set => codPart = value; }
    public string NamPhase { get => namPhase; set => namPhase = value; }
    public int VlrIniPlayerMoney { get => vlrIniPlayerMoney; 
    set => vlrIniPlayerMoney = value; }
    public int VlrIniIAMoney { get => vlrIniIAMoney; set => vlrIniIAMoney = value; }
    public string IdtPhaseType { get => idtPhaseType; set => idtPhaseType = value; }
}