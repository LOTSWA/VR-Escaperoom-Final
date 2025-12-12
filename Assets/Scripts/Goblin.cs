using System.Collections;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    // Assign these two GameObjects in the Unity Inspector to define the boundaries
    public Transform point1;
    public Transform point2;
    public float moveSpeed = 3f;
    public float waitTimeBetweenMoves = 1f;

    private Vector3 minBounds;
    private Vector3 maxBounds;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        // Calculate the actual min and max bounds from the two points
        minBounds = Vector3.Min(point1.position, point2.position);
        maxBounds = Vector3.Max(point1.position, point2.position);

        // Start the movement routine
        StartCoroutine(MoveToRandomPositionRoutine());
    }

    private IEnumerator MoveToRandomPositionRoutine()
    {
        while (true) // Infinite loop for continuous movement
        {
            // 1. Determine a new random target position within the bounds
            targetPosition = GetRandomPositionInBounds();
            isMoving = true;

            // 2. Move towards the target position
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null; // Wait until the next frame
            }

            // 3. Reached the target, now wait before picking a new one
            isMoving = false;
            yield return new WaitForSeconds(waitTimeBetweenMoves);
        }
    }

    private Vector3 GetRandomPositionInBounds()
    {
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);
        float randomZ = Random.Range(minBounds.z, maxBounds.z); // Adjust if moving in 2D

        return new Vector3(randomX, randomY, randomZ);
    }
}
