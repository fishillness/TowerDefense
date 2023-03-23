using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{    
    public class TDLevelController : LevelController
    {
        private new void Start()
        {
            base.Start();
            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                ResultPanelController.Instance.ShowResults(false);
            };
        }     
        private void StopLevelActivity()
        {
            Debug.Log("level stopped");
        }
    }
}
