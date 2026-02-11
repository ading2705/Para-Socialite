using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Starting HP shown on the health bar.
    [SerializeField] private int _maxHealth = 6;
    // UI bridge for displaying current HP.
    [SerializeField] private HealthBar _healthBar;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => _maxHealth;

    void Start()
    {
        // Initialize runtime health from configured max value.
        CurrentHealth = _maxHealth;

        if (_healthBar != null)
            _healthBar.SetMaxHealth(_maxHealth);
    }

    public bool CanSpend(int amount)
    {
        // Negative costs are treated as zero to keep callers safe. And once it hits zero its game over.
        amount = Mathf.Max(0, amount);
        return CurrentHealth >= amount;
    }

    public bool SpendHealth(int amount)
    {
        // Clamp invalid negative inputs.
        amount = Mathf.Max(0, amount);

        if (CurrentHealth < amount)
            return false;

        // Spend HP and keep the health UI synced.
        CurrentHealth -= amount;

        if (_healthBar != null)
            _healthBar.SetHealth(CurrentHealth);

        return true;
    }
}
