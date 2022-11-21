using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public AudioListener audioCamera ;
    public Material onMat;
    public Material offMat;
    Renderer ballRenderer;

    private bool isActivated;
    public bool IsActivated { get=>isActivated;
        set {
            isActivated = value;
            if (isActivated) {
                ballRenderer.material = onMat;
            }
            else {
                ballRenderer.material = offMat;
            }
        }
    }

    // Starts when object is active
    private void Awake()
    {
        ballRenderer = GetComponent<Renderer>();
    }

    // Let's us test in the unity editor
    // Attribute
    [ContextMenu(nameof(test))]
    public void test() { IsActivated = !IsActivated; }
}
