using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GameUser : Entity {

    private string idtGoogleAccount;
    private int numCurrentPhase;
    private string namUser;

    [JsonIgnore] public string IdtGoogleAccount { get => idtGoogleAccount; set => idtGoogleAccount = value; }
    [JsonIgnore] public int NumCurrentPhase { get => numCurrentPhase; set => numCurrentPhase = value; }
    [JsonIgnore] public string NamUser { get => namUser; set => namUser = value; }

    // Runtime members:
    [JsonIgnore] public Phase CurrentPhase { get; set; }

    public GameUser() {
    }

    public GameUser(string idtGoogleAccount, int numCurrentPhase, string namUser) {
        this.idtGoogleAccount = idtGoogleAccount;
        this.numCurrentPhase = numCurrentPhase;
        this.namUser = namUser;
    }

    public override string ToString() {
        return $"id: {IdtGoogleAccount}\ncurrentPhase: {numCurrentPhase}\nname: {namUser}";
    }

}