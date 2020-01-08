using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticTurret : BasicTurret
{
    public GameObject TurretHead1, TurretHead2;
    public GameObject TurretHead1Anim, TurretHead2Anim;
    public GameObject SingleTurretHead, DualTurretHead;
    private bool DualHeads;

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
                return "Deals heavy damage to enemies. ";
            default:
                return "Max upgrades reached.";


        }
    }
    public override string Upgrade2Info()
    {
        if (Level1UpgradeRank > 1 && Level2UpgradeRank == 1 && !InUpgradePanel)
            return "Max upgrades reached";
        switch (Level2UpgradeRank)
        {
            case 0:
                return "Increases attack speed. ";
            case 1:
                return "Increases Radius. ";
            case 2:
                return "Gives the turret another head. ";
            default:
                return "Max upgrades reached.";
        }
    }
    protected override IEnumerator FindEnemies()
    {
        if (Placed)
        {
            Hit = Physics2D.OverlapCircleAll(transform.position, Radius / 1.9f);
            float Distance = 0;
            foreach (Collider2D enemy in Hit)
            {
                if (enemy.tag == "Enemy" && Distance <= enemy.GetComponent<BaseEnemy>().DistanceTravelled)
                {                   
                    if (enemy.GetComponent<BaseEnemy>().isCamo == false)
                    {
                        Enemy = enemy.GetComponent<BaseEnemy>();
                        Distance = Enemy.DistanceTravelled;
                        GetEnemy();                       
                    }
                    else if (enemy.GetComponent<BaseEnemy>().isCamo == true && canSeeCamo == true)
                    {
                        Enemy = enemy.GetComponent<BaseEnemy>();
                        Distance = Enemy.DistanceTravelled;
                        GetEnemy();
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

        }
        yield return new WaitForSeconds(FireRate);
        if (ShootAnim != null)
            ShootAnim.SetActive(false);
        if(DualHeads)
        {
            TurretHead1Anim.SetActive(false);
            TurretHead2Anim.SetActive(false);
        }
        StartCoroutine(FindEnemies());
    }

    private void GetEnemy()
    {
        Vector3 relativePos = Enemy.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.back);
        if (!DualHeads)
            TurretHead.transform.rotation = rotation;
        else
            DualTurretHead.transform.rotation = rotation;
        if (ShootAnim != null)
            ShootAnim.SetActive(true);
        if (DualHeads)
        {
            TurretHead1Anim.SetActive(true);
            TurretHead2Anim.SetActive(true);
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
                    T1TurretUpgradeCost += 400;                   
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
                    T1TurretUpgradeCost += 500;                  
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
                    Damage += 1;
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
                    T2TurretUpgradeCost += 300;                    
                    FireRate -= 0.2f;
                    Level2UpgradeRank++;
                }
                break;
            case 1:
                if (Player.Instance.money >= T2TurretUpgradeCost && Level1UpgradeRank <= 1)
                {
                    Player.Instance.money -= T2TurretUpgradeCost;
                    SellPrice += T2TurretUpgradeCost / 2;
                    T2TurretUpgradeCost += 500;                  
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
                    FireRate -= 0.2f;
                    Destroy(SingleTurretHead);
                    DualTurretHead.SetActive(true);
                    DualHeads = true;
                    Level2UpgradeRank++;
                }
                break;
        }
        Player.Instance.UpdateUpgradeText();
    }
}
