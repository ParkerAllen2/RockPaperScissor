using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuelController : MonoBehaviour
{
    public DuelInputController player1;
    public DuelInputController player2;

    public float timeBetweenDuel;
    public float timeForDuel;

    public RoundInfo roundInfo;

    private int counter = 0;

    [SerializeField] GameObject[] cover;

    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] Text winnerText;

    [SerializeField] Animator ramen, sushi;

    public void Start()
    {
        roundInfo = new RoundInfo(0, 0);
        //StartCoroutine(BetweenDuel());
    }

    public void StartDuel()
    {
        StartCoroutine(BetweenDuel());
    }

    IEnumerator BetweenDuel()
    {
        player1.AnimSlideBack(timeBetweenDuel / 4);
        player2.AnimSlideBack(timeBetweenDuel / 4);
        ramen.Play("Flicker");
        sushi.Play("FlickerSushi");

        //DebugDuelInfo();
        //print("inbetween rounds");

        yield return new WaitForSeconds(timeBetweenDuel / 4);
        cover[0].SetActive(true);
        cover[1].SetActive(true);
        player1.AnimIdle();
        player2.AnimIdle();

        yield return new WaitForSeconds(timeBetweenDuel / 4);
        cover[2].SetActive(true);
        cover[3].SetActive(true);

        yield return new WaitForSeconds(timeBetweenDuel / 4);
        player1.AnimCharge();
        player2.AnimCharge();

        yield return new WaitForSeconds(.4f);
        cover[4].SetActive(true);

        yield return new WaitForSeconds(timeBetweenDuel / 4);

        StartCoroutine(ResolveDuel());
    }

    IEnumerator ResolveDuel()
    {
        //print("resolving round");
        roundInfo = new RoundInfo(player1.GetInputAttack(), player2.GetInputAttack());
        roundInfo.ResolveDamage(player1, player2);

        player1.UseAttack(roundInfo.outcome == 0, roundInfo.outcome == 2);
        player2.UseAttack(roundInfo.outcome == 1, false);

        player1.inputAttack = 0;
        player2.inputAttack = 0;
        yield return new WaitForSeconds(timeForDuel);

        player1.SetHealth(roundInfo.player1Damage);
        player2.SetHealth(roundInfo.player2Damage);

        foreach (GameObject c in cover)
        {
            c.SetActive(false);
        }

        if(player1.isDead() || player2.isDead())
        {
            gameOverCanvas.SetActive(true);
            if (player2.isDead())
            {
                winnerText.text = "Player 1\nWins!";
            }
            if (player1.isDead())
            {
                winnerText.text = "Player 2\nWins!";
            }
        }
        else
            StartCoroutine(BetweenDuel());
    }

    public class RoundInfo
    {
        public int player1Input;
        public int player2Input;

        public int player1Damage;
        public int player2Damage;

        //0 = player1 win, 1 = player2 win, 2 = draw
        public int outcome;

        public int[,] OUTCOMEMATRIX = new int[,] {  { 2, 1, 1, 1},
                                                    { 0, 2, 1, 0},
                                                    { 0, 0, 2, 1},
                                                    { 0, 1, 0, 2} };

        public RoundInfo(int p1i, int p2i)
        {
            player1Input = p1i;
            player2Input = p2i;
            outcome = OUTCOMEMATRIX[player1Input, player2Input];
            player1Damage = 0;
            player2Damage = 0;
        }

        public void ResolveDamage(DuelInputController player1, DuelInputController player2)
        {
            if (outcome == 0)
            {
                if(player1.IsMastery())
                {
                    player2Damage = player1.masteryDamage;
                }
                else
                {
                    player2Damage = player1.damage;
                }
            }
            else if (outcome == 1)
            {
                if(player2.IsMastery())
                {
                    player1Damage = player2.masteryDamage;
                }
                else
                {
                    player1Damage = player2.damage;
                }
            }
        }
    }

    public void DebugDuelInfo()
    {
        print("Player 1: health = " + player1.health +
            "\nMove = " + roundInfo.player1Input +
            "\nDamage = " + roundInfo.player1Damage);
        print("Player 2: health = " + player2.health +
            "\nMove = " + roundInfo.player2Input +
            "\nDamage = " + roundInfo.player2Damage);
    }
}
