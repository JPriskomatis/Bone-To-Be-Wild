using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace InfinityPBR.Lighting
{
    [AddComponentMenu("SFBayStudios/Lighting/Scene Lighting")]
    public class DemoLighting : MonoBehaviour
    {
        #region Variables

        //Public
        [Header("Setup Settings")]
        public Material m_skybox;
        public Light m_sunLight;
        [Tooltip("Used to organize and set transform parents on sun and post processing volume.")]
        public bool m_parentSystems = true;
        [Header("Skybox Settings")]
        public float m_sunSize = 0.0325f;
        public float m_sunSizeConvergence = 1f;
        public float m_atmosphereThickness = 1f;
        public Color m_skyTint = new Color(0.5f, 0.5f, 0.5f, 1f);
        public Color m_groundColor = new Color(0.7254902f, 0.8509804f, 0.9333333f, 1f);
        public float m_skyExposure = 1f;
        [Header("Sun Settings")]
        public Color m_sunColor = new Color(1f, 0.925706f, 0.8392157f, 1f);
        public float m_sunIntensity = 1.8f;
        [Header("Fog Settings")]
        public bool m_renderFog = true;
        public FogMode m_fogMode = FogMode.Linear;
        public Color m_fogColor = new Color(0.7254902f, 0.8509804f, 0.9333333f, 1f);
        public float m_fogDensity = 0.01f;
        public float m_fogStart = 0f;
        public float m_fogEnd = 300f;
        [Header("Post Processing Settings")]
        public bool m_renderPostProcessing = true;
        public PostProcessLayer.Antialiasing m_antiAliasingMode = PostProcessLayer.Antialiasing.TemporalAntialiasing;
        public PostProcessProfile m_postProcessingProfile;
        public const string POSTFXOBJECTNAME = "SF Post Processing Volume";

        #endregion

        private void OnEnable()
        {
            //Load Up config
            SetupSystem();
        }

        #region Functions

        /// <summary>
        /// Function used to apply all settings
        /// Only apply when not playing application
        /// </summary>
        public void ApplySettings()
        {
            //Only apply if not playing
            if (!Application.isPlaying)
            {
                ApplySkybox();
                ApplySun();
                ApplyFog();
                ApplyPostFX();
                Organize();
            }
        }

        /// <summary>
        /// Used to find the sun light and to setup anything else
        /// </summary>
        private void SetupSystem()
        {
            if (m_sunLight == null)
            {
                Light[] lights = FindObjectsOfType<Light>();
                foreach (Light light in lights)
                {
                    if (light.type == LightType.Directional)
                    {
                        m_sunLight = light;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Apply skybox settings
        /// </summary>
        private void ApplySkybox()
        {
            if (m_skybox != null)
            {
                RenderSettings.skybox = m_skybox;
                if (m_sunLight != null)
                {
                    RenderSettings.sun = m_sunLight;
                }

                m_skybox.SetFloat("_SunSize", m_sunSize);
                m_skybox.SetFloat("_SunSizeConvergence", m_sunSizeConvergence);
                m_skybox.SetFloat("_AtmosphereThickness", m_atmosphereThickness);
                m_skybox.SetColor("_SkyTint", m_skyTint);
                m_skybox.SetColor("_GroundColor", m_groundColor);
                m_skybox.SetFloat("_Exposure", m_skyExposure);
            }
        }

        /// <summary>
        /// Apply sun settings
        /// </summary>
        private void ApplySun()
        {
            if (m_sunLight != null)
            {
                m_sunLight.color = m_sunColor;
                m_sunLight.intensity = m_sunIntensity;
            }
        }

        /// <summary>
        /// Apply fog settings
        /// </summary>
        private void ApplyFog()
        {
            RenderSettings.fog = m_renderFog;
            if (m_renderFog)
            {
                RenderSettings.fogMode = m_fogMode;
                RenderSettings.fogColor = m_fogColor;
                RenderSettings.fogDensity = m_fogDensity;
                RenderSettings.fogStartDistance = m_fogStart;
                RenderSettings.fogEndDistance = m_fogEnd;
            }
        }

        /// <summary>
        /// Apply post processing settings
        /// </summary>
        private void ApplyPostFX()
        {
            if (m_renderPostProcessing)
            {
                GameObject volume = GameObject.Find(POSTFXOBJECTNAME);
                if (volume == null)
                {
                    volume = new GameObject(POSTFXOBJECTNAME);
                }

                volume.layer = 1;

                PostProcessVolume processVolume = volume.GetComponent<PostProcessVolume>();
                if (processVolume == null)
                {
                    processVolume = volume.AddComponent<PostProcessVolume>();
                }

                processVolume.isGlobal = true;
                if (m_postProcessingProfile != null)
                {
                    processVolume.sharedProfile = m_postProcessingProfile;
                }

                Camera camera = Camera.main;
                if (camera == null)
                {
                    Camera[] cameras = FindObjectsOfType<Camera>();
                    foreach(Camera cam in cameras)
                    {
                        if (cam.tag == "MainCamera" || cam.tag == "Player")
                        {
                            camera = cam;
                            break;
                        }
                    }
                }

                PostProcessLayer layer = camera.GetComponent<PostProcessLayer>();
                if (layer == null)
                {
                    layer = camera.gameObject.AddComponent<PostProcessLayer>();
                }

                layer.antialiasingMode = m_antiAliasingMode;
                layer.volumeLayer = 2;
            }
            else
            {
                GameObject volume = GameObject.Find(POSTFXOBJECTNAME);
                if (volume != null)
                {
                    DestroyImmediate(volume);
                }

                PostProcessLayer layer = FindObjectOfType<PostProcessLayer>();
                if (layer != null)
                {
                    DestroyImmediate(layer);
                }
            }
        }

        /// <summary>
        /// Used to organize and set transform parents on sun and post processing volume
        /// </summary>
        private void Organize()
        {
            if (m_parentSystems)
            {
                if (m_sunLight != null)
                {
                    m_sunLight.transform.SetParent(gameObject.transform);
                }

                GameObject volume = GameObject.Find(POSTFXOBJECTNAME);
                if (volume != null)
                {
                    volume.transform.SetParent(gameObject.transform);
                }
            }
        }

        #endregion
    }
}