using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestbuttonsScript : MonoBehaviour {

    public void KillAllTroops() {

        GameObject[] troops = GameObject.FindGameObjectsWithTag("Troop");

        foreach (GameObject troop in troops) {

            troop.SetActive(false);
            Destroy(troop);

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

}
