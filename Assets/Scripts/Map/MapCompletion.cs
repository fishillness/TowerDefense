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

        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
                Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
            else
                Debug.Log($"Episode complete with score {levelScore}");
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

        [SerializeField] private EpisodeScore[] m_completionDate;
        private int totalScore;

        public int TotalScore => totalScore;

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(m_filename, ref m_completionDate);
            foreach (var episodeScore in m_completionDate)
            {
                totalScore += episodeScore.m_score;
            }
        }
        public int GetEpisodeScore(Episode episode)
        {
            foreach (var data in m_completionDate)
            {
                if (data.m_episode == episode)
                    return
                         data.m_score;
            }
            return 0;
        }
        /*public bool TryIndex(int id, out Episode episode, out int score)
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
        }*/
    }
}
