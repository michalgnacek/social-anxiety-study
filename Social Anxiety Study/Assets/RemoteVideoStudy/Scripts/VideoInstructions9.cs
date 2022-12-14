using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoInstructions9 : MonoBehaviour
{
    public AffectScale affectScaleScript;
    public VideoManager videoManagerScript;
    [SerializeField] private VideoPlayer _videoPlayer;

    int InstructionsWaitTime = 5;
    Vector2 affectTargetLocation = new Vector2(0.115f, 0.260f);

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
