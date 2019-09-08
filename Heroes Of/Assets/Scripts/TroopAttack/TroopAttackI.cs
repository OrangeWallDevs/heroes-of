using System.Collections;
using UnityEngine;

public abstract class TroopAttackI {

    protected IsometricCharacterAnimator troopAnimations;
    protected RunTimeTroopData troopData;

    public TroopAttackI(IsometricCharacterAnimator isometricCharacterAnimations, RunTimeTroopData troopData) {

        troopAnimations = isometricCharacterAnimations;

    }

    public abstract IEnumerator Attack(RunTimeData entityData);

}
