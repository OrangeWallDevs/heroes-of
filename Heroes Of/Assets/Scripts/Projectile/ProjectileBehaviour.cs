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

        switch (objectHit.tag) {

            case ("Troop"):

                handleTroopHit(objectHit);
                break;

            case ("Hero"):

                handleHeroHit(objectHit);
                break;

            case ("Tower"):

                handleTowerHit(objectHit);
                break;

        }

    }

    private void handleTroopHit(Transform troopHit) {

        TroopIA enemyIA = troopHit.GetComponent<TroopIA>();

        if (enemyIA != null) {

            enemyIA.ReceiveDamage(damage);

        }

        if (collisionEffectPrefab != null) {

            Instantiate(collisionEffectPrefab, transform.position, transform.rotation);

        } 

        Destroy(gameObject);

    }

    private void handleHeroHit(Transform heroHit) {

    }

    private void handleTowerHit(Transform towerHit) {

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
