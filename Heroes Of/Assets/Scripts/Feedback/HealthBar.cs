using UnityEngine;

public class HealthBar : MonoBehaviour {

    public Transform healthBar;
    public FloatEvent healthChangedEvent;

    private float maxHealth;

    private void Start() {

        HealthController healthController = GetComponentInParent<HealthController>();
        maxHealth = healthController.Health;

        healthChangedEvent.RegisterListener(UpdateHealthBar);

    }

    private void OnDestroy() {

        healthChangedEvent.UnregisterListener(UpdateHealthBar);

    }

    private void UpdateHealthBar(float newHealthValue) {
    
        float healthPercentage = (newHealthValue / maxHealth);

        SetSize(healthPercentage);

    }

    private void SetSize(float barSize) {

        healthBar.localScale = new Vector3(barSize, 1f, 1f);

    }

}
