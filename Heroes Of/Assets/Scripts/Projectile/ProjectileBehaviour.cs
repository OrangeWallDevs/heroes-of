using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    public float speed = 1f;

    public GameObject collisionEffectPrefab;

    private int damage = 0;
    private bool hadCollided = false;
    private Vector2 lastPosition;

    private void OnTriggerEnter2D(Collider2D collision) {

        Transform objectHit = collision.transform.parent;

        if (objectHit == null) {

            return;

        }

        HealthController objectHitHealthController = objectHit.GetComponent<HealthController>();

        if (objectHitHealthController != null) {

            hadCollided = true;
            handleHitDamage(objectHitHealthController);

        }

    }

    private void handleHitDamage(HealthController healthController) {

        if (healthController != null) {

            healthController.ReceiveDamage(damage);

        }

        if (collisionEffectPrefab != null) {

            Instantiate(collisionEffectPrefab, transform.position, transform.rotation);

        } 

        Destroy(gameObject);

    }

    public void MoveToTarget(Vector2 targetPosition) {

        StartCoroutine(MoveTowardsTarget(targetPosition));

    }

    private IEnumerator MoveTowardsTarget(Vector2 targetPosition) {

        while (Mathf.Abs(lastPosition.x - transform.position.x) >= 0.008f || 
            Mathf.Abs(lastPosition.y - transform.position.y) >= 0.008f) {

            lastPosition = transform.position;

            transform.position = Vector2.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

            yield return new WaitForFixedUpdate();

        }

        if(!hadCollided) {

            Destroy(gameObject);

        }

    }

    public int Damage {

        get { return damage; }
        set { damage = value; }

    }

}
