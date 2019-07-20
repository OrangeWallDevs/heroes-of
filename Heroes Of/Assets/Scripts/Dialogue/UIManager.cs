using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject nextButton;
    public GameObject startButton;
    public GameObject sceneImage;

    public void DialogueStartUI() {

        nextButton.SetActive(true);
        sceneImage.SetActive(true);
        startButton.SetActive(false);

    }
}
