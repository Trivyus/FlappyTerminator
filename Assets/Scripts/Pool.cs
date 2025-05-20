using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : PoolableObject<T>
{
    [SerializeField] private T _objectPrefab;
    [SerializeField] private float _objectLifetime = 3f;

    private Queue<T> _availableObjects = new();
    private Coroutine _coroutine;

    public event Action<T> ObjectTakedFromPool;

    private void OnDisable()
    {
        foreach (var availableObject in _availableObjects)
        {
            availableObject.ResetState();
        }
    }

    public T GetObject()
    {
        if (_availableObjects.Count == 0)
            CreateNewObject();

        T objectToDeque = _availableObjects.Dequeue();
        ObjectTakedFromPool?.Invoke(objectToDeque);
        _coroutine = StartCoroutine(DeactivateAfterLifetime(objectToDeque));
        return objectToDeque;
    }

    public void ReturnObject(T poolableObject)
    {
        if (poolableObject == null)
            return;

        StopWaitingDeactivation();
        poolableObject.ResetState();
        poolableObject.gameObject.SetActive(false);
        _availableObjects.Enqueue(poolableObject);
    }

    private void CreateNewObject()
    {
        T newObject = Instantiate(_objectPrefab, transform);
        newObject.gameObject.SetActive(false);
        _availableObjects.Enqueue(newObject);
    }

    private IEnumerator DeactivateAfterLifetime(T poolebleObject)
    {
        yield return new WaitForSeconds(_objectLifetime);

        if (poolebleObject.gameObject.activeInHierarchy)
            ReturnObject(poolebleObject);
    }

    private void StopWaitingDeactivation()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
}
