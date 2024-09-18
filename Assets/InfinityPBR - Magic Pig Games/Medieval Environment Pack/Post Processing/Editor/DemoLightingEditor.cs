using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.PostProcessing;

namespace InfinityPBR.Lighting
{
    [CustomEditor(typeof(DemoLighting))]
    public class DemoLightingEditor : Editor
    {
        private static DemoLighting m_profile;
        private GUIStyle m_boxStyle;

        public override void OnInspectorGUI()
        {
            //Get Profile
            if (m_profile == null)
            {
                m_profile = (DemoLighting)target;
            }

            //Set up the box style
            if (m_boxStyle == null)
            {
                m_boxStyle = new GUIStyle(GUI.skin.box);
                m_boxStyle.normal.textColor = GUI.skin.label.normal.textColor;
                m_boxStyle.fontStyle = FontStyle.Bold;
                m_boxStyle.alignment = TextAnchor.UpperLeft;
            }

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginVertical(m_boxStyle);
            EditorGUILayout.LabelField("Setup Settings", EditorStyles.boldLabel);
            m_profile.m_parentSystems = EditorGUILayout.Toggle("Parent Systems", m_profile.m_parentSystems);
            if (m_profile.m_parentSystems)
            {
                EditorGUILayout.HelpBox("Used to organize and set transform parents on sun and post processing volume.", MessageType.Info);
            }
            m_profile.m_skybox = (Material)EditorGUILayout.ObjectField("Skybox Material", m_profile.m_skybox, typeof(Material), false);
            m_profile.m_sunLight = (Light)EditorGUILayout.ObjectField("Sun Light", m_profile.m_sunLight, typeof(Light), true);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(m_boxStyle);
            EditorGUILayout.LabelField("Skybox Settings", EditorStyles.boldLabel);
            m_profile.m_sunSize = EditorGUILayout.Slider("Sun Size", m_profile.m_sunSize, 0f, 1f);
            m_profile.m_sunSizeConvergence = EditorGUILayout.Slider("Sun Size Convergence", m_profile.m_sunSizeConvergence, 0f, 10f);
            m_profile.m_atmosphereThickness = EditorGUILayout.Slider("Atmosphere Thickness", m_profile.m_atmosphereThickness, 0f, 5f);
            m_profile.m_skyTint = EditorGUILayout.ColorField("Skybox Tint", m_profile.m_skyTint);
            m_profile.m_groundColor = EditorGUILayout.ColorField("Skybox Ground Color", m_profile.m_groundColor);
            m_profile.m_skyExposure = EditorGUILayout.Slider("Skybox Exposure", m_profile.m_skyExposure, 0f, 8f);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(m_boxStyle);
            EditorGUILayout.LabelField("Sun Settings", EditorStyles.boldLabel);
            m_profile.m_sunColor = EditorGUILayout.ColorField("Sun Color", m_profile.m_sunColor);
            m_profile.m_sunIntensity = EditorGUILayout.FloatField("Sun Intensity", m_profile.m_sunIntensity);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(m_boxStyle);
            EditorGUILayout.LabelField("Fog Settings", EditorStyles.boldLabel);
            m_profile.m_renderFog = EditorGUILayout.Toggle("Render Fog", m_profile.m_renderFog);
            if (m_profile.m_renderFog)
            {
                m_profile.m_fogMode = (FogMode)EditorGUILayout.EnumPopup("Fog Mode", m_profile.m_fogMode);
                m_profile.m_fogColor = EditorGUILayout.ColorField("Fog Color", m_profile.m_fogColor);
                if (m_profile.m_fogMode == FogMode.Linear)
                {
                    m_profile.m_fogStart = EditorGUILayout.FloatField("Fog Start Distance", m_profile.m_fogStart);
                    m_profile.m_fogEnd = EditorGUILayout.FloatField("Fog End Distance", m_profile.m_fogEnd);
                }
                else
                {
                    m_profile.m_fogDensity = EditorGUILayout.Slider("Fog Density", m_profile.m_fogDensity, 0f, 1f);
                }
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(m_boxStyle);
            EditorGUILayout.LabelField("Post Processing Settings", EditorStyles.boldLabel);
            m_profile.m_renderPostProcessing = EditorGUILayout.Toggle("Render Post Processing FX", m_profile.m_renderPostProcessing);
            if (m_profile.m_renderPostProcessing)
            {
                m_profile.m_antiAliasingMode = (PostProcessLayer.Antialiasing)EditorGUILayout.EnumPopup("Anti-aliasing Mode", m_profile.m_antiAliasingMode);
                m_profile.m_postProcessingProfile = (PostProcessProfile)EditorGUILayout.ObjectField("Post Processing Profile", m_profile.m_postProcessingProfile, typeof(PostProcessProfile), false);
            }
            EditorGUILayout.EndVertical();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(m_profile, "Changes Made");
                EditorUtility.SetDirty(m_profile);
                m_profile.ApplySettings();
            }
        }
    }
}