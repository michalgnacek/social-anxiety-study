using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoInstructions2 : MonoBehaviour
{

    public VideoManager videoManagerScript;
    //public Animator greenTouchPadAnimator;
    //public MeshRenderer greenTouchPadMesh;

    public GameObject instructionsController;

    bool thumbstickPress;
    // Start is called before the first frame update
    void Start()
    {
        videoManagerScript.SetCanUserProgressToNextStageWithTriggerButton(false);
        //greenTouchPadAnimator.enabled = true;
        //greenTouchPadMesh.enabled = true;

        instructionsController.SetActive(true);
    }

    private void OnDisable()
    {
        //greenTouchPadAnimator.enabled = false;
        //greenTouchPadMesh.enabled = false;
        videoManagerScript.SetCanUserProgressToNextStageWithTriggerButton(true);
        instructionsController.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") || videoManagerScript.desiredController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out thumbstickPress) && thumbstickPress)
        {
            videoManagerScript.VideoSceneNextStage();
        }
    }
}
