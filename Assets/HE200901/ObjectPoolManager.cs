using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<GameObjectPoolInfo> ALL_OBJECT_POOL_LIST = new List<GameObjectPoolInfo>();

    public enum PoolType
    {
        Bullet,
        None
    }

    public static PoolType PoolingType;

    private static GameObject _bullet_game_object_holder;
    private static GameObject _other_game_object_holder;

    private static void setup_empty_game_objects_to_hold_pooled_objects()
    {
        _bullet_game_object_holder = new GameObject("bullet_game_object_pool");
        _other_game_object_holder = new GameObject("other_game_object_pool");
    }

    private static GameObject set_parent_object(PoolType pool_type)
    {
        if(_bullet_game_object_holder == null)
        {
            setup_empty_game_objects_to_hold_pooled_objects();
        }

        switch (pool_type)
        {
            case PoolType.Bullet:
                return _bullet_game_object_holder;
            case PoolType.None:
                return _other_game_object_holder;
            default:
                return null;
        }
    }

    public static GameObject SpawnNewGameObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
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
            GameObject _parent_object = set_parent_object(poolType);
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            if (_parent_object != null)
            {
                spawnableObj.transform.SetParent(_parent_object.transform);
            }
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
