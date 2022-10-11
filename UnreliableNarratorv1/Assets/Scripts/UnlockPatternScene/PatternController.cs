using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PatternController : MonoBehaviour
{
    public List<GameObject> patternDots = new List<GameObject>();
    public GameObject linePrefab;
    public TemporaryLineController TemporaryLineController;
    public List<int> CorrectPatternIndexes = new List<int>();
    
    /*
        0 1 2
        3 4 5
        6 7 8 
    */

    public bool isInDrawing = false;
    public int currentDotDrawing = -1;

    public class Connection
    {
        public int From;
        public int To;
        public GameObject LineObject;

        public Connection(int from, int to, GameObject lineObject = null)
        {
            this.From = from;
            this.To = to;
            if (lineObject) this.LineObject = lineObject;
        }
    }

    public List<Connection> Connections = new List<Connection>();
    
    // Start is called before the first frame update
    void Start()
    {
        this.TemporaryLineController.gameObject.SetActive(false);
    }

    public void Connect(int from, int to)
    {
        // Add connection as line representation
        GameObject newLine = Instantiate(this.linePrefab);
        LineRenderer newLineRenderer = newLine.GetComponent<LineRenderer>();
        
        // newLine.gameObject.transform.SetParent(this.gameObject.transform);
        
        // Add connection as data
        Connection newConnection = new Connection(from, to, newLine);
        this.Connections.Add(newConnection);

        // starting position
        newLineRenderer.SetPosition(0, this.GetDotPositionFromIndex(from));
        
        // ending position
        newLineRenderer.SetPosition(1, this.GetDotPositionFromIndex(to));
    }

    private void ClearConnections()
    {
        for (int i = 0; i < this.Connections.Count; i++)
        {
            Destroy(this.Connections[i].LineObject);
        }
        this.Connections.Clear();
    }

    private Vector3 GetDotPositionFromIndex(int index)
    {
        Vector3 position = this.patternDots[index].transform.position;
        position.z = 0;
        return position;
    }

    public void OnDotClicked(int dotIndex)
    {
        if (!this.isInDrawing)
        {
            this.StartDrawing(dotIndex);
        }
    }

    private void StartDrawing(int startingDotIndex)
    {
        this.isInDrawing = true;
        this.ClearConnections();

        this.currentDotDrawing = startingDotIndex;
        this.TemporaryLineController.gameObject.SetActive(true);
        this.TemporaryLineController.AttachStartingPointObject(this.patternDots[startingDotIndex].gameObject);
    }

    private void FinishDrawing()
    {
        if (this.Connections.Count >= 1)
        {
            this.EvaluatePattern();
        }
        // this.ClearConnections();
        this.isInDrawing = false;
        this.TemporaryLineController.gameObject.SetActive(false);
    }

    private bool IsDrawnPatternCorrect()
    {
        Debug.Log("A");
        if (this.Connections.Count != this.CorrectPatternIndexes.Count - 1) return false;
        Debug.Log("B");
        
        for (int i = 0; i < this.CorrectPatternIndexes.Count - 1; i++)
        {
            if (this.Connections[i].From != this.CorrectPatternIndexes[i]) return false;
        }
        Debug.Log("C");


        // if the last connection's To is equal to the last correct pattern index, then return true
        // otherwise return false because it does not match the correct pattern
        return (this.CorrectPatternIndexes[this.CorrectPatternIndexes.Count - 1] == 
               this.Connections[this.Connections.Count - 1].To);
    }

    private void EvaluatePattern()
    {
        if (this.IsDrawnPatternCorrect())
        {
            Debug.Log("PATTERN CORRECT");
            for (int i = 0; i < this.Connections.Count; i++)
            {
                this.Connections[i].LineObject.GetComponent<LineRenderer>().SetColors(Color.green, Color.green);
            }
        }
        else
        {
            Debug.Log("PATTERN WRONG!!!");
            for (int i = 0; i < this.Connections.Count; i++)
            {
                this.Connections[i].LineObject.GetComponent<LineRenderer>().SetColors(Color.red, Color.red);
            }
        }
    }

    public void OnMouseEnterDot(PatternDot dot)
    {
        int dotIndex = dot.index;
        if (this.isInDrawing && dotIndex != this.currentDotDrawing)
        {
            this.Connect(this.currentDotDrawing, dotIndex);
            this.currentDotDrawing = dotIndex;
            this.TemporaryLineController.AttachStartingPointObject(this.patternDots[dotIndex].gameObject);
            dot.PlayZoomInOutAnimation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            this.FinishDrawing();
        }
    }
}
