using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolManger : MonoBehaviour
{
    public static List<PoolObjectInfo> ObjectPools = new List<PoolObjectInfo>();

    public static GameObject SpawnObject(GameObject objcetToSpawn, Vector2 spawnPosition, Quaternion spawnRotation)
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
        if(pool == null)
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
        if(spawnableObj == null)
        {
            //If there are no inactivate objects, create a new one
            spawnableObj = Instantiate(objcetToSpawn, spawnPosition, spawnRotation);
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
    
    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7); // by taking off 7, we are removing the (Clone) from the name of the passed in obj

        PoolObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

        if(pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
        }

        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }
}

public class PoolObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
