using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    [CreateAssetMenu()]
    public class SoundProperties : ScriptableObject
    {
        [SerializeField] private AudioClip[] m_sounds;
        public AudioClip this[Sound s] => m_sounds[(int) s];

 #if UNITY_EDITOR
        [CustomEditor(typeof(SoundProperties))]
        public class SoundsInspector : Editor
        {
            private static readonly int soundsCount = Enum.GetValues(typeof(Sound)).Length;
            private new SoundProperties target => base.target as SoundProperties;
            public override void OnInspectorGUI()
            {
                if (target.m_sounds.Length < soundsCount)
                {
                    Array.Resize(ref target.m_sounds, soundsCount);
                }

                for (int i = 0; i < target.m_sounds.Length; i++)
                {
                    target.m_sounds[i] = EditorGUILayout.ObjectField(
                        $"{(Sound)i}: ", target.m_sounds[i], typeof(AudioClip), false) as AudioClip;
                }

                EditorUtility.SetDirty(target);
            }
        }
#endif

    }
}
