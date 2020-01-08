using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBertha : BasicTurret
{
    private bool StopEnemies;
    public override string Upgrade1Info()
    {
        if (Level2UpgradeRank > 1 && Level1UpgradeRank == 1 && !InUpgradePanel)
            return "Max upgrades reached.";
        switch (Level1UpgradeRank)
        {
            case 0:
                return "Can see camo enemies. ";
            case 1:
                return "Increases Damage. ";
            case 2:
                return "Increase damage further. ";
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
                return "Increases attack speed. ";
            case 1:
                return "Stops enemies for a short period. ";
            case 2:
                return "Increase Attack speed further. ";
            default:
                return "Max upgrades reached.";
        }
    }
    protected override IEnumerator FindEnemies()
    {
        if (Placed)
        {
            Hit = Physics2D.OverlapCircleAll(transform.position, 15);
            float Distance = 0;
            foreach (Collider2D enemy in Hit)
            {
                if (enemy.tag == "Enemy" && Distance <= enemy.GetComponent<BaseEnemy>().DistanceTravelled)
                {
                    if (enemy.GetComponent<BaseEnemy>().isCamo == false)
                    {
                        Enemy = enemy.GetComponent<BaseEnemy>();
                        Distance = Enemy.DistanceTravelled;
                        TurretHead.transform.LookAt(Enemy.transform);
                        TurretHead.transform.localEulerAngles = new Vector3(0, TurretHead.transform.localEulerAngles.y + 90, 0);
                        if (ShootAnim != null)
                            ShootAnim.SetActive(true);
                    }
                    else if (enemy.GetComponent<BaseEnemy>().isCamo == true && canSeeCamo == true)
                    {
                        Enemy = enemy.GetComponent<BaseEnemy>();
                        Distance = Enemy.DistanceTravelled;
                        TurretHead.transform.LookAt(Enemy.transform);
                        TurretHead.transform.localEulerAngles = new Vector3(0, TurretHead.transform.localEulerAngles.y + 90, 0);
                        if (ShootAnim != null)
                            ShootAnim.SetActive(true);
                    }
                }
               
            }
            if (Enemy != null)
            {
                Enemy.GetComponent<IDamagable>().TakeDamage(Damage, MoneyUpgrade);
                KillCount++;
                Player.UpdateKillCount();
                if (StopEnemies)
                    StartCoroutine(Enemy.Stop());
                Enemy = null;
            }
        }
        yield return new WaitForSeconds(FireRate);
        if (ShootAnim != null)
            ShootAnim.SetActive(false);
        StartCoroutine(FindEnemies());
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
                    T1TurretUpgradeCost += 400;                    
                    canSeeCamo = true;
                    Player.Instance.TurretDamageText.text = "" + Damage;
                    Level1UpgradeRank++;
                }
                break;
            case 1:
                if (Player.Instance.money >= T1TurretUpgradeCost && Level2UpgradeRank <= 1)
                {
                    Player.Instance.money -= T1TurretUpgradeCost;
                    SellPrice += T1TurretUpgradeCost / 2;
                    T1TurretUpgradeCost += 400;                
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
                    FireRate -= 0.3f;
                    Level2UpgradeRank++;
                    SetPoints();
                }
                break;
            case 1:
                if (Player.Instance.money >= T2TurretUpgradeCost && Level1UpgradeRank <= 1)
                {
                    Player.Instance.money -= T2TurretUpgradeCost;
                    SellPrice += T2TurretUpgradeCost / 2;
                    T2TurretUpgradeCost += 500;                  
                    StopEnemies = true;
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
