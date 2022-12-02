using EmteqLabs.Video;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoInstructions8 : MonoBehaviour
{
    //public GameObject videoPlayerObject;
    //Emteq.Widgets.VideoPlayer.VideoPlayer videoPlayer;
    [SerializeField] private VideoClip[] _trainingVideoClips = new VideoClip[0];
    public AffectScale affectScaleScript;
    public VideoManager videoManagerScript;

    // [SerializeField] private SrtVideoPlayer _videoPlayer;
    [SerializeField] private VideoPlayer _videoPlayer;

    int instructionsWaitTime = 5;
    int affectTargetDelayTime = 5;
    Vector2 affectTargetLocation = new Vector2(0.055f, 0.15f);

    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        //_videoPlayer = videoPlayerObject.GetComponent<SrtVideoPlayer>();
        //_videoPlayer.SetVideoClip(_trainingVideoClips[0]);
        _videoPlayer.clip = _trainingVideoClips[0];
        //videoPlayer.SetVideoClips(_trainingVideoClips);

        //videoPlayer = videoPlayerObject.GetComponent<Emteq.Widgets.VideoPlayer.VideoPlayer>();
        //videoPlayer.SetVideoClips(_trainingVideoClips);

        _videoPlayer.Play();
        canvas = this.gameObject.GetComponent<Canvas>();
        canvas.enabled = false;
        StartCoroutine(DisplayInstructions());
        videoManagerScript.SetCanUserProgressToNextStageWithTriggerButton(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Display instructions after the training video has been playing for a few seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator DisplayInstructions()
    {
        yield return new WaitForSeconds(instructionsWaitTime);
        _videoPlayer.Pause();
        canvas.enabled = true;
        affectScaleScript.ShowAffectTarget(affectTargetLocation, affectTargetDelayTime);
    }
}
