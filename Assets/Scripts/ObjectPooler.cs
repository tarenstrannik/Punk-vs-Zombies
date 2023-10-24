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
   
    private Dictionary<GameObject, int> objectToPoolDictionary;
    private Dictionary<GameObject, List<GameObject>> objectsDictionary;

    [SerializeField] private int amountToPool=0;

    public bool isPoolingFinished = false;
    void Awake()
    {
        SharedInstance = this;
        objectToPoolDictionary = new Dictionary<GameObject, int>();
        objectsDictionary = new Dictionary<GameObject, List<GameObject>>();
    }
    public void PushObjectToPool(GameObject pushObject, int count)
    {
        objectToPoolDictionary.Add(pushObject, count);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        foreach (KeyValuePair <GameObject,int> poolPair in objectToPoolDictionary)
        {
            objectsDictionary.Add(poolPair.Key, new List<GameObject> { });
        }
        foreach (KeyValuePair<GameObject,List<GameObject>> pair in objectsDictionary)
        {
            int objCount = 0;
            if (objectToPoolDictionary.TryGetValue(pair.Key, out objCount))
            {
                amountToPool = objCount;
            }
            else
            {
                amountToPool = 0;
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

            GameObject obj = (GameObject)Instantiate(objectType);
            obj.SetActive(true);
            objList.Add(obj);
            obj.transform.SetParent(this.transform);
            return obj;
            // otherwise, return null   
            //return null;
        }
        else
        {
            return null;
        }

        
    }

}
