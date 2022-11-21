//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class VideoInstructions15 : MonoBehaviour
//{
//    public GameObject videoPlayerObject;
//    Emteq.Widgets.VideoPlayer.VideoPlayer videoPlayer;
//    public AffectScale affectScaleScript;
//    public VideoManager videoManagerScript;

//    int InstructionsWaitTime = 5;
//    Vector2 affectTargetLocation = new Vector2(-0.075f, 0.26f);

//    Canvas canvas;

//    // Start is called before the first frame update
//    void Start()
//    {
//        videoPlayer = videoPlayerObject.GetComponent<Emteq.Widgets.VideoPlayer.VideoPlayer>();
//    }

//    void OnEnable()
//    {
//        videoPlayer.Play();
//        canvas = this.gameObject.GetComponent<Canvas>();
//        canvas.enabled = false;
//        StartCoroutine(EndTraining());
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    /// <summary>
//    /// Display instructions after the training video has been playing for a few seconds
//    /// </summary>
//    /// <returns></returns>
//    IEnumerator EndTraining()
//    {
//        yield return new WaitForSeconds(InstructionsWaitTime);
//        videoPlayer.Pause();
//        canvas.enabled = true;
//       // videoManagerScript.SetCanUserProgressToNextStageWithTriggerButton(false);
//    }
//}
