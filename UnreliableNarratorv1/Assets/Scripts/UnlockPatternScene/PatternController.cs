using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatternController : MonoBehaviour
{
    public List<GameObject> patternDots = new List<GameObject>();
    public GameObject linePrefab;
    /*
        0 1 2
        3 4 5
        6 7 8 
    */

    public class Connection
    {
        public int From;
        public int To;

        public Connection(int from, int to)
        {
            this.From = from;
            this.To = to;
        }
    }

    public List<Connection> Connections = new List<Connection>();
    
    // Start is called before the first frame update
    void Start()
    {
        this.Connect(1, 2);
    }

    public void Connect(int from, int to)
    {
        // Add connection as data
        Connection newConnection = new Connection(from, to);
        this.Connections.Add(newConnection);
        
        // Add connection as line representation
        GameObject newLine = Instantiate(this.linePrefab);
        LineRenderer newLineRenderer = newLine.GetComponent<LineRenderer>();
        
        // starting position
        newLineRenderer.SetPosition(0, this.GetDotPositionFromIndex(from));
        
        // ending position
        newLineRenderer.SetPosition(1, this.GetDotPositionFromIndex(to));
    }

    private Vector3 GetDotPositionFromIndex(int index)
    {
        return this.patternDots[index].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
