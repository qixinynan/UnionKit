using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class UnionManager : MonoBehaviour
{
    public static UnionManager instance;

    /// <summary>
    /// Allow debug output and other features or not
    /// </summary>
    public bool debug = true;

    private Dictionary<string, Manager> managersDict = new Dictionary<string, Manager>();


    private void Awake()
    {
        // TODO Make instance more safe
        instance = this;
    }

    private void Start()
    {
        ShowWelcomeInfo();
        LoadManagers();
        GetManager<TestManager>().hello();
        GetManager<GoodManager>().hello();
    }

    // UnionManager Script will be automatic mounted to new object named UnionManager when any scenes started
    [RuntimeInitializeOnLoadMethod]
    private static void OnSceneStarted()
    {
        new GameObject("UnionManager")
            .AddComponent<UnionManager>();
    }

    private void ShowWelcomeInfo()
    {
        UDebug.Debug("Welcome to UnionKit, if you want to modify the setting of UnionKit ..."); // TODO setting of UnionManager
    }


    private Manager CreateManagerByType(Type type)
    {
        return (Manager) Activator.CreateInstance(type);
    }

    private void LoadManagers()
    {
        UDebug.Debug("Loading managers");
        DateTime startTime = DateTime.Now;

        List<Type> managerTypes = GetAllManagerTypes();

        managerTypes.ForEach(type => managersDict.Add(type.Name, CreateManagerByType(type)));

        int takeTime = DateTime.Now.Subtract(startTime).Milliseconds;
        UDebug.Debug(managerTypes.Count + " managers have been loaded, it took " + takeTime + "ms");
    }

    public static List<Type> GetAllManagerTypes()
    {
        Type typeofManager = typeof(Manager);
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes().Where(t => t.BaseType == typeofManager))
            .ToList();
    }

    /// <summary>
    /// Get manager's instance that based Manager class
    /// </summary>
    /// <typeparam name="T">the manager's type that based Manager</typeparam>
    /// <returns>manager's instance</returns>
    public T GetManager<T>()
    {
        if (managersDict.ContainsKey(typeof(T).Name))
        {
            Manager manager = managersDict[typeof(T).Name];
            return (T)(object)manager;
        }
        else
        {
            Debug.LogError("Cannot get manager by " + typeof(T).Name);
        }
        return default(T);
    }
}
