
using UnityEngine;

public class MovePlayer1 : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public bool isJumping;
    public bool isGrounded;

    public Transform groundCheck;
    public float groundcheckRadius;
    public LayerMask collisionLayers;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public Vector3 velocity = Vector3.zero;

    public bool crouch = false;

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundcheckRadius, collisionLayers);
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = Vector2.up * jumpForce;
            }
        }

        rb.velocity += Physics2D.gravity * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Vector2.right, juste le vector (1,0)
            //Time.deltaTime, temps de diff entre la frame précedente et maintenant
            //Vector2 decalage = Vector2.right * max_speed * Time.deltaTime;
            //rd.position = (Vector2) transform.position + decalage;
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Vector2.left, juste le vector (1,0)
            //Time.deltaTime, temps de diff entre la frame précedente et maintenant
            //Vector2 decalage = Vector2.left * max_speed * Time.deltaTime;
            //rd.position = (Vector2)transform.position + decalage;
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }


        if (Input.GetKey(KeyCode.DownArrow))
        {
            crouch = true;
        }
        else
        {
            crouch = false;
        }
        animator.SetBool("crouch", crouch);

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
    }


    private void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        } else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundcheckRadius);
    }
}
