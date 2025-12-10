using UnityEngine;
[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class FallablePlatform : MonoBehaviour, IFallable
{
    public float delayBeforeFall = 0.5f;
    public float destroyAfter = 2f;
    private Rigidbody2D rb;
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFalling && collision.collider.CompareTag("Player"))
        {
            isFalling = true;
            Shake();
            //Invoke(nameof(Fall), delayBeforeFall);
        }
    }

    public void Fall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyAfter);
    }
    public void Shake()
    {
        // Start a coroutine to shake the platform
        StartCoroutine(ShakeCoroutine());
    }

    private System.Collections.IEnumerator ShakeCoroutine()
    {
        Vector3 originalPosition = transform.localPosition;
        float shakeDuration = delayBeforeFall;
        float elapsed = 0f;
        float shakeMagnitude = 0.1f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.localPosition = originalPosition + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
        Fall(); // Call Fall after shaking
    }
}
