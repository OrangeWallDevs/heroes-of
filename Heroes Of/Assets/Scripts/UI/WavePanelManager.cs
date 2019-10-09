using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WavePanelManager : MonoBehaviour {

    public GameObject counterPrefab;
    public GameEvent waveEndEvent;

    public int maxCountersDisplay;
    private ArrayList waveCounters;

    private WaveManager waveManager;
    private int actualCounterIndex, maxWaves;

    private struct WaveCounterUIElements {

        public GameObject counter;
        public TextMeshProUGUI number;
        public Image image;

    }

    private void Awake() {

        waveManager = GameObject.FindGameObjectWithTag("Wave_Manager").GetComponent<WaveManager>();

        maxWaves = waveManager.maxWaves;

        CreateCounters();

        waveEndEvent.RegisterListener(UpdateCounters);

    }

    private void CreateCounters() {

        waveCounters = new ArrayList();

        int waveCountersSize = (maxWaves >= maxCountersDisplay) ? maxCountersDisplay : maxWaves;

        for (int i = 0; i < waveCountersSize; i++) {

            WaveCounterUIElements waveCounter = new WaveCounterUIElements();

            GameObject newCounter = Instantiate(counterPrefab, counterPrefab.transform.position, counterPrefab.transform.rotation);

            waveCounter.counter = newCounter;
            waveCounter.image = newCounter.GetComponent<Image>();
            waveCounter.number = newCounter.GetComponentInChildren<TextMeshProUGUI>();

            newCounter.transform.SetParent(transform, false);
            waveCounter.number.text = ("" + (i + 1));

            waveCounters.Add(waveCounter);

        }

        actualCounterIndex = 0;
        ResetCounterOpacity(actualCounterIndex);

    }

    public void UpdateCounters() {

        WaveCounterUIElements finalCounter = (WaveCounterUIElements) waveCounters[waveCounters.Count - 1];
        int finalCounterNumber = int.Parse(finalCounter.number.text);

        if (actualCounterIndex < waveCounters.Count - 1) { // Didn't reach the end

            ReduceCounterOpacity(actualCounterIndex);

            actualCounterIndex++;

            ResetCounterOpacity(actualCounterIndex);

        }
        else if (finalCounterNumber < maxWaves) { // Has reached the end but not the final wave

            ArrayList destroyCounters = new ArrayList();

            foreach (WaveCounterUIElements counter in waveCounters) {

                int updatedNumber = (waveCounters.Count + int.Parse(counter.number.text));

                if (updatedNumber > maxWaves) {
                    destroyCounters.Add(counter);
                } 
                else {
                    counter.number.text = "" + updatedNumber;
                }

            }

            foreach (WaveCounterUIElements counter in destroyCounters) {

                waveCounters.Remove(counter);
                Destroy(counter.counter);

            }

            actualCounterIndex = 0;
            ReduceCounterOpacity(waveCounters.Count - 1);
            ResetCounterOpacity(actualCounterIndex);

        }

    }

    private void ReduceCounterOpacity(int i) {

        WaveCounterUIElements counter = (WaveCounterUIElements) waveCounters[i];

        counter.image.color = new Color(255, 255, 255, 0.7f);

    }

    private void ResetCounterOpacity(int i) {

        WaveCounterUIElements counter = (WaveCounterUIElements) waveCounters[i];

        counter.image.color = new Color(255, 255, 255, 1f);

    }

}
