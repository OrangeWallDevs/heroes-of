using TMPro;
using UnityEngine;

public class ConclusionScreenManager : MonoBehaviour {

    public GameObject conclusionScreen;
    public TextMeshProUGUI screenTitle;

    public void OpenWinScreen() {

        conclusionScreen.SetActive(true);

    }

    public void OpenDefeatScreen() {

        conclusionScreen.SetActive(true);
        screenTitle.text = "Derrota";

    }

}
