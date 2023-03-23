using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{    
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneNickname = "Main menu";
        public Episode CurrentEpissode { get; private set; }
        public int CurrentLevel { get; private set; }
        public static SpaceShip PlayerShip { get; set; }
        public bool LastLevelResult { get; private set; }
        public PlayerStatistics levelStatistics { get; private set; }
        public static SpaceShip playerShip { get; set; }

        public void StartEpisode(Episode e)
        {
            CurrentEpissode = e;
            CurrentLevel = 0;

            levelStatistics = new PlayerStatistics();
            levelStatistics.Reset();

            SceneManager.LoadScene(e.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            //SceneManager.LoadScene(CurrentEpissode.Levels[CurrentLevel]);
            SceneManager.LoadScene(0);
        }

        public void FinishCurrentLevel(bool success)
        {
            LastLevelResult = success;
            //CalculateLevelStatistic();

            ResultPanelController.Instance.ShowResults(success);
            //ResultPanelController.Instance.ShowResults(levelStatistics, success);
        }

        public void AdvanceLevel()
        {
            levelStatistics.Reset();
            CurrentLevel++;

            if (CurrentEpissode.Levels.Length <= CurrentLevel)  
            {
                SceneManager.LoadScene(MainMenuSceneNickname);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpissode.Levels[CurrentLevel]);
            }
        }

        private void CalculateLevelStatistic()
        {
            levelStatistics.score = Player.Instance.Score;
            levelStatistics.numKills = Player.Instance.NumKills;
            levelStatistics.time = (int)LevelController.Instance.LevelTime;
        }
    }
}

