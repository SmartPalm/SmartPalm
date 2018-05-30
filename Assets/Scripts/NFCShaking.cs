using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NFCShaking : MonoBehaviour
{

    private const string NFC_CLASS = "com.smartpalm.nativebluetoothplugin.NFCService";

    float accelerometerUpdateInterval = 1.0f / 60.0f;
    // The greater the value of LowPassKernelWidthInSeconds, the slower the
    // filtered value will converge towards current input sample (and vice versa).
    float lowPassKernelWidthInSeconds = 1.0f;
    // This next parameter should be initialized to 2.0 per Apple's recommendation
    float shakeDetectionThreshold = 3.0f;
    int shakeCounter = 0;
    bool isLandscape;

    float lowPassFilterFactor;
    Vector3 lowPassValue;

    void Start()
    {
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;

        using (AndroidJavaClass nfcServiceClass = new AndroidJavaClass(NFC_CLASS))
        {
            AndroidJavaObject nfcService = nfcServiceClass.CallStatic<AndroidJavaObject>("getInstance");
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject context = jc.GetStatic<AndroidJavaObject>("currentActivity");

            nfcService.Call("setMainContext", context);
        }
    }

    void Update()
    {
        Vector3 acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        Vector3 deltaAcceleration = acceleration - lowPassValue;

        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            isLandscape = true;
        }
        else
        {
            isLandscape = false;
        }

        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold/* && isLandscape*/)
        {
            // Perform your "shaking actions" here. If necessary, add suitable
            // guards in the if check above to avoid redundant handling during
            // the same shake (e.g. a minimum refractory period).
            Debug.Log("Shake event detected at time " + Time.time);
            shakeAction();
        }
    }

    void shakeAction()
    {
        using (AndroidJavaClass nfcServiceClass = new AndroidJavaClass(NFC_CLASS))
        {
            AndroidJavaObject nfcService = nfcServiceClass.CallStatic<AndroidJavaObject>("getInstance");
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject context = jc.GetStatic<AndroidJavaObject>("currentActivity");

            nfcService.Call("sendFileViaNfc");
        }
    }
}
