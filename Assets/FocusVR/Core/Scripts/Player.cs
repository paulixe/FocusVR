using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class Player : MonoBehaviour
{
    [SerializeField] SteamVR_Action_Vibration hapticAction;
    [SerializeField] HapticDatas quickVibration;
    public static Player Instance { get; private set; }
    private void Awake()
    {
        if (Instance!=null)
            Debug.LogWarning("There is 2 Players");

        Instance = this;
    }
    public void PlayQuickVibration(float timeDelay, SteamVR_Input_Sources source)
        =>PlayVibration(timeDelay,quickVibration,source);
    public void PlayVibration(float timeDelay, HapticDatas hapticDatas, SteamVR_Input_Sources source)
       =>hapticAction.Execute(timeDelay, hapticDatas.duration, hapticDatas.frequency, hapticDatas.amplitude, source);

}
