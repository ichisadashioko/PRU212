using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<GameObjectPoolInfo> ALL_OBJECT_POOL_LIST = new List<GameObjectPoolInfo>();
    public static List<GameObject> ALL_TEXT_OBJECT_LIST = new List<GameObject>();
    public static int MAX_ALL_TEXT_OBJECT_LIST = 100;

    public static bool check_text_limit()
    {
        int active_count = 0;
        bool has_null = false;
        foreach (var go in ALL_TEXT_OBJECT_LIST)
        {
            if (go == null)
            {
                has_null = true;
                continue;
            }
            if (go.activeInHierarchy)
            {
                active_count++;
            }
        }

        if (has_null)
        {
            ALL_TEXT_OBJECT_LIST = ALL_TEXT_OBJECT_LIST.Where(go => go != null).ToList();
        }

        return (active_count < MAX_ALL_TEXT_OBJECT_LIST);
    }

    public enum PoolType
    {
        Bullet,
        Text,
        Exp,
        RestoreHP,
        Enemies,
        None
    }

    public static PoolType PoolingType;

    private static Dictionary<object, GameObject> ENUM_TO_PARENT_GAME_OBJECT_DICT = new();

    private static GameObject set_parent_object(PoolType pool_type)
    {
        //var values = Enum.GetValues(typeof(PoolType));
        //foreach(var enum_value in values)
        //{
        //    ENUM_TO_PARENT_GAME_OBJECT_DICT.Add(enum_value, new GameObject($"{enum_value}_PARENT_GAME_OBJECT"));
        //}

        if (!ENUM_TO_PARENT_GAME_OBJECT_DICT.ContainsKey(pool_type))
        {
            ENUM_TO_PARENT_GAME_OBJECT_DICT.Add(pool_type, new GameObject($"{pool_type}_PARENT_GAME_OBJECT"));
        }

        var go = ENUM_TO_PARENT_GAME_OBJECT_DICT[pool_type];
        if (go == null)
        {
            go = new GameObject($"{pool_type}_PARENT_GAME_OBJECT");
            ENUM_TO_PARENT_GAME_OBJECT_DICT[pool_type] = go;
        }

        if (go.activeInHierarchy)
        {
            return go;
        }

        go = new GameObject($"{pool_type}_PARENT_GAME_OBJECT");
        ENUM_TO_PARENT_GAME_OBJECT_DICT[pool_type] = go;

        return go;
    }
    public static GameObject SpawnNewTextGameObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None, bool active = true)
    {
        if (!check_text_limit())
        {
            return null;
        }

        var go = SpawnNewGameObject(objectToSpawn, spawnPosition, spawnRotation, poolType, active);
        if (!ALL_TEXT_OBJECT_LIST.Contains(go))
        {
            ALL_TEXT_OBJECT_LIST.Add(go);
        }

        return go;
    }

    public static GameObject SpawnNewGameObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None, bool active = true)
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
        foreach (GameObject obj in pool.AvailableObjectList)
        {
            if (obj != null)
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
            if (active)
            {
                spawnableObj.SetActive(true);
            }
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
