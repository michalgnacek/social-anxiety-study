using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoInstructions12 : MonoBehaviour
{
    public AffectScale affectScaleScript;
    public VideoManager videoManagerScript;
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private VideoClip[] _trainingVideoClips = new VideoClip[0];

    int InstructionsWaitTime = 3;
    Vector2 affectTargetLocation = new Vector2(0f, 0.25f);

    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        _videoPlayer.clip = _trainingVideoClips[1];
        _videoPlayer.Play();
        canvas = this.gameObject.GetComponent<Canvas>();
        canvas.enabled = false;
        StartCoroutine(DisplayNextVideo());
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
        yield return new WaitForSeconds(InstructionsWaitTime);
        _videoPlayer.Pause();
        canvas.enabled = true;
        affectScaleScript.ShowAffectTarget(affectTargetLocation, 5);

    }

    IEnumerator DisplayNextVideo()
    {
        yield return new WaitForSeconds(3);
        _videoPlayer.Play();
        StartCoroutine(DisplayInstructions());
    }
}
