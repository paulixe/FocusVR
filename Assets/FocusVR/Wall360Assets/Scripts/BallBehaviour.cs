using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public Material ActivatedMat;
    public Material DeactivatedMat;
    private Renderer renderer;
    private bool isActivated;
    
    public bool IsActivated {
        get
        {
            return isActivated;
        }
        set
        {
            if (value)
            {
                renderer.material=ActivatedMat;
            }
            else
            {
                renderer.material=DeactivatedMat;
            }
        }
    }
  
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
