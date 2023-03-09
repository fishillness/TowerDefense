using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// The base class for all interactive game objects in the scene.
    /// Базовый класс всех интерактивных игровых объектов на сцене.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// Name of the object for the user.
        /// Название объекта для пользователя.
        /// </summary>
        [SerializeField] private string m_Nickname;
        public string Nickname => m_Nickname;
    }
}
