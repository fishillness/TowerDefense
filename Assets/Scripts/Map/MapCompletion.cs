using UnityEngine;
using SpaceShooter;
using System;

namespace TowerDefense
{
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        [Serializable]
        private class EpisodeScore
        {
            public Episode m_episode;
            public int m_score;
        }
        [SerializeField] private EpisodeScore[] m_completiomDate;
        public bool TryIndex(int id, out Episode episode, out int score)
        {
            if (id >= 0 && id < m_completiomDate.Length)
            {
                episode = m_completiomDate[id].m_episode;
                score = m_completiomDate[id].m_score;
                return true;
            }
            episode = null;
            score = 0;
            return false;
        }
        public static void SaveEpisodeResult(int levelScore)
        {
            Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
        }
        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in m_completiomDate)
            {
                if (item.m_episode == currentEpisode)
                {
                    if (levelScore > item.m_score)
                        item.m_score = levelScore;
                }
            }
        }
    }
}
