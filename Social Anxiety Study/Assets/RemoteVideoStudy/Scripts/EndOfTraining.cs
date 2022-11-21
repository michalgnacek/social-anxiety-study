//using EmteqLabs;
//using Pvr_UnitySDKAPI;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EndOfTraining : MonoBehaviour
//{

//    public VideoManager videoManagerScript;
//    public GameObject videoPlayerGameObject;
//    public GameObject endOfTrainingGameObject;
//    public AffectScale affectScaleScript;
//    public GameObject trainingRatingText;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER) || Input.GetKeyDown("space"))
//        {
//            TrainingFinished();
//        }

//        if (Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TOUCHPAD) || Input.GetKeyDown(KeyCode.R))
//        {
//            RepeatTraining();
//        }
//    }

//    public void TrainingFinished()
//    {
//        EmteqManager.SetDataPoint("Video rating training finished");
//        affectScaleScript.SetTrainingFinished(true);
//        trainingRatingText.SetActive(false);
//        videoPlayerGameObject.SetActive(true);
//        videoManagerScript.VideoSceneNextStage();

//    }

//    public void RepeatTraining()
//    {
//        EmteqManager.SetDataPoint("Video rating training repeated");
//        endOfTrainingGameObject.SetActive(false);
//        videoPlayerGameObject.SetActive(true);
//        videoManagerScript.RepeatTraining();

//    }
//}
