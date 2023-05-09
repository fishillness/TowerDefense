using UnityEngine;
using SpaceShooter;
using UnityEngine.UI;
namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private RectTransform m_resultPanel;
        [SerializeField] private Image[] m_resultImages;
        [SerializeField] private Sprite m_starOn;
        private Episode m_episode;

        public bool IsComplete => gameObject.activeSelf &&
            m_resultPanel.gameObject.activeSelf;

        public void LoadLevel()
        {            
            LevelSequenceController.Instance.StartEpisode(m_episode);
        }

        public void SetLevelDate(Episode episode, int score)
        {
            m_episode = episode;
            m_resultPanel.gameObject.SetActive(score > 0);
            for (int i = 0; i < score; i++)
            {
                m_resultImages[i].sprite = m_starOn;
            }
        }
    }
}
