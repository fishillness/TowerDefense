using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SpaceShooter;

namespace TowerDefense
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private SceneProperties m_sceneProperties;
        [SerializeField] private GameObject m_pausePanel;
        [SerializeField] private Button m_openPausePanelButton;
        [SerializeField] private Button m_continueLevelButton;
        [SerializeField] private Button m_restartLevelButton;
        [SerializeField] private Button m_mapButton;
        [SerializeField] private Button m_mainMenuButton;

        private void Start()
        {
            m_pausePanel.SetActive(false);

            m_openPausePanelButton.onClick.AddListener(OpenPausePanel);
            m_continueLevelButton.onClick.AddListener(ContinueLevel);
            m_restartLevelButton.onClick.AddListener(RestartLevel);
            m_mapButton.onClick.AddListener(GoToMap);
            m_mainMenuButton.onClick.AddListener(GoToMainMenu);
        }
        private void OnDestroy()
        {
            m_openPausePanelButton.onClick.RemoveListener(OpenPausePanel);
            m_continueLevelButton.onClick.RemoveListener(ContinueLevel);
            m_restartLevelButton.onClick.RemoveListener(RestartLevel);
            m_mapButton.onClick.RemoveListener(GoToMap);
            m_mainMenuButton.onClick.RemoveListener(GoToMainMenu);
        }

        private void OpenPausePanel()
        {
            Time.timeScale = 0;
            m_pausePanel.SetActive(true);
        }

        private void ContinueLevel()
        {
            Time.timeScale = 1;
            m_pausePanel.SetActive(false);
        }

        private void RestartLevel()
        {
            Time.timeScale = 1;
            LevelSequenceController.Instance.RestartLevel();
        }

        private void GoToMap()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(m_sceneProperties.MapNumberInBuild);
        }

        private void GoToMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(m_sceneProperties.MainMenuNumberInBuild);
        }
    }
}
