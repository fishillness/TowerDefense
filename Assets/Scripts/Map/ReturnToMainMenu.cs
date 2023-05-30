using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    public class ReturnToMainMenu : MonoBehaviour
    {
        [SerializeField] private SceneProperties m_sceneProperties;
        [SerializeField] private Button m_mainMenuButton;

        private void Start()
        {
            m_mainMenuButton.onClick.AddListener(GoToMainMenu);
        }
        private void OnDestroy()
        {
            m_mainMenuButton.onClick.RemoveListener(GoToMainMenu);
        }

        private void GoToMainMenu()
        {
            SceneManager.LoadScene(m_sceneProperties.MainMenuNumberInBuild);
        }
    }
}
