using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WavePanelManager : MonoBehaviour {

    public WaveManager waveManager;
    public GameObject counterPrefab;

    public int maxWaves, actualWave;

    private int counters, actualCounterIndex;
    private ArrayList waveCounters;

    private void Awake() {

        //maxWaves = waveManager.GetMaxWaves();

        //actualWave = waveManager.GetActualWave();

        CreateCounters();

    }

    private void CreateCounters() {

        int waveCountersSize = (maxWaves >= 5) ? 5 : maxWaves;
        GameObject waveCounter;

        waveCounters = new ArrayList();

        for (int i = 0; i < waveCountersSize; i++) {

            waveCounter = Instantiate(counterPrefab, counterPrefab.transform.position, counterPrefab.transform.rotation);
            waveCounter.transform.SetParent(transform, false);

            TextMeshProUGUI waveCounterNumber = waveCounter.GetComponentInChildren<TextMeshProUGUI>();
            waveCounterNumber.text = ("" + (i + 1));

            waveCounters.Add(waveCounter);

        }

        actualCounterIndex = 0;
        ResetCounterOpacity(actualCounterIndex);

    }

    public void UpdateCounters() {

        GameObject finalCounter = (GameObject) waveCounters[waveCounters.Count - 1];
        int finalCounterNumber = int.Parse(finalCounter.GetComponentInChildren<TextMeshProUGUI>().text);

        if (actualCounterIndex < waveCounters.Count - 1) {

            ReduceCounterOpacity(actualCounterIndex);

            actualCounterIndex++;

            ResetCounterOpacity(actualCounterIndex);

        }
        else if (finalCounterNumber < maxWaves) {

            ArrayList destroyCounters = new ArrayList();

            foreach (GameObject counter in waveCounters) {

                TextMeshProUGUI counterNumber = counter.GetComponentInChildren<TextMeshProUGUI>();

                int updatedNumber = (waveCounters.Count + int.Parse(counterNumber.text));

                if (updatedNumber > maxWaves) {

                    destroyCounters.Add(counter);

                } 
                else {

                    counterNumber.text = ("" + (waveCounters.Count + int.Parse(counterNumber.text)));

                }

            }

            foreach (GameObject counter in destroyCounters) {

                waveCounters.Remove(counter);
                Destroy(counter);

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
