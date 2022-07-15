using System;
using UnityEngine;

public class UDebug
{
    /// <summary>
    /// Debug will show only when UnionManager.debug is true
    /// </summary>
    public static void Debug(object message)
    {
        if (UnionManager.instance.debug == true)
        {
            UnityEngine.Debug.Log("[Union Debug] " + message);
        }
    }
}
