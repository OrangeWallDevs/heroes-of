using UnityEngine;

[System.Serializable]
public class Dialogue {

    public AnimationClip[] animations;

    [TextArea(1, 40)]
    public string[] sentences;

}
