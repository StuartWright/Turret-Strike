using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingTurret : BaseTurret
{
    public GameObject[] points = new GameObject[4];
    public int currentPoint;
    public int WingRadius;
    private bool ShootMissiles;
    public bool InUpgrade;
    private float speed = 2;
    public GameObject prefab, ShootAnim2, MissileGO;
    public Transform FP1, FP2;
    private float distance;
    private Rigidbody2D rb;
    public Collider2D[] hit;
    private BaseEnemy Enemy2;

    public override string Upgrade1Info()
    {
        if (Level2UpgradeRank > 1 && Level1UpgradeRank == 1 && !InUpgradePanel)
            return "Max upgrades reached.";
        switch (Level1UpgradeRank)
        {
            case 0:
                return "Increases attack range. ";
            case 1:
                return "Increased attack range further. ";
            case 2:
                return "Drone shoots missiles. ";
            default:
                return "Max upgrades reached.";
        }
    }
    public override string Upgrade2Info()
    {
        if (Level1UpgradeRank > 1 && Level2UpgradeRank == 1 && !InUpgradePanel)
            return "Max upgrades reached.";
        switch (Level2UpgradeRank)
        {
            case 0:
                return "Increases damage. ";
            case 1:
                return "Increases attack speed and can see camo enemies. ";
            case 2:
                return "Increase Attack speed further. ";
            default:
                return "Max upgrades reached.";
        }
    }
    protected new void Start()
    {
        base.Start();
        Placed = true;
        rb = GetComponent<Rigidbody2D>();
        //transform.position = new Vector3(points[0].transform.position.x, points[0].transform.position.y, points[0].transform.position.z);
        currentPoint = 0;
    }
    protected override IEnumerator FindEnemies()
    {
        if (Placed)
        {
            Hit = Physics2D.OverlapCircleAll(FP1.position, WingRadius);
            foreach (Collider2D enemy in Hit)
            {
                if (enemy.tag == "Enemy" && distance <= enemy.GetComponent<BaseEnemy>().DistanceTravelled)
                {
                    Enemy = enemy.GetComponent<BaseEnemy>();
                    distance = Enemy.DistanceTravelled;
                }
            }
            if(Enemy != null)
            {
                if(ShootMissiles)
                {
                    Missile missile = Instantiate(MissileGO, FP1.position, FP1.rotation).GetComponent<Missile>();
                    //missile.IsHoming = true;
                    missile.speed = 5;
                    missile.damage = 1;
                    missile.radius = 1;
                    missile.moneyUpgrade = MoneyUpgrade;
                    missile.target = Enemy.gameObject;
                    missile.sender = this;
                }
                else
                {
                    ShootAnim.transform.LookAt(Enemy.transform);
                    ShootAnim.transform.localEulerAngles = new Vector3(0, ShootAnim.transform.localEulerAngles.y, 0);
                    if (ShootAnim != null)
                        ShootAnim.SetActive(true);
                    Enemy.TakeDamage(Damage, MoneyUpgrade);
                    KillCount++;
                    Player.UpdateKillCount();
                }                
            }

            hit = Physics2D.OverlapCircleAll(FP2.position, WingRadius);
            foreach (Collider2D enemy in hit)
            {
                if (enemy.tag == "Enemy" && enemy != Enemy && distance >= enemy.GetComponent<BaseEnemy>().DistanceTravelled)
                {
                    Enemy2 = enemy.GetComponent<BaseEnemy>();
                    distance = Enemy2.DistanceTravelled;
                }
            }
            if (Enemy2 != null)
            {
                if (ShootMissiles)
                {
                    Missile missile = Instantiate(MissileGO, FP2.position, FP2.rotation).GetComponent<Missile>();
                    //missile.IsHoming = true;
                    missile.speed = 5;
                    missile.damage = Damage;
                    missile.radius = 1;
                    missile.moneyUpgrade = MoneyUpgrade;
                    missile.target = Enemy2.gameObject;
                    missile.sender = this;
                }
                else
                {
                    ShootAnim2.transform.LookAt(Enemy2.transform);
                    ShootAnim2.transform.localEulerAngles = new Vector3(0, ShootAnim2.transform.localEulerAngles.y, 0);
                    if (ShootAnim2 != null)
                        ShootAnim2.SetActive(true);
                    Enemy2.TakeDamage(Damage, MoneyUpgrade);
                    KillCount++;
                    Player.UpdateKillCount();
                }                  
            }
        }
        Enemy = null;
        Enemy2 = null;
        yield return new WaitForSeconds(FireRate);
        if (ShootAnim != null)
            ShootAnim.SetActive(false);
        if (ShootAnim2 != null)
            ShootAnim2.SetActive(false);
        StartCoroutine(FindEnemies());
    }


    protected new void Update()
    {
        if(!InUpgrade)
        {
            rb.transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].transform.position, speed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, points[currentPoint].transform.position);
            prefab.transform.LookAt(points[currentPoint].transform, transform.right);
            prefab.transform.localEulerAngles = new Vector3(prefab.transform.localEulerAngles.x, prefab.transform.localEulerAngles.y, prefab.transform.localEulerAngles.z - 90);
            if (transform.position.x > 0)
            {
                prefab.transform.localEulerAngles = new Vector3(prefab.transform.localEulerAngles.x, prefab.transform.localEulerAngles.y, prefab.transform.localEulerAngles.z + 180);
            }
            if (distance <= 0)
            {
                currentPoint++;
            }
            if (currentPoint >= 4)
                currentPoint = 0;
        }       
    }

    public override void UpgradeType1()
    {
        switch (Level1UpgradeRank)
        {
            case 0:
                if (Player.Instance.money >= T1TurretUpgradeCost)
                {
                    Player.Instance.money -= T1TurretUpgradeCost;
                    SellPrice += T1TurretUpgradeCost / 2;
                    T1TurretUpgradeCost += 1000;
                    WingRadius++;
                    Player.Instance.TurretDamageText.text = "" + Damage;
                    Level1UpgradeRank++;
                }
                break;
            case 1:
                if (Player.Instance.money >= T1TurretUpgradeCost && Level2UpgradeRank <= 1)
                {
                    Player.Instance.money -= T1TurretUpgradeCost;
                    SellPrice += T1TurretUpgradeCost / 2;
                    T1TurretUpgradeCost += 1000;
                    WingRadius++;
                    Player.Instance.TurretDamageText.text = "" + Damage;
                    Level1UpgradeRank++;
                }
                break;
            case 2:
                if (Player.Instance.money >= T1TurretUpgradeCost)
                {
                    Player.Instance.money -= T1TurretUpgradeCost;
                    SellPrice += T1TurretUpgradeCost / 2;
                    ShootMissiles = true;
                    Player.Instance.TurretDamageText.text = "" + Damage;
                    Level1UpgradeRank++;
                }
                break;
        }
        Player.Instance.UpdateUpgradeText();
    }

    public override void UpgradeType2()
    {
        switch (Level2UpgradeRank)
        {
            case 0:
                if (Player.Instance.money >= T2TurretUpgradeCost)
                {
                    Player.Instance.money -= T2TurretUpgradeCost;
                    SellPrice += T2TurretUpgradeCost / 2;
                    T2TurretUpgradeCost += 1000;                   
                    Damage++;
                    Player.Instance.TurretDamageText.text = "" + Damage;
                    Level2UpgradeRank++;
                    SetPoints();
                }
                break;
            case 1:
                if (Player.Instance.money >= T2TurretUpgradeCost && Level1UpgradeRank <= 1)
                {
                    Player.Instance.money -= T2TurretUpgradeCost;
                    SellPrice += T2TurretUpgradeCost / 2;
                    T2TurretUpgradeCost += 1000;
                    canSeeCamo = true;
                    FireRate -= 0.3f;
                    Level2UpgradeRank++;
                    SetPoints();
                }
                break;
            case 2:
                if (Player.Instance.money >= T2TurretUpgradeCost)
                {
                    Player.Instance.money -= T2TurretUpgradeCost;
                    SellPrice += T2TurretUpgradeCost / 2;
                    FireRate -= 0.4f;
                    Level2UpgradeRank++;
                }
                break;
        }
        Player.Instance.UpdateUpgradeText();
    }
}
