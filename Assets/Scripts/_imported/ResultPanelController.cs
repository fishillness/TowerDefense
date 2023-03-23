using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private GameObject m_WinPanel;
        [SerializeField] private GameObject m_LosePanel;

        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;
        [SerializeField] private Text m_Result;
        [SerializeField] private Text m_ButtonText;

        private bool m_Success;

        private void Start()
        {
            //gameObject.SetActive(false);
        }
        public void ShowResults(bool success)//(PlayerStatistics levelResult, bool success)
        {
            m_WinPanel?.gameObject.SetActive(success);
            m_LosePanel?.gameObject.SetActive(!success);

            m_Success = success;
            /*gameObject.SetActive(true);            
            m_Result.text = success ? "Win" : "Lose";
            m_ButtonText.text = success ? "Next" : "Restart";*/

            /*m_Kills.text = "Kills: " + levelResult.numKills.ToString();
            m_Score.text = "Score: " + levelResult.score.ToString();
            m_Time.text = "Time: " + levelResult.time.ToString();

            if (success)
            {
                GameStatistics.Instance.AddLevelKills(levelResult.numKills);
                GameStatistics.Instance.AddLevelScore(levelResult.score);
                GameStatistics.Instance.AddLevelTime(levelResult.time);
            }
            if (!success)
                levelResult.Reset();*/

           // Time.timeScale = 0;
        }
        ///////////////////////////////////////////////////////
        public void OnPlayNext()
        {
           // Time.timeScale = 1;
            LevelSequenceController.Instance.AdvanceLevel();
        }

        public void OnRestartLevel()
        {
           // Time.timeScale = 1;
            LevelSequenceController.Instance.RestartLevel();
        }
        ///////////////////////////////////////////////////////

        /*public void OnButtonNextAction()
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
        }*/

    }
}

