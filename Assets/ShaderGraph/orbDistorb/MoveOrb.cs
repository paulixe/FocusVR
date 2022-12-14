using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOrb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal")*Time.deltaTime;
        float yInput = Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position+=new Vector3(xInput,yInput,0);
    }
}
