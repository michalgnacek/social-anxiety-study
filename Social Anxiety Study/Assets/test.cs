using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    List<UnityEngine.XR.InputDevice> leftHandedControllers;
    UnityEngine.XR.InputDeviceCharacteristics desiredCharacteristics;
    UnityEngine.XR.InputDevice desiredController;
    Vector2 desiredControllerThumbstickPosition;
    void Start()
    {
        leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);

        foreach (var device in leftHandedControllers)
        {
            Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", device.name, device.characteristics.ToString()));
            desiredController = device;
        }
    }

    // Update is called once per frame
    void Update()
    {
        desiredController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out desiredControllerThumbstickPosition);
        print(desiredControllerThumbstickPosition);
    }
}
