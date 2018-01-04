using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理所有的子对象池
/// </summary>
public class ObjectPool : Singleton<ObjectPool>
{
    /// <summary> 预设路径 </summary>
    public string ResourceDir = "";

    /// <summary> 名字--子对象池 </summary>
    private Dictionary<string, SubPool> _pools = new Dictionary<string, SubPool>();

    /// <summary>
    /// 从池中取出一个对象
    /// </summary>
    public GameObject Spawn(string name)
    {
        if (!_pools.ContainsKey(name)) RegisterNew(name);
        SubPool subPool = _pools[name];
        return subPool.Spawn();
    }

    /// <summary>
    /// 回收一个对象
    /// </summary>
    public void Unspawn(GameObject gameObject)
    {
        SubPool subPool = null;

        foreach (SubPool pool in _pools.Values)
        {
            if (!pool.Contains(gameObject)) continue;

            subPool = pool;
            break;
        }
        if (subPool != null) subPool.Unspawn(gameObject);
    }

    /// <summary>
    /// 回收这个池中所有对象
    /// </summary>
    public void UnspawnAll()
    {
        foreach (SubPool pool in _pools.Values)
            pool.UnspawnAll();
    }

    /// <summary>
    /// 创建一个新池子
    /// </summary>
    /// <param name="name"></param>
    private void RegisterNew(string name)
    {
        //预设路径
        string path = "";

        if (string.IsNullOrEmpty(ResourceDir))
            path = name;
        else
            path = ResourceDir + "/" + name;

        //加载预设体
        GameObject prefab = Resources.Load<GameObject>(path);

        //创建子对象池
        SubPool subPool = new SubPool(transform, prefab);

        _pools.Add(subPool.Name, subPool);
    }
}
