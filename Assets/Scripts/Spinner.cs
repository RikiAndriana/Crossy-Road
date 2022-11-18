using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 v3;


    private void Update()
    {
        transform.Rotate(v3 * Time.deltaTime * speed);
    }
}
