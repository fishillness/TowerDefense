using UnityEngine;

[DisallowMultipleComponent] //Атрибут для защиты от двойного добавления элемента на сцену
public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
                    //Тип Instance не определен изначально
                    // SingletonBase может содержать какой-то тип, который может быть наследником только от MonoBehaviour
{
    /// <summary>
    /// Чекбокс: Уничтоэен ли объект?
    /// </summary>
    [Header("Singleton")]
    [SerializeField] private bool m_DoNotDestroyOnLoad;

    /// <summary>
    /// Свойство класса (типа) Т
    /// </summary>
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("MonoSingleton: object of type already exists, instance will be destroyed = " + typeof(T).Name);
            Destroy(this);
            return;
        }
        Instance = this as T; //приравниваем как тип Т

        if (m_DoNotDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}
