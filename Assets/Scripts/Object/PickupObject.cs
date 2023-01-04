using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory.instance.AddGems(1);
            CurrentSceneManager.instance.GemsPickedUpInThisCount++; 
            Destroy(gameObject);
        }
    }
}
