using UnityEngine;

public class TroopHealthController : HealthController {

    public TroopEvent troopDeathEvent;
    public GameObject deathEffect;

    private RunTimeTroopData troopData;

    private void Start() {

        troopData = GetComponent<RunTimeTroopData>();

        Health = troopData.vlrHp;

    }

    public override void Die() {

        if (deathEffect != null) {

            Instantiate(deathEffect, transform.position, transform.rotation);

        }

        Destroy(gameObject);

    }

    public override void ReceiveDamage(int vlrDamageReceived) {

        troopData.vlrHp -= vlrDamageReceived;
        Health = troopData.vlrHp;

        if (troopData.vlrHp <= 0) {

            troopDeathEvent.Raise(troopData);
            Die();

        }

    }

}
