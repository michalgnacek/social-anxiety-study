using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset_trigger : MonoBehaviour
{
    public VideoManager videoManagerScript;
    bool triggerValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (videoManagerScript.desiredController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && !triggerValue)
        {
            videoManagerScript.SetCanUserProgressToNextStageWithTriggerButton(true);
        }

        if (Input.GetKeyUp("space"))
        {
            videoManagerScript.SetCanUserProgressToNextStageWithTriggerButton(true);
        }
    }
}
