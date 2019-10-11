using UnityEngine;

public class TowerHealthController : HealthController {

    public TowerEvent towerDestroyedEvent;
    public TowerEvent towerBeingAttackedEvent;
    public GameObject destructionEffectPrefab;

    private RunTimeTowerData towerData;

    private bool wasDestroyed;

    private void Start() {

        towerData = GetComponent<RunTimeTowerData>();

        wasDestroyed = false;
        Health = towerData.valHp;

    }

    public override void Die() {

        if (destructionEffectPrefab != null) {

            Instantiate(destructionEffectPrefab, transform.position, transform.rotation);

        }

        Destroy(gameObject);

    }

    public override void ReceiveDamage(int valDamageReceived) {

        towerData.valHp -= valDamageReceived;
        Health = towerData.valHp;

        towerBeingAttackedEvent.Raise(towerData);

        if (Health <= 0 && !wasDestroyed) {

            wasDestroyed = true;
            towerDestroyedEvent.Raise(towerData);
            Die();

        }

    }

}
