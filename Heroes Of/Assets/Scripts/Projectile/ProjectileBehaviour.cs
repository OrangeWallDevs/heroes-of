using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    public float speed = 1f;

    public GameObject collisionEffectPrefab;

    private int damage = 0;
    private bool hadCollided = false;

    private void OnTriggerEnter2D(Collider2D collision) {

        Transform objectHit = collision.transform.parent;

        if (objectHit == null) {

            return;

        }

        HealthController objectHitHealthController = null;

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

        Vector2 actualPosition = transform.position;

        while (Mathf.Ceil(Mathf.Abs(targetPosition.x)) != Mathf.Ceil(Mathf.Abs(actualPosition.x)) ||
            Mathf.Ceil(Mathf.Abs(targetPosition.y)) != Mathf.Ceil(Mathf.Abs(actualPosition.y))) {

            actualPosition = transform.position;

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
