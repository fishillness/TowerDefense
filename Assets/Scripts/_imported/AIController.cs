using System;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        #region Properties
        /// <summary>
        /// Types of AI behavior. Null - no behavior, Patrol - the ship will patrol.
        /// ���� ��������� ��. Null - ��� ���������, Patrol - ������� �����������.
        /// </summary>
        public enum AIBehaviour
        {
            Null,
            Patrol
        }
        /// <summary>
        /// Store the behavior type.
        /// �������� ���� ���������.
        /// </summary>
        [SerializeField] private AIBehaviour m_AIBehaviour;
        /// <summary>
        /// Link to patrol zone.
        /// ������ �� ���� ��������������.
        /// </summary>
        [SerializeField] private AIPointPatrol m_PatrolPoint;
        /// <summary>
        /// Movement speed.
        /// �������� ������������.
        /// </summary>
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;
        /// <summary>
        /// Speed of development.
        /// �������� ��������.
        /// </summary>
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;
        /// <summary>
        /// Time to change position.
        /// ����� ��� ��������� �������.
        /// </summary>
        [SerializeField] private float m_RandomSelectMovePointTime;
        /// <summary>
        /// Time to change target.
        /// ����� ��� ��������� ����.
        /// </summary>
        [SerializeField] private float m_FindNewTargetTime;
        /// <summary>
        /// Shooting delay.
        /// �������� ��������.
        /// </summary>
        [SerializeField] private float m_ShootDelay;
        /// <summary>
        /// Raycast length.
        /// ����� ��������.
        /// </summary>
        [SerializeField] private float m_EvadeRayLenght;

        private SpaceShip m_SpaceShip;
        private Vector3 m_MovePosition;
        private Destructible m_SelectedTarget;

        private Timer m_RandomizeDirectionTimer;
        private Timer m_FireTimer;
        private Timer m_FindNewTargetTimer;
        #endregion

        #region Unity Events

        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();
            InitTimers();
        }

        private void Update()
        {
            UpdateTimers();

            UpdateAI();
        }

        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Null)
                return;

            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }
        }

        private void UpdateBehaviourPatrol()
        {
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
            ActionEvadeCollision();
        }
        private void ActionFindNewMovePosition()
        {
            if (m_AIBehaviour == AIBehaviour.Null)
                return;

            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if(m_SelectedTarget != null)
                {
                    m_MovePosition = m_SelectedTarget.transform.position;
                }
                else
                {
                    if(m_PatrolPoint != null)
                    {
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;
                        
                        if(isInsidePatrolZone == true)
                        {
                            if (m_RandomizeDirectionTimer.IsFinished)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;
                                m_MovePosition = newPoint;

                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }
                        }
                        else
                        {
                            m_MovePosition = m_PatrolPoint.transform.position;
                        }
                    }
                }
            }
        }

        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLenght) == true)
            {
                m_MovePosition = transform.position + transform.right * 100.0f;
            }
        }

        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLinear;
            m_SpaceShip.TorqueControl = ComputeAliginTorgueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }

        private const float MAX_ANGLE = 45.0f;
        private static float ComputeAliginTorgueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);
            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;
            return -angle;
        }

        private void ActionFindNewAttackTarget()
        {
            if (m_FindNewTargetTimer.IsFinished == true)
            {
                m_SelectedTarget = FindNearestDestructableTarget();
                m_FindNewTargetTimer.Start(m_ShootDelay);
            }
        }

        private void ActionFire()
        {
            if (m_SelectedTarget != null)
            {
                if (m_FireTimer.IsFinished == true)
                {
                    m_SpaceShip.Fire(TurretMode.Primary);
                    m_FireTimer.Start(m_ShootDelay);
                }
            }
        }

        private Destructible FindNearestDestructableTarget()
        {
            float minDist = float.MaxValue;
            Destructible potentialTarget = null;

            foreach (var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == m_SpaceShip) continue;

                if (v.TeamId == Destructible.TeamIdNeutral) continue;

                if (v.TeamId == m_SpaceShip.TeamId) continue;

                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position); ;
                if (dist < minDist)
                {
                    minDist = dist;
                    potentialTarget = v;
                }
            }

            return potentialTarget;
        }


        #endregion

        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoint = point;
        }

        #region Timers

        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            m_FireTimer = new Timer(m_ShootDelay);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
    }

        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
        }

        #endregion
    }
}
