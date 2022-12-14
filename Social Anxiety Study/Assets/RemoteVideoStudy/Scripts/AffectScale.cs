using EmteqLabs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffectScale : MonoBehaviour
{
    public GameObject touchObject;
    public GameObject yValueObject;
    public GameObject xValueObject;
    private Text yValueText;
    private Text xValueText;
    private MeshRenderer touchRenderer;
    private int hand;
    private int arousal;
    private int valence;
    private int oldArousal;
    private int oldValence;
    private int minimumDifferenceBetweenRatings = 1;

    public Text yPicoRaw;
    public Text xPicoRaw;

    public float mockValueX;
    public float mockValueY;

    public float mockMultiplier;

    public bool useConvertedCircleToSquareMethod;
    public bool useMockValues;

    float x_cordinate;
    float y_cordinate;

    public bool isTraining;
    public float valanceTrainingTarget;
    public float arousalTrainingTarget;

    public GameObject AffectTarget;

    public GameObject fingerLiftedMessage;
    public bool isFingerLifted = true;

    public int testX = 0;
    public int testY = 0;

    private bool newRatingDelayFinished = true;
    private float newRatingMarkerDelayInSeconds = 0.1f;

    private bool trainingFinished;

    // Controller variables
    List<UnityEngine.XR.InputDevice> heldControllers;
    UnityEngine.XR.InputDeviceCharacteristics desiredCharacteristics;
    public UnityEngine.XR.InputDevice desiredController;
    Vector2 desiredControllerThumbstickPosition;


    // Start is called before the first frame update
    void Start()
    {
        //Get controller
        heldControllers = new List<UnityEngine.XR.InputDevice>();
        //desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        //desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Left;
        desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, heldControllers);
        desiredController = heldControllers[0];

        touchRenderer = touchObject.GetComponent<MeshRenderer>();
        hand = 0;
        yValueText = yValueObject.GetComponent<Text>();
        xValueText = xValueObject.GetComponent<Text>();

        mockValueX = 0f;
        mockValueY = 0f;

        mockMultiplier = 0f;

        arousal = 0;
        valence = 0;

        oldValence = valence;
        oldArousal = arousal;

        trainingFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        // touchObject.transform.localPosition = new Vector3(0f + Time.deltaTime, 0, 0);
        //  UpdateFingerPositionTextValues();
        //  if (Controller.UPvr_IsTouching(0))
        //    {
        UpdateFingerPosition();
        UpdateRawPicoTextValues();
        //  }

        //ConvertCircleToSquare();

        //if (Input.GetKey(KeyCode.I))
        //{
        //    EmteqManager.SetDataPoint("test");
        //}

    }

    void UpdateFingerPosition()
    {
        //if (Controller.UPvr_IsTouching(0))
        //
        //   touchRenderer.enabled = true;
        //touchObject.transform.localPosition = new Vector3(1.3f - Controller.UPvr_GetTouchPadPosition(hand).y * 0.01f, 1.6f, -1.7f - Controller.UPvr_GetTouchPadPosition(hand).x * 0.01f);
        //touchObject.transform.localPosition = new Vector3(1.3f - Controller.UPvr_GetTouchPadPosition(hand).y * 0.001f, -1.7f - Controller.UPvr_GetTouchPadPosition(hand).x * 0.001f, -0.01f);

        //Using mock values
        // touchObject.transform.localPosition = new Vector3(0f - mockValueX * 0.0005f, 0.3f - mockValueY * 0.0005f, -0.01f);
        //touchObject.transform.localPosition = new Vector3(-0.15f + mockValueX * mockMultiplier, 0.3635f - mockValueY * 0.001f, -0.01f);

        //working mock with raw pico values
        //  touchObject.transform.localPosition = new Vector3(-0.152f + mockValueX * 0.0012f, 0.11f + mockValueY * 0.0012f, 0.15f);

        //working pico controller with raw pico values
        // touchObject.transform.localPosition = new Vector3(-0.152f + Controller.UPvr_GetTouchPadPosition(hand).y * 0.0012f, 0.11f + Controller.UPvr_GetTouchPadPosition(hand).x * 0.0012f, 0.15f);

        // mock with translated values to -1 and 1 range
        //  touchObject.transform.localPosition = new Vector3(ConvertPicoTouchpadCoridinateSystem(mockValueX) * 0.15f, 0.26f + ConvertPicoTouchpadCoridinateSystem(mockValueY) * 0.15f, 0.15f);


        //Debug.Log("X_converted: " + ConvertPicoTouchpadCoridinateSystem(mockValueX));
        //Debug.Log("Y_converted: " + ConvertPicoTouchpadCoridinateSystem(mockValueY));

        if (useMockValues)
        {
            x_cordinate = mockValueX;
            y_cordinate = mockValueY;
        }
        else
        {
            //Pico has weird cordinate system, x and y are reversed
            //x_cordinate = Controller.UPvr_GetTouchPadPosition(hand).y;
            //y_cordinate = Controller.UPvr_GetTouchPadPosition(hand).x;

            desiredController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out desiredControllerThumbstickPosition);
            x_cordinate = desiredControllerThumbstickPosition.x;
            y_cordinate = desiredControllerThumbstickPosition.y;
            //print("x: " + desiredControllerThumbstickPosition);
        }
        //if (x_cordinate == 0 && y_cordinate == 0)

        //{
        //    if (!isFingerLifted)
        //    {
        //        isFingerLifted = true;
        //        fingerLiftedMessage.SetActive(true);
        //        EmteqManager.SetDataPoint("Finger lifted");
        //    }
        //}
        //else
        //{
            //if (isFingerLifted)
            //{
            //    isFingerLifted = false;
            //    EmteqManager.SetDataPoint("Finger back on touchpad");
            //    fingerLiftedMessage.SetActive(false);
            //}


            //x_cordinate = ConvertPicoTouchpadCoridinateSystem(x_cordinate);
            //y_cordinate = ConvertPicoTouchpadCoridinateSystem(y_cordinate);

            //Debug.Log("X_converted: " + x_cordinate);
            //Debug.Log("Y_converted: " + y_cordinate);

            if (useConvertedCircleToSquareMethod)
            {
                float xSquared = Mathf.Pow(x_cordinate, 2);
                float ySquared = Mathf.Pow(y_cordinate, 2);
                if (xSquared >= ySquared)
                {
                    float new_x = Mathf.Sign(x_cordinate) * Mathf.Sqrt(xSquared + ySquared);
                    float new_y = Mathf.Sign(x_cordinate) * (y_cordinate / x_cordinate) * Mathf.Sqrt(xSquared + ySquared);
                    x_cordinate = new_x;
                    y_cordinate = new_y;
                }
                else
                {
                    float new_x = Mathf.Sign(y_cordinate) * (x_cordinate / y_cordinate) * Mathf.Sqrt(xSquared + ySquared);
                    float new_y = Mathf.Sign(y_cordinate) * Mathf.Sqrt(xSquared + ySquared);
                    x_cordinate = new_x;
                    y_cordinate = new_y;
                }

            }
            if (!float.IsNaN(x_cordinate) && !float.IsNaN(y_cordinate))
            {
                //touchObject.transform.localPosition = new Vector3(x_cordinate * 0.15f, 0.26f + y_cordinate * 0.15f, 0.15f);
                //touchObject.transform.localPosition = new Vector3(x_cordinate * 0.1f, 0.28f + y_cordinate * 0.1f, 0.14f);
                touchObject.transform.localPosition = new Vector3(x_cordinate*0.15f, 0.25f + (y_cordinate*0.15f), 0.14f);
                //valence = ConvertCordinatesToAffectValues(x_cordinate);
                //arousal = ConvertCordinatesToAffectValues(y_cordinate);



                valence = (int)ConvertCordinatesToValence(touchObject.transform.localPosition.x);
                arousal = (int)ConvertCordinatesToArousal(touchObject.transform.localPosition.y);

                //TODO Disabled arousal/valence marker - pending workaround

                //DSL can not handle too many messages. Reduce the number of messages by introducing delay between them.
                if (trainingFinished && newRatingDelayFinished && AreNewRatingsSufficientlyDifferent())
                {
                    EmteqManager.SetDataPoint("Valence:" + valence + ", Arousal:" + arousal + ", RawX:" + x_cordinate + ", RawY:" + y_cordinate);
                    newRatingDelayFinished = false;
                    oldValence = valence;
                    oldArousal = arousal;
                    StartCoroutine(NewRatingDelay());
                }
                UpdateFingerPositionTextValues();
            }

        //}


        //valence = ConvertCordinatesToValence(touchObject.transform.localPosition.x);
        //arousal = ConvertCordinatesToArousal(touchObject.transform.localPosition.y);

        // }
        // else
        //{
        //touchRenderer.enabled = false;
        // }

        //if (isTraining)
        //{
        //    CheckAffectTargetValues();
        //}
    }

    void UpdateFingerPositionTextValues()
    {
        yValueText.text = "Arousal: " + arousal.ToString("F0");
        xValueText.text = "Valence: " + valence.ToString("F0");
    }

    IEnumerator NewRatingDelay()
    {
        yield return new WaitForSeconds(newRatingMarkerDelayInSeconds);
        newRatingDelayFinished = true;
    }

    bool AreNewRatingsSufficientlyDifferent()
    {
        if (Mathf.Abs(valence - oldValence) >= minimumDifferenceBetweenRatings)
        {
            return true;
        }
        if (Mathf.Abs(arousal - oldArousal) >= minimumDifferenceBetweenRatings)
        {
            return true;
        }
        return false;
    }

    public void ShowAffectTarget(Vector2 targetVector2Location, int delay)
    {
        StartCoroutine(DisplayAffectTarget(delay));
        AffectTarget.GetComponent<Transform>().localPosition = new Vector3(targetVector2Location.x, targetVector2Location.y, 0.15f);
    }

    IEnumerator DisplayAffectTarget(int delay)
    {
        yield return new WaitForSeconds(delay);
        AffectTarget.SetActive(true);
    }

    public Vector3 GetAffectValue()
    {
        return touchObject.transform.position;
    }

    void UpdateRawPicoTextValues()
    {
        yPicoRaw.text = "Pico raw Y2: " + y_cordinate.ToString();
        xPicoRaw.text = "Pico raw X2: " + x_cordinate.ToString();
    }

    void ConvertCircleToSquare()
    {
        float normalizedValue = Mathf.InverseLerp(0, 255, mockValueX);
        float result = Mathf.Lerp(-1, 1, normalizedValue);
        Debug.Log("Mock Value X: " + mockValueX + " normalisedValue: " + normalizedValue + " result: " + result);
    }

    /// <summary>
    /// Pico touchpad input returns value between 0 and 255. We need range from -1 to 1.
    /// </summary>
    float ConvertPicoTouchpadCoridinateSystem(float PicoCoridnateValue)
    {
        float normalizedValue = Mathf.InverseLerp(0, 255, PicoCoridnateValue);
        return Mathf.Lerp(-1, 1, normalizedValue);
    }

    /// <summary>
    /// Cordinates are in -1 to 1 range but arousal and valence are normally calculated usuing 1-9 scale.
    /// </summary>
    float ConvertCordinatesToAffectValues(float originalValue)
    {
        float normalizedValue = Mathf.InverseLerp(-1, 1, originalValue);
        return Mathf.Lerp(1, 9, normalizedValue);
    }

    float ConvertCordinatesToArousal(float originalValue)
    {
        float normalizedValue = Mathf.InverseLerp(0.14f, 0.34f, originalValue);
        return Mathf.Lerp(1, 9, normalizedValue);
    }

    float ConvertCordinatesToValence(float originalValue)
    {
        float normalizedValue = Mathf.InverseLerp(-0.10f, 0.10f, originalValue);
        return Mathf.Lerp(1, 9, normalizedValue);
    }

    public void SetTrainingFinished(bool trainingFinished)
    {
        this.trainingFinished = trainingFinished;
    }
}
