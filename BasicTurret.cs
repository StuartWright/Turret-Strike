using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : BaseTurret
{
    //public delegate void Laser(Transform Sender);
    //public event Laser UpdateLaser;
    [SerializeField] GameObject LaserTurret, Turret;
    [SerializeField] GameObject LaserRotate;
    private bool TwoShot;
    public override string Upgrade1Info()
    {
        if (Level2UpgradeRank > 1 && Level1UpgradeRank == 1 && !InUpgradePanel)
            return "Max upgrades reached.";
        switch (Level1UpgradeRank)
        {
            case 0:
                return "Increases Damage. ";
            case 1:
                return "Increases Damage further. ";
            case 2:
                return "Deals heavy damage to enemies and hits 2 targets. ";
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
                return "Increases Radius and attack speed. ";
            case 1:
                return "Increases Radius and attack speed further. ";
            case 2:
                return "Turns turret into a laser turret. ";
            default:
                return "Max upgrades reached.";
        }
    }
    protected override IEnumerator FindEnemies()
    {
        if (Placed)
        {
            Hit = Physics2D.OverlapCircleAll(transform.position, Radius/1.9f);
            Collider2D Target2 = null;
            float Distance = 0;
            foreach (Collider2D enemy in Hit)
            {
                if (enemy.tag == "Enemy" && Distance <= enemy.GetComponent<BaseEnemy>().DistanceTravelled)
                {
                    if (enemy.GetComponent<BaseEnemy>().isCamo == false)
                    {
                        Enemy = enemy.GetComponent<BaseEnemy>();
                        GetEnemy();
                        Distance = Enemy.DistanceTravelled;
                    }                       
                    else if (enemy.GetComponent<BaseEnemy>().isCamo == true && canSeeCamo == true)
                    {
                        Enemy = enemy.GetComponent<BaseEnemy>();
                        GetEnemy();
                        Distance = Enemy.DistanceTravelled;
                    }
                        
                }
            }
            if(TwoShot)
            {
                foreach (Collider2D enemy in Hit)
                {
                    if (Enemy != null && enemy.tag == "Enemy" && enemy.gameObject != Enemy.gameObject)
                    {
                        if (enemy.GetComponent<BaseEnemy>().isCamo == false)
                            Target2 = enemy;
                        else if (enemy.GetComponent<BaseEnemy>().isCamo == true && canSeeCamo == true)
                        {
                            Target2 = enemy;
                        }
                    }
                }
            }
            if (Enemy != null)
            {               
                Enemy.GetComponent<IDamagable>().TakeDamage(Damage, MoneyUpgrade);
                KillCount++;
                Player.UpdateKillCount();
                Enemy = null;
            }
            if (TwoShot && Target2 != null)
            {
                Target2.GetComponent<IDamagable>().TakeDamage(Damage, MoneyUpgrade);
                KillCount++;
                Player.UpdateKillCount();
                Target2 = null;
            }
        }
        yield return new WaitForSeconds(FireRate);
        if(ShootAnim != null)
        ShootAnim.SetActive(false);
        StartCoroutine(FindEnemies());
    }

    private void GetEnemy()
    {
        Vector3 relativePos = Enemy.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.back);
        TurretHead.transform.rotation = rotation;
        //UpdateLaser(Target.transform);
        if (ShootAnim != null)
            ShootAnim.SetActive(true);
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
                    T1TurretUpgradeCost += 480;                    
                    Damage++;
                    Player.Instance.TurretDamageText.text = "" + Damage;
                    Level1UpgradeRank++;
                }
                break;
            case 1:
                if (Player.Instance.money >= T1TurretUpgradeCost && Level2UpgradeRank <= 1)
                {
                    Player.Instance.money -= T1TurretUpgradeCost;
                    SellPrice += T1TurretUpgradeCost / 2;
                    T1TurretUpgradeCost += 700;                   
                    Damage++;
                    Player.Instance.TurretDamageText.text = "" + Damage;
                    Level1UpgradeRank++;
                }
                break;
            case 2:
                if (Player.Instance.money >= T1TurretUpgradeCost)
                {
                    Player.Instance.money -= T1TurretUpgradeCost;
                    SellPrice += T1TurretUpgradeCost / 2;
                    Damage++;
                    TwoShot = true;
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
                    T2TurretUpgradeCost += 400;                    
                    FireRate -= 0.2f;
                    Radius += 0.5f;
                    Level2UpgradeRank++;
                    SetPoints();
                }
                break;
            case 1:
                if (Player.Instance.money >= T2TurretUpgradeCost && Level1UpgradeRank <= 1)
                {
                    Player.Instance.money -= T2TurretUpgradeCost;
                    SellPrice += T2TurretUpgradeCost / 2;
                    T2TurretUpgradeCost += 600;                    
                    FireRate -= 0.2f;
                    Radius += 0.5f;
                    Level2UpgradeRank++;
                    SetPoints();
                }
                break;
            case 2:
                if (Player.Instance.money >= T2TurretUpgradeCost)
                {
                    Player.Instance.money -= T2TurretUpgradeCost;
                    SellPrice += T2TurretUpgradeCost / 2;
                    FireRate -= 0.35f;
                    Radius += 0.5f;
                    ShootAnim = null;
                    //Laser.enabled = true;
                    TurretHead = LaserRotate;
                    Destroy(Turret);
                    LaserTurret.SetActive(true);
                    Level2UpgradeRank++;
                    SetPoints();
                }
                break;
        }
        Player.Instance.UpdateUpgradeText();
    }
}
