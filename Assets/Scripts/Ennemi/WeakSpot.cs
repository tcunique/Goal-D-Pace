using UnityEngine;
using System.Collections;

public class WeakSpot : MonoBehaviour
{
    public GameObject objectToDestroy;

    public Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Permet de voir si tout gameobject de tag player entre en collision ou non
        if(collision.CompareTag("Player"))
        {
            animator.SetBool("death", true);
            StartCoroutine(Wait());
            //permet de supprimer tous les fichiers ennemi_graphics
            Destroy(objectToDestroy); 
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
    }
}