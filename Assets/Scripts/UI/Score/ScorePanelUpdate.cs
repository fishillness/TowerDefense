using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class ScorePanelUpdate : MonoBehaviour
    {
        [SerializeField] private Text m_scoreText;

        private void Start()
        {
            m_scoreText.text = MapCompletion.Instance.GetTotalScore().ToString();
        }
    }
}
