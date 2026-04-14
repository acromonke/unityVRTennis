using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // For VR controller support

public class TennisRacket : MonoBehaviour
{
    [Header("Physics Settings")]
    public float hitForce = 2.0f;

    [Header("Juice Settings")]
    public AudioClip hitSound; // Drag your 'Pop' sound here!

    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component on the same object
        audioSource = GetComponent<AudioSource>();

        // If the Unity UI is being stubborn, we assign the clip via code
        if (audioSource != null && hitSound != null)
        {
            audioSource.clip = hitSound;
            audioSource.playOnAwake = false; // Ensure it doesn't pop at start
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if we hit the ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            // 1. Play the sound
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }

            // 2. Add force to the ball
            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRb != null)
            {
                // Calculate direction from racket to ball
                Vector3 hitDir = (collision.transform.position - transform.position).normalized;

                // Apply velocity: Current speed + our extra 'oomph'
                ballRb.linearVelocity = hitDir * (ballRb.linearVelocity.magnitude + hitForce);

                Debug.Log("Hit the ball!");
            }

            // 3. VR Haptics (Vibration)
            // This works when you're actually holding the controller in VR
            var controller = GetComponentInParent<ActionBasedController>();
            if (controller != null)
            {
                controller.SendHapticImpulse(0.7f, 0.1f);
            }
        }
    }
}