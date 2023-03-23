using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode m_episode;
        public void LoadLevel()
        {            
            LevelSequenceController.Instance.StartEpisode(m_episode);
        }
    }
}
