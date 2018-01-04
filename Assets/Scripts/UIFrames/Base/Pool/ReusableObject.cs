using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池中的单个对象的基类
/// </summary>
public abstract class ReusableObject : MonoBehaviour, IReusable
{
    public abstract void OnSpawn();
    public abstract void OnUnspawn();
}
