using UnityEngine;

public abstract class HealthController : MonoBehaviour {

    private float health;

    public abstract void ReceiveDamage(int vlrDamageReceived);

    public abstract void Die();

    public float Health {

        get { return health; }
        protected set { health = value; }

    }

}
