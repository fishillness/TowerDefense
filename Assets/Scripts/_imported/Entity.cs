using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// The base class for all interactive game objects in the scene.
    /// ������� ����� ���� ������������� ������� �������� �� �����.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// Name of the object for the user.
        /// �������� ������� ��� ������������.
        /// </summary>
        [SerializeField] private string m_Nickname;
        public string Nickname => m_Nickname;
    }
}
