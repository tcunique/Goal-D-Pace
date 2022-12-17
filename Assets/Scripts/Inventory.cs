using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int gemsCount;
    public Text gemsCountText;

    public static Inventory instance;

    //fonction lu avant toutes les autres fonctions, même avant start
    //Ca permet d'accéder au scrip inventory n'importe où
    //grâce à static
    private void Awake()
    {
        if (instance != null)
        {
            //Permet de faire en sorte qu'il y ait un seul inventaire
            Debug.LogWarning("Il y a plus d'une instance de Inveotyr dans la scène");
            return;
        }

        instance = this;
    }

    public void AddGems(int count)
    {
        gemsCount += count;
        gemsCountText.text = gemsCount.ToString();
    }
}
