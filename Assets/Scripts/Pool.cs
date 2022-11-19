using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    GameObject prototype;
    Stack<GameObject> stack;

    public Pool(GameObject prototype)
    {
        this.prototype = prototype;
        this.stack = new Stack<GameObject>();
    }

    private GameObject Create(Vector3 position = default, Quaternion rotation = default)
    {
        return MonoBehaviour.Instantiate(prototype, position, rotation);
    }

    public GameObject Get(Vector3 position = default, Quaternion rotation = default)
    {
        if (stack.Count == 0)
            return Create(position, rotation);
        else
        {
            var item = stack.Pop();
            item.transform.position = position;
            item.transform.rotation = rotation;
            item.SetActive(true);
            return item;
        }
    }

    public void Release(GameObject item)
    {
        item.SetActive(false);
        stack.Push(item);
    }

    public void Clear(bool desrtoyItem)
    {
        if (desrtoyItem)
        {
            foreach (var item in stack)
            {
                MonoBehaviour.Destroy(item);
            }
        }
        stack.Clear();
    }
}
