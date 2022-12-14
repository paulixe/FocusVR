using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
namespace FocusVR
{
    [RequireComponent(typeof(VisualEffect))]
    [RequireComponent(typeof(AudioSource))]
    public class Jarvis : MonoBehaviour
    {
        AudioSource jarvisSound;
        VisualEffect JarvisVFX; 
        Material jarvisMaterial;

        const string sizePropertyJarvisVFX="Size";
        const string intensityPropertyJarvisMaterial ="_Intensity";

        [SerializeField] float minTwist=0.1f;
        [SerializeField] float twistExpansion=12f;
        [SerializeField] float sizeExpansionVFX = 12f;
        [SerializeField][ColorUsage(true,true)] Color color;
        [SerializeField] float colorExpansion=3f;
        [SerializeField] int numberOfSamples = 30;
        private void Awake()
        {
            jarvisSound = GetComponent<AudioSource>();
            jarvisMaterial = GetComponent<Renderer>().material;
            JarvisVFX = GetComponent<VisualEffect>();
        }
        private void Update()
        {
            UpdateVisuals();
        }
        private void UpdateVisuals()
        {
            float clipLoudness = GetClipLoudness();
            Color newColor = color * (1 + clipLoudness * colorExpansion);
            newColor.a = color.a;
            JarvisVFX.SetVector4("Color", newColor);
            jarvisMaterial.SetColor("_Color", newColor);
            jarvisMaterial.SetFloat(intensityPropertyJarvisMaterial, minTwist + clipLoudness * twistExpansion);
            JarvisVFX.SetFloat(sizePropertyJarvisVFX, 1 + sizeExpansionVFX * clipLoudness);
        }
        public float GetClipLoudness()
        {
            float[] samples = new float[numberOfSamples];
            jarvisSound.GetOutputData(samples, 0);
            float clipLoudness = 0;
            foreach (var sample in samples)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= samples.Length;

            return clipLoudness;
        }
    }
}
