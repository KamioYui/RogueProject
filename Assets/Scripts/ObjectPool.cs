using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{

    private static ObjectPool instance;
    //使用字典存储不同的物体
    private Dictionary<string,Queue<GameObject>> objectPool = new Dictionary<string,Queue<GameObject>>();
    //所有生成物体的父物体
    private GameObject pool;
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance;
        }
    }

    //生成物体
    public GameObject GetObject(GameObject prefab)
    {
        GameObject _object;
        //检查池内是否存在预制体的名字和待分配物体数
        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0) 
        { 
            //没有就生成一个新物体
            _object = GameObject.Instantiate(prefab);
            PushObject(_object);
            //判断场景中是否存在对象池父物体
            if(pool==null)
                pool = new GameObject("ObjectPool");
            //查找场景中是否存在子对象池的父物体
            GameObject childPool = GameObject.Find(prefab.name + "Pool");
            if (!childPool)
            {
                childPool = new GameObject(prefab.name + "Pool");
                //设为对象池物体的子物体
                childPool.transform.SetParent(prefab.transform);
            }
            //将刚生成的物体设为对应子对象池物体的子物体
            _object.transform.SetParent(childPool.transform);
        }
        _object = objectPool[prefab.name].Dequeue();
        _object.SetActive(true);
        return _object;
    }
    //将用完的物体放回池内
    public void PushObject(GameObject prefab) 
    {
        //获取已经用完的对象的名称
        string _name = prefab.name.Replace("(Clone)", string.Empty);
        //不存在对象池则新建一个
        if(!objectPool.ContainsKey(_name))
            objectPool.Add(_name, new Queue<GameObject>());
        //将该物体放入池中并取消激活
        objectPool[_name].Enqueue(prefab);
        prefab.SetActive(false);
    }

}
