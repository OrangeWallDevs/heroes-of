using UnityEngine;

public class User : Entity {

    private string idtGoogleAccount;
    private int numCurrentPhase;
    private string namUser;

    public User(GameObject gameObject) : base(gameObject) {

    }

    public User(string idtGoogleAccount, int numCurrentPhase, string namUser)
    : base(null) {
        this.idtGoogleAccount = idtGoogleAccount;
        this.numCurrentPhase = numCurrentPhase;
        this.namUser = namUser;
    }

    public string IdtGoogleAccount { get => idtGoogleAccount;
     set => idtGoogleAccount = value; }
    public int NumCurrentPhase { get => numCurrentPhase; 
    set => numCurrentPhase = value; }
    public string NamUser { get => namUser; set => namUser = value; }
}