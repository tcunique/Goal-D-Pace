using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    //Pour afficher et désafficher le menu
    public GameObject gameOverUI;

    public static GameOverManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il n'y a plus d'instance de GameOverManager dans la scène");
            return;
        }

        instance = this;
    }

    public void OnPlayerDeath()
    {
        if(CurrentSceneManager.instance.isPlayerPresentByDefault)
        {
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        }

        gameOverUI.SetActive(true);
    }

    //Recommencer le niveau
    public void RetryButton()
    {
        //Reset le nombre de pièce ramasser durant la scène
        Inventory.instance.RemoveGems(CurrentSceneManager.instance.GemsPickedUpInThisCount);

        //Recharge la scène
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //Replace le joueur au Spawn
        //Réactiver les mouvements du joueur et qu'on lui redonne de la vie
        PlayerHealth.instance.Respawn();
        

        gameOverUI.SetActive(false);
    }

    public void MainMenuButton()
    {
        //Retour au menu principal
    }

    public void QuitButton()
    {
        //Fermer le jeu
        Application.Quit();
    }
}
