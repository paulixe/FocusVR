using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Menu : MonoBehaviour
    {
        public static Menu Instance { get; private set; }

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
            gameObject.SetActive(false);

        }

    }

