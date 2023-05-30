namespace TowerDefense
{
    public enum Sound
    {
        Arrow = 0,
        ArrowHit = 1,
        EnemyDie = 2,
        EnemyAttack = 3,
        PlayerWinPanel = 4,
        PlayerLosePanel = 5,
        BGM = 6,
    }

    public static class SoundExtensions
    {
        public static void Play(this Sound sound)
        {
            SoundPlayer.Instance.Play(sound);
        }
    }
}
