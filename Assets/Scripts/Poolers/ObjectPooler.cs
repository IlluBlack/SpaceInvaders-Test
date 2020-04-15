using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField]
    private List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolsDictionary;

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        poolsDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject GO = Instantiate(pool.prefab);
                GO.SetActive(false);
                objectPool.Enqueue(GO);
            }

            poolsDictionary.Add(pool.key.ToString(), objectPool);
        }
    }

    public GameObject SpawnFromPool(TypeOfPool poolKey, Vector3 position, Quaternion rotation)
    {
        string keyName = poolKey.ToString();

        if (!poolsDictionary.ContainsKey(keyName))
        {
            Debug.LogWarning("Pool with key " + keyName + " doesn't exists");
            return null;
        }

        GameObject objectToSpawn = poolsDictionary[keyName].Dequeue();

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        objectToSpawn.SetActive(true);

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        if (pooledObject != null)
            pooledObject.OnObjectSpawn();
        else
            Debug.LogWarning("The instantiated object doesn't have an IPooledObject");

        poolsDictionary[keyName].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void Restart()
    {
        foreach (Pool pool in pools)
        {
            string keyName = pool.key.ToString();

            foreach (GameObject GO in poolsDictionary[keyName])
            {
                GO.SetActive(false);
            }
        }
    }

}


[System.Serializable]
public class Pool
{
    [SerializeField]
    public TypeOfPool key;
    public GameObject prefab;
    public int size;
}

public enum TypeOfPool
{
    Enemies,
    PlayerBullets,
    FXBullet
}
