using UnityEngine;

public class BallMachine : MonoBehaviour
{
    public GameObject ballPrefab; // Drag your Ball Prefab here
    public float spawnInterval = 3f; // Seconds between balls
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnBall();
            timer = 0;
        }
    }

    void SpawnBall()
    {
        GameObject newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = newBall.GetComponent<Rigidbody>();

        // This pushes the ball toward the player (negative Z direction)
        // Adjust the -5f to make the serve faster or slower
        rb.AddForce(new Vector3(0, 2f, -5f), ForceMode.Impulse);

        Destroy(newBall, 10f);
    }
}