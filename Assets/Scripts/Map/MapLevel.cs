using UnityEngine;
using SpaceShooter;
using UnityEngine.UI;
namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Text m_scoreText;
        private Episode m_episode;

        public void LoadLevel()
        {            
            LevelSequenceController.Instance.StartEpisode(m_episode);
        }

        public void SetLevelDate(Episode episode, int score)
        {
            m_episode = episode;
            m_scoreText.text = $"{score}/3";
        }
    }
}
