using UnityEngine;

public class WreckingBall : MonoBehaviour
{
    public Rigidbody2D rb;
    public float swingForce;
    public int ballDamage = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right *swingForce ,ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerStat>();
            player.TakeDamage(ballDamage);
        }
    }
}
