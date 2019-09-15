using UnityEngine;

public class TroopAttackActions : MonoBehaviour {

    public GameObject projectilePrefab = null;
    public Transform shotingPoint = null;

    private Coroutine attackCoroutine = null;

    private RunTimeTroopData troopData;
    private IsometricCharacterAnimator troopAnimations;

    private TroopAttackI attackType;

    void Start() {

        troopData = GetComponent<RunTimeTroopData>();
        troopAnimations = GetComponentInChildren<IsometricCharacterAnimator>();

        if (troopData.attackAtDistance) {

            attackType = new RangeAttack(troopAnimations, troopData, projectilePrefab, shotingPoint);

        }
        else {

            attackType = new PhysicalAttack(troopAnimations, troopData);

        }
        
    }

    public void Attack(RunTimeData entityData) {

        Attack(entityData.GameObject);

    }

    public void Attack(GameObject entity) {

        AttackCoroutine = attackType.Attack(entity);

    }

    public void StopAttack() {

        StopCoroutine(AttackCoroutine);
        AttackCoroutine = null;

    }

    public Coroutine AttackCoroutine {

        get { return attackCoroutine; }
        private set { attackCoroutine = value; }

    }

    public bool IsAttacking {

        get { return AttackCoroutine != null; }

    } 

}
