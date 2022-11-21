using EmteqLabs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RatingsFinished : MonoBehaviour
{
    private ProgressManager progressManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        progressManagerScript = GameObject.FindGameObjectWithTag("ProgressManager").GetComponent<ProgressManager>();
        progressManagerScript.isVideoSegmentFinished = true;
        StartCoroutine(RatingsFinishedExit());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RatingsFinishedExit()
    {
        EmteqManager.SetDataPoint("Video ratings study: finished data recording");
        yield return new WaitForSeconds(5);
        EmteqManager.StopRecordingData();
        SceneManager.LoadScene("Menu");
    }
}
