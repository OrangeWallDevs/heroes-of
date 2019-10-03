using UnityEngine;

public class Main : MonoBehaviour {

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void LoadMain() {
        GameObject main = GameObject.Instantiate(Resources.Load("Main")) as GameObject;
        
        GameObject.DontDestroyOnLoad(main);
    }

}
