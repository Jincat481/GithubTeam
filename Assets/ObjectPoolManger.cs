using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolManger : MonoBehaviour
{
    public static List<PoolObjectInfo> ObjectPools = new List<PoolObjectInfo>();

    private GameObject _objectPoolEmptyHolder;

    private static GameObject _particleSystemsEmpty;
    private static GameObject _gameObjectsEmpty;
    private static GameObject _noneEmpty;
    public enum PoolType
    {
        ParticleSystem,
        GameObject,
        None
    }
    public static PoolType PoolingType;

    private void Awake()
    {
        SetupEmpties();
    }

    private void SetupEmpties()
    {
        _objectPoolEmptyHolder = new GameObject("Pooled Objects");

        _particleSystemsEmpty = new GameObject("Particle Effects");
        _particleSystemsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _gameObjectsEmpty = new GameObject("GameObjects");
        _gameObjectsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _noneEmpty = new GameObject("None");
        _noneEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);
    }

    public static GameObject SpawnObject(GameObject objcetToSpawn, Vector2 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        PoolObjectInfo pool = ObjectPools.Find(p => p.LookupString == objcetToSpawn.name);

        // PoolObjectInfo pool = null;
        // foreach(PoolObjectInfo p in ObjectPools)
        // {
        //     if(p.LookupString == objcetToSpawn.name)
        //     {
        //         pool = p;
        //         break;
        //     }
        // }

        // If the pool doesn't exist, creat it
        if (pool == null)
        {
            pool = new PoolObjectInfo() { LookupString = objcetToSpawn.name };
            ObjectPools.Add(pool);
        }

        // Check if there are any iactive objects in the pool
        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

        // GameObject spawnableObj = null;
        // foreach(GameObject obj in pool.InactiveObjects)
        // {
        //     if(obj != null)
        //     {
        //         spawnableObj = obj;
        //         break;
        //     }
        // }

        // 비활성 게임 개체 목록에서 무엇을 찾았는지 확인
        if (spawnableObj == null)
        {
            //Find the parent of the empty object
            GameObject parentObject = SetParenObject(poolType);

            //If there are no inactivate objects, create a new one
            spawnableObj = Instantiate(objcetToSpawn, spawnPosition, spawnRotation);

            if (parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }

        else
        {
            //If there is an inactive object, reactive it
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj, PoolType poolType = PoolType.None)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7); // by taking off 7, we are removing the (Clone) from the name of the passed in obj

        PoolObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

        if (pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
        }

        else
        {
            obj.SetActive(false);
            GameObject parentObject = SetParenObject(poolType);
            if (parentObject != null)
            {
                obj.transform.SetParent(parentObject.transform);
            }
            pool.InactiveObjects.Add(obj);
        }
    }

    private static GameObject SetParenObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.ParticleSystem:
                return _particleSystemsEmpty;

            case PoolType.GameObject:
                return _gameObjectsEmpty;

            case PoolType.None:
                return _noneEmpty;

            default:
                return null;
        }
    }
}

public class PoolObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
