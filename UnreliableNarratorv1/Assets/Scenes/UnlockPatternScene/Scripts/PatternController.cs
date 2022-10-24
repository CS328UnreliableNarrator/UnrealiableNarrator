using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PatternController : MonoBehaviour
{
    public List<GameObject> patternDots = new List<GameObject>();
    /*
        0 1 2
        3 4 5
        6 7 8 
    */
    
    public GameObject linePrefab;
    public TemporaryLineController TemporaryLineController;
    public List<int> CorrectPatternIndexes = new List<int>();
    
    public int numberOfTries = 3;
    public float numberOfSecondsInLocked = 10;
    private int missedTries = 0;
    private bool isDeviceLocked = false;
    private float secondsToUnlock = 0;

    private bool isInDrawing = false;
    private bool isInResultView = false;
    private int currentDotDrawing = -1;

    public GameObject lockDeviceInformation;
    public GameObject dotsContainer;

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

    private void OnEnable()
    {
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Cursor.visible = false;
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
        FindObjectOfType<AudioManager>().Play("NinePinPuzzleNodeConnect");
    }

    private void ClearConnections(bool useExitAnimation = false)
    {
        for (int i = 0; i < this.Connections.Count; i++)
        {
            if (true) this.Connections[i].LineObject.GetComponent<LineController>().PlayExitAnimation();
            Destroy(this.Connections[i].LineObject, 1);
        }
        this.Connections.Clear();
    }

    private Vector3 GetDotPositionFromIndex(int index)
    {
        Vector3 position = this.patternDots[index].transform.position;
        position.z = 0;
        return position;
    }

    public void OnDotClicked(PatternDot dot)
    {
        if (this.isDeviceLocked)
        {
            this.dotsContainer.GetComponent<Animator>().SetTrigger("Shake");
            return;
        }
        if (!this.isInDrawing)
        {
            this.StartDrawing(dot.index);
            dot.PlaySelectionAnimation();
        }
    }

    private void StartDrawing(int startingDotIndex)
    {
        this.isInDrawing = true;
        this.isInResultView = false;
        this.ClearConnections();

        for (int i = 0; i < this.patternDots.Count; i++)
        {
            if (i != startingDotIndex)
            {
                this.patternDots[i].GetComponent<PatternDot>().BackToDefaultState();
            }
        }

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
        this.isInResultView = true;
        this.TemporaryLineController.gameObject.SetActive(false);
    }

    private bool IsDrawnPatternCorrect()
    {
        if (this.Connections.Count != this.CorrectPatternIndexes.Count - 1) return false;

        for (int i = 0; i < this.CorrectPatternIndexes.Count - 1; i++)
        {
            if (this.Connections[i].From != this.CorrectPatternIndexes[i]) return false;
        }
        
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
            FindObjectOfType<AudioManager>().Play("NinePinPuzzleWin");
            for (int i = 0; i < this.Connections.Count; i++)
            {
                this.Connections[i].LineObject.GetComponent<LineController>().PlayCorrectPatternAnimation();
                
                int dotIndex = this.Connections[i].To;
                this.patternDots[dotIndex].GetComponent<PatternDot>().PlayMarkCorrectAnimation();
            }

            LineController line = this.Connections[0].LineObject.GetComponent<LineController>();
            this.patternDots[this.Connections[0].From].GetComponent<PatternDot>().PlayMarkCorrectAnimation();
            
            StartCoroutine(this.PlayExitAnimation());
            PlayerPrefs.SetInt("OfficeDoorLock", 0);
            line.Invoke("SwitchScene", 1);
        }
        else
        {
            Debug.Log("PATTERN WRONG!!!");
            FindObjectOfType<AudioManager>().Play("NinePinPuzzleFail");
            for (int i = 0; i < this.Connections.Count; i++)
            {
                this.Connections[i].LineObject.GetComponent<LineController>().PlayWrongPatternAnimation();

                int dotIndex = this.Connections[i].To;
                this.patternDots[dotIndex].GetComponent<PatternDot>().PlayMarkWrongAnimation();
            }
            this.patternDots[this.Connections[0].From].GetComponent<PatternDot>().PlayMarkWrongAnimation();
            this.missedTries++;
        }
    }
    
    IEnumerator PlayExitAnimation()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < this.Connections.Count; i++)
        {
            this.Connections[i].LineObject.GetComponent<LineController>().PlayExitAnimation();
        }
        
        for (int i = 0; i < this.patternDots.Count; i++)
        {
            this.patternDots[i].GetComponent<Animator>().SetTrigger("Exit");
        }
    }

    public void OnMouseEnterDot(PatternDot dot)
    {
        if (this.isDeviceLocked) return;
        int dotIndex = dot.index;
        if (this.isInDrawing && dotIndex != this.currentDotDrawing)
        {
            this.Connect(this.currentDotDrawing, dotIndex);
            this.currentDotDrawing = dotIndex;
            this.TemporaryLineController.AttachStartingPointObject(this.patternDots[dotIndex].gameObject);
            dot.PlaySelectionAnimation();
        }
    }

    public void LockDevice()
    {
        // this.lockDeviceInformation.SetActive(true);
        this.lockDeviceInformation.GetComponent<Animator>().SetTrigger("Show");
        this.isDeviceLocked = true;
        this.secondsToUnlock = (int)this.numberOfSecondsInLocked;
    }

    private void UnlockDevice()
    {
        // this.lockDeviceInformation.SetActive(false);
        this.lockDeviceInformation.GetComponent<Animator>().SetTrigger("Hide");
        this.isDeviceLocked = false;
        this.missedTries = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isDeviceLocked)
        {
            this.secondsToUnlock -= Time.deltaTime;

            if (this.secondsToUnlock < 0)
            {
                this.UnlockDevice();
                this.secondsToUnlock = 0;
            }
            this.lockDeviceInformation.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    "Wait " + Math.Round(this.secondsToUnlock, 0) + "s to try again";
        }
        else
        {
            if (this.missedTries == this.numberOfTries)
            {
                this.LockDevice();
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            if (this.isInDrawing) this.FinishDrawing();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (this.isInResultView) this.ClearConnections(true);
        }
    }
}
