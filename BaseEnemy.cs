using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class BaseEnemy : MonoBehaviour, IDamagable
{
    public PathCreator PC;
    private WaveManager WM;
    public EndOfPathInstruction End;
    public float Speed;
    public float DistanceTravelled;
    public int KillMoney;
    public int DamageToPlayer;
    public bool isSlow;
    public bool isCamo;
    private int enemyHealth;
    private int maxEnemyHealth;
    private Color color;
    public GameObject enemyToSpawn;
    private float alpha = .3f;
    private SpriteRenderer SP;
    private Animator Anim;
    [SerializeField]
    private int EnemyLevel;
    public int enemyLevel
    {
        get { return EnemyLevel; }
        set
        {
            EnemyLevel = value;
            switch (enemyLevel)
            {
                case 1:
                    if (isSlow)
                        Speed = Speed / 2;
                    else
                        Speed = 1.5f;
                    DamageToPlayer = 1;
                    KillMoney = 1;
                    color = SP.color;                  
                    Anim.SetInteger("Enemy2", 0);
                    if (isCamo)
                        color.a = alpha;
                    SP.color = color;
                    transform.localScale = new Vector2(2.6f,2.6f);
                    break;
                case 2:
                    if (isSlow)
                        Speed = Speed / 2;
                    else
                        Speed = 2.5f;
                    DamageToPlayer = 2;
                    KillMoney = 2;
                    color = SP.color;
                    Anim.SetInteger("Enemy2", 1);
                    if (isCamo)
                        color.a = alpha;
                    SP.color = color;
                    transform.localScale = new Vector2(2.8f, 2.8f);
                    break;
                case 3:
                    if (isSlow)
                        Speed = Speed / 2;
                    else
                        Speed = 3.5f;
                    DamageToPlayer = 3;
                    KillMoney = 3;
                    color = SP.color;
                    Anim.SetInteger("Enemy2", 2);
                    if (isCamo)
                        color.a = alpha;
                    SP.color = color;
                    transform.localScale = new Vector2(3, 3);
                    break;
                case 4:
                    if (isSlow)
                        Speed = Speed / 2;
                    else
                        Speed = 4.5f;
                    DamageToPlayer = 4;
                    KillMoney = 4;
                    color = SP.color;
                    Anim.SetInteger("Enemy2", 3);
                    if (isCamo)
                        color.a = alpha;
                    SP.color = color;
                    transform.localScale = new Vector2(3.2f, 3.2f);
                    break;
                case 5:
                    Speed = 1;
                    DamageToPlayer = 5;
                    KillMoney = 5;
                    Anim.SetInteger("Enemy2", 4);
                    color = SP.color;
                    SP.color = color;
                    enemyHealth = 15;
                    maxEnemyHealth = 1;
                    transform.localScale = new Vector2(3.4f, 3.4f);
                    break;
                case 6:
                    Speed = 0.9f;
                    DamageToPlayer = 6;
                    KillMoney = 6;
                    Anim.SetInteger("Enemy2", 5);
                    color = SP.color;
                    if (isCamo)
                        color.a = alpha;
                    SP.color = color;
                    enemyHealth = 30;
                    maxEnemyHealth = 2;
                    transform.localScale = new Vector2(3.6f, 3.6f);
                    break;
                case 7:
                    Speed = 0.8f;
                    DamageToPlayer = 7;
                    KillMoney = 7;
                    Anim.SetInteger("Enemy2", 6);
                    color = SP.color;
                    if (isCamo)
                        color.a = alpha;
                    SP.color = color;
                    enemyHealth = 50;
                    maxEnemyHealth = 3;
                    transform.localScale = new Vector2(3.8f, 3.8f);
                    break;
                case 8:
                    Speed = 0.7f;
                    DamageToPlayer = 8;
                    KillMoney = 8;
                    Anim.SetInteger("Enemy2", 7);
                    color = SP.color;
                    if (isCamo)
                        color.a = alpha;
                    SP.color = color;
                    enemyHealth = 80;
                    maxEnemyHealth = 4;
                    transform.localScale = new Vector2(4, 4);
                    break;
            }
        }
    }

    private void OnEnable()
    {
        DistanceTravelled = 0;
    }
    void Awake()
    {
        PC = GameObject.Find("PathCreator").GetComponent<PathCreator>();
        WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        SP = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        transform.position = PC.path.GetPoint(0);
        gameObject.SetActive(false);
        //enemyLevel = enemyLevel;
    }

    void Update()
    {
        DistanceTravelled += Speed * Time.deltaTime;
        transform.position = PC.path.GetPointAtDistance(DistanceTravelled, End);
       // transform.rotation = PC.path.GetRotationAtDistance(DistanceTravelled, End);
       
    }

    public void TakeDamage(int Damage, bool? MoneyUpgrade)
    {

        if(enemyLevel >= 5)
        {
            enemyHealth -= Damage;
            if (enemyHealth <= 0)
            {
                WM.RemainingEnemies--;
                float currentDistance = DistanceTravelled;
                for(int i = 0; i <= 4; i++)
                {
                    //GameObject enemy = Instantiate(enemyToSpawn, new Vector3(0, 0, 0), Quaternion.identity);
                    GameObject enemy = ObjectPooler.Instance.SpawnFromPool("Enemy");
                    BaseEnemy newEnemy = enemy.GetComponent<BaseEnemy>();
                    newEnemy.DistanceTravelled = currentDistance - .5f;
                    currentDistance = newEnemy.DistanceTravelled;
                    newEnemy.enemyLevel = this.maxEnemyHealth;
                    WM.RemainingEnemies++;

                }
                transform.position = PC.path.GetPointAtDistance(0);
                gameObject.SetActive(false);
            }
        }
        else
        {           
            if (MoneyUpgrade == true)
                Player.Instance.money += KillMoney + 1;
            else
                Player.Instance.money += KillMoney;
            enemyLevel -= Damage;
            if (enemyLevel <= 0)
            {
                WM.RemainingEnemies--;
                if (WM.RemainingEnemies <= 0)
                    Player.Instance.WaveResult(true);
                transform.position = PC.path.GetPointAtDistance(0);
                gameObject.SetActive(false);
            }
        }
        WM.UpdateREnemies();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EndPoint")
        {
            Player.Instance.TakeDamage(DamageToPlayer, null);
            if (WM.RemainingEnemies <= 0)
                Player.Instance.WaveResult(true);
            transform.position = PC.path.GetPointAtDistance(0);
            gameObject.SetActive(false);
        }
    }

    public IEnumerator SlowDown(float slowTime, float SlowAmount)
    {
        Speed = Speed / SlowAmount;
        isSlow = true;
        yield return new WaitForSeconds(slowTime);
        Speed = Speed * SlowAmount;
        isSlow = false;
    }
    public IEnumerator Reverse()
    {
        float CurrentSpeed = Speed;
        Speed -= Speed + 1;
        isSlow = true;
        yield return new WaitForSeconds(1.5f);
        Speed = CurrentSpeed;
        isSlow = false;
    }
    public IEnumerator Stop()
    {
        float CurrentSpeed = Speed;
        Speed = 0;
        yield return new WaitForSeconds(2);
        Speed = CurrentSpeed;
    }
}
