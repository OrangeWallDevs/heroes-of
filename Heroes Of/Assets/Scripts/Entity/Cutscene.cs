using System.Collections.Generic;
using UnityEngine;

public class Cutscene : Entity {

    private int codCutscene;
    private int codPart;

    public int CodCutscene { get => codCutscene; set => codCutscene = value; }
    public int CodPart { get => codPart; set => codPart = value; }

    // Runtime members:
    public List<Scene> Scenes { get; set; }

    public Cutscene(GameObject gameObject) : base(gameObject) {

    }

    public Cutscene(int codCutscene, int codPart) : base(null) {
        this.codCutscene = codCutscene;
        this.codPart = codPart;
    }
}

