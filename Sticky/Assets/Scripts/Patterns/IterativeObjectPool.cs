﻿using UnityEngine;
using System.Collections.Generic;

//Base class for an iterative object pool
public abstract class IterativeObjectPool<T> where T : Object
{
    protected List<T> pooledObjects = new List<T>();
    protected T poolObject = null;
    protected Transform parentTransform = null;

    public List<T> currentPool { get { return pooledObjects; } }

    protected int poolIndex = 0;

    protected abstract void InitialisePool(T _poolObject, int _startSize, Transform _parent = null);
    public abstract T GetPooledObject();
}

//Iterative object pool for GameObjects
public class IterativeGameObjectPool : IterativeObjectPool<GameObject>
{
    public IterativeGameObjectPool(GameObject _poolObject, int _startSize = 0, Transform _parent = null)
    {
        InitialisePool(_poolObject, _startSize, _parent);
    }

    //Sets up pool
    protected override void InitialisePool(GameObject _poolObject, int _startSize, Transform _parent = null)
    {
        poolIndex = 0;
        poolObject = _poolObject;
        parentTransform = _parent;
        for (int i = 0; i < _startSize; ++i)
        {
            GameObject _obj = Object.Instantiate(poolObject);
            _obj.transform.SetParent(parentTransform);
            _obj.SetActive(false);
            pooledObjects.Add(_obj);
        }
    }

    public override GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; ++i)
        {
            if (pooledObjects[poolIndex].activeSelf)
            {
                ++poolIndex;
                if(poolIndex == pooledObjects.Count)
                {
                    poolIndex = 0;
                }
            }
            else
            {
                pooledObjects[poolIndex].SetActive(true);
                return pooledObjects[poolIndex];
            }
        }

        GameObject _obj = Object.Instantiate(poolObject);
        _obj.transform.SetParent(parentTransform);
        _obj.SetActive(true);
        pooledObjects.Add(_obj);
        return _obj;
    }
}

//Iterative object pool for referencing a script directly on a GameObject
public class IterativeBehaviourPool<T> : IterativeObjectPool<T> where T : Behaviour
{
    public IterativeBehaviourPool(T _poolObject, int _startSize = 0, Transform _parent = null)
    {
        InitialisePool(_poolObject, _startSize, _parent);
    }

    //Sets up pool
    protected override void InitialisePool(T _poolObject, int _startSize, Transform _parent = null)
    {
        poolIndex = 0;
        poolObject = _poolObject;
        parentTransform = _parent;
        for (int i = 0; i < _startSize; ++i)
        {
            T _obj = Object.Instantiate(poolObject);
            _obj.transform.SetParent(parentTransform);
            _obj.gameObject.SetActive(false);
            pooledObjects.Add(_obj);
        }
    }

    public override T GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; ++i)
        {
            if (pooledObjects[poolIndex].gameObject.activeSelf)
            {
                ++poolIndex;
                if (poolIndex == pooledObjects.Count)
                {
                    poolIndex = 0;
                }
            }
            else
            {
                pooledObjects[poolIndex].gameObject.SetActive(true);
                return pooledObjects[poolIndex];
            }
        }

        T _obj = Object.Instantiate(poolObject);
        _obj.transform.SetParent(parentTransform);
        _obj.gameObject.SetActive(true);
        pooledObjects.Add(_obj);
        return _obj;
    }
}
