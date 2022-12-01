using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

public class DebugLogger : MonoBehaviour
{
    private const string INFO = "<color=cyan>INFO";
    private const string DEBUG = "<color=lime>DEBUG";
    private const string WARNING = "<color=yellow>WARNING";
    private const string ERROR = "<color=red>ERROR";
    private const string END = "</color>";
    private const string EMPTY = "";

    private static int _callNumber;
    

    public static void Info(string methodName, [CanBeNull] string message = EMPTY, [CanBeNull] Object classReference = null)
    {
        Log(INFO, methodName, message, classReference);
    }

    public static void Debug(string methodName, [CanBeNull] string message = EMPTY, [CanBeNull] Object classReference = null)
    {
        Log(DEBUG, methodName, message, classReference);
    }
    
    public static void Warning(string methodName, [CanBeNull] string message = EMPTY, [CanBeNull] Object classReference = null)
    {
        Log(WARNING, methodName, message, classReference);
    }
    
    public static void Error(string methodName, [CanBeNull] string message = EMPTY, [CanBeNull] Object classReference = null)
    {
        Log(ERROR, methodName, message, classReference);
    }
    
    private static void Log(string level, string methodName, string message = EMPTY, [CanBeNull] Object classReference = null)
    {
        if (message.Equals(EMPTY))
        {
            message = $"{_callNumber++}";
            CoroutineCaller.Instance.StartCoroutine(CallCoroutine());
        }
            
        UnityEngine.Debug.Log($"{level} | {methodName}: {message}{END}", classReference);
    }
    
    public static bool IsNullInfo<T>(string methodName, T objectToCheck, string objectName = EMPTY, string additionalMessage = EMPTY,
        [CanBeNull] Object classReference = null)
    {
        if (objectToCheck != null) return false;

        if (objectName.Equals(EMPTY)) objectName = $"Object of Type {typeof(T)}";
        
        Log(INFO, methodName, $"{objectName} is null. {additionalMessage}", classReference);
        
        return true;
    }
    
    public static bool IsNullInfo<T>(T objectToCheck, string methodName = EMPTY, string objectName = EMPTY, string additionalMessage = EMPTY,
        [CanBeNull] Object classReference = null)
    {
        if (objectToCheck != null) return false;

        if (objectName.Equals(EMPTY)) objectName = $"Object of Type {typeof(T)}";
        
        Log(INFO, methodName, $"{objectName} is null. {additionalMessage}", classReference);
        
        return true;
    }
    
    public static bool IsNullDebug<T>(string methodName, T objectToCheck, string objectName = EMPTY, string additionalMessage = EMPTY,
        [CanBeNull] Object classReference = null)
    {
        if (objectToCheck != null) return false;

        if (objectName.Equals(EMPTY)) objectName = $"Object of Type {typeof(T)}";
        
        Log(DEBUG, methodName, $"{objectName} is null. {additionalMessage}", classReference);
        
        return true;
    }
    
    public static bool IsNullDebug<T>(T objectToCheck, string methodName = EMPTY, string objectName = EMPTY, string additionalMessage = EMPTY,
        [CanBeNull] Object classReference = null)
    {
        if (objectToCheck != null) return false;

        if (objectName.Equals(EMPTY)) objectName = $"Object of Type {typeof(T)}";
        
        Log(DEBUG, methodName, $"{objectName} is null. {additionalMessage}", classReference);
        
        return true;
    }

    public static bool IsNullWarning<T>(T objectToCheck, string methodName = EMPTY,  string objectName = EMPTY, string additionalMessage = EMPTY,
        [CanBeNull] Object classReference = null)
    {
        if (objectToCheck != null) return false;

        if (objectName.Equals(EMPTY)) objectName = $"Object of Type {typeof(T)}";
        
        Log(WARNING, methodName, $"{objectName} is null. {additionalMessage}", classReference);
        
        return true;
    }
    
    public static bool IsNullWarning<T>(string methodName, T objectToCheck, string objectName = EMPTY, string additionalMessage = EMPTY,
        [CanBeNull] Object classReference = null)
    {
        if (objectToCheck != null) return false;

        if (objectName.Equals(EMPTY)) objectName = $"Object of Type {typeof(T)}";
        
        Log(WARNING, methodName, $"{objectName} is null. {additionalMessage}", classReference);
        
        return true;
    }
    
    public static bool IsNullError<T>(T objectToCheck, string methodName = EMPTY, string objectName = EMPTY, string additionalMessage = EMPTY,
        [CanBeNull] Object classReference = null)
    {
        if (objectToCheck != null) return false;

        if (objectName.Equals(EMPTY)) objectName = $"Object of Type {typeof(T)}";
        
        Log(ERROR, methodName, $"{objectName} is null. {additionalMessage}", classReference);
        
        return true;
    }
    
    public static bool IsNullError<T>(string methodName, T objectToCheck,  string objectName = EMPTY, string additionalMessage = EMPTY,
        [CanBeNull] Object classReference = null)
    {
        if (objectToCheck != null) return false;

        if (objectName.Equals(EMPTY)) objectName = $"Object of Type {typeof(T)}";
        
        Log(ERROR, methodName, $"{objectName} is null. {additionalMessage}", classReference);
        
        return true;
    }

    private static IEnumerator CallCoroutine()
    {
        yield return new WaitForFixedUpdate();
        _callNumber = 0;
    }
}
