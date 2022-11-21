//using EmteqLabs;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Timer : MonoBehaviour
//{
//    public float timeLeft;
//    Text timerText;
//    bool isTimerFinished;
//    bool startTimer;
//    public GameObject headMovementManager;
//    public GameObject pointer;
//    CursorCubeTracking cursorCubeTracking;
//    Animator cubeAnimator;
//    public GameObject cube;

//    int recordingTextFontSize = 45;
//    int timerTextFontSize = 180;

//    Vector3 cubeStartingPosition;

//    Animator timerAnimator;

//    Color timerColor;
//    Color recordingColor;

//    // Start is called before the first frame update
//    void Start()
//    {
//        timerText = this.GetComponent<Text>();
//        cursorCubeTracking = pointer.GetComponent<CursorCubeTracking>();
//        cubeStartingPosition = cube.GetComponent<Transform>().position;
//        timerColor = timerText.color;
//        recordingColor = new Color(255, 0, 0);
//        timerAnimator = this.GetComponent<Animator>();
//    }

//    void OnEnable()
//    {
//        timerText = this.GetComponent<Text>();
//        cubeAnimator = cube.GetComponent<Animator>();
//        timerAnimator = this.GetComponent<Animator>();

//        // Debug.Log("on enable called");
//        timeLeft = 3.0f;
//        isTimerFinished = false;
//        startTimer = false;
//        timerText.fontSize = timerTextFontSize;
//        timerText.text = "";
//        cubeAnimator.enabled = false;
//    }

//    void OnDisable()
//    {
//        cube.GetComponent<Transform>().position = cubeStartingPosition;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (startTimer)
//        {
//            if (cursorCubeTracking.IsCursorOverCube)
//            {
//                if (!cubeAnimator.enabled)
//                {
//                    cubeAnimator.enabled = true;
//                    EmteqManager.SetDataPoint("Baseline recording finished");
//                }

//                if (timeLeft > 0)
//                {
//                    timeLeft -= Time.deltaTime;
//                   // timerText.text = (timeLeft).ToString("0");
//                }

//                else
//                {
//                    if (!isTimerFinished)
//                    {
//                        DisappearTimer();

//                    }
//                }
//            }

//            else if (cubeAnimator.enabled)
//            {
//                cubeAnimator.enabled = false;
//                timeLeft = 3.0f;
//            } else
//            {
//                timeLeft = 3.0f;
//            }


//        }

//        else if(cursorCubeTracking.IsCursorOverCube && !cubeAnimator.enabled)
//        {
//            StartTimer();
//        }
//    }

//    void DisappearTimer()
//    {
//        //timerText.text = "Follow the object: Recording data";
//        timerText.fontSize = recordingTextFontSize;
//        isTimerFinished = true;
//        startTimer = false;
//        timerAnimator.enabled = true;
//        timerText.color = new Color(255, 0, 0);
//    }

//    void StartTimer()
//    {
//        timeLeft = 3.0f;
//        cubeAnimator.enabled = true;
//        startTimer = true;
//        timerAnimator.enabled = false;
//        timerText.color = timerColor;
//        EmteqManager.SetDataPoint("Baseline recording started for " + this.transform.parent.name);
//        headMovementManager.GetComponent<HeadMovement>().StartEmteqDataRecording();
//    }
//}
