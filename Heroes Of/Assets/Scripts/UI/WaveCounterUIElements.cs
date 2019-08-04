using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveCounterUIElements : MonoBehaviour {

    private GameObject counter;
    private TextMeshProUGUI number;
    private Image image;

    private void Awake() {

        number = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponent<Image>();
        counter = gameObject;

    }

    public GameObject CounterObject { get { return counter; } }

    public TextMeshProUGUI Number { get { return number; } }

    public Image Image { get { return image; } }

}
