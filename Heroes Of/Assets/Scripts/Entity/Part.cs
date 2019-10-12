using UnityEngine;

public class Part : Entity {

    private int codPart;
    private string namPart;

    public int CodPart { get => codPart; set => codPart = value; }
    public string NamPart { get => namPart; set => namPart = value; }
    
    public Part () {
    }

    public Part(int codPart, string namPart) {
        this.codPart = codPart;
        this.namPart = namPart;
    }
    
}