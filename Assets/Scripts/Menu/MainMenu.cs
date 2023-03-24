using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button m_continueButton;
        private void Start()
        {
            m_continueButton.interactable = FileHandler.HasFile(MapCompletion.m_filename);
        }
        public void NewGame()
        {
            FileHandler.Reset(MapCompletion.m_filename);
            SceneManager.LoadScene(1);
        }
        public void Continue()
        {

            SceneManager.LoadScene(1);
        }
        public void OnApplicationQuit()
        {
            Application.Quit();
        }
    }
}

