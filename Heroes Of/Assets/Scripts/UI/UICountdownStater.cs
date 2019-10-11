using System.Collections;
using TMPro;
using UnityEngine;

public class UICountdownStater : MonoBehaviour {

    public GameObject UICounter;
    public GameEvent startCountDownEvent;

    private WaveManager waveManager;
    private Animator animator;
    private TextMeshProUGUI counterText;

    private void Awake() {

        waveManager = GameObject.FindGameObjectWithTag("Wave_Manager").GetComponent<WaveManager>();
        counterText = UICounter.GetComponentInChildren<TextMeshProUGUI>();
        animator = UICounter.GetComponent<Animator>();

    }

    private void Start() {

        startCountDownEvent.RegisterListener(UpdateCounter);
        
    }

    private void UpdateCounter() {

        UICounter.SetActive(true);

        float countDownValue = Mathf.Floor(waveManager.CountDown);

        if (countDownValue <= 0) {

            counterText.text = "GO!";

            StartCoroutine(HideCounter());

        }
        else {

            counterText.text = countDownValue.ToString();

        }

    }

    private IEnumerator HideCounter() {

        yield return new WaitForSeconds(1f);

        animator.Play("PopUp_Out");

        yield return new WaitForSeconds(1f);

        UICounter.SetActive(false);

    }
    
}
