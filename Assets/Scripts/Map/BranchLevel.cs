using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private MapLevel m_rootLevel;
        [SerializeField] private Text m_pointsText;
        [SerializeField] private int m_needPoints = 3;

        public void TryActivate()
        {
            gameObject.SetActive(m_rootLevel.IsComplete);
            if (m_needPoints > MapCompletion.Instance.GetTotalScore())
            {
                m_pointsText.text = m_needPoints.ToString();
            }
            else
            {
                m_pointsText.transform.parent.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialise();
            }
        }
    }
}
