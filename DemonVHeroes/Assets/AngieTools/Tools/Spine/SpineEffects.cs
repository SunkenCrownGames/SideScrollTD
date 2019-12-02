using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngieTools.Spine
{
    public class SpineEffects : MonoBehaviour
    {
        private static SpineEffects m_instance;

        private void Awake()
        {
            BindInstance();
        }

        private void BindInstance()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
        }

        public static void Flash(MeshRenderer p_renderer, int p_flashCount, Color p_color, float p_alpha)
        {
            if (m_instance != null)
            {
                m_instance.StartCoroutine("FlashStart", new object[] { p_renderer, p_flashCount, p_color, p_alpha });
            }
            else
            {
                Debug.LogError("Instance is null");
            }
        }

        private void Update()
        {

        }



        protected IEnumerator FlashStart(object[] parms)
        {
            MeshRenderer renderer = parms[0] as MeshRenderer;
            int flashCount = (int)parms[1];
            Color color = (Color)parms[2];
            float alpha = (float)parms[3];

            if (renderer != null)
            {

                if (renderer)
                {
                        //Debug.Log("Flashing");


                        MaterialPropertyBlock block = new MaterialPropertyBlock();
                        renderer.SetPropertyBlock(block);

                        int fillAlpha = Shader.PropertyToID("_FillPhase");
                        int fillColor = Shader.PropertyToID("_FillColor");


                        for (int i = 0; i < flashCount; i++)
                        {

                            block.SetFloat(fillAlpha, alpha);


                            if (renderer != null)
                            {
                                block.SetColor(fillColor, color);
                                renderer.SetPropertyBlock(block);
                            }

                            yield return new WaitForSeconds(0.15f);

                        if (renderer != null)
                            {
                                block.SetFloat(fillAlpha, 0);
                                renderer.SetPropertyBlock(block);
                            }
                            yield return new WaitForSeconds(0.15f);
                        }
                }
            }
            else
            {
                Debug.LogError("NULL MESH RENDERER");
            }
        }
    }
}