using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInputController : DuelInputController
{
    public int difficulty;
    int[] attackOrder;
    int currentAttack;

    public void Start()
    {
        base.Start();
        attackOrder = new int[difficulty];
        for(int i = 0; i < difficulty; i++)
        {
            attackOrder[i] = Random.Range(1, 4);
        }
        currentAttack = 0;
    }

    public override int GetInputAttack()
    {
        CalculateAttackInput();
        return inputAttack;
    }

    public void CalculateAttackInput()
    {
        inputAttack = attackOrder[currentAttack];
        currentAttack = (currentAttack + 1) % difficulty;
    }
}
