using UnityEngine;

public class Indicator : MonoBehaviour
{
    private int movementDirection;
    private int movementSpeed;
    private int speedIncrement;

    public void Awake()
    {
        movementDirection = 1;
        movementSpeed = 1;
        speedIncrement = 1;
    }
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y += movementSpeed * movementDirection * Time.deltaTime;
        transform.position = pos;
    }

    private void ChangeDirection()
    {
        movementDirection *= -1;
    }

    public void OnCollisionEnter2D()
    {
        ChangeDirection();
    }

    public void IncrementMovementSpeed()
    {

        movementSpeed += speedIncrement;
    }
}