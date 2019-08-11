using UnityEngine;

public class Minion : Entity {

    private string name;

    private int damage;
    private int health;

    private int movementSpeed;
    private int attackSpeed;

    private int killPoints;
    private int killMoney;

    public Minion(GameObject gameObject) : base (gameObject) { } 

    public string Name {

        get { return name; }

        set { name = value; }

    }

    public int Damage {

        get { return damage; }

        set { damage = value; }
    }

    public int Health {

        get { return health; }

        set { health = value; }

    }

    public int MovementSpeed {

        get { return movementSpeed; }

        set { movementSpeed = value; }

    }

    public int AttackSpeed {

        get { return attackSpeed; }

        set { attackSpeed = value; }

    }

    public int KillPoints {

        get { return killPoints; }

        set { killPoints = value; }

    }

    public int KillMoney {

        get { return killMoney; }

        set { killMoney = value; }

    }

}
