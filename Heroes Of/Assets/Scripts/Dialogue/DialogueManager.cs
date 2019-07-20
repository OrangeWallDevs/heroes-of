using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    private ArrayList sentences;
    private ArrayList animations;
    private int countSentences;
    private int indexAnimation;

    public Text sceneText;
    public Text sceneTitle;
    public UIManager managerUI;
    public Animator sceneImageAnimator;

    private DialogueManager() { }

    void Start() {

        sentences = new ArrayList();
        animations = new ArrayList();
        countSentences = 0;
        indexAnimation = 0;

    }

    public void StartConversation(Dialogue dialogue) {

        Debug.Log("Starting conversation");

        managerUI.DialogueStartUI();

        // Adiciona as falas e imagens ao ArrayList
        sentences.Clear();
        sentences.AddRange(dialogue.sentences);

        animations.Clear();
        animations.AddRange(dialogue.animations);

        // Começa a exibir as falas
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {

        // Caso tenha atingindo o final do ArrayList finaliza o dialogo
        if (countSentences >= sentences.Count) {

            EndDialog();
            return;

        }

        // Troca a imagem exibida a cada 12 falas
        if (countSentences % 12 == 0) {

            AnimationClip animation = (AnimationClip) animations[indexAnimation];

            sceneImageAnimator.Play(animation.name, -1);
            indexAnimation++;

        }

        // Atualiza o título de acordo com o precept do Zote
        sceneTitle.text = "Precept " + ((countSentences <= 2) ? 1 : countSentences - 1);

        string sentence = sentences[countSentences].ToString();

        // Evita de continuar animando as letras depois de ter pulado a fala
        StopAllCoroutines();

        // Inicia a escrita de cada letra no Text da UI
        StartCoroutine(TypeSentence(sentence));

        // Incrementa para exibir a próxima fala
        countSentences++;
    }

    IEnumerator TypeSentence (string sentence) {

        sceneText.text = "";

        // Transforma a string em um array de char
        foreach (char letter in sentence.ToCharArray()) {

            sceneText.text += letter;

            // Faz a função "esperar"
            yield return null;

        }

    }

    void EndDialog() {

        Debug.Log("Final");

        // Re-inicia o dialogo do Zote
        countSentences = 2;
        indexAnimation = 1;

    }

}
