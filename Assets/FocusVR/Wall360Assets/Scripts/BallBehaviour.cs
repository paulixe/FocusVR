using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR;
[RequireComponent(typeof(AudioSource))]
public class BallBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField] Material ActivatedMat;
    [SerializeField] Material DeactivatedMat;

    private Renderer ballRenderer;
    private AudioSource ballAudioSource;
    private bool isActivated;
   
    public bool IsActivated {
        get=> isActivated;
        set
        {
            if (value)
                Activate();
            else
                Deactivate();
        }
    }
    public void TriggerWith(SteamVR_Input_Sources source)
    {
        if (IsActivated)
        {
            Player.Instance.PlayQuickVibration(0,source);
            IsActivated = false;
        }

    }
    //called before start
    void Awake()
    {
        ballAudioSource=GetComponent<AudioSource>();
        ballRenderer = GetComponent<Renderer>();
        UpdateMaterial();
    }
    private void UpdateMaterial()
    {
        if (IsActivated)
            ballRenderer.material = ActivatedMat;
        else
            ballRenderer.material = DeactivatedMat;
    }
    private void Activate()
    {
        isActivated = true;
        UpdateMaterial();
        ballAudioSource.Play();
    }
    private void Deactivate()
    {
        isActivated = false;
        ballRenderer.material = DeactivatedMat;
        ballAudioSource.Play();
    }
}
