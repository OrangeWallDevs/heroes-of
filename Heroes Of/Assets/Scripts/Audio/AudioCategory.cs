using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
[CreateAssetMenu(menuName="ScriptableObject/AudioCategory")]
public class AudioCategory : ScriptableObject {

    public BoolEvent AudioToggleEvent;

    public bool IsMuted {
        get; private set;
    }

    public void ToggleMute() {
        IsMuted = !IsMuted;
        AudioToggleEvent.Raise(IsMuted);
    }

}
