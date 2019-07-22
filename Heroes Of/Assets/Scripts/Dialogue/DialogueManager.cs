using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public string cutsceneTitle; // Get on BD

    public TextMeshProUGUI textField;
    public TextMeshProUGUI titleField;
    public Image sceneImageField;

    private ArrayList sentences;
    private int countSentences;

    void Start() {

        sentences = new ArrayList();
        countSentences = 0;

        titleField.text = cutsceneTitle;

        StartConversation(GetComponent<Dialogue>());

        //StartConversation(LoadCutsceneDialogue());

    }

    private Dialogue LoadCutsceneDialogue() {

        // titleField.text = TO:DO Get from the BD the actual cutscene title
        // Dialogue dialogue = TO:DO Get from the BD the sentences and imgs of the actual cutscene

        return null;

    }

    public void StartConversation(Dialogue dialogue) {

        sentences.Clear();
        sentences.AddRange(dialogue.speeches);

        DisplayNextSentence();

    }

    public void DisplayNextSentence() {

        if (countSentences >= sentences.Count) { // Check if has reach the final speech

            EndDialog();
            return;

        }

        Speech actualSentence = (Speech) sentences[countSentences];

        if (actualSentence.image != null) 
            sceneImageField.sprite = actualSentence.image;

        // Stop the letters animation when the speech is skiped
        StopAllCoroutines();

        // Start Coroutine to write the character speech
        StartCoroutine(TypeSentence(actualSentence.setence));

        countSentences++;

    }

    IEnumerator TypeSentence (string sentence) {

        textField.text = "";

        foreach (char letter in sentence.ToCharArray()) {

            textField.text += letter;

            yield return null;

        }

    }

    void EndDialog() {

        // Finish the cutscene and load the fase
        countSentences = sentences.Count;

        Debug.Log("Final");

    }

}
