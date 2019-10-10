using System.Collections.Generic;
using UnityEngine;

public class Scene : Entity {

    private int codCutscene;
    private int codScene;
    private string desScene;
    private string txtImagePath;

    public int CodCutscene { get => codCutscene; set => codCutscene = value; }
    public int CodScene { get => codScene; set => codScene = value; }
    public string DesScene { get => desScene; set => desScene = value; }
    public string TxtImagePath { get => txtImagePath; set => txtImagePath = value; }

    // Runtime members:
    public List<Speak> Texts { get; set; }

    public Scene(GameObject gameObject) : base(gameObject) {

    }

    public Scene(int codCutscene, int codScene, string desScene) : base(null) {
        this.codCutscene = codCutscene;
        this.codScene = codScene;
        this.desScene = desScene;
    }

}

