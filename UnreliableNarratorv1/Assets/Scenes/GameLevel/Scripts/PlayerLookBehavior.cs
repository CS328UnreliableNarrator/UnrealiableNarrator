using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookBehavior : MonoBehaviour
{
    [Header("Component References")]
    public Camera cam3D;

    [Header("Look Settings")]
    public float lookSensitivity = 10.0f;
    public float lookSpeedX = 10.0f;
    public float lookSpeedY = 5.0f;

    [Header("Look Sphere")]
    public bool limitLookSphereAboutX = true;
    public bool limitLookSphereAboutY = false;
    [Header("(Left to Right)")]
    public float minX = -90.0f;
    public float maxX = 90.0f;
    [Header("(Up to Down)")]
    public float minY = -90.0f;
    public float maxY = 70.0f;

    private Vector3 lookDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateLookData(Vector3 newLookDirection)
    {
        lookDirection = newLookDirection;
    }
    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        MovePlayerLook();
    }

    void MovePlayerLook()
    {
        Vector3 look = lookDirection * lookSensitivity * Time.deltaTime;
        Vector3 currentLook = cam3D.transform.localEulerAngles;

        //I know this looks wrong but it isn't, this is "rotation about an axis"
        currentLook.x += -look.y * lookSpeedY;
        currentLook.y += look.x * lookSpeedX;

        if (limitLookSphereAboutX)
        {
            if (currentLook.x > 180)
            {
                currentLook.x -= 360f;
            }
            currentLook.x = Mathf.Clamp(currentLook.x, minY, maxY);
        }
        if (limitLookSphereAboutY)
        {
            if (currentLook.y > 180)
            {
                currentLook.y -= 360f;
            }
            currentLook.x = Mathf.Clamp(currentLook.y, minX, maxX);
        }
        cam3D.transform.localEulerAngles = currentLook;
    }
}
