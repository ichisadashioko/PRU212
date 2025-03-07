using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<GameObjectPoolInfo> ALL_OBJECT_POOL_LIST = new List<GameObjectPoolInfo>();

    public static GameObject SpawnNewGameObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        GameObjectPoolInfo pool = null;
        foreach (GameObjectPoolInfo p in ALL_OBJECT_POOL_LIST)
        {
            if (p.GameObjectPrefabName == objectToSpawn.name)
            {
                pool = p;
                break;
            }
        }

        if (pool == null)
        {
            pool = new GameObjectPoolInfo() { GameObjectPrefabName = objectToSpawn.name };
            ALL_OBJECT_POOL_LIST.Add(pool);
        }

        GameObject spawnableObj = null;
        foreach(GameObject obj in pool.AvailableObjectList)
        {
            if(obj != null)
            {
                spawnableObj = obj;
                break;
            }
        }

        if (spawnableObj == null)
        {
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
        }
        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.AvailableObjectList.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnGameObjectToPool(GameObject obj)
    {
        if (obj.name.Contains("(Clone)"))
        {
            string goName = obj.name.Substring(0, obj.name.Length - 7);

            GameObjectPoolInfo pool = null;
            foreach (GameObjectPoolInfo p in ALL_OBJECT_POOL_LIST)
            {
                if (p.GameObjectPrefabName == goName)
                {
                    pool = p;
                    break;
                }
            }

            if (pool == null)
            {
                //pool = new PooledObjectInfo() { LookupString = goName };
                //Debug.LogWarning($"trying to release an object that is not pooled: {obj.name} - {goName}");
            }
            else
            {
                obj.SetActive(false);
                pool.AvailableObjectList.Add(obj);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
    }
}

public class GameObjectPoolInfo
{
    public string GameObjectPrefabName;
    public List<GameObject> AvailableObjectList = new List<GameObject>();
}
