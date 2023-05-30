using UnityEngine;
using UnityEngine.SceneManagement;
using TowerDefense;

namespace SpaceShooter
{    
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        [SerializeField] private SceneProperties sceneProperties;

        public static string MainMenuSceneNickname = "Map";
        public Episode CurrentEpisode { get; private set; }
        public int CurrentLevel { get; private set; }
        public static SpaceShip PlayerShip { get; set; }
        public bool LastLevelResult { get; private set; }
        public PlayerStatistics levelStatistics { get; private set; }
        public static SpaceShip playerShip { get; set; }

        public void StartEpisode(Episode e)
        {
            CurrentEpisode = e;
            CurrentLevel = 0;

            levelStatistics = new PlayerStatistics();
            levelStatistics.Reset();

            SceneManager.LoadScene(e.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        public void GoToMap()
        {
            //SceneManager.LoadScene(CurrentEpissode.Levels[CurrentLevel]);
            SceneManager.LoadScene(sceneProperties.MapNumberInBuild);
            //SceneManager.LoadScene(1);
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

            if (CurrentEpisode.Levels.Length <= CurrentLevel)  
            {
                //SceneManager.LoadScene(MainMenuSceneNickname);
                SceneManager.LoadScene(sceneProperties.MapNumberInBuild);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
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

