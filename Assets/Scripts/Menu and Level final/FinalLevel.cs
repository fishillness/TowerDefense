using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    public class FinalLevel : MonoBehaviour
    {
        [SerializeField] private SceneProperties m_sceneProperties;
        [SerializeField] private Button m_mapButton;
        [SerializeField] private Button m_mainMenuButton;

        private void Start()
        {
            m_mainMenuButton.onClick.AddListener(GoMainMenu);
            m_mapButton.onClick.AddListener(GoMap);
        }

        private void OnDestroy()
        {
            m_mainMenuButton.onClick.RemoveListener(GoMainMenu);
            m_mapButton.onClick.RemoveListener(GoMap);
        }

        private void GoMainMenu()
        {
            SceneManager.LoadScene(m_sceneProperties.MainMenuNumberInBuild);
        }

        private void GoMap()
        {
            SceneManager.LoadScene(m_sceneProperties.MapNumberInBuild);
        }
    }
}

