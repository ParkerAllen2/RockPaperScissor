using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : DuelInputController
{
    [SerializeField] KeyCode[] keys;

    void Update()
    {
        if (Input.GetKeyDown(keys[0]))
        {
            inputAttack = 1;
            //print("p : attack");
        }
        if (Input.GetKeyDown(keys[1]))
        {
            inputAttack = 2;
            //print("p : counter");
        }
        if (Input.GetKeyDown(keys[2]))
        {
            inputAttack = 3;
            //print("p : feint");
        }
    }

    public override int GetInputAttack()
    {
        return inputAttack;
    }
}
