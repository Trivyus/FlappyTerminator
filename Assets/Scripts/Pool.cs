using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : PoolableObject<T>
{
    [SerializeField] private T _objectPrefab;

    private Queue<T> _availableObjects = new();

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
        return objectToDeque;
    }

    public void ReturnObject(T poolableObject)
    {
        if (poolableObject == null)
            return;

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
}
