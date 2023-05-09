using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private MapLevel rootLevel;
        [SerializeField] private Text pointsText;
        [SerializeField] private int needPoints = 3;

        public void TryActivate()
        {
            gameObject.SetActive(rootLevel.IsComplete);
            if (needPoints > MapCompletion.Instance.TotalScore)
            {
                pointsText.text = needPoints.ToString();
            }
            else
            {
                pointsText.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
