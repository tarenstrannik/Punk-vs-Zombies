using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //This allows the IComparable Interface
using UnityEngine.Pool;

//This is the class you will be storing
//in the different collections. In order to use
//a collection's Sort() method, this class needs to
//implement the IComparable interface.


public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
   
    [SerializeField] private GameObject[] objectToPool;
    private Dictionary<GameObject, List<GameObject>> objectsDictionary;

    [SerializeField] private int amountToPool=0;

    public bool isPoolingFinished = false;
    void Awake()
    {
        SharedInstance = this;
        objectsDictionary = new Dictionary<GameObject, List<GameObject>>();
        foreach(GameObject gameObj in objectToPool)
        {
            objectsDictionary.Add(gameObj, new List<GameObject> {});
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (KeyValuePair<GameObject,List<GameObject>> pair in objectsDictionary)
        {

            if (amountToPool == 0)
            {
                amountToPool = GetComponent<SpawnManager>().MaxObjectCount(pair.Key);
            }
            // Loop through list of pooled objects,deactivating them and adding them to the list 
           
            for (int i = 0; i < amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(pair.Key);
                obj.SetActive(false);
                pair.Value.Add(obj);
                obj.transform.SetParent(this.transform); // set as children of Spawn Manager
            }
        }
        isPoolingFinished = true;
    }

    public GameObject GetPooledObject(GameObject objectType)
    {
        List<GameObject> objList;
        if (objectsDictionary.TryGetValue(objectType, out objList))
        {
            // For as many objects as are in the pooledObjects list
            for (int i = 0; i < objList.Count; i++)
            {
                // if the pooled objects is NOT active, return that object 
                if (!objList[i].activeInHierarchy)
                {
                    return objList[i];
                }
            }
            // otherwise, return null   
            return null;
        }
        else
        {
            return null;
        }

        
    }

}
