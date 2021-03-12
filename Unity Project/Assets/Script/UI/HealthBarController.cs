using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] GameObject[] healthBars;

    int health;

    public void Start()
    {
        health = 9;
    }

    public void UpdateHealthBar(int num)
    {
        for(int i = 0; i < num && health - i >= 0; i++)
        {
            healthBars[health - i].SetActive(false);
        }
        health -= num;
    }
}
