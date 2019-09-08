using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    public float speed = 1f;

    public GameObject collisionEffectPrefab;

    private int damage = 0;

    private void OnTriggerEnter2D(Collider2D collision) {

        Transform objectHit = collision.transform.parent;

        if (objectHit == null) {

            return;

        }

        HealthController objectHitHealthController = null;
        RunTimeTroopData x = null;

        switch (objectHit.tag) {

            case ("Troop"):

                objectHitHealthController = objectHit.GetComponent<TroopHealthController>();
                break;

            case ("Hero"):

                Debug.Log("TO:DO Create a Hero HealthController");
                break;

            case ("Tower"):

                objectHitHealthController = objectHit.GetComponent<TowerHealthController>();
                break;

        }

        if (objectHitHealthController != null) {

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

        Vector2 actualPosition = transform.position;

        while (Mathf.Abs(targetPosition.x) != Mathf.Abs(actualPosition.x) ||
            Mathf.Abs(targetPosition.y) != Mathf.Abs(actualPosition.y)) {

            actualPosition = transform.position;

            transform.position = Vector2.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

            yield return new WaitForFixedUpdate();

        }

    }

    public int Damage {

        get { return damage; }
        set { damage = value; }

    }

}
