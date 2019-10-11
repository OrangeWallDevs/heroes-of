using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreHealthController : HealthController {

    public GameEvent coreDestroyedEvent;
    public GameObject destructionEffectPrefab;

    private RunTimeCoreData coreData;
    private bool wasDestroyed;

    private void Start() {

        coreData = GetComponent<RunTimeCoreData>();
        wasDestroyed = false;

        Health = coreData.valHp;

    }

    public override void Die() {

        if (destructionEffectPrefab != null) {

            Instantiate(destructionEffectPrefab, transform.position, transform.rotation);

        }

        Destroy(gameObject);

    }

    public override void ReceiveDamage(int valDamageReceived) {

        coreData.valHp -= valDamageReceived;
        Health = coreData.valHp;

        if (!wasDestroyed && Health <= 0) {

            wasDestroyed = true;
            coreDestroyedEvent.Raise();
            Die();

        }

    }

}
