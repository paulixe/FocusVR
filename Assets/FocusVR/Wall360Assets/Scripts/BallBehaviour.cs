using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(AudioSource))]
public class BallBehaviour : MonoBehaviour
{
    [SerializeField] Material ActivatedMat;
    [SerializeField] Material DeactivatedMat;
    private Renderer ballRenderer;
    private AudioSource ballAudioSource;
    private bool isActivated;
   
    public event Action OnActivation = delegate { };
    public event Action OnDeactivation = delegate { };
    public bool IsActivated {
        get=> isActivated;
        set
        {
            if (value)
                OnActivation();
            else
                OnDeactivation();
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
    private void OnEnable()
    {
        OnActivation += Activate;
        OnDeactivation += Deactivate;
    }
    private void OnDisable()
    {
        OnActivation -= Activate;
        OnDeactivation -= Deactivate;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (IsActivated)
            IsActivated=false;
    }

    [ContextMenu("Test")]
    public void TEst()=>IsActivated = !IsActivated;

}
