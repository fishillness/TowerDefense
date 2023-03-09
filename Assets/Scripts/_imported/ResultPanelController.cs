using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;
        [SerializeField] private Text m_Result;
        [SerializeField] private Text m_ButtonText;

        private bool m_Success;

        private void Start()
        {
            gameObject.SetActive(false);
        }
        public void ShowResults(PlayerStatistics levelResult, bool success)
        {
            gameObject.SetActive(true);
            m_Success = success;
            m_Result.text = success ? "Win" : "Lose";
            m_ButtonText.text = success ? "Next" : "Restart";

            m_Kills.text = "Kills: " + levelResult.numKills.ToString();
            m_Score.text = "Score: " + levelResult.score.ToString();
            m_Time.text = "Time: " + levelResult.time.ToString();

            if (success)
            {
                GameStatistics.Instance.AddLevelKills(levelResult.numKills);
                GameStatistics.Instance.AddLevelScore(levelResult.score);
                GameStatistics.Instance.AddLevelTime(levelResult.time);
            }
            if (!success)
                levelResult.Reset();
            
            Time.timeScale = 0;
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            if (m_Success)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
                LevelSequenceController.Instance.RestartLevel();
            }
        }

    }
}

