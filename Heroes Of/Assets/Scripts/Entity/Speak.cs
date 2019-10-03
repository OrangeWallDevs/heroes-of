using UnityEngine;

public class Speak : Entity {

    private int codSpeak;
    private int codCutscene;
    private int codScene;
    private string txtSpeak;
    private string txtSceneImgPath;

    public Speak(GameObject gameObject) : base(gameObject) {

    }

    public Speak(int codSpeech, int codCutscene, int codScene, string txtSpeech
    , string txtSceneImgPath) : base(null) {
        this.codSpeak = codSpeech;
        this.codCutscene = codCutscene;
        this.codScene = codScene;
        this.txtSpeak = txtSpeech;
        this.txtSceneImgPath = txtSceneImgPath;
    }

    public int CodSpeech { get => codSpeak; set => codSpeak = value; }
    public int CodCutscene { get => codCutscene; set => codCutscene = value; }
    public int CodScene { get => codScene; set => codScene = value; }
    public string TxtSpeech { get => txtSpeak; set => txtSpeak = value; }
    public string TxtSceneImgPath { get => txtSceneImgPath;
    set => txtSceneImgPath = value; }
}

