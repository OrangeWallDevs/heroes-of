using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName="ScriptableObject/GameEvent")]
public class GameEvent : ScriptableObject {

    List<GameEventListener> gameEventListeners = new List<GameEventListener>();
    List<UnityAction> simpleRuntimeListeners = new List<UnityAction>();

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

}

/* Parameterized Events Preset */

[CreateAssetMenu(menuName="ScriptableObject/IntEvent")]
public class IntEvent : GameEvent<int> {}

[CreateAssetMenu(menuName="ScriptableObject/FloatEvent")]
public class FloatEvent : GameEvent<float> {}

[CreateAssetMenu(menuName="ScriptableObject/StringEvent")]
public class StringEvent : GameEvent<string> {}

[CreateAssetMenu(menuName="ScriptableObject/BoolEvent")]
public class BoolEvent : GameEvent<bool> {}

[CreateAssetMenu(menuName="ScriptableObject/ObjectEvent")]
public class ObjectEvent : GameEvent<object> {}