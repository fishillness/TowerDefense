using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu()]
    public class SoundProperties : ScriptableObject
    {
        public AudioClip[] m_sounds;
        public AudioClip this[Sound s] => m_sounds[(int) s];
    }
}
