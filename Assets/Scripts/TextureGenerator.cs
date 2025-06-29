using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InteractiveEffector.UserInterface;

namespace InteractiveEffector.Common
{
    [ExecuteInEditMode]
    public class TextureGenerator : MonoBehaviour
    {
        public float CustomTime
        {
            get => time;
            set => time = value;
        }
        [SerializeField] private Material material;
        [SerializeField] private RenderTexture output;
        [SerializeField] private float time = 0;

        private void Awake()
        {
            time = 0;
        }

        private void Update()
        {
            time += Time.deltaTime;
            if (material != null)
            {
                material.SetFloat("_CustomTime", time);
                material.SetFloatArray("_InputTimes", UserInterfaceManager.InputTimes);
                Graphics.Blit(null, output, material);
            }
        }

        public void SetTime(float t)
        {
            time = t;
        }
        public void SetMaterial(Material mat)
        {
            material = mat;
        }
    }
}
