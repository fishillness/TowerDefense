using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TimeLevelCondition : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private float timeLimit;
        private void Start()
        {
            timeLimit += Time.time;
        }
        public bool isCompleted => Time.time > timeLimit;        
    }
}
