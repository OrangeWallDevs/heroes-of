using System.Collections;
using UnityEngine;

public class WavePanelManager : MonoBehaviour {

    public WaveManager waveManager;
    public GameObject counterPrefab;

    public int maxWaves, actualWave;

    private int actualCounterIndex;
    private ArrayList waveCounters;

    private void Awake() {

        //maxWaves = waveManager.GetMaxWaves();

        //actualWave = waveManager.GetActualWave();

        CreateCounters();

    }

    private void CreateCounters() {

        waveCounters = new ArrayList();

        int waveCountersSize = (maxWaves >= 5) ? 5 : maxWaves;

        for (int i = 0; i < waveCountersSize; i++) {

            GameObject newCounter = Instantiate(counterPrefab, counterPrefab.transform.position, counterPrefab.transform.rotation);
            WaveCounterUIElements waveCounter = newCounter.GetComponent<WaveCounterUIElements>();

            newCounter.transform.SetParent(transform, false);
            waveCounter.Number.text = ("" + (i + 1));

            waveCounters.Add(waveCounter);

        }

        actualCounterIndex = 0;
        ResetCounterOpacity(actualCounterIndex);

    }

    public void UpdateCounters() {

        WaveCounterUIElements finalCounter = (WaveCounterUIElements) waveCounters[waveCounters.Count - 1];
        int finalCounterNumber = int.Parse(finalCounter.Number.text);

        if (actualCounterIndex < waveCounters.Count - 1) { // Didn't reach the end

            ReduceCounterOpacity(actualCounterIndex);

            actualCounterIndex++;

            ResetCounterOpacity(actualCounterIndex);

        }
        else if (finalCounterNumber < maxWaves) { // Has reached the end but not the final wave

            ArrayList destroyCounters = new ArrayList();

            foreach (WaveCounterUIElements counter in waveCounters) {

                int updatedNumber = (waveCounters.Count + int.Parse(counter.Number.text));

                if (updatedNumber > maxWaves) {
                    destroyCounters.Add(counter);
                } else {
                    counter.Number.text = "" + updatedNumber;
                }

            }

            foreach (WaveCounterUIElements counter in destroyCounters) {

                waveCounters.Remove(counter);
                Destroy(counter.CounterObject);

            }

            actualCounterIndex = 0;
            ReduceCounterOpacity(waveCounters.Count - 1);
            ResetCounterOpacity(actualCounterIndex);

        }

    }

    private void ReduceCounterOpacity(int i) {

        WaveCounterUIElements counter = (WaveCounterUIElements) waveCounters[i];

        counter.Image.color = new Color(255, 255, 255, 0.7f);

    }

    private void ResetCounterOpacity(int i) {

        WaveCounterUIElements counter = (WaveCounterUIElements) waveCounters[i];

        counter.Image.color = new Color(255, 255, 255, 1f);

    }

}
