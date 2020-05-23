using UnityEngine;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    //Creates a pool of objects to be used in game 

    public static ObjectPooling current;    //Creates an instance of this script to be used as a reference in other scripts
    public GameObject pooledObject;         //Object to be pooled
    public int pooledAmount = 20;           //Amount to objects to create in the pool
    public bool willGrow = true;            //Bool to control if the pool can increase in game

    public List<GameObject> pooledObjects;  //pool 
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    //Gets called on Awake
    void Awake()
    {
        current = this;
    }
    // --------------------------------------------------------------------
    //Use this for initialization
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    // --------------------------------------------------------------------
    //Method to find an availible object in the pool to be used in game
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //if pool is empty, create an object in the pool 
            if (pooledObjects[i] == null)
            {
                GameObject obj = (GameObject)Instantiate(pooledObject);
                obj.SetActive(false);
                pooledObjects[i] = obj;
                return pooledObjects[i];
            }

            //Finds an inactive object in the pool and returns it
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        //creates a extra object and add it into the pool if the pool is full and 'WillGrow' is set to true
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }
    // --------------------------------------------------- End Methods --------------------------------------------
}