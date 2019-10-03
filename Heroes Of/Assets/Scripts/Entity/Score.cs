using UnityEngine;

public class Score : Entity {

    private string idtGoogleAccount;
    private int numPhase;
    private int vlrRecordPoints;
    
    public Score (GameObject gameObject) : base(gameObject) {

    }

    public Score(string idtGoogleAccount, int numPhase, int vlrRecordPoints)
     : base(null){
        this.idtGoogleAccount = idtGoogleAccount;
        this.numPhase = numPhase;
        this.vlrRecordPoints = vlrRecordPoints;
    }

    public string IdtGoogleAccount { get => idtGoogleAccount; 
    set => idtGoogleAccount = value; }
    public int NumPhase { get => numPhase; set => numPhase = value; }
    public int VlrRecordPoints { get => vlrRecordPoints; 
    set => vlrRecordPoints = value; }
}