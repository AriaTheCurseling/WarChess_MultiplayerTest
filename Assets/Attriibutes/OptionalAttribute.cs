using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class OptionalAttribute : PropertyAttribute
{
    public bool Enabled { get; set; }
}
