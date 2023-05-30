using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class SceneProperties : ScriptableObject
    {
        [Header("Scene numbers in the build.")]
        [SerializeField] private int mainMenu;
        [SerializeField] private int map;
        [SerializeField] private int finalLevel;

        public int MainMenuNumberInBuild => mainMenu;
        public int MapNumberInBuild => map;
        public int FinalLevelNumberInBuild => finalLevel;
    }
}
