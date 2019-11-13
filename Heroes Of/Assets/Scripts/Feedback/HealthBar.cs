using UnityEngine;

public class HealthBar : MonoBehaviour {

    public Transform healthBar;
    public FloatEvent healthChangedEvent;

    private HealthController healthController;

    private float maxHealth, previusHealth;

    private void Start() {

        healthController = GetComponentInParent<HealthController>();
        maxHealth = healthController.Health;
        previusHealth = healthController.Health;

        healthChangedEvent.RegisterListener(UpdateHealthBar);

    }

    private void OnDestroy() {

        healthChangedEvent.UnregisterListener(UpdateHealthBar);

    }

    private void UpdateHealthBar(float newHealthValue) {
    
        if (previusHealth > healthController.Health) {

            previusHealth = healthController.Health;

            float healthPercentage = (newHealthValue / maxHealth);

            SetSize(healthPercentage);

        }

    }

    private void SetSize(float barSize) {

        healthBar.localScale = new Vector3(barSize, 1f, 1f);

    }

}
