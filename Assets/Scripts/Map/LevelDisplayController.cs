using UnityEngine;

namespace TowerDefense
{
    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField] private MapLevel[] levels;
        [SerializeField] private BranchLevel[] branchLevels;
        private void Start()
        {
            var drawLevel = 0;
            var score = 1;
            while (score != 0 && drawLevel < levels.Length)// && 
               // MapCompletion.Instance.TryIndex(drawLevel, out var episode, out score))
            {
                //levels[drawLevel].SetLevelDate(episode, score);
                levels[drawLevel].Initialise();
                drawLevel++;
            }

            for (int i = drawLevel; i < levels.Length; i++)
            {
                levels[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < branchLevels.Length; i++)
            {
                branchLevels[i].TryActivate();
            }
        }
    }
}

