using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveCounter : MonoBehaviour {

    private GameObject counter;
    private TextMeshProUGUI number;
    private Image image;

    private void Start() {

        number = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponent<Image>();
        counter = gameObject;

    }

    public GameObject Counter { get { return counter; } }

    public TextMeshProUGUI Number { get { return number; } }

    public Image Image { get { return image; } }

}
