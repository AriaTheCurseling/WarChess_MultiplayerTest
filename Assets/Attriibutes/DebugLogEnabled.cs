using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
public class DebugLogEnabled : Attribute
{
    public DebugLogEnabled(bool enableLogger)
    {
        Debug.unityLogger.logEnabled = enableLogger;
    }
}