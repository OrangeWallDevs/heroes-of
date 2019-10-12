using UnityEngine;

public class Speak : Entity {

    private int codSpeak;
    private int codCutscene;
    private int codScene;
    private string txtSpeak;

    public int CodSpeech { get => codSpeak; set => codSpeak = value; }
    public int CodCutscene { get => codCutscene; set => codCutscene = value; }
    public int CodScene { get => codScene; set => codScene = value; }
    public string TxtSpeech { get => txtSpeak; set => txtSpeak = value; }

    public Speak() {
    }

    public Speak(int codSpeech, int codCutscene, int codScene, string txtSpeech,
            string txtSceneImgPath) {
        this.codSpeak = codSpeech;
        this.codCutscene = codCutscene;
        this.codScene = codScene;
        this.txtSpeak = txtSpeech;
    }
}

