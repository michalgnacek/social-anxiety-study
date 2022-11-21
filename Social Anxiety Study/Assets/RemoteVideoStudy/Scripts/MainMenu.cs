using EmteqLabs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private ProgressManager progressManagerScript;

    public GameObject slowMovementStatus;
    public GameObject fastMovementStatus;
    public GameObject videoStatus;

    public GameObject slowMovementArrow;
    public GameObject fastMovementArrow;
    public GameObject videoArrow;
    public GameObject exitArrow;

    // Start is called before the first frame update
    void Start()
    {
        progressManagerScript = GameObject.FindGameObjectWithTag("ProgressManager").GetComponent<ProgressManager>();
        DisplaySegmentStatuses();
        DisplayNextSegmentArrow();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ExitApplication();
        }
    }

    public void ExitApplication()
    {
        Debug.Log("Closing session, exiting application");
        //This might not be needed anymore as session should be closed automatically
        //EmteqManager.StopSession();
        Application.Quit();
    }

    public void LoadSlowMovementScene()
    {
        SceneManager.LoadScene("Slow_movement_SignalCheck");
    }

    public void LoadFastMovementScene()
    {
        SceneManager.LoadScene("Fast_movement_SignalCheck");
    }

    public void LoadVideoScene()
    {
        SceneManager.LoadScene("Video_SignalCheck");
    }
    private void DisplaySegmentStatuses()
    {
        if (progressManagerScript.isSlowSegmentFinished)
        {
            slowMovementStatus.SetActive(true);
        }
        else
        {
            slowMovementStatus.SetActive(false);
        }

        if (progressManagerScript.isFastSegmentFinished)
        {
            fastMovementStatus.SetActive(true);
        }
        else
        {
            fastMovementStatus.SetActive(false);
        }

        if (progressManagerScript.isVideoSegmentFinished)
        {
            videoStatus.SetActive(true);
        }
        else
        {
            videoStatus.SetActive(false);
        }
    }

    private void DisplayNextSegmentArrow()
    {
        if (!progressManagerScript.isSlowSegmentFinished)
        {
            slowMovementArrow.SetActive(true);
        }
        else if (!progressManagerScript.isFastSegmentFinished)
        {
            fastMovementArrow.SetActive(true);
        }
        else if (!progressManagerScript.isVideoSegmentFinished)
        {
            videoArrow.SetActive(true);
        }
        else
        {
            exitArrow.SetActive(true);
        }
    }
}
