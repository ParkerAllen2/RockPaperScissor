using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private bool randomMastery = true;
    private int player2Mastery;
    private int player1Mastery;
    private int aiDifficulty = 1;
    private bool isPVP;

    [SerializeField] GameObject menuCanvas;

    [SerializeField] Slider difficultySlider;

    [SerializeField] DuelController duelController;
    [SerializeField] GameObject player1, player2, ai;

    public void Start()
    {
        //StartDuelController();
    }

    public void SetRandomMastery()
    {
        randomMastery = !randomMastery;
    }

    public void SetPlayer1Mastery(int i)
    {
        if (player1Mastery == i)
            player1Mastery = 0;
        else
            player1Mastery = i;
    }

    public void SetPlayer2Mastery(int i)
    {
        if (player2Mastery == i)
            player2Mastery = 0;
        else
            player2Mastery = i;
    }

    public void SetAIDifficulty()
    {
        aiDifficulty = (int)difficultySlider.value;
    }

    public void SetPVP()
    {
        isPVP = !isPVP;
    }

    public void StartDuelController()
    {
        GameObject[] pGo = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject g in pGo)
        {
            Destroy(g);
        }

        SpawnPlayer1();
        if (isPVP)
            SpawnPlayer2();
        else
            SpawnPlayerAI();

        menuCanvas.SetActive(false);
        duelController.StartDuel();
    }

    public void SpawnPlayer1()
    {
        PlayerInputController pic = Instantiate(player1).GetComponent<PlayerInputController>();
        pic.mastery = player1Mastery;
        duelController.player1 = pic;
    }

    public void SpawnPlayer2()
    {
        PlayerInputController pic = Instantiate(player2).GetComponent<PlayerInputController>();
        pic.mastery = player2Mastery;
        duelController.player2 = pic;
    }

    public void SpawnPlayerAI()
    {
        AIInputController pic = Instantiate(ai).GetComponent<AIInputController>();
        pic.difficulty = aiDifficulty;
        if (randomMastery)
        {
            pic.mastery = Random.Range(1, 4);
        }
        else
            pic.mastery = player2Mastery;

        duelController.player2 = pic;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
