using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoInstructions14 : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    public AffectScale affectScaleScript;
    public VideoManager videoManagerScript;

    int InstructionsWaitTime = 12;
    Vector2 affectTargetLocation = new Vector2(-0.075f, 0.26f);

    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        _videoPlayer.Play();
        canvas = this.gameObject.GetComponent<Canvas>();
        canvas.enabled = false;
        StartCoroutine(DisplayInstructions());
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
}
