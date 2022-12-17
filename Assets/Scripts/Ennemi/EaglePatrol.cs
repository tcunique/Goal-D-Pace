using UnityEngine;

public class EaglePatrol : MonoBehaviour
{
    public float speed;

    //C'est le tableau où on va mettre tous les différents points, où l'ennemi va se déplacer
    public Transform[] waypoints;

    public SpriteRenderer graphics;

    public int damageOnCollision = 20;

    //C'est la variable qui va permettre de faire des mouvements répétitifs
    private Transform target;
    private int destPoint = 0;


    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        //transform c'est la méthode de déplacement de unity
        Vector3 dir = target.position - transform.position;
        //normalized permet de transformer la norme du vecteur égale à 1
        //de sorte à ce que les vecteurs aient toujours la taille égale à 1

        //space.world permet de le faire déplacer dans notre monde
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //Si l'ennemi est quasiement arrivé à destination 
        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }
}
