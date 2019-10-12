using UnityEngine;

public class Score : Entity {

    private string idtGoogleAccount;
    private int numPhase;
    private int valRecordPoints;

    public string IdtGoogleAccount { get => idtGoogleAccount; set => idtGoogleAccount = value; }
    public int NumPhase { get => numPhase; set => numPhase = value; }
    public int ValRecordPoints { get => valRecordPoints; set => valRecordPoints = value; }
    
    public Score () {
    }

    public Score(string idtGoogleAccount, int numPhase, int valRecordPoints) {
        this.idtGoogleAccount = idtGoogleAccount;
        this.numPhase = numPhase;
        this.valRecordPoints = valRecordPoints;
    }
}