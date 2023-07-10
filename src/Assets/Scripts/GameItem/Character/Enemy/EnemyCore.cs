using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;
public class EnemyCore : MonoBehaviour, IReciveDamage
{
    [SerializeField] private int maxHp = 3;
    private int hp;
    [SerializeField] private List<AShooter> shooter = new List<AShooter>();
    [SerializeField] private AMover mover;
    [SerializeField] private HpUISetter hpUI;
    [SerializeField] private AHitEvent hitEvent;
    [SerializeField] private GameObject spawnEffect;
    [SerializeField] private GameObject body;
    [SerializeField] private Collider2D col;
    [SerializeField] private GameObject skillItem;
    [SerializeField] private ChangeHpUIColor changeColor;
    [SerializeField] private ExclamationScript exclamationScript;

    private ScoreManager scoreManager;
    private GetNearistEnemyScript getNearistEnemy;
    private GameManager gameManager;
    private int index;

    public bool isDeath { get; set; }
    private bool isAlly;
    public bool getIsAlly { get { return isAlly; } }

    private Transform taregt;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        scoreManager = GameObject.Find("score").GetComponent<ScoreManager>();
        getNearistEnemy = new GetNearistEnemyScript();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        taregt = GameObject.Find("Player").transform;
    }

    public void Init()
    {
        SetAlly(false);
        col.enabled = false;
        body.SetActive(false);
        hp = maxHp;
        hpUI.NoAnimationSetHpFill((float)hp, (float)maxHp);

        foreach (AShooter s in shooter)
        {
            s.getIsShootNow = true;
        }

        StartCoroutine(delayInit());
    }

    void OnDisable()
    {
        StopCoroutine(delayInit());
    }

    IEnumerator delayInit()
    {
        var e = ObjectPool.Instance.GenerateObject(spawnEffect);
        e.transform.position = transform.position;

        SEManager.Instance.Play(SEPath.RESULT);

        yield return new WaitForSeconds(1.2f);

        SEManager.Instance.Play(SEPath.MISSION);

        isDeath = false;
        foreach (AShooter s in shooter)
        {
            s.getIsShootNow = false;
        }

        col.enabled = true;
        body.SetActive(true);
    }

    public void Shoot()
    {
        foreach (AShooter s in shooter)
        {
            if (s.getIsShootNow == true) return;
        }

        shooter[0].Shoot(isAlly == true ? BulletType.ally : BulletType.enemy);
    }

    public void Move(Transform allyTaregt, Transform player)
    {
        foreach (AShooter s in shooter)
        {
            if (s.getIsShootNow == true)
            {
                mover.Stop();
                return;
            }
        }

        var target = isAlly ? allyTaregt : player;

        if (target == null)
        {
            mover.Stop();
            return;
        }

        mover.Direction(target.position);

        if (isAlly == true)
        {
            mover.Stop();
            return;
        }

        mover.Move(target.position);
    }

    public void ReciveDamage(int attackPoint, BulletType type)
    {
        if (isAlly && type == BulletType.player)
        {
            gameManager.SetAllyToEnemy();
        }

        if (type == BulletType.playerAlly)
        {
            SEManager.Instance.Play(SEPath.CARE);
            SetAlly(true);
            return;
        }

        hp -= attackPoint;
        if (type == BulletType.ally)
        {
            hp -= 1;
        }

        hitEvent.HitEvent(hp <= 0);

        if (hp <= 0)
        {
            isDeath = true;
            gameObject.SetActive(false);
            scoreManager.AddScore(10);
            scoreManager.AddCombo();

            int itemCount = Random.Range(2, 5);
            for (int i = 0; i < itemCount; i++)
            {
                var s = ObjectPool.Instance.GenerateObject(skillItem);
                s.transform.position = (Vector2)transform.position + new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            }

            return;
        }

        hpUI.SetHpFill((float)hp, (float)maxHp);
    }

    public void SetAlly(bool isally)
    {
        if (isAlly == true && isally == false)
        {
            exclamationScript.ShowExclamation();
        }

        isAlly = isally;
        gameObject.tag = isAlly ? "Ally" : "Enemy";
        changeColor.SetColor(isAlly);
    }
}
