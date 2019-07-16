using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPoolManager : Singleton<AudioPoolManager> {

    List<AudioSourceController> _pool = new List<AudioSourceController>();

    public AudioSourceController GetController() {
        AudioSourceController output;

        if (_pool.Count > 0) {
            output = _pool[0];
            _pool.Remove(output);
        } else {
            GameObject goController = new GameObject("AudioController");
            output = goController.AddComponent<AudioSourceController>();
        }

        return output;
    }

    public void PutController(AudioSourceController controller) {
        if (!_pool.Contains(controller)) {
            _pool.Add(controller);
        }
    }

}
