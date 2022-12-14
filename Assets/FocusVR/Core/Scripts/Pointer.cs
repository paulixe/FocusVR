using UnityEngine;
using UnityEngine.EventSystems;

namespace VR
{

    [RequireComponent(typeof(Camera))]
    public class Pointer : MonoBehaviour
    {
        [SerializeField] Transform m_camera;
        [SerializeField] GameObject m_Dot;
        [SerializeField] float dotAngle = 1f;

        LineRenderer lineRenderer = null;
        const float defaultLength = 100f;

        public float TanAngle { get { return Mathf.Tan(dotAngle * Mathf.PI / 180f); } }
        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        void Update()
        {
            Updateline();
        }

        private void Updateline()
        {
            PointerEventData data = VRInputModule.instance.GetData();

            Vector3 endPosition=Vector3.zero;
            bool isUIHit = data.pointerCurrentRaycast.distance != 0;

            if (isUIHit)
                endPosition = data.pointerCurrentRaycast.worldPosition;
            else
                endPosition = transform.position + (transform.forward * defaultLength);


            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, endPosition);

            SetDot(endPosition);
        }
        private void SetDot(Vector3 position)
        {
            float scale = DotScale(m_camera.worldToLocalMatrix.MultiplyPoint3x4(position).magnitude);
            m_Dot.transform.position = position;
            m_Dot.transform.localScale = new Vector3(scale, scale, scale);
        }
        public float DotScale(float distance)
        {
            return TanAngle * distance / 2;
        }

    }
}