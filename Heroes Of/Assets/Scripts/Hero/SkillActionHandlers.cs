using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillActionHandlers : ScriptableObject {
    
    public GameRuntimeData runtimeData;

    // Skill actions:

    public void TestSkill(Hero hero) {
        hero.ValAttackSpeed += 1;
    }

}
