using UnityEngine;

public class BoundaryTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("Out of Bounds!");
            Destroy(other.gameObject); // Cleanup the missed ball
        }
    }
}