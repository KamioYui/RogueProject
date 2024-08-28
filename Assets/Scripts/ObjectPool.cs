using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{

    private static ObjectPool instance;
    //ʹ���ֵ�洢��ͬ������
    private Dictionary<string,Queue<GameObject>> objectPool = new Dictionary<string,Queue<GameObject>>();
    //������������ĸ�����
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

    //��������
    public GameObject GetObject(GameObject prefab)
    {
        GameObject _object;
        //�������Ƿ����Ԥ��������ֺʹ�����������
        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0) 
        { 
            //û�о�����һ��������
            _object = GameObject.Instantiate(prefab);
            PushObject(_object);
            //�жϳ������Ƿ���ڶ���ظ�����
            if(pool==null)
                pool = new GameObject("ObjectPool");
            //���ҳ������Ƿ�����Ӷ���صĸ�����
            GameObject childPool = GameObject.Find(prefab.name + "Pool");
            if (!childPool)
            {
                childPool = new GameObject(prefab.name + "Pool");
                //��Ϊ����������������
                childPool.transform.SetParent(prefab.transform);
            }
            //�������ɵ�������Ϊ��Ӧ�Ӷ���������������
            _object.transform.SetParent(childPool.transform);
        }
        _object = objectPool[prefab.name].Dequeue();
        _object.SetActive(true);
        return _object;
    }
    //�����������Żس���
    public void PushObject(GameObject prefab) 
    {
        //��ȡ�Ѿ�����Ķ��������
        string _name = prefab.name.Replace("(Clone)", string.Empty);
        //�����ڶ�������½�һ��
        if(!objectPool.ContainsKey(_name))
            objectPool.Add(_name, new Queue<GameObject>());
        //�������������в�ȡ������
        objectPool[_name].Enqueue(prefab);
        prefab.SetActive(false);
    }

}
