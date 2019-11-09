using UnityEngine;

public class ScoreRecorder : MonoBehaviour {

    public TowerEvent towerDestroyedEvent;
    public TroopEvent troopDeathEvent;
    public HeroEvent heroDeathEvent;
    public GameEvent gameEndEvent;

    public GoldManager goldManager;

    public int TroopsKilledScore { private set; get; }
    public int TroopsSurvivedScore { private set; get; }
    public int TowersDestroyedScore { private set; get; }
    public int TowersProtectedScore { private set; get; }
    public int HeroKilledScore { private set; get; }
    public int HeroSurvivedScore { private set; get; }
    public int GoldLeftScore { private set; get; }
    public int TotalScore { private set; get; }

    private RunTimePhaseData phaseData;
    
    private void Start() {

        phaseData = GetComponent<RunTimePhaseData>();

        heroDeathEvent.RegisterListener(HandleHeroDeath);
        troopDeathEvent.RegisterListener(HandleTroopDeath);
        towerDestroyedEvent.RegisterListener(HandleTowerDestruction);

        gameEndEvent.RegisterListener(HandleFinalWaveEnd);

    }

    private void HandleHeroDeath(RunTimeHeroData hero) {

        if (hero.isEnemy) {

            HeroKilledScore += hero.valScore;

        }

        TotalScore = CalculeTotalScore();

    }

    private void HandleTroopDeath(RunTimeTroopData troop) {

        if (troop.isEnemy) {

            TroopsKilledScore += troop.valScore;

        }

        TotalScore = CalculeTotalScore();

    }

    private void HandleTowerDestruction(RunTimeTowerData tower) {

        if (tower.isEnemy) {

            TowersDestroyedScore += tower.valScore;

        }

        TotalScore = CalculeTotalScore();

    }

    private void HandleFinalWaveEnd() {

        TroopsSurvivedScore = CountTroopsSurvivedScore();
        GoldLeftScore = CountGoldLeftScore();

        if (phaseData.idtPhaseType == PhaseObjectives.DEFEND) {

            TowersProtectedScore = CountTowersDefendedScore();
            HeroSurvivedScore = CountHeroSurvivedScore();

        }

        TotalScore = CalculeTotalScore();

    }

    private int CountTroopsSurvivedScore() {

        int score = 0;
        GameObject[] troopsInGame = GameObject.FindGameObjectsWithTag("Troop");

        foreach (GameObject troop in troopsInGame) {

            RunTimeTroopData troopData = troop.GetComponent<RunTimeTroopData>();
            
            if (!troopData.isEnemy) {

                score += troopData.valScore;

            }

        }

        return score;

    }

    private int CountGoldLeftScore() {

        int score = goldManager.playerGoldReserve.currentGold;

        return score;

    }

    private int CountTowersDefendedScore() {

        int score = 0;
        GameObject[] towersInGame = GameObject.FindGameObjectsWithTag("Tower");

        foreach (GameObject tower in towersInGame) {

            RunTimeTowerData towerData = tower.GetComponent<RunTimeTowerData>();
            score += towerData.valScore;

        }

        return score;

    }

    private int CountHeroSurvivedScore() {

        int score = 0;
        GameObject[] herosInGame = GameObject.FindGameObjectsWithTag("Hero");

        foreach (GameObject hero in herosInGame) {

            RunTimeHeroData heroData = hero.GetComponent<RunTimeHeroData>();

            if (!heroData.isEnemy) {

                score += heroData.valScore;

            }

        }

        return score;

    }

    private int CalculeTotalScore() {

        int score = GoldLeftScore + HeroKilledScore + HeroSurvivedScore 
                     + TowersDestroyedScore + TowersProtectedScore + TroopsKilledScore
                     + TroopsSurvivedScore;

        return score;
            
    }

}
