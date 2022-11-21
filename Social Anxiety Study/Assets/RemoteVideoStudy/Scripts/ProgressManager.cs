using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public bool isSlowSegmentFinished { get; set; }
    public bool isFastSegmentFinished { get; set; }
    public bool isVideoSegmentFinished { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Progress manager started");
        isSlowSegmentFinished = false;
        isFastSegmentFinished = false;
        isVideoSegmentFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
