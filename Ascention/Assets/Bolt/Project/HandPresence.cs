using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;
    public bool primaryBtn;
    public bool secondaryBtn;
    public float trigger;
    public float grip;
    public Vector2 joystick2DAxis;
    public bool joystick2DAxisClick;


    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
        
        primaryBtn = false;
        secondaryBtn = false;
        trigger = 0.0f;
        grip = 0.0f;
        joystick2DAxis = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {

        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
           // Debug.Log("Pressing Primary Button");
        primaryBtn = primaryButtonValue;

        if (targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue) && secondaryButtonValue)
           // Debug.Log("Pressing Secondary Button");
        secondaryBtn = secondaryButtonValue;

        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
           // Debug.Log("Pressing Trigger" + triggerValue);
            trigger = triggerValue;
        }
        else 
        {
            trigger = 0.0f;
        }
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue) && gripValue > 0.1f)
        {
           // Debug.Log("Pressing Grip" + gripValue);
            grip = gripValue;
        }
        else
        {
            grip = 0.0f;
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
        {
           // Debug.Log("Primary Touchpad" + primary2DAxisValue);
            joystick2DAxis = primary2DAxisValue;
        }
        else 
        {
            joystick2DAxis = new Vector2(0f, 0f);

           /* if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool primary2DAxisClickValue) && primary2DAxisClickValue)
           // Debug.Log("Pressing Primary2DAxisClick");
            joystick2DAxisClick = primary2DAxisClickValue; */
        }
    }
}
