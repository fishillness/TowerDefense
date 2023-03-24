using UnityEngine;
using SpaceShooter;
using System;

namespace TowerDefense
{
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        public const string m_filename = "completion.dat";
        /*public static void ResetSavedData()
        {
            Saver<EpisodeScore[]>.Reset(m_filename);
        }*/
        [Serializable]
        private class EpisodeScore
        {
            public Episode m_episode;
            public int m_score;
        }
        #region Static method
        public static void SaveEpisodeResult(int levelScore)
        {
            Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
        }
        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in m_completionDate)
            {
                if (item.m_episode == currentEpisode)
                {
                    if (levelScore > item.m_score)
                    {
                        item.m_score = levelScore;
                        Saver<EpisodeScore[]>.Save(m_filename, m_completionDate);
                    }
                }
            }
        }
        #endregion
        [SerializeField] private EpisodeScore[] m_completionDate;
        
        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(m_filename, ref m_completionDate);
        }

        public bool TryIndex(int id, out Episode episode, out int score)
        {
            if (id >= 0 && id < m_completionDate.Length)
            {
                episode = m_completionDate[id].m_episode;
                score = m_completionDate[id].m_score;
                return true;
            }
            episode = null;
            score = 0;
            return false;
        }
            

       
    }
}
