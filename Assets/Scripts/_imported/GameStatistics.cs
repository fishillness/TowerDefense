using UnityEngine;

namespace SpaceShooter
{
    public class GameStatistics : SingletonBase<GameStatistics>
    {
        private static int m_Numkills;
        public int GameNumkills => m_Numkills;
        private static int m_Score;
        public int GameScore => m_Score;
        private static int m_Time;
        public int GameTime => m_Time;

        public void AddLevelKills(int levelKills)
        {
            m_Numkills += levelKills;
        }
        public void AddLevelScore(int levelScore)
        {
            m_Score += levelScore;
        }
        public void AddLevelTime(int levelTime)
        {
            m_Time += levelTime;
        }
    }
}

