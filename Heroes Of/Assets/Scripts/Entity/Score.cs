using UnityEngine;
using Newtonsoft.Json;

public class Score : Entity {

    private string idtGoogleAccount;
    private int numPhase;
    private int valRecordPoints;

    [JsonIgnore] public string IdtGoogleAccount { get => idtGoogleAccount; set => idtGoogleAccount = value; }
    [JsonIgnore] public int NumPhase { get => numPhase; set => numPhase = value; }
    [JsonIgnore] public int ValRecordPoints { get => valRecordPoints; set => valRecordPoints = value; }
    
    public Score () {
    }

    public Score(string idtGoogleAccount, int numPhase, int valRecordPoints) {
        this.idtGoogleAccount = idtGoogleAccount;
        this.numPhase = numPhase;
        this.valRecordPoints = valRecordPoints;
    }
}