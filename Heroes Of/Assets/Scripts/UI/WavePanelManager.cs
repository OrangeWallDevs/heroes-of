using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WavePanelManager : MonoBehaviour {

    public WaveManager waveManager;
    public GameObject counterPrefab;

    public int maxWaves, actualWave;

    private int counters, actualCounterIndex;
    private ArrayList waveCounters, waveCountersText;

    private void Awake() {

        //maxWaves = waveManager.GetMaxWaves();

        //actualWave = waveManager.GetActualWave();

        CreateCounters();

    }

    private void CreateCounters() {

        waveCounters = new ArrayList();
        waveCountersText = new ArrayList();

        int waveCountersSize = (maxWaves >= 5) ? 5 : maxWaves;

        for (int i = 0; i < waveCountersSize; i++) {

            GameObject waveCounter = Instantiate(counterPrefab, counterPrefab.transform.position, counterPrefab.transform.rotation);
            TextMeshProUGUI waveCounterNumber = waveCounter.GetComponentInChildren<TextMeshProUGUI>();

            waveCounter.transform.SetParent(transform, false);
            waveCounterNumber.text = ("" + (i + 1));

            waveCounters.Add(waveCounter);
            waveCountersText.Add(waveCounterNumber);

        }

        actualCounterIndex = 0;
        ResetCounterOpacity(actualCounterIndex);

    }

    public void UpdateCounters() {

        TextMeshProUGUI finalCounterText = (TextMeshProUGUI) waveCountersText[waveCountersText.Count - 1];
        int finalCounterNumber = int.Parse(finalCounterText.text);

        if (actualCounterIndex < waveCounters.Count - 1) { // Didn't reach the end

            ReduceCounterOpacity(actualCounterIndex);

            actualCounterIndex++;

            ResetCounterOpacity(actualCounterIndex);

        }
        else if (finalCounterNumber < maxWaves) { // Has reached the end but not the final wave

            ArrayList destroyCounters = new ArrayList();

            for (int i = 0; i < waveCounters.Count; i++) {

                TextMeshProUGUI waveCounterNumber = (TextMeshProUGUI) waveCountersText[i];

                int updatedNumber = (waveCounters.Count + int.Parse(waveCounterNumber.text));

                if (updatedNumber > maxWaves) {
                    destroyCounters.Add(i);
                }
                else {
                    waveCounterNumber.text = "" + updatedNumber;
                }

            }

            for (int i = destroyCounters.Count - 1; i >= 0; i--) {

                int counterIndex = (int) destroyCounters[i];

                GameObject waveCounter = (GameObject) waveCounters[counterIndex];
                TextMeshProUGUI waveCounterNumber = (TextMeshProUGUI) waveCountersText[counterIndex];

                waveCounters.Remove(waveCounter);
                waveCountersText.Remove(waveCounterNumber);

                Destroy(waveCounter);

            }

            actualCounterIndex = 0;
            ReduceCounterOpacity(waveCounters.Count - 1);
            ResetCounterOpacity(actualCounterIndex);

        }

    }

    private void ReduceCounterOpacity(int i) {

        GameObject counter = (GameObject) waveCounters[i];

        counter.GetComponent<Image>().color = new Color(255, 255, 255, 0.7f);

    }

    private void ResetCounterOpacity(int i) {

        GameObject counter = (GameObject) waveCounters[i];

        counter.GetComponent<Image>().color = new Color(255, 255, 255, 1f);

    }

}
