using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var gameManager = GameManager.gameManager;

            gameManager.level++;
            if (gameManager.level > gameManager.maxLevel)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                var player = FindObjectOfType<ThirdPersonMovement>();
                gameManager.playerHealth = player.GetComponent<PlayerHealth>().GetHealth();
                gameManager.playerMagic = player.GetComponent<PlayerMagic>().GetMagic();

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
