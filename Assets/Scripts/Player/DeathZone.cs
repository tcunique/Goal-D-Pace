using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    //On fait une variable de type Transform car on veut juste changer la position,
    //Donc pas besoin de faire un gameobject, pour de la praticité
    private Transform playerSpawn;
    private Animator fadeSystem;

    //Faire cette étape permet de ne pas recalculer à chaque fois le playerspawn
    //Ca nous permet d'économiser de la ram
    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(ReplacePlayer(collision));
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(20);
        }   
    }

    private IEnumerator ReplacePlayer(Collider2D collision)
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        collision.transform.position = playerSpawn.position;
    }
}
