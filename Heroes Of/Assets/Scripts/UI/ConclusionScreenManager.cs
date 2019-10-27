using TMPro;
using UnityEngine;

public class ConclusionScreenManager : MonoBehaviour {

    public GameObject conclusionScreen;
    public GameObject dataPainel;
    public TextMeshProUGUI screenTitle;

    public void OpenWinScreen() {

        conclusionScreen.SetActive(true);

    }

    public void OpenDefeatScreen() {

        conclusionScreen.SetActive(true);
        dataPainel.SetActive(false);
        screenTitle.text = "Derrota";

    }

}
