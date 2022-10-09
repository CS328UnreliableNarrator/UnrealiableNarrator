using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehavior : MonoBehaviour
{
    [Header("Component References")]
    public Rigidbody playerRigidbody;

    [Header("Movement Settings")]
    public float movementSpeed = 3f;
    public float turnSpeed = 0.1f;

    private Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateMovementData(Vector3 newMovementDirection)
    {
        movementDirection = newMovementDirection;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        MovePlayer();
        TurnPlayer();
    }

    void MovePlayer()
    {
        //Vector3 movement = CameraDirection(movementDirection) * movementSpeed * Time.deltaTime;
        Vector3 movement =  movementDirection * movementSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void TurnPlayer()
    {

    }
}
