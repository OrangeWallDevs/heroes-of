using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController : MonoBehaviour {

    public AudioCategory Category {
        set {
            if (_category) {
                _category.AudioToggleEvent.UnregisterListener(ToggleMute);
            }
            _category = value;
            _category.AudioToggleEvent.RegisterListener(ToggleMute);
        }
    }

    public Vector3 Position {
        set { _position = value; }
    }

    public Transform ParentTransform {
        set {
            _parentTransform = value;
        }
    }

    AudioSource _source;
    AudioCategory _category;
    Vector3 _position;
    Transform _transform;
    Transform _parentTransform;
    bool _claimed;

    void Awake() {
        _transform = transform;
        _source = GetComponent<AudioSource>();
        if (!_source) {
            // throw new System.Exception("Audio Source Controller is missing the corresponding Audio Source component");
            _source = gameObject.AddComponent<AudioSource>();
        }
        _source.dopplerLevel = 0;
    }

    void Start() {
        if (!_source.clip) {
            Debug.LogWarning("Nenhum clipe informado.");
        }
    }

    void LateUpdate() {
        if (_claimed && !_source.isPlaying) {
            Stop();
            return;
        }
        if (_parentTransform) {
            _transform.position = _parentTransform.position;
        }
    }

    public void SetSourceProperties(AudioClip clip, float volume, float picth, bool loop, float spacialBlend) {
        _source.clip = clip;
        _source.volume = volume;
        _source.pitch = picth;
        _source.loop = loop;
        _source.spatialBlend = spacialBlend;
    }

    public void Play() {
        _transform.position = _position != null ? _position : Camera.main.transform.position;
        _claimed = true;
        _source.Play();
    }

    public void Stop() {
        _source.Stop();
        Reset();
        AudioPoolManager.Instance.PutController(this);
    }

    void Reset() {
        _parentTransform = null;
        _claimed = false;
    }

    void ToggleMute(bool isMuted) {
        _source.mute = !_source.mute;
    }

}
