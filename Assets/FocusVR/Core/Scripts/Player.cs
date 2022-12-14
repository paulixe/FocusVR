using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
    {
        [SerializeField] SteamVR_Action_Vibration hapticAction;
        [SerializeField] HapticDatas quickVibration;
        [SerializeField] SteamVR_Action_Boolean menuAction;
        [SerializeField] GameObject pointer;
        GameObject menu => Menu.Instance.gameObject;
        public static Player Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }


        }
        public void Update()
        {

            if (menuAction.stateDown)
            {
                menu.SetActive(!menu.activeInHierarchy);
            }
            pointer.SetActive(menu.activeInHierarchy);
        }
    public void LoadScene(string sceneName)
    {
        menu.SetActive(false);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

    }
    public void PlayQuickVibration(float timeDelay, SteamVR_Input_Sources source)
            => PlayVibration(timeDelay, quickVibration, source);
        public void PlayVibration(float timeDelay, HapticDatas hapticDatas, SteamVR_Input_Sources source)
           => hapticAction.Execute(timeDelay, hapticDatas.duration, hapticDatas.frequency, hapticDatas.amplitude, source);

    }

