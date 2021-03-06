using UnityEngine;
using UnityEditor;
using MLAgents.Sensors;

namespace MLAgents.Editor
{
    [CustomEditor(typeof(CameraSensorComponent))]
    [CanEditMultipleObjects]
    internal class CameraSensorComponentEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var so = serializedObject;
            so.Update();

            // Drawing the CameraSensorComponent
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(so.FindProperty("m_Camera"), true);
            EditorGUI.BeginDisabledGroup(Application.isPlaying);
            {
                // These fields affect the sensor order or observation size,
                // So can't be changed at runtime.
                EditorGUILayout.PropertyField(so.FindProperty("m_SensorName"), true);
                EditorGUILayout.PropertyField(so.FindProperty("m_Width"), true);
                EditorGUILayout.PropertyField(so.FindProperty("m_Height"), true);
                EditorGUILayout.PropertyField(so.FindProperty("m_Grayscale"), true);
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.PropertyField(so.FindProperty("m_Compression"), true);

            var requireSensorUpdate = EditorGUI.EndChangeCheck();
            so.ApplyModifiedProperties();

            if (requireSensorUpdate)
            {
                UpdateSensor();
            }
        }

        void UpdateSensor()
        {
            var sensorComponent = serializedObject.targetObject as CameraSensorComponent;
            sensorComponent?.UpdateSensor();
        }
    }
}
