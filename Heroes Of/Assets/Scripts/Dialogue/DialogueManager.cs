using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public GameRuntimeData runtimeData;
    private Cutscene cutscene;
    public string cutsceneTitle; // Get on BD
    
    public LevelManager levelLoader;

    public TextMeshProUGUI textField;
    public TextMeshProUGUI titleField;
    public Image sceneImageField;
    private List<Speak> sentences;
    private int countSentences;
    private int countScenes;

    void Start() {

        Logger.Instance.PrintObject(runtimeData.User.CurrentPhase);

        foreach (Cutscene cut in runtimeData.Cutscenes) {
            if(cut.CodPart == runtimeData.User.CurrentPhase.CodPart)
                cutscene = cut;     
        }
        sentences = new List<Speak>();
        countSentences = 0;
        countScenes = 0;

        StartConversations(cutscene.Scenes[countSentences]);

        // titleField.text = cutsceneTitle;

        // StartConversation(GetComponent<Dialogue>());


    }

    private void StartConversations(Scene scene) {
        
            titleField.text = scene.DesScene;
            // sceneImageField.sprite = scene.Sprite;
            Logger.Instance.PrintObject(scene);
            
            sentences.Clear();
            sentences.AddRange(scene.Texts);
            DisplayNextSentence();
            
    }


    public void DisplayNextSentence() {

        if (countSentences >= sentences.Count) { // Check if has reach the final speech
                if(countScenes >= cutscene.Scenes.Count - 1){
                    EndDialog();
                    return;
                } else {
                    countScenes++;
                    countSentences = 0;
                    StartConversations(cutscene.Scenes[countScenes]);
                    return;
                }
        } 

        Speak actualSentence = sentences[countSentences];

        // if (actualSentence.image != null) 
        //   sceneImageField.sprite = actualSentence.image;

        // Stop the letters animation when the speech is skiped
        StopAllCoroutines();

        // Start Coroutine to write the character speech
        StartCoroutine(TypeSentence(actualSentence.TxtSpeech));

        countSentences++;

    }

    IEnumerator TypeSentence (string sentence) {

        textField.text = "";

        foreach (char letter in sentence.ToCharArray()) {

            textField.text += letter;

            yield return null;

        }

    }

    public void EndDialog() {

        // Finish the cutscene and load the fase
        countSentences = sentences.Count;
        levelLoader.LoadScene(1);
        Debug.Log("Final");

    }

}
