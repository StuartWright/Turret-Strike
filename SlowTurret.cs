using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTurret : BaseTurret
{
    public float SlowTime;
    public float SlowAmount;
    public bool TakeDamage;
    private bool Reverse;
    public override string Upgrade1Info()
    {
        if (Level2UpgradeRank > 1 && Level1UpgradeRank == 1 && !InUpgradePanel)
            return "Max upgrades reached.";
        switch (Level1UpgradeRank)
        {
            case 0:
                return "Increases how long enemies are slow for. ";
            case 1:
                return "Increases how long enemies are slow for further. ";
            case 2:
                return "Enemies travel backwards. ";
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
                return "Can see camo enemies and slows enemies down more. ";
            case 1:
                return "Increases fire rate and radius. ";
            case 2:
                return "Enemies affected by turret take damage. ";
            default:
                return "Max upgrades reached.";
        }
    }
    protected override IEnumerator FindEnemies()
    {
        if (Placed)
        {
            Hit = Physics2D.OverlapCircleAll(transform.position, Radius + 0.1f);
            foreach (Collider2D enemy in Hit)
            {

                if (enemy.tag == "Enemy" && enemy.GetComponent<BaseEnemy>().isSlow == false)
                {
                    if(enemy.GetComponent<BaseEnemy>().isCamo == false)
                    {
                        SlowEnemies(enemy);
                    }
                    else if(enemy.GetComponent<BaseEnemy>().isCamo == true && canSeeCamo == true)
                    {
                        SlowEnemies(enemy);
                    }
                }
            }
        }
        yield return new WaitForSeconds(FireRate);
        if(ShootAnim != null)
        ShootAnim.SetActive(false);
        StartCoroutine(FindEnemies());
    }

    public void SlowEnemies(Collider2D enemy)
    {
        if (ShootAnim != null)
            ShootAnim.SetActive(true);
        Enemy = enemy.GetComponent<BaseEnemy>();
        if (TakeDamage)
        {
            Enemy.TakeDamage(Damage, MoneyUpgrade);
            KillCount++;
            Player.UpdateKillCount();
        }
        if (!Reverse)
            StartCoroutine(Enemy.SlowDown(SlowTime, SlowAmount));
        else
            StartCoroutine(Enemy.Reverse());

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
                    SlowTime += 1;
                    Level1UpgradeRank++;
                }
                break;
            case 1:
                if (Player.Instance.money >= T1TurretUpgradeCost && Level2UpgradeRank <= 1)
                {
                    Player.Instance.money -= T1TurretUpgradeCost;
                    SellPrice += T1TurretUpgradeCost / 2;
                    T1TurretUpgradeCost += 650;                  
                    SlowAmount = 2;
                    Level1UpgradeRank++;
                }
                break;
            case 2:
                if (Player.Instance.money >= T1TurretUpgradeCost)
                {
                    Player.Instance.money -= T1TurretUpgradeCost;
                    SellPrice += T1TurretUpgradeCost / 2;
                    Reverse = true;
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
                    canSeeCamo = true;
                    SlowAmount = 2;
                    Level2UpgradeRank++;
                }
                break;
            case 1:
                if (Player.Instance.money >= T2TurretUpgradeCost && Level1UpgradeRank <= 1)
                {
                    Player.Instance.money -= T2TurretUpgradeCost;
                    SellPrice += T2TurretUpgradeCost / 2;
                    T2TurretUpgradeCost += 700;                   
                    Radius += 0.5f;
                    FireRate -= 0.5f;
                    Level2UpgradeRank++;
                    SetPoints();
                }
                break;
            case 2:
                if (Player.Instance.money >= T2TurretUpgradeCost)
                {
                    Player.Instance.money -= T2TurretUpgradeCost;
                    SellPrice += T2TurretUpgradeCost / 2;
                    TakeDamage = true;
                    Damage = 1;
                    Player.Instance.TurretDamageText.text = "" + Damage;
                    Level2UpgradeRank++;
                }
                break;
        }
        Player.Instance.UpdateUpgradeText();
    }
}
