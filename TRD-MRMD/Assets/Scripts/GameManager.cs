using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int level = 1;
    public int maxLevel = 5;

    public static GameManager gameManager;

    public Light sunlight;

    [Header("Player stats")]
    public int playerHealth;
    public int playerMagic;
    public int playerScore;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            Destroy(gameObject);
        }
        if (scene.name == "SampleScene" && FindObjectOfType<ThirdPersonMovement>() != null)
        {
            SetPlayerStats();
            SetupSunlightIntensity();
        }
    }

    void SetPlayerStats()
    {
        var player = FindObjectOfType<ThirdPersonMovement>();
        player.GetComponent<PlayerHealth>().SetHealth(playerHealth);
        player.GetComponent<PlayerMagic>().SetMagic(playerMagic);
    }

    void SetupSunlightIntensity()
    {
        sunlight = FindObjectOfType<Light>();
        sunlight.intensity = 0.7f - (level - 1) * 0.15f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
