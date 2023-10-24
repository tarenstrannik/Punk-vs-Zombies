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
   
    
    private Dictionary<GameObject, List<GameObject>> objectsDictionary;


    void Awake()
    {
        SharedInstance = this;
       
        objectsDictionary = new Dictionary<GameObject, List<GameObject>>();
    }
    public void PushObjectToPool(GameObject pushObject, int count)
    {
        List<GameObject> objList;
        if (objectsDictionary.TryGetValue(pushObject, out objList))
        {
            for (int i = 0; i < count; i++)
            {
                GameObject obj = (GameObject)Instantiate(pushObject);
                obj.SetActive(false);
                objList.Add(obj);
                obj.transform.SetParent(this.transform); // set as children of Spawn Manager
            }
        }
        else
        {
            objectsDictionary.Add(pushObject, new List<GameObject> { });
            
            for (int i = 0; i < count; i++)
            {
                GameObject obj = (GameObject)Instantiate(pushObject);
                obj.SetActive(false);
                objectsDictionary[pushObject].Add(obj);
                obj.transform.SetParent(this.transform); // set as children of Spawn Manager
            }
        }
        
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
            obj.SetActive(false);
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
