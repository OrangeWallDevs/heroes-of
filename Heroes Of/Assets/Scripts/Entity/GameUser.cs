using System.Collections.Generic;
using UnityEngine;

public class GameUser : Entity {

    private string idtGoogleAccount;
    private int numCurrentPhase;
    private string namUser;

    public string IdtGoogleAccount { get => idtGoogleAccount; set => idtGoogleAccount = value; }
    public int NumCurrentPhase { get => numCurrentPhase; set => numCurrentPhase = value; }
    public string NamUser { get => namUser; set => namUser = value; }

    // Runtime members:
    public Phase CurrentPhase { get; set; }

    public GameUser(GameObject gameObject) : base(gameObject) {

    }

    public GameUser(string idtGoogleAccount, int numCurrentPhase, string namUser)
    : base(null) {
        this.idtGoogleAccount = idtGoogleAccount;
        this.numCurrentPhase = numCurrentPhase;
        this.namUser = namUser;
    }

}