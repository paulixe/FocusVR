using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using UnityEngine.InputSystem.UI;
namespace VR
{
    public class VRInputModule : InputSystemUIInputModule
    {
        public static VRInputModule instance;
        public Camera m_camera;
        public SteamVR_Input_Sources m_TargetSource;
        public SteamVR_Action_Boolean m_ClickAction;

        private GameObject m_CurrentObject = null;
        private PointerEventData m_Data = null;

        protected override void Awake()
        {
            base.Awake();
            instance = this;
            m_Data = new PointerEventData(eventSystem);
        }
        public override void Process()
        {
            base.Process();
            //Reset data, set camera
            m_Data.Reset();

            //Set position of pointer
            Vector2 pointerPosition = new Vector2(Screen.width / 2, Screen.height / 2);
            //Vector2 pointerPosition = m_camera.WorldToScreenPoint(m_camera.transform.position);
            m_Data.position = pointerPosition;


            //Raycast
            eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
            m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
            m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject;

            //Clear
            m_RaycastResultCache.Clear();

            //Hover
            HandlePointerExitAndEnter(m_Data, m_CurrentObject);

            //Press
            if (m_ClickAction.GetStateDown(m_TargetSource))
                ProcessPress(m_Data);

            //Release
            if (m_ClickAction.GetStateUp(m_TargetSource))
            {
                ProcessRelease(m_Data);
            }
        }
        public PointerEventData GetData()
        {
            return m_Data;
        }
        private void ProcessPress(PointerEventData data)
        {
            //set raycast
            data.pointerPressRaycast = data.pointerCurrentRaycast;

            //check for object hit, get the down handler, call
            GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerDownHandler);

            //if no down handler, try and get click handler
            if (newPointerPress == null)
            {
                newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);
            }
            //Set data
            data.pressPosition = data.position;
            data.pointerPress = newPointerPress;
            data.rawPointerPress = m_CurrentObject;
        }
        private void ProcessRelease(PointerEventData data)
        {
            //Execute pointer up
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);
            //check for click handler
            GameObject pointerUphandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);
            //check if actual
            if (data.pointerPress == pointerUphandler)
            {
                ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
            }
            //Clear selected gameobject
            eventSystem.SetSelectedGameObject(null);

            //reset data
            data.pressPosition = Vector2.zero;
            data.pointerPress = null;
            data.rawPointerPress = null;

        }

    }
}

