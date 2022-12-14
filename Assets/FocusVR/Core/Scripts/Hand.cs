using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
[RequireComponent(typeof(SteamVR_Behaviour_Pose))]
public class Hand : MonoBehaviour
{
    SteamVR_Input_Sources source;
    void Awake()
    {
        source=GetComponent<SteamVR_Behaviour_Pose>().inputSource;
    }

    public void OnTriggerEnter(Collider other)
    {
        IInteractable[] interactables=other.gameObject.GetComponents<IInteractable>();
        foreach (IInteractable interactable in interactables)
            interactable.TriggerWith(source);
    }
}
