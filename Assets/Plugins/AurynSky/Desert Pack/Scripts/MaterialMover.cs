using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Desert
{
    public class MaterialMover : MonoBehaviour
    {

        public float scrollSpeed = 0.5F;
        public Renderer rend;
        void Start()
        {
            rend = GetComponent<Renderer>();
        }
        void Update()
        {
            float offset = Time.time * scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        }
    }
}