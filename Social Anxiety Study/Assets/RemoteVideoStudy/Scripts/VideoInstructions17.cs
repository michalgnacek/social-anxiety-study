using UnityEngine;

public class VideoInstructions17 : MonoBehaviour
{
    public VideoManager videoManagerScript;
    public GameObject videoPlayerGameObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        videoManagerScript.SetCanUserProgressToNextStageWithTriggerButton(false);
        videoManagerScript.PlayRestVideo();
        videoPlayerGameObject.SetActive(true);
        //videoManagerScript.PlayNextStudyVidoesCategory();

    }
}
