using UnityEngine;

public class PickUpCherry : MonoBehaviour
{
    public int CherryHealth = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth.instance.GiveHealth(CherryHealth);
            Destroy(gameObject); 
        }
    }
}
