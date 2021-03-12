using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIToggle : MonoBehaviour
{
    [SerializeField] GameObject[] go;
    
    public void Activate()
    {
        foreach(GameObject g in go)
        {
            g.SetActive(!g.activeSelf);
        }
    }
}
