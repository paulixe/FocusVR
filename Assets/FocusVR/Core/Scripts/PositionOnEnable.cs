using static UnityEngine.Mathf;
using UnityEngine;

namespace FocusVR
{
    public class PositionOnEnable : MonoBehaviour
    {
        Transform cameraTransform;


        [Header("SpawnInFrontOfCamera")]
        [SerializeField] bool IsSpawnInFrontOfCameraOn = true;
        [SerializeField] float distance = 2f;
        [SerializeField] float angle = -15f;
        float Angle => angle * Deg2Rad;


        [Header("LookAtCamera")]
        [SerializeField] bool IsLookAtCameraOn = true;
        [SerializeField] Vector3 rotationOffset;


        private void Awake()
        {
            cameraTransform = Camera.main.transform;
        }
        private void OnEnable()
        {
            if (IsSpawnInFrontOfCameraOn) RepositionUiInFrontOfCamera(distance, Angle);
            if (IsLookAtCameraOn) LookAtCamera();
        }

        private void LookAtCamera()
        {   
            transform.LookAt(cameraTransform.position);
            transform.Rotate(rotationOffset);
        }

        private void RepositionUiInFrontOfCamera(float distanceFromCamera,float angleFromForward)
        {
            Vector3 positionFromcamera = distanceFromCamera * new Vector3(0, Sin(angleFromForward), Cos(angleFromForward));
            Vector3 worldPosition = cameraTransform.TransformPoint(positionFromcamera);
            transform.position = worldPosition;
        }
    }
}
