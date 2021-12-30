using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 100;
    public int currentHealth;
    public float invulnerabilityTime = 2.0f;
    [SerializeField] private float invulnerabilityTimer;
    [SerializeField] private bool invulnerability;

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
        if (GameManager.gameManager.playerHealth == 0)
        {
            currentHealth = MaxHealth;
            healthBar.SetMaxHealth(MaxHealth);
        }

        
        invulnerabilityTimer = 0;
        invulnerability = false;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(debugKeyDamage)) //TODO use a keycode which doesn't make conflict
        {
            DebugTakeDamage(debugDamageAmt);
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

        Invulnerability();
    }

    void DebugTakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void Invulnerability()
    {
        if (invulnerability)
        {
            invulnerabilityTimer += Time.deltaTime;
            if(invulnerabilityTimer >= invulnerabilityTime)
            {
                invulnerabilityTimer = 0;
                invulnerability = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (invulnerability) return;
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        invulnerability = true;
    }

    public void RestoreHealth(int health)
    {
        currentHealth += health;
        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }

    public void SetHealth(int health)
    {
        currentHealth = health;
        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthBar.SetHealth(currentHealth);
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    void TakeDeath()
    {
        FindObjectOfType<DragonBehaviour>().IsDying = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
