using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VIdeoInstructions16 : MonoBehaviour
{
    public VideoManager videoManagerScript;
    [SerializeField] private VideoPlayer _videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _videoPlayer.Pause();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnEnable()
    {
        //videoManagerScript.SetCanUserProgressToNextStageWithTriggerButton(true);
        videoManagerScript.SetupStudyVideos();
    }
}
