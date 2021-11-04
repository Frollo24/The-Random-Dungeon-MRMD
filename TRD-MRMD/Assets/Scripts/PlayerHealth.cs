using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 100;
    public int currentHealth;

    public HealthBarBehaviour healthBar;

    // Debug stuff
    [Header("Debugging options")]
    [SerializeField] private KeyCode debugKeyDamage = KeyCode.Space;
    [SerializeField] private int debugDamageAmt = 10;
    [SerializeField] private KeyCode debugKeyHealth = KeyCode.Return;
    [SerializeField] private int debugHealthAmt = 5;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(debugKeyDamage)) //TODO use a keycode which doesn't make conflict
        {
            TakeDamage(debugDamageAmt);
        }
        if (Input.GetKeyDown(debugKeyHealth)) //TODO use a keycode which doesn't make conflict
        {
            RestoreHealth(debugHealthAmt);
        }
#endif
        if (currentHealth <= 0)
        {
            TakeDeath();
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void RestoreHealth(int health)
    {
        currentHealth += health;
        healthBar.SetHealth(currentHealth);
    }

    void TakeDeath()
    {
        //TODO make proper death
        Debug.Log("Death");
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
    }
}
