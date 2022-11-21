//using EmteqLabs;
//using EmteqLabs.Faceplate;
//using EmteqLabs.MaskProtocol;
//using EmteqLabs.Models;
//using Pvr_UnitySDKAPI;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class SignalCheck : MonoBehaviour
//{
//    public Text fitStateText;

//    public Image rightFrontalisLED;
//    public Image rightZygomaticusLED;
//    public Image rightOrbicularisLED;
//    public Image centerCorrugatorLED;
//    public Image leftOrbicularisLED;
//    public Image leftZygomaticusLED;
//    public Image leftFrontalisLED;

//    Color unsettledColor = new Color(255, 0, 0);
//    Color settledColor = new Color(0, 255, 0);

//    // Start is called before the first frame update

//    FitState fitState;

//    private List<SensorGUIObject> _sensors;
//    private Dictionary<MuscleMapping, ushort> _emgAmplitudeRms;

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown("space") || Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER))
//        {
//            Continue();
//        }

//        _emgAmplitudeRms = EmteqManager.GetEmgAmplitudeRms();
//        foreach (SensorGUIObject sensor in _sensors)
//        {
//            sensor.SetSensorValue(_emgAmplitudeRms[sensor.SensorName]);
//        }
//    }


//    private void Start()
//    {
//        EmteqManager.OnDeviceFitStateChange += OnDeviceFitStateChange;
//        EmteqManager.OnSensorContactStateChange += OnSensorContactStateChange;
//        ChangeAllSensorsToUnsettled();
//        EmteqManager.StartRecordingData();
//        StartCoroutine(WaitStartRecording());
//    }

//    IEnumerator WaitStartRecording()
//    {
//        yield return new WaitForSeconds(1);
//        EmteqManager.SetDataPoint("Start of signal check. Started data recording. " + SceneManager.GetActiveScene().name);
//    }

//    private void OnDisable()
//    {

//        EmteqManager.OnDeviceConnect -= OnEmteqDeviceConnectionSuccess;
//    }

//    private void OnEmteqDeviceConnectionSuccess()
//    {
//        print("blabla");
//    }

//    public void OnDestroy()
//    {
//        EmteqManager.OnDeviceFitStateChange -= OnDeviceFitStateChange;
//        EmteqManager.OnSensorContactStateChange -= OnSensorContactStateChange;
//    }

//    void OnDeviceFitStateChange(FitState f)
//    {
//        fitStateText.text = ((int)Mathf.Lerp(0, 100, Mathf.InverseLerp(0, 9, (int)f))).ToString() + "%";
//        fitState = f;
//    }

//    private void OnSensorContactStateChange(Dictionary<MuscleMapping, ContactState> sensorcontactstate)
//    {
//        foreach (SensorGUIObject sensor in _sensors)
//        {
//            sensor.SetContactState(sensor.SensorName, sensorcontactstate[sensor.SensorName]);
//        }
//    }

//    void checkSensorSignal(int muscleCounter, ContactState contactState)
//    {
//        Image ledImageToChange = null;

//            if (muscleCounter == (int)Muscle.RightFrontalis)
//            {
//                ledImageToChange = rightFrontalisLED;
//            } else if (muscleCounter == (int)Muscle.RightZygomaticus)
//            {
//                ledImageToChange = rightZygomaticusLED;
//            } else if (muscleCounter == (int)Muscle.RightOrbicularis)
//            {
//                ledImageToChange = rightOrbicularisLED;
//            } else if (muscleCounter == (int)Muscle.CenterCorrugator)
//            {
//                ledImageToChange = centerCorrugatorLED;
//            } else if (muscleCounter == (int)Muscle.LeftOrbicularis)
//            {
//                ledImageToChange = leftOrbicularisLED;
//            } else if (muscleCounter == (int)Muscle.LeftZygomaticus)
//            {
//                ledImageToChange = leftZygomaticusLED;
//            } else if (muscleCounter == (int)Muscle.LeftFrontalis)
//            {
//                ledImageToChange = leftFrontalisLED;
//            } else
//            {
//                Debug.LogError("Error checking contact state for sensor: " + muscleCounter);
//            }

//        if (ledImageToChange != null)
//        {
//            if (contactState == ContactState.Settled)
//            {
//                ledImageToChange.color = settledColor;
//            }
//            else
//            {
//                ledImageToChange.color = unsettledColor;
//            }
//        }
        
//    }

//    public void Continue()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
//        EmteqManager.SetDataPoint("Signal check finished. Fit state: " + fitState.ToString() + " value = " + (int)fitState);
//    }

//    public void BackToMainMenu()
//    {
//        EmteqManager.SetDataPoint("Stopped data recording for signal check. Back button pressed.");
//        EmteqManager.StopRecordingData();
//        SceneManager.LoadScene("Menu");
//    }

//    public void ChangeAllSensorsToUnsettled()
//    {
//        Image[] images = new Image[7] { rightFrontalisLED, rightZygomaticusLED, rightOrbicularisLED, centerCorrugatorLED, leftOrbicularisLED, leftZygomaticusLED, leftFrontalisLED };

//        foreach(Image image in images)
//        {
//            image.color = unsettledColor;
//        }

//    }

//}
