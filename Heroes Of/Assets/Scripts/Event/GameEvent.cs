using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName="ScriptableObject/GameEvent")]
public class GameEvent : ScriptableObject {

    List<GameEventListener> gameEventListeners = new List<GameEventListener>();
    List<UnityAction> simpleRuntimeListeners = new List<UnityAction>();

    private void OnEnable() {

        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode) {

        ClearListeners();

    }

    public void Raise() {
        for (int i = gameEventListeners.Count - 1; i >= 0; i--) {
            gameEventListeners[i].OnEventRaised();
        }
        for (int i = simpleRuntimeListeners.Count - 1; i >= 0; i--) {
            simpleRuntimeListeners[i]();
        }
    }

    public void RegisterListener(GameEventListener listener) {
        gameEventListeners.Add(listener);
    }

    public void RegisterListener(UnityAction action) {
        simpleRuntimeListeners.Add(action);
    }

    public void UnregisterListener(GameEventListener listener) {
        gameEventListeners.Remove(listener);
    }

    public void UnregisterListener(UnityAction action) {
        simpleRuntimeListeners.Remove(action);
    }

    public void ClearListeners() {

        gameEventListeners.Clear();
        simpleRuntimeListeners.Clear();

    }

    public int ListenersCount {
        get { return gameEventListeners.Count + simpleRuntimeListeners.Count; }
    }

}

public class GameEvent<T> : ScriptableObject {

    List<UnityAction<T>> simpleRuntimeListeners = new List<UnityAction<T>>();

    public void Raise(T obj) {
        for (int i = simpleRuntimeListeners.Count - 1; i >= 0; i--) {
            simpleRuntimeListeners[i](obj);
        }
    }

    public void RegisterListener(UnityAction<T> action) {
        simpleRuntimeListeners.Add(action);
    }

    public void UnregisterListener(UnityAction<T> action) {
        simpleRuntimeListeners.Remove(action);
    }

    public int ListenersCount {
        get { return simpleRuntimeListeners.Count; }
    }

}