using EmteqLabs;
using UnityEngine;
using UnityEngine.Video;

public class EndOfTraining : MonoBehaviour
{

    public VideoManager videoManagerScript;
    public GameObject videoPlayerGameObject;
    public GameObject endOfTrainingGameObject;
    public AffectScale affectScaleScript;
    public GameObject trainingRatingText;

    [SerializeField] private VideoPlayer _videoPlayer;

    // Controller variables
    bool triggerValue;
    bool thumbstickPress;


    // Start is called before the first frame update
    void Start()
    {
        _videoPlayer.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") || videoManagerScript.desiredController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            TrainingFinished();
        }


        if (Input.GetKeyDown(KeyCode.R) || videoManagerScript.desiredController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out thumbstickPress) && thumbstickPress)
        {
            RepeatTraining();
        }
    }

    public void TrainingFinished()
    {
        EmteqManager.SetDataPoint("Video rating training finished");
        _videoPlayer.Pause();
        affectScaleScript.SetTrainingFinished(true);
        trainingRatingText.SetActive(false);
        videoPlayerGameObject.SetActive(true);
        videoManagerScript.VideoSceneNextStage();

    }

    public void RepeatTraining()
    {
        EmteqManager.SetDataPoint("Video rating training repeated");
        endOfTrainingGameObject.SetActive(false);
        _videoPlayer.Pause();
        videoPlayerGameObject.SetActive(true);
        videoManagerScript.RepeatTraining();

    }
}
