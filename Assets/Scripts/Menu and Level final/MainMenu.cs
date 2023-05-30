using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private SceneProperties m_sceneProperties;
        [SerializeField] private Button m_continueButton;
        
        private void Start()
        {
            m_continueButton.interactable = FileHandler.HasFile(MapCompletion.m_filename);
        }
        public void NewGame()
        {
            FileHandler.Reset(MapCompletion.m_filename);
            FileHandler.Reset(Upgrades.m_filename);
            SceneManager.LoadScene(m_sceneProperties.MapNumberInBuild);
        }
        public void Continue()
        {
            SceneManager.LoadScene(m_sceneProperties.MapNumberInBuild);
        }
        public void OnApplicationQuit()
        {
            Application.Quit();
        }
    }
}

