using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int damage = 10; // Damage dealt by this hitbox
    public string targetTag = "Enemy"; // Tag to identify valid targets
    public float knockbackForce = 5f; // Force of the knockback effect

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            Hurtbox hurtbox = collision.GetComponent<Hurtbox>();
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            if (hurtbox != null)
            {
                hurtbox.TakeDamage(damage);

                // Apply knockback effect if the target has a Rigidbody
                if (rb != null)
                {
                    Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                    rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
