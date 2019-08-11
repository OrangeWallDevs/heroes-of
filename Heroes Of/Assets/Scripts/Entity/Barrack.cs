using UnityEngine;

public class Barrack : Entity {

    private string name;
    private string description;

    private float spawnFrequency;
    private int spawnLimit;

    private int moneyValue;

    public Barrack(GameObject gameObject) : base (gameObject) { }

    public string Name {

        get { return name; }

        set { name = value; }

    }

    public string Description {

        get { return description; }

        set { description = value; }

    }

    public float SpawnFrequency {

        get { return spawnFrequency; }

        set {

            spawnFrequency = value;

            Spawner spawnerScript = GameObject.GetComponent<Spawner>();
            spawnerScript.SpawnFrequency = spawnFrequency;

        }

    }

    public int SpawnLimit {

        get { return spawnLimit; }

        set {

            spawnLimit = value;

            Spawner spawnerScript = GameObject.GetComponent<Spawner>();
            spawnerScript.SpawnLimit = spawnLimit;

        }

    }

    public int MoneyValue {

        get { return moneyValue; }

        set { moneyValue = value; }

    }

}
