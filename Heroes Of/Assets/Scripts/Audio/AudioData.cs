using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObject/AudioData")]
public class AudioData : ScriptableObject {

    public AudioClip Audio;
    public List<AudioClip> AlternativeAudios = new List<AudioClip>();
    public AudioCategory Category;

    // Audio properties:
	public bool loop;
    [Range(0, 1)] public float volume = 1f;
	[Range(.25f, 3)] public float pitch = 1f;
	[Range(0f, 1f)] public float spacialBlend = 1f;

    public AudioSourceController Play() {
        AudioSourceController controller = AudioPoolManager.Instance.GetController();

        controller.SetSourceProperties(GetClip(), volume, pitch, loop, spacialBlend);
        controller.Category = Category;
        controller.Play();

        return controller;
    }

    public AudioSourceController Play(Vector3 position) {
        AudioSourceController controller = AudioPoolManager.Instance.GetController();

        controller.SetSourceProperties(GetClip(), volume, pitch, loop, spacialBlend);
        controller.Position = position;
        controller.Category = Category;
        controller.Play();

        return controller;
    }

    public AudioSourceController Play(Transform parent) {
        AudioSourceController controller = Play(parent.position);
        controller.ParentTransform = parent;
        return controller;
    }

    AudioClip GetClip() {
        if (!Audio && AlternativeAudios.Count == 0) {
            Debug.LogWarning("AudioData does not contain any clip.");
            return null;
        }
        
        if (AlternativeAudios.Count > 0) {
            if (Audio) {
                int index = Random.Range(0, AlternativeAudios.Count + 1);
                return index == AlternativeAudios.Count ? Audio : AlternativeAudios[index];
            }
            return AlternativeAudios[Random.Range(0, AlternativeAudios.Count)];
            
        }

        return Audio;
    }

}
