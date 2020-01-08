using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : BaseTurret
{
    protected new void Start()
    {
        Player.Instance.Radars.Add(this);
        base.Start();
    }

    public override string Upgrade1Info()
    {
        if (Level2UpgradeRank > 1 && Level1UpgradeRank == 1 && !InUpgradePanel)
            return "Max upgrades reached.";
        switch (Level1UpgradeRank)
        {
            case 0:
                return "Can see camo enemies. ";
            case 1:
                return "Increases money from enemies. ";
            case 2:
                return "Increase turret damage. ";
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
                return "Decrease turret upgrade cost. ";
            case 1:
                return "Increases turret radius. ";
            case 2:
                return "Increase turret fire rate. ";
            default:
                return "Max upgrades reached.";
        }
    }
    protected override IEnumerator FindEnemies()
    {
        yield return new WaitForSeconds(0);
    }

    public void FindAllies(bool Sold)
    {
        Hit = Physics2D.OverlapCircleAll(transform.position, Radius + 0.1f); 
        foreach (Collider2D enemy in Hit)
        {
            if (enemy.tag == "Turret" && !enemy.GetComponent<Radar>())
            {
                BaseTurret AffectedTurret = enemy.GetComponent<BaseTurret>();
                if(!Sold)
                {
                    if (!AffectedTurret.RadarT1LvlUpgrade && Level1UpgradeRank >= 1)
                    {
                        AffectedTurret.RadarT1LvlUpgrade = true;
                    }
                    if (!AffectedTurret.RadarT2LvlUpgrade && Level1UpgradeRank >= 2)
                    {
                        AffectedTurret.RadarT2LvlUpgrade = true;
                    }
                    if (!AffectedTurret.RadarT3LvlUpgrade && Level1UpgradeRank >= 3)
                    {
                        AffectedTurret.RadarT3LvlUpgrade = true;
                    }
                    if (!AffectedTurret.RadarT4LvlUpgrade && Level2UpgradeRank >= 1)
                    {
                        AffectedTurret.RadarT4LvlUpgrade = true;
                    }
                    if (!AffectedTurret.RadarT5LvlUpgrade && Level2UpgradeRank >= 2)
                    {
                        AffectedTurret.RadarT5LvlUpgrade = true;
                    }
                    if (!AffectedTurret.RadarT6LvlUpgrade && Level2UpgradeRank >= 3)
                    {
                        AffectedTurret.RadarT6LvlUpgrade = true;
                    }
                }
                else
                {
                    if(AffectedTurret.RadarT1LvlUpgrade)
                    AffectedTurret.RadarT1LvlUpgrade = false;
                    if(AffectedTurret.RadarT1LvlUpgrade)
                    AffectedTurret.RadarT2LvlUpgrade = false;
                    if(AffectedTurret.RadarT3LvlUpgrade)
                    AffectedTurret.RadarT3LvlUpgrade = false;
                    if(AffectedTurret.RadarT4LvlUpgrade)
                    AffectedTurret.RadarT4LvlUpgrade = false;
                    if(AffectedTurret.RadarT5LvlUpgrade)
                    AffectedTurret.RadarT5LvlUpgrade = false;
                    if(AffectedTurret.RadarT6LvlUpgrade)
                    AffectedTurret.RadarT6LvlUpgrade = false;
                }                
            }
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
                    T1TurretUpgradeCost += 500;                    
                    Level1UpgradeRank++;
                }
                break;
            case 1:
                if (Player.Instance.money >= T1TurretUpgradeCost && Level2UpgradeRank <= 1)
                {
                    Player.Instance.money -= T1TurretUpgradeCost;
                    SellPrice += T1TurretUpgradeCost / 2;
                    T1TurretUpgradeCost += 1000;                    
                    Level1UpgradeRank++;
                }
                break;
            case 2:
                if (Player.Instance.money >= T1TurretUpgradeCost)
                {
                    Player.Instance.money -= T1TurretUpgradeCost;
                    SellPrice += T1TurretUpgradeCost / 2;
                    Level1UpgradeRank++;
                }
                break;
        }
        FindAllies(false);
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
                    T2TurretUpgradeCost += 500;                    
                    Level2UpgradeRank++;
                }
                break;
            case 1:
                if (Player.Instance.money >= T2TurretUpgradeCost && Level1UpgradeRank <= 1)
                {
                    Player.Instance.money -= T2TurretUpgradeCost;
                    SellPrice += T2TurretUpgradeCost / 2;
                    T2TurretUpgradeCost += 1000;                   
                    Level2UpgradeRank++;
                    SetPoints();
                }
                break;
            case 2:
                if (Player.Instance.money >= T2TurretUpgradeCost)
                {
                    Player.Instance.money -= T2TurretUpgradeCost;
                    SellPrice += T2TurretUpgradeCost / 2;
                    Level2UpgradeRank++;
                }
                break;
        }
        FindAllies(false);
        Player.Instance.UpdateUpgradeText();
    }
}
