using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子对象池，每个池中装有不同的object
/// </summary>
public class SubPool
{
    private Transform _parent;
    /// <summary> 存放的单个物体的模板 </summary>
    private GameObject _prefab;
    /// <summary> 存放这个池中所有的物体 </summary>
    private List<GameObject> _objects = new List<GameObject>();

    /// <summary> 对象的名字 </summary>
    public string Name { get { return _prefab.name; } }


    public SubPool(Transform parent, GameObject prefab)
    {
        this._parent = parent;
        this._prefab = prefab;
    }

    /// <summary>
    /// 从池中取出一个对象
    /// </summary>
    public GameObject Spawn()
    {
        GameObject go = null;

        foreach (GameObject gameObject in _objects)
        {
            if (gameObject.activeSelf) continue;
            go = gameObject;
            break;
        }

        if (go == null)
        {
            go = Object.Instantiate<GameObject>(_prefab);
            go.transform.parent = _parent;
            _objects.Add(go);
        }

        go.SetActive(true);
        go.SendMessage("OnSpawn",SendMessageOptions.DontRequireReceiver);

        return go;
    }

    /// <summary>
    /// 回收一个对象
    /// </summary>
    public void Unspawn(GameObject gameObject)
    {
        if (Contains(gameObject))
        {
            gameObject.SendMessage("OnUnspawn", SendMessageOptions.DontRequireReceiver);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 回收这个池中所有对象
    /// </summary>
    public void UnspawnAll()
    {
        foreach (GameObject gameObject in _objects)
        {
            if (gameObject.activeSelf) Unspawn(gameObject);
        }
    }

    /// <summary>
    /// 判断回收的是否在这个池子中
    /// </summary>
    public bool Contains(GameObject gameObject)
    {
        return _objects.Contains(gameObject);
    }
}
