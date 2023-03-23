using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public interface ILevelCondition
    {
        bool isCompleted { get; }
    }
    public class LevelController : SingletonBase<LevelController>
    {   
        [SerializeField] private int m_ReferenceTime;
        public int ReferenceTime => m_ReferenceTime;

        [SerializeField] protected UnityEvent m_EventLevelCompleted;

        private ILevelCondition[] m_Conditions;
        private bool m_IsLevelCompleted;
        private float m_LevelTime;
        public float LevelTime => m_LevelTime;

        protected void Start()
        {
            m_Conditions = GetComponentsInChildren<ILevelCondition>();    
        }
        private void Update()
        {
            if (!m_IsLevelCompleted)
            {
                m_LevelTime += Time.deltaTime;
                CheckLeveLCondition();
            }
        }

        private void CheckLeveLCondition()
        {
            if(m_Conditions == null || m_Conditions.Length == 0)       
                return;

            int numCompleted = 0;

            foreach(var v in m_Conditions)
            {
                if (v.isCompleted)
                    numCompleted++;
            }
            if (numCompleted == m_Conditions.Length)
            {
                m_IsLevelCompleted = true;
                m_EventLevelCompleted?.Invoke();

                LevelSequenceController.Instance?.FinishCurrentLevel(true);
            }
        }


    }
}

