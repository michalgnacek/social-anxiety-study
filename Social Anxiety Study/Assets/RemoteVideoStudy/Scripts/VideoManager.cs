using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Linq;
using EmteqLabs;
using EmteqLabs.Video;

public class VideoManager : MonoBehaviour
{

    bool canUserProgressToNextStageWithTriggerButton;
    private int stageCounter = 0;
    public GameObject[] stages;
    public bool debugNextStageOnStart;
    public int debugStageCounter;
    public bool debugUseTestVideos;
    private int restPeriodBetweenVideosInSeconds = 120;
    //private int restPeriodBetweenVideosInSeconds = 5;

    public GameObject videoPlayerObject;
    [SerializeField] private SrtVideoPlayer _videoPlayer;
    private List<VideoClip> _nextVideoClips = new List<VideoClip>();

    [SerializeField] List<VideoClip> _neutralVideoClipsList = new List<VideoClip>();
    [SerializeField] List<VideoClip> _negativeVideoClipsList = new List<VideoClip>();
    [SerializeField] List<VideoClip> _positiveVideoClipsList = new List<VideoClip>();
    [SerializeField] List<VideoClip> _restVideoClip = new List<VideoClip>();


    [SerializeField] List<VideoClip> _testneutralVideoClipsList = new List<VideoClip>();
    [SerializeField] List<VideoClip> _testnegativeVideoClipsList = new List<VideoClip>();
    [SerializeField] List<VideoClip> _testpositiveVideoClipsList = new List<VideoClip>();

    private int[] originalCategorySequenceArray = new int[] { 1, 2, 3 };
    private int[] categorySequenceArray = new int[3];
    int categoryCounter = 0;

    public GameObject relaxationVideoText;
    public GameObject relaxationVideoRatingReminderText;

    public GameObject welcomeGameObject;
    public GameObject welcomeText;

    // Controller variables
    List<UnityEngine.XR.InputDevice> heldControllers;
    UnityEngine.XR.InputDeviceCharacteristics desiredCharacteristics;
    public UnityEngine.XR.InputDevice desiredController;
    bool triggerValue;

    // Start is called before the first frame update
    void Start()
    {
        heldControllers = new List<UnityEngine.XR.InputDevice>();
        desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, heldControllers);
        desiredController = heldControllers[0];

        EmteqManager.SetDataPoint("Cinema scene started");
        StartCoroutine(ShowWelcomeScreen());


        if (debugStageCounter != 0)
        {
            stageCounter = debugStageCounter;
        }
        canUserProgressToNextStageWithTriggerButton = true;
        if (debugNextStageOnStart)
        {
            VideoSceneNextStage();
        }
    }

    // every 2 seconds perform the print()
    private IEnumerator ShowWelcomeScreen()
    {
        yield return new WaitForSeconds(3);
        welcomeGameObject.SetActive(true);
        welcomeText.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (canUserProgressToNextStageWithTriggerButton)
        {
            if (Input.GetKeyDown("space") || desiredController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                VideoSceneNextStage();
                if (welcomeGameObject.activeSelf)
                {
                    welcomeGameObject.SetActive(false);
                    welcomeText.SetActive(false);
                }
            }
        }

        //if (controller.upvr_getkeydown(0, pvr_keycode.app) || input.getkeydown(keycode.q))
        //{
        //    loadmenu();
        //}
    }

    public void LoadMenu()
    {
        EmteqManager.SetDataPoint("Stopped data recording for video ratings. Exited without finishing.");
        EmteqManager.StopRecordingData();
        SceneManager.LoadScene("Menu");
    }

    public void VideoSceneNextStage()
    {
        if (stageCounter < stages.Length)
        {

            if (stageCounter != 0)
            {
                stages[stageCounter - 1].SetActive(false);

            }

            stages[stageCounter].SetActive(true);
            stageCounter++;
        }
        else
        {
            EmteqManager.SetDataPoint("Stopped data recording for cinema");
            EmteqManager.StopRecordingData();
            LoadMenu();
        }
    }

    public void SetCanUserProgressToNextStageWithTriggerButton(bool canUserProgressToNextStageWithTriggerButton)
    {
        this.canUserProgressToNextStageWithTriggerButton = canUserProgressToNextStageWithTriggerButton;
    }

    public void RepeatTraining()
    {
        stageCounter = 6;
        VideoSceneNextStage();
        SetCanUserProgressToNextStageWithTriggerButton(true);
    }

    public void SetupStudyVideos()
    {
        //TODO videoPlayer = videoPlayerObject.GetComponent<Emteq.Widgets.VideoPlayer.VideoPlayer>();
        categorySequenceArray = originalCategorySequenceArray.Shuffle().ToArray();
        EmteqManager.SetDataPoint("Category sequence array numbers: " + categorySequenceArray[0] + ", " + categorySequenceArray[1] + ", " + categorySequenceArray[2]);
        EmteqManager.SetDataPoint("Category sequence: " + CategoryNumberToName(categorySequenceArray[0]) + "," +
            " " + CategoryNumberToName(categorySequenceArray[1]) + ", " +
            CategoryNumberToName(categorySequenceArray[2]));
    }

    private String CategoryNumberToName(int categoryNumber)
    {
        String categoryName;
        switch (categoryNumber)
        {
            case 1:
                categoryName = "Neutral";
                break;
            case 2:
                categoryName = "Negative";
                break;
            case 3:
                categoryName = "Positive";
                break;
            default:
                categoryName = "Invalid Category";
                break;
        }
        return categoryName;
    }

    public void PlayNextStudyVidoesCategory()
    {
        if (canUserProgressToNextStageWithTriggerButton)
        {
            SetCanUserProgressToNextStageWithTriggerButton(false);
        }
        if (categoryCounter < 3)
        {
            int categoryNumber = categorySequenceArray[categoryCounter];

            if (debugUseTestVideos)
            {
                switch (categoryNumber)
                {
                    case 1:
                        _nextVideoClips = _testneutralVideoClipsList;
                        break;
                    case 2:
                        _nextVideoClips = _testnegativeVideoClipsList;
                        break;
                    case 3:
                        _nextVideoClips = _testpositiveVideoClipsList;
                        break;
                }
            }
            else
            {
                switch (categoryNumber)
                {
                    case 1:
                        _nextVideoClips = _neutralVideoClipsList;
                        break;
                    case 2:
                        _nextVideoClips = _negativeVideoClipsList;
                        break;
                    case 3:
                        _nextVideoClips = _positiveVideoClipsList;
                        break;
                }
            }



            //TODO videoPlayer.SetVideoClips(_nextVideoClips.ToArray());
            //Debug.Log("Playing category number: " + categoryNumber + " Category name: " + CategoryNumberToName(categoryNumber));
            EmteqManager.SetDataPoint("Playing category number: " + categoryNumber + " Category name: " + CategoryNumberToName(categoryNumber));
            PlayNextStudyVideoInCategory();
            categoryCounter++;
        }
        else
        {
            EmteqManager.SetDataPoint("Finished playing all videos");
            VideoSceneNextStage();
            SetCanUserProgressToNextStageWithTriggerButton(true);
        }
    }

    public void PlayNextStudyVideoInCategory()
    {
        //Random overload for int max value is not included
        int rnd = UnityEngine.Random.Range(0, _nextVideoClips.Count);
        //TODO videoPlayer.Play(rnd);
        // Debug.Log("Playing video number: " + _nextVideoClips[rnd].name);
        EmteqManager.SetDataPoint("Playing video number: " + _nextVideoClips[rnd].name);
        StartCoroutine(VideoPlaying(rnd));
    }

    public void PlayRestVideo()
    {
        //StartCoroutine(Delay(1));
        ////Debug.Log("Playing rest video");
        //EmteqManager.StopRecordingData();
        //StartCoroutine(Delay(1));
        //EmteqManager.StartRecordingData();
        //StartCoroutine(Delay(1));
        //EmteqManager.SetDataPoint("Playing rest video");
        //videoPlayer.SetVideoClips(_restVideoClip.ToArray());
        //videoPlayer.Play(0);
        //relaxationVideoText.SetActive(true);
        //StartCoroutine(RestVideoPlaying());
        StartCoroutine(PlayRestVideoWithDelay());
    }

    IEnumerator VideoPlaying(int videoNumberInList)
    {
        yield return new WaitForSeconds(30);
        //Debug.Log("Finished playing video number: " + _nextVideoClips[videoNumberInList].name);
        EmteqManager.SetDataPoint("Finished playing video number: " + _nextVideoClips[videoNumberInList].name);
        if (_nextVideoClips.Count > 0)
        {
            _nextVideoClips.RemoveAt(videoNumberInList);
        }

        if (_nextVideoClips.Count != 0)
        {
            //TODO videoPlayer.SetVideoClips(_nextVideoClips.ToArray());
            PlayNextStudyVideoInCategory();
        }
        else
        {
            Debug.Log("Video category finished");
            EmteqManager.SetDataPoint("Video category finished");
            PlayRestVideo();
        }

    }

    IEnumerator RestVideoPlaying()
    {
        yield return new WaitForSeconds(restPeriodBetweenVideosInSeconds - 10);
        relaxationVideoRatingReminderText.SetActive(true);
        yield return new WaitForSeconds(10);
        relaxationVideoRatingReminderText.SetActive(false);
        Debug.Log("Finished playing rest video");
        EmteqManager.SetDataPoint("Finished playing rest video");
        relaxationVideoText.SetActive(false);
        PlayNextStudyVidoesCategory();
    }

    IEnumerator Delay(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    IEnumerator PlayRestVideoWithDelay()
    {
        yield return new WaitForSeconds(1);
        EmteqManager.StopRecordingData();
        yield return new WaitForSeconds(3);
        EmteqManager.StartRecordingData();
        yield return new WaitForSeconds(1);
        EmteqManager.SetDataPoint("Playing rest video");
        //TODO videoPlayer.SetVideoClips(_restVideoClip.ToArray());
        //TODO videoPlayer.Play(0);
        StartCoroutine(RestVideoPlaying());
        relaxationVideoText.SetActive(true);
    }
}
