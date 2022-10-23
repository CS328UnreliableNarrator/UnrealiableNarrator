using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehavior : MonoBehaviour
{
    [Header("Component References")]
    public Rigidbody playerRigidbody;
    public Transform cam3D;

    [Header("Movement Settings")]
    [Range(1f, 5f)] public float movementAcceleration = 3f;
    [Range(1f, 5f)] public float maxSpeed = 5f;
    private Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        this.playerRigidbody.maxLinearVelocity = maxSpeed;
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
        Vector3 movement = movementDirection * movementAcceleration * Time.deltaTime;
        Vector3 forwardRelative = cam3D.forward.normalized * movement.z;
        Vector3 rightRelative = cam3D.right.normalized * movement.x;
        Vector3 playerForward = forwardRelative + rightRelative;
        playerForward.y = 0.0f;
        //playerRigidbody.MovePosition(transform.position + forwardRelative + rightRelative);
        Transform ForcePosition = this.transform.GetChild(1);
        playerRigidbody.AddForceAtPosition((playerForward) * 500, transform.position);
    }

    void TurnPlayer()
    {

    }
}
