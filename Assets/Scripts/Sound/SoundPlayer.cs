using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : SingletonBase<SoundPlayer>
    {
        [SerializeField] private SoundProperties m_soundProperties;
        [SerializeField] private AudioClip m_BGM;

        private AudioSource m_audioSource;

        private new void Awake()
        {
            base.Awake();

            m_audioSource = GetComponent<AudioSource>();
            Instance.m_audioSource.clip = m_BGM;
            Instance.m_audioSource.Play();
        }

        public void Play(Sound sound)
        {
            m_audioSource.PlayOneShot(m_soundProperties[sound]);
        }
    }
}
