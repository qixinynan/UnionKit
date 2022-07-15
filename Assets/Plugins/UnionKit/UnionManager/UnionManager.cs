using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class UnionManager : MonoBehaviour
{
    public static UnionManager instance;

    public bool debug = true;
    public Dictionary<string, Manager> managersDict = new Dictionary<string, Manager>();


    private void Awake()
    {
        instance = this; // TODO Make instance more safe
    }

    private void Start()
    {
        LoadManagers();
        GetManager<TestManager>().hello();
        GetManager<GoodManager>().hello();
    }

    private Manager CreateManagerByType(Type type)
    {
        return (Manager) Activator.CreateInstance(type);
    }

    private void LoadManagers()
    {
        UDebug.Debug("Loading managers");
        DateTime startTime = DateTime.Now;
        Type typeofManager = typeof(Manager);

        List<Type> managerTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes().Where(t => t.BaseType == typeofManager))
            .ToList();

        managerTypes.ForEach(type => managersDict.Add(type.Name, CreateManagerByType(type)));

        int takeTime = DateTime.Now.Subtract(startTime).Milliseconds;
        UDebug.Debug(managerTypes.Count + " managers have loaded, it took " + takeTime + "ms");
    }

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
