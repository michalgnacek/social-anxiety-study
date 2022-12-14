using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Linq;
using EmteqLabs;
using EmteqLabs.Video;
using EmteqLabs.Models;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class VideoManager : MonoBehaviour
{

    bool canUserProgressToNextStageWithTriggerButton = false;
    private int stageCounter = 0;
    public GameObject[] stages;
    public bool debugNextStageOnStart;
    public int debugStageCounter;
    public bool debugUseTestVideos;
    private int restPeriodBetweenVideosInSeconds = 60;
    //private int restPeriodBetweenVideosInSeconds = 5;

    [SerializeField] private VideoPlayer _videoPlayer;
    private List<VideoClip> _nextVideoClips = new List<VideoClip>();

    //[SerializeField] List<VideoClip> _neutralVideoClipsList = new List<VideoClip>();
    //[SerializeField] List<VideoClip> _negativeVideoClipsList = new List<VideoClip>();
    //[SerializeField] List<VideoClip> _positiveVideoClipsList = new List<VideoClip>();
    [SerializeField] List<VideoClip> _restVideoClip = new List<VideoClip>();

    [SerializeField] List<VideoClip> _nonSocialNeutralVideoClipsList = new List<VideoClip>();
    [SerializeField] List<VideoClip> _nonSocialPositiveVideoClipsList = new List<VideoClip>();
    [SerializeField] List<VideoClip> _nonSocialNegativeVideoClipsList = new List<VideoClip>();

    [SerializeField] List<VideoClip> _socialBUNeutralVideoClipsList = new List<VideoClip>();
    [SerializeField] List<VideoClip> _socialBUPositiveVideoClipsList = new List<VideoClip>();
    [SerializeField] List<VideoClip> _socialBUNegativeVideoClipsList = new List<VideoClip>();

    [SerializeField] List<VideoClip> _socialNeutralVideoClipsList = new List<VideoClip>();
    [SerializeField] List<VideoClip> _socialPositiveVideoClipsList = new List<VideoClip>();
    [SerializeField] List<VideoClip> _socialNegativeVideoClipsList = new List<VideoClip>();


    [SerializeField] List<VideoClip> _testneutralVideoClipsList = new List<VideoClip>();
    [SerializeField] List<VideoClip> _testnegativeVideoClipsList = new List<VideoClip>();
    [SerializeField] List<VideoClip> _testpositiveVideoClipsList = new List<VideoClip>();

    private int[] originalCategorySequenceArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private int[] categorySequenceArray = new int[9];
    int categoryCounter = 0;

    private int[] neutralCategories = new int[] { 1, 4, 7 };
    private int[] positiveCategories = new int[] { 2, 5, 8 };
    private int[] negativeCategories = new int[] { 3, 6, 9 };

    public GameObject relaxationVideoText;
    public GameObject relaxationVideoRatingReminderText;

    public GameObject welcomeGameObject;
    public GameObject welcomeText;

    public TextMeshProUGUI relaxCounterText;

    public GameObject calibrationGameObject;
    [SerializeField] private CustomCalibration _customCalibration;

    // Controller variables
    List<UnityEngine.XR.InputDevice> heldControllers;
    UnityEngine.XR.InputDeviceCharacteristics desiredCharacteristics;
    public UnityEngine.XR.InputDevice desiredController;
    bool triggerValue;
    public XRInteractorLineVisual leftXRInteractorLineVisual;
    public XRInteractorLineVisual rightXRInteractorLineVisual;

    private bool welcomeScreenShown = false;
    public bool skipCalibration;

    private void Awake()
    {
        _customCalibration.OnExpressionCalibrationComplete += OnExpressionCalibrationComplete;
    }

    private void OnExpressionCalibrationComplete(EmgCalibrationData expressionCalibrationData)
    {
        calibrationGameObject.SetActive(false);
        leftXRInteractorLineVisual.lineLength = 0f;
        rightXRInteractorLineVisual.lineLength = 0f;
        StartCoroutine(ShowWelcomeScreen());
    }

    // Start is called before the first frame update
    void Start()
    {
        heldControllers = new List<UnityEngine.XR.InputDevice>();
        desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, heldControllers);
        desiredController = heldControllers[0];

        EmteqManager.SetDataPoint("Cinema scene started");
        if (skipCalibration)
        {
           // StartCoroutine(ShowWelcomeScreen());
        } else
        {
            //calibrationGameObject.SetActive(true);
        }


        if (debugStageCounter != 0)
        {
            stageCounter = debugStageCounter;
        }
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
        SetCanUserProgressToNextStageWithTriggerButton(true);
        welcomeScreenShown = true;
    }
    // Update is called once per frame
    void Update()
    {

        if (canUserProgressToNextStageWithTriggerButton)
        {
            if (Input.GetKeyDown("space") || desiredController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                canUserProgressToNextStageWithTriggerButton = false;
                VideoSceneNextStage();
                if (welcomeGameObject.activeSelf)
                {
                    welcomeGameObject.SetActive(false);
                    welcomeText.SetActive(false);
                }
            }
        }
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

    public void SetupStudyVideosOld()
    {
        categorySequenceArray = originalCategorySequenceArray.Shuffle().ToArray();

        EmteqManager.SetDataPoint("Category sequence array numbers: " + categorySequenceArray[0] + ", " + categorySequenceArray[1] + ", " + categorySequenceArray[2] + ", " + categorySequenceArray[2] + ", " + categorySequenceArray[3] + ", " + categorySequenceArray[4] + ", " + categorySequenceArray[5] + ", " + categorySequenceArray[6] + ", " + categorySequenceArray[7] + ", " + categorySequenceArray[8]);
        EmteqManager.SetDataPoint("Category sequence: " + CategoryNumberToName(categorySequenceArray[0]) + "," +
            " " + CategoryNumberToName(categorySequenceArray[1]) + ", " +
            CategoryNumberToName(categorySequenceArray[2]) + ", " +
            CategoryNumberToName(categorySequenceArray[3]) + ", " +
            CategoryNumberToName(categorySequenceArray[4]) + ", " +
            CategoryNumberToName(categorySequenceArray[5]) + ", " +
            CategoryNumberToName(categorySequenceArray[6]) + ", " +
            CategoryNumberToName(categorySequenceArray[7]) + ", " +
            CategoryNumberToName(categorySequenceArray[8]));
    }

    //Returns true if two videos are of the same category (pos,neu,neg)
    private bool CheckVideosCategoryOrder(int videoCategoryA, int videoCategoryB)
    {
        
        if(positiveCategories.Contains(videoCategoryA) && positiveCategories.Contains(videoCategoryB))
        {
            return true;
        }
        else if (neutralCategories.Contains(videoCategoryA) && neutralCategories.Contains(videoCategoryB))
        {
            return true;
        }
        else if (negativeCategories.Contains(videoCategoryA) && negativeCategories.Contains(videoCategoryB))
        {
            return true;
        } else
        {
            return false;
        }

    }

    public void SetupStudyVideos()
    {
        //categorySequenceArray = originalCategorySequenceArray.Shuffle().ToArray();
        List<int> originalCategorySequenceList = originalCategorySequenceArray.ToList();
        List<int> randomisedCategorySequenceList = new List<int>();

        //Get first category by random
        int startingCategory = UnityEngine.Random.Range(1, originalCategorySequenceList.Count);
        randomisedCategorySequenceList.Add(startingCategory);
        originalCategorySequenceList.Remove(startingCategory);

        //Go through each category left
        for (int i=0; i<=7;i++)
        {
            int rnd = UnityEngine.Random.Range(0, originalCategorySequenceList.Count);
            while (CheckVideosCategoryOrder(randomisedCategorySequenceList[i], originalCategorySequenceList[rnd]))
            {
                rnd = UnityEngine.Random.Range(0, originalCategorySequenceList.Count);
                //Loop ended up with last element that cannot be placed in new category, restart from beginning
                if(i==7)
                {
                    //Reset values
                    originalCategorySequenceList = originalCategorySequenceArray.ToList();
                    randomisedCategorySequenceList = new List<int>();
                    startingCategory = UnityEngine.Random.Range(1, originalCategorySequenceList.Count);
                    randomisedCategorySequenceList.Add(startingCategory);
                    originalCategorySequenceList.Remove(startingCategory);
                    i = 0;
                }
            }
            randomisedCategorySequenceList.Add(originalCategorySequenceList[rnd]);
            originalCategorySequenceList.Remove(originalCategorySequenceList[rnd]);
        }

        //categorySequenceArray[0] = originalCategorySequenceArray.

        categorySequenceArray = randomisedCategorySequenceList.ToArray();

        EmteqManager.SetDataPoint("Category sequence array numbers: " + categorySequenceArray[0] + ", " + categorySequenceArray[1] + ", " + categorySequenceArray[2] + ", " + categorySequenceArray[3] + ", " + categorySequenceArray[4] + ", " + categorySequenceArray[5] + ", " + categorySequenceArray[6] + ", " + categorySequenceArray[7] + ", " + categorySequenceArray[8]);
        EmteqManager.SetDataPoint("Category sequence: " + CategoryNumberToName(categorySequenceArray[0]) + "," +
            " " + CategoryNumberToName(categorySequenceArray[1]) + ", " +
            CategoryNumberToName(categorySequenceArray[2]) + ", " +
            CategoryNumberToName(categorySequenceArray[3]) + ", " +
            CategoryNumberToName(categorySequenceArray[4]) + ", " +
            CategoryNumberToName(categorySequenceArray[5]) + ", " +
            CategoryNumberToName(categorySequenceArray[6]) + ", " +
            CategoryNumberToName(categorySequenceArray[7]) + ", " +
            CategoryNumberToName(categorySequenceArray[8]));
    }
    private String CategoryNumberToName(int categoryNumber)
    {
        String categoryName;
        switch (categoryNumber)
        {
            case 1:
                categoryName = "Non-Social Neutral";
                break;
            case 2:
                categoryName = "Non-Social Positive";
                break;
            case 3:
                categoryName = "Non-Social Negative";
                break;
            case 4:
                categoryName = "SocialBU Neutral";
                break;
            case 5:
                categoryName = "SocialBU Positive";
                break;
            case 6:
                categoryName = "SocialBU Negative";
                break;
            case 7:
                categoryName = "Social Neutral";
                break;
            case 8:
                categoryName = "Social Positive";
                break;
            case 9:
                categoryName = "Social Negative";
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
        if (categoryCounter < 9)
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
                        _nextVideoClips = _nonSocialNeutralVideoClipsList;
                        break;
                    case 2:
                        _nextVideoClips = _nonSocialPositiveVideoClipsList;
                        break;
                    case 3:
                        _nextVideoClips = _nonSocialNegativeVideoClipsList;
                        break;
                    case 4:
                        _nextVideoClips = _socialBUNeutralVideoClipsList;
                        break;
                    case 5:
                        _nextVideoClips = _socialBUPositiveVideoClipsList;
                        break;
                    case 6:
                        _nextVideoClips = _socialBUNegativeVideoClipsList;
                        break;
                    case 7:
                        _nextVideoClips = _socialNeutralVideoClipsList;
                        break;
                    case 8:
                        _nextVideoClips = _socialPositiveVideoClipsList;
                        break;
                    case 9:
                        _nextVideoClips = _socialNegativeVideoClipsList;
                        break;
                }
            }



            //TODO videoPlayer.SetVideoClips(_nextVideoClips.ToArray());
            Debug.Log("Playing category number: " + categoryNumber + " Category name: " + CategoryNumberToName(categoryNumber));
            EmteqManager.SetDataPoint("Playing category number: " + categoryNumber + " Category name: " + CategoryNumberToName(categoryNumber));
            PlayNextStudyVideoInCategory2();
            categoryCounter++;
        }
        else
        {
            EmteqManager.SetDataPoint("Finished playing all videos");
            VideoSceneNextStage();
            SetCanUserProgressToNextStageWithTriggerButton(true);
        }
    }

    public void PlayNextStudyVideoInCategory2()
    {
        //Random overload for int max value is not included
        int rnd = UnityEngine.Random.Range(0, _nextVideoClips.Count);
        _videoPlayer.clip = _nextVideoClips[rnd];
        _videoPlayer.Play();
        Debug.Log("Playing video number: " + _nextVideoClips[rnd].name);
        EmteqManager.SetDataPoint("Playing video number: " + _nextVideoClips[rnd].name);
        StartCoroutine(VideoPlaying(rnd));
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
        relaxCounterText.text = (categoryCounter+1) + " out of 9";
        StartCoroutine(PlayRestVideoWithDelay());
    }

    IEnumerator VideoPlaying(int videoNumberInList)
    {
        yield return new WaitForSeconds((float)_nextVideoClips[videoNumberInList].length);
        //Debug.Log("Finished playing video number: " + _nextVideoClips[videoNumberInList].name);
        EmteqManager.SetDataPoint("Finished playing video number: " + _nextVideoClips[videoNumberInList].name);
        yield return new WaitForSeconds(0.2f);
        if (_nextVideoClips.Count > 0)
        {
            _nextVideoClips.RemoveAt(videoNumberInList);
        }

        if (_nextVideoClips.Count != 0)
        {
            //TODO videoPlayer.SetVideoClips(_nextVideoClips.ToArray());
            PlayNextStudyVideoInCategory2();
        }
        else
        {
            //Debug.Log("Video category finished");
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
        //Debug.Log("Finished playing rest video");
        EmteqManager.SetDataPoint("Finished playing rest video");
        relaxationVideoText.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        PlayNextStudyVidoesCategory();
    }

    IEnumerator Delay(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    IEnumerator PlayRestVideoWithDelay()
    {
        //yield return new WaitForSeconds(1);
        //EmteqManager.StopRecordingData();
        //yield return new WaitForSeconds(3);
        //EmteqManager.StartRecordingData();
        _videoPlayer.clip = _restVideoClip[0];
        yield return new WaitForSeconds(1);
        EmteqManager.SetDataPoint("Playing rest video");
        _videoPlayer.Play();
        StartCoroutine(RestVideoPlaying());
        relaxationVideoText.SetActive(true);
    }
}
