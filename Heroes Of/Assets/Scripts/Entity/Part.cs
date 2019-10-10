using UnityEngine;

public class Part : Entity {

    private int codPart;
    private string namPart;

    public int CodPart { get => codPart; set => codPart = value; }
    public string NamPart { get => namPart; set => namPart = value; }
    
    public Part (GameObject gameObject) : base(gameObject) {

    }

    public Part(int codPart, string namPart) : base(null) {
        this.codPart = codPart;
        this.namPart = namPart;
    }
    
}