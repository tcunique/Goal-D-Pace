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

    public float HurtTime = 0.5f;
    public bool isHurt = false;
    public Animator animator;

    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il n'y a plus d'instance de PlayerHealth dans la scène");
            return;
        }

        instance = this;
    }

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
            TakeDamage(60);
        }
    }

    public void TakeDamage(int damage)
    {
        //Si le joueur n'est pas invincible, permet de prendre des dégâts
        if (!isInvincible)
        {
            if (currentHealth > 0)
            {
                currentHealth -= damage;
                healthbar.SetHealth(currentHealth);
                isInvincible = true;

                // vérifier si le joueur est toujours vivant
                if (currentHealth <= 0)
                {
                    Die();
                    return;
                }

                isHurt = true;
                animator.SetBool("hurt", isHurt);

                //car c'est un coroutine cette fonction (IEnumerator)
                StartCoroutine(InvicibilityFlash());
                StartCoroutine(HandleInvincibilityDelay());
                StartCoroutine(HandleHurtDelay());
            }
        }
    }

    public void Die()
    {
        Debug.Log("Le joueur est éliminée");
        // bloquer  les mouvements du personnage
        // Jouer l'animation d'élimination
        // empêcher les interactions physiques avec les autres éléments de la scène

        MovePlayer1.instance.enabled = false;
        MovePlayer1.instance.animator.SetTrigger("Death");
        MovePlayer1.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        MovePlayer1.instance.playerCollider.enabled = false;
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        MovePlayer1.instance.enabled = true;
        MovePlayer1.instance.animator.SetTrigger("Respawn");
        MovePlayer1.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        MovePlayer1.instance.playerCollider.enabled = true;
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth);
    }

    public void GiveHealth(int health)
    {
        if ((currentHealth+health) > 100)
        {
            currentHealth = 100;
            healthbar.SetHealth(currentHealth);
        } else
        {
            currentHealth += health;
            healthbar.SetHealth(currentHealth);
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

    public IEnumerator HandleHurtDelay()
    {
        yield return new WaitForSeconds(HurtTime);
        isHurt = false;
        animator.SetBool("hurt", isHurt);
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(InvincibilityTimeAfterHit);
        isInvincible = false;
    }
}
