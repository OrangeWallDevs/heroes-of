using UnityEngine;

public class HeroHealthController : HealthController {

    public GameObject deathEffect;
    public HeroEvent heroDeathEvent;

    private RunTimeHeroData heroData;

    private void Start() {

        heroData = GetComponent<RunTimeHeroData>();

        Health = heroData.vlrHp;

    }

    public override void ReceiveDamage(int vlrDamageReceived) {

        heroData.vlrHp -= vlrDamageReceived;
        Health = heroData.vlrHp;

        if (Health <= 0) {

            heroDeathEvent.Raise(heroData);

            Die();

        }

    }

    public override void Die() {

        if (deathEffect != null) {

            Instantiate(deathEffect, transform.position, transform.rotation);

        }

        Destroy(gameObject);

    }

}
