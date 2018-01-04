using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有使用对象池存的物体，都要实现此方法
/// </summary>
public interface IReusable
{
    /// <summary> object取出初始化 </summary>
    void OnSpawn();
    /// <summary> object放回初始化 </summary>
    void OnUnspawn();
}
