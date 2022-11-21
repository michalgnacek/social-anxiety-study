//using Pvr_UnitySDKAPI;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class VideoInstructions2 : MonoBehaviour
//{

//    public VideoManager videoManagerScript;
//    public Animator greenTouchPadAnimator;
//    public MeshRenderer greenTouchPadMesh;

//    // Start is called before the first frame update
//    void Start()
//    {
//        videoManagerScript.SetCanUserProgressToNextStageWithTriggerButton(false);
//        greenTouchPadAnimator.enabled = true;
//        greenTouchPadMesh.enabled = true;
//    }

//    private void OnDisable()
//    {
//        greenTouchPadAnimator.enabled = false;
//        greenTouchPadMesh.enabled = false;
//        videoManagerScript.SetCanUserProgressToNextStageWithTriggerButton(true);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown("space") || Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TOUCHPAD))
//        {
//            videoManagerScript.VideoSceneNextStage();
//        }
//    }
//}
