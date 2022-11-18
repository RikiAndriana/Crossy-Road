using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    private void Awake()
    {
        // GameObject[] bgm = GameObject.FindGameObjectsWithTag("AudioManager");
        // if (bgm.Length > 1)
        //     Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
