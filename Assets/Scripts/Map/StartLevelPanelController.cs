using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;

namespace TowerDefense
{
    public class StartLevelPanelController : MonoBehaviour
    {
        [SerializeField] private GameObject m_StartLevelPanel;
        [SerializeField] private Text m_levelName;
        [SerializeField] private Image m_levelImage;
        [SerializeField] private Button YesButton;
        [SerializeField] private Button NoButton;

        private MapLevel m_mapLevel;

        private void Start()
        {
            m_StartLevelPanel.SetActive(false);

            MapLevel.OnStartLevel += OpenPanel;
            YesButton.onClick.AddListener(LoadLevel);
            NoButton.onClick.AddListener(ClosePanel);
        }

        private void OnDestroy()
        {
            MapLevel.OnStartLevel -= OpenPanel;
            YesButton.onClick.RemoveListener(LoadLevel);
            NoButton.onClick.RemoveListener(ClosePanel);
        }

        private void OpenPanel(MapLevel mapLevel, Episode episode)
        {
            m_StartLevelPanel.SetActive(true);
            m_mapLevel = mapLevel;

            m_levelName.text = episode.EpisodeName;
            m_levelImage.sprite = episode.PreviewImage;
        }

        private void LoadLevel()
        {
            if (m_mapLevel)
                m_mapLevel.LoadLevel();
        }

        private void ClosePanel()
        {
            m_StartLevelPanel.SetActive(false);
        }
    }
}
