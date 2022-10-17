using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    [SerializeField] private SpriteRenderer wireEnd;
    [SerializeField] private GameObject lightOn;
    private Vector3 startPoint;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.parent.position;
        Debug.Log(startPoint);
    }

    private void OnMouseDrag()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        newPos.z = 0;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPos, 0.2f);
        foreach (Collider2D collider in colliders)
        {
            if(collider.gameObject != gameObject)
            {
                UpdateWire(collider.transform.position);
                if(transform.parent.name.Equals(collider.transform.parent.name))
                {
                    Main.Instance.UpdateCount(1);
                    collider.GetComponent<Wire>()?.Done();
                    Done();
                }
                return;
            }
        }
        UpdateWire(newPos);
    }

    private void OnMouseUp()
    {
        UpdateWire(startPoint);
    }

    private void Done()
    {
        lightOn.SetActive(true);
        Destroy(this);
    }
    private void UpdateWire(Vector3 newPos)
    {
        transform.position = newPos;

        Vector3 direction = newPos - startPoint;
        transform.right = direction * transform.lossyScale.x;

        float dist = Vector2.Distance(startPoint, newPos);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);
    }
}
