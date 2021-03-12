using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelInputController : MonoBehaviour
{
    public int maxHealth = 10;
    [HideInInspector] public int health;

    //0 = nothing, 1 = attack, 2 = counter, 3 = fients
    [HideInInspector] public int inputAttack;
    [HideInInspector] public int mastery = 0;

    public int damage = 1, masteryDamage = 2;

    public DuelController duelController;
    public SwordController swordController;
    [SerializeField] HealthBarController healthBar;

    private Animator animator;
    public Animator pushOffanim;
    private SpriteRenderer pushOffSprite;
    [SerializeField] GameObject dragDustPrfab;

    AudioSource chargeAudio;
    AudioSource dragAudio;

    public void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        pushOffSprite = pushOffanim.gameObject.GetComponent<SpriteRenderer>();

        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        duelController = go.GetComponent<DuelController>();

        chargeAudio = GameObject.Find("Launch").GetComponent<AudioSource>();
        dragAudio = GameObject.Find("DragAudio").GetComponent<AudioSource>();
    }

    public void Start()
    {
        health = maxHealth;
    }

    public bool IsMastery()
    {
        return inputAttack == mastery;
    }

    public virtual int GetInputAttack()
    {
        return Random.Range(0, 4);
    }

    public void UseAttack(bool outcome , bool tie)
    {
        swordController.win = outcome;
        swordController.tie = tie;
        if (inputAttack == 1)
        {
            UseSlash();
        }
        else if (inputAttack == 2)
        {
            UseCounter();
        }
        else if (inputAttack == 3)
        {
            UseFient();
        }
        chargeAudio.Play();
        pushOffanim.Play("Play Dust");
    }

    public void UseSlash()
    {

        pushOffSprite.color = swordController.Slash(IsMastery());
    }

    public void UseCounter()
    {

        pushOffSprite.color = swordController.Counter(IsMastery());
    }

    public void UseFient()
    {

        pushOffSprite.color = swordController.Fient(IsMastery());
    }

    public void SetHealth(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(damage);
    }

    public void AnimIdle()
    {
        animator.SetInteger("State", 0);
    }

    public void AnimCharge()
    {
        animator.SetInteger("State", 1);
    }

    public void AnimSlideBack(float t)
    {
        animator.SetInteger("State", 2);
        StartCoroutine(SlideBackwards(t));
        StartCoroutine(DragDust(t));
    }

    IEnumerator SlideBackwards(float t)
    {
        float counter = 0;
        Vector3 origin = transform.position;
        transform.position = new Vector3(origin.x / 2, origin.y);
        while(counter < t)
        {
            transform.position = Vector3.Lerp(transform.position, origin, counter / t);
            counter += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator DragDust(float t)
    {
        float time = t * 2 / 3;
        int count = 3;
        Vector3 center = new Vector3((transform.rotation.y == 0) ? -1 : 1, 1);
        for (int i = 0; i < count; i++)
        {
            dragAudio.Play();
            Vector3 pos = transform.position - center;
            GameObject go = Instantiate(dragDustPrfab, pos, transform.rotation);
            SpriteRenderer sprite = go.GetComponent<SpriteRenderer>();
            sprite.color = pushOffSprite.color;
            
            yield return new WaitForSeconds(time / count);
        }
    }

    public bool isDead()
    {
        return health <= 0;
    }
}
