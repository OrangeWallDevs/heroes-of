
using UnityEngine;

[CreateAssetMenu]
public class SkillAction : ScriptableObject {
    
    public HeroUnityEvent action;

    public void PerformAction(Hero hero) {
        action.Invoke(hero);
    }

}
