using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource
        {
            Gold,
            Life
        }
        [SerializeField] private UpdateSource source;
        private Text m_text;

        private void Awake()
        {
            m_text = GetComponent<Text>();
            switch (source)
            {
                case UpdateSource.Gold:
                    TDPlayer.OnGoldUpdate += UpdateText;
                    break;
                case UpdateSource.Life:
                    TDPlayer.OnLifeUpdate += UpdateText;
                    break;
            }            
        }

        private void UpdateText(int value)
        {
            m_text.text = value.ToString();
        }
    }
}
