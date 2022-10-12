using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class TemporaryLineController : MonoBehaviour
{
    public GameObject StartingPointObject;
    private Vector3 StartingPointPosition;
    public Vector3 TEST;
    public Vector3 TEST2;
    
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<LineRenderer>().startColor = new Color(1, 0.8f, 1, 0.2f);
        this.GetComponent<LineRenderer>().endColor = new Color(1, 0.8f, 1, 0.2f);
    }

    public void AttachStartingPointObject(GameObject startingPointObject)
    {
        this.StartingPointObject = startingPointObject;
        this.StartingPointPosition = this.StartingPointObject.transform.position;
        this.StartingPointPosition.z = 0;
        
        this.GetComponent<LineRenderer>().SetPosition(0, this.StartingPointPosition);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = Mouse.current.position.ReadValue();
        worldPosition.z = 9;
        worldPosition = Camera.main.ScreenToWorldPoint(worldPosition);
        worldPosition.z = 0;
        this.GetComponent<LineRenderer>().SetPosition(1, worldPosition);
    }
}
