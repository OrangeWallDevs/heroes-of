using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackConstruction : MonoBehaviour {

    private GameObject barrackPrefab;
    private Sprite barrackSprite;

    private bool isPlaceSelected;

    private void Start() {

        barrackPrefab = (GameObject) Resources.Load("Prefabs/Barrack", typeof(GameObject));
        barrackSprite = (Sprite) Resources.Load("Sprites/Barracks/Hunter_Barrack", typeof(Sprite));

    }

    public void SelectBuildPosition() {

        isPlaceSelected = false;
        StartCoroutine(BuildPositionDetection());

    }

    private IEnumerator BuildPositionDetection() {

        while (!isPlaceSelected) {

            if (Input.GetMouseButtonDown(0)) {

                isPlaceSelected = true;

                Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                BuildBarrack(clickPosition);

                yield break;

            } 
            else {

                yield return new WaitForFixedUpdate();

            }

        }

    }

    private Barrack BuildBarrack(Vector2 position) {

        GameObject barrackGameObject = Instantiate(barrackPrefab, position, new Quaternion());
        barrackGameObject.GetComponent<SpriteRenderer>().sprite = barrackSprite;

        Barrack barrack = new Barrack(barrackGameObject);

        barrack.Name = "Hunter Barrack";
        barrack.Description = "Spawn a bunch of Hunters";
        barrack.SpawnFrequency = 1f;
        barrack.SpawnLimit = 15;
        barrack.MoneyValue = 25;

        return barrack;

    }

    public Sprite BarrackSprite {

        set { barrackSprite = value; }

    }

}
