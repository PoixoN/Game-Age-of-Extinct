using UnityEngine;

/// <summary>
/// Реализация синглтона для наследования.
/// </summary>
/// <typeparam name="T">Класс, который нужно сделать синглтоном</typeparam>
/// <remarks>
/// Если необходимо обращаться к классу во время OnDestroy или OnApplicationQuit
/// необходимо проверять наличие объекта через IsAlive. Объект может быть уже 
/// уничтожен, и обращение к нему вызовет его еще раз.
/// 
/// 
/// При использовании в дочернем классе Awake, OnDestroy, 
/// OnApplicationQuit необходимо вызывать базовые методы
/// base.Awake() и тд.
/// 
/// Добавил скрываемый метод Initialization - чтобы перегружать его и использовать 
/// необходимые действия.
/// 
/// Создание объекта производится через unity, поэтому использовать блокировку 
/// объекта нет необходимости. Однако ее можно добавить, в случае если 
/// понадобится обращение к объекту из других потоков.
/// 
/// Из книг:
///     - Рихтер "CLR via C#"
///     - Chris Dickinson "Unity 2017 Game optimization"
///</remarks>

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{

    private static T instance = null;

    private bool alive = true;

    protected bool dontDestroyOnLoad = true;

    public static T Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                //Find T
                T[] managers = GameObject.FindObjectsOfType<T>();
                if (managers != null)
                {
                    if (managers.Length == 1)
                    {
                        instance = managers[0];
                        DontDestroyOnLoad(instance);
                        return instance;
                    }
                    else
                    {
                        if (managers.Length > 1)
                        {
                            Debug.LogError($"Have more that one {typeof(T).Name} in scene. " +
                                            "But this is Singleton! Check project.");
                            for (int i = 0; i < managers.Length; ++i)
                            {
                                T manager = managers[i];
                                Destroy(manager.gameObject);
                            }
                        }
                    }
                }
                //create 
                GameObject go = new GameObject(typeof(T).Name, typeof(T));
                instance = go.GetComponent<T>();
                instance.Initialization();
                DontDestroyOnLoad(instance.gameObject);
                return instance;
            }
        }

        //Can be initialized externally
        set
        {
            instance = value as T;
        }
    }

    /// <summary>
    /// Check flag if need work from OnDestroy or OnApplicationExit
    /// </summary>
    public static bool IsAlive
    {
        get
        {
            if (instance == null)
                return false;
            return instance.alive;
        }
    }

    protected void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this as T;
            Initialization();
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    protected void OnDestroy() { alive = false; }

    protected void OnApplicationQuit() { alive = false; }

    protected virtual void Initialization() { }
}