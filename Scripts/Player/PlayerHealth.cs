using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float InvincibilityTimeAfterHit = 3f;
    public float InvincibilityFlashDelay = 0.15f; 
    public bool isInvincible = false;

    public SpriteRenderer graphics;

    public HealthBar healthbar; 

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        //Si le joueur n'est pas invincible, permet de prendre des dégâts
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthbar.SetHealth(currentHealth);
            isInvincible = true;

            //car c'est un coroutine cette fonction (IEnumerator)
            StartCoroutine(InvicibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    //Cette fonction sert à créer l'animation d'invicibilité
    //Le principe correspond à changer l'opacité du joueur à chaque fois
    public IEnumerator InvicibilityFlash()
    {
        //graphics.color a 4 couleurs, r, g, b, a, avec a l'opacité
        //On ne change donc pas les r g b mais que le a
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);

            //permet de créer un délai entre les deux
            yield return new WaitForSeconds(InvincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            //On remet un délai car il n'y a pas de délai entre les tours de while
            yield return new WaitForSeconds(InvincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(InvincibilityTimeAfterHit);
        isInvincible = false;
    }
}
