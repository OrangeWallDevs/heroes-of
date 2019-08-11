﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackConstruction : MonoBehaviour {

    public WaveManager waveManager;

    private GameObject barrackPrefab;
    private List<Sprite> barrackSprites;

    private int barrackID;

    private bool isPlaceSelected;

    private void Start() {

        barrackPrefab = (GameObject) Resources.Load("Prefabs/Barrack", typeof(GameObject));

        barrackSprites = new List<Sprite>();
        foreach (Sprite sprite in Resources.LoadAll("Sprites/Barracks", typeof(Sprite))) {

            barrackSprites.Add(sprite);

        }

    }

    public void SelectBuildPosition(int barrackID) {

        isPlaceSelected = false;
        this.barrackID = barrackID;

        StartCoroutine(BuildPositionDetection());

    }

    private IEnumerator BuildPositionDetection() {

        while (!isPlaceSelected) {

            if (Input.GetMouseButtonDown(0)) {

                isPlaceSelected = true;

                Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Barrack newBarrack = BuildBarrack(clickPosition);
                Spawner barrackSpawner = newBarrack.GameObject.GetComponent<Spawner>();

                waveManager.AddSpawner(barrackSpawner);

                yield break;

            } 
            else {

                yield return new WaitForFixedUpdate();

            }

        }

    }

    private Barrack BuildBarrack(Vector2 position) {

        GameObject barrackGameObject = Instantiate(barrackPrefab, position, new Quaternion());

        Barrack barrack = LoadBarrackData(barrackGameObject);

        foreach (Sprite sprite in barrackSprites) {

            if (sprite.name == barrack.Type + "_Barrack") {

                barrack.GameObject.GetComponent<SpriteRenderer>().sprite = sprite;
                break;

            }

        }

        return barrack;

    }

    private Barrack LoadBarrackData(GameObject gameObject) {

        Barrack barrack = new Barrack(gameObject);

        switch (barrackID) {

            case 1:
                barrack.Type = "Assassin";
                barrack.Name = "Assassins Barrack";
                barrack.Description = "Spawn a bunch of Assassins";
                barrack.SpawnFrequency = 1f;
                barrack.SpawnLimit = 15;
                barrack.MoneyValue = 25;
                break;

            case 2:
                barrack.Type = "Hunter";
                barrack.Name = "Hunters Barrack";
                barrack.Description = "Spawn a bunch of Hunters";
                barrack.SpawnFrequency = 1f;
                barrack.SpawnLimit = 15;
                barrack.MoneyValue = 25;
                break;

            case 3:
                barrack.Type = "Knight";
                barrack.Name = "Knights Barrack";
                barrack.Description = "Spawn a bunch of Knights";
                barrack.SpawnFrequency = 1f;
                barrack.SpawnLimit = 15;
                barrack.MoneyValue = 25;
                break;

            case 4:
                barrack.Type = "Mage";
                barrack.Name = "Mages Barrack";
                barrack.Description = "Spawn a bunch of Mages";
                barrack.SpawnFrequency = 1f;
                barrack.SpawnLimit = 15;
                barrack.MoneyValue = 25;
                break;

            case 5:
                barrack.Type = "Soldier";
                barrack.Name = "Soldiers Barrack";
                barrack.Description = "Spawn a bunch of Soldiers";
                barrack.SpawnFrequency = 1f;
                barrack.SpawnLimit = 15;
                barrack.MoneyValue = 25;
                break;

        }

        return barrack;

    }

}
