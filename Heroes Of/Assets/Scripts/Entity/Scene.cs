using UnityEngine;

public class Scene : Entity {

    private int codCutscene;
    private int codScene;
    private string desScene;

    public Scene(GameObject gameObject) : base(gameObject) {

    }

    public Scene(int codCutscene, int codScene, string desScene) : base(null) {
        this.codCutscene = codCutscene;
        this.codScene = codScene;
        this.desScene = desScene;
    }

    public int CodCutscene { get => codCutscene; set => codCutscene = value; }
    public int CodScene { get => codScene; set => codScene = value; }
    public string DesScene { get => desScene; set => desScene = value; }

    public override string ToString() {
        return $"cutscene: {codCutscene}, scene: {codScene}, desScene: {desScene}";
    }

}

