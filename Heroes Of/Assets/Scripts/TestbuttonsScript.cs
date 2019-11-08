using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestbuttonsScript : MonoBehaviour {

    private GameObject[] towers;
    private int indexTowerToDestory = 0;

    private void Awake() {

        towers = GameObject.FindGameObjectsWithTag("Tower");

    }

    public void KillAllTroops() {

        GameObject[] troops = GameObject.FindGameObjectsWithTag("Troop");

        foreach (GameObject troop in troops) {

            HealthController healthController = troop.GetComponent<HealthController>();
            healthController.ReceiveDamage((int) healthController.Health);

        }

    }

    public void KillTheCore() {

        GameObject core = GameObject.FindGameObjectWithTag("Core");

        HealthController healthController = core.GetComponent<HealthController>();
        healthController.ReceiveDamage((int) healthController.Health);

    }

    public void KillHero() {

        GameObject core = GameObject.FindGameObjectWithTag("Hero");

        HealthController healthController = core.GetComponent<HealthController>();
        healthController.ReceiveDamage((int) healthController.Health);

    }

    public void DestroyTower() {

        if (indexTowerToDestory <= towers.Length) {

            towers[indexTowerToDestory].GetComponent<TowerHealthController>().ReceiveDamage(100000000);
            indexTowerToDestory++;

        }

    }

}
