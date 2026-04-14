using UnityEngine;
using System.Collections;

public class TargetHit : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject explosionEffect; // Drag your 'HitEffect' prefab here
    public Color hitColor = Color.white; // The color it flashes when hit

    private Color originalColor;
    private Renderer targetRenderer;

    // 'static' means this score is shared by all targets in the scene
    public static int totalScore = 0;

    void Start()
    {
        targetRenderer = GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            originalColor = targetRenderer.material.color;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Only trigger if the ball hits us
        if (collision.gameObject.CompareTag("Ball"))
        {
            // 1. Add to the global score
            totalScore += 100;
            Debug.Log("<color=cyan>TARGET HIT!</color> Total Score: " + totalScore);

            // 2. Spawn the 'Juice' (particles) at the point of impact
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, collision.contacts[0].point, Quaternion.identity);
            }

            // 3. Make the target flash so we know it worked
            StopAllCoroutines();
            StartCoroutine(FlashTarget());

            // 4. Destroy the ball so it doesn't hit the target twice
            Destroy(collision.gameObject);
        }
    }

    IEnumerator FlashTarget()
    {
        targetRenderer.material.color = hitColor;
        yield return new WaitForSeconds(0.15f); // Stay white for a moment
        targetRenderer.material.color = originalColor;
    }
}