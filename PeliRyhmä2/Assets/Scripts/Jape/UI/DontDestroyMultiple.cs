using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMultiple : MonoBehaviour
{
    public string objectID;

    private void Awake()
    {
        objectID = name + transform.position.ToString();
    }
    void Start()
    {
        for (int i = 0; i < Object.FindObjectsOfType<DontDestroyMultiple>().Length; i++)
        {
            if (Object.FindObjectsOfType<DontDestroyMultiple>()[i] != this)
            {
                if (Object.FindObjectsOfType<DontDestroyMultiple>()[i].objectID == objectID)
                {
                    Destroy(gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);

    }
}
