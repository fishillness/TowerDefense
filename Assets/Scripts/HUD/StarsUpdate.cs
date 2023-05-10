using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class StarsUpdate : MonoBehaviour
    {
        [SerializeField] private Image[] m_starImages;
        [SerializeField] private Sprite m_starOn;

        private void Start()
        {
            TDLevelController.OnLevelCompleted += UpdateStars;
        }

        private void OnDestroy()
        {
            TDLevelController.OnLevelCompleted -= UpdateStars;
        }

        private void UpdateStars(int levelScore)
        {
            for (int i = 0; i < levelScore; i++)
            {
                m_starImages[i].sprite = m_starOn;
            }
        }
    }
}
