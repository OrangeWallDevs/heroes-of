using UnityEngine;

public class TowerHealthController : HealthController {

    public TowerEvent towerDestroyedEvent;
    public TowerEvent towerBeingAttackedEvent;
    public GameEvent waveEndEvent;
    public GameObject destructionEffectPrefab;

    private RunTimeTowerData towerData;

    private void Start() {

        towerData = GetComponent<RunTimeTowerData>();

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

        if (Health <= 0) {

            towerDestroyedEvent.Raise(towerData);
            waveEndEvent.Raise();
            Die();

        }

    }

}
