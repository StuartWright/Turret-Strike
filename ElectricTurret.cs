using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTurret : BaseTurret
{
    public Transform LightningEnd;
    public Transform LightningStart;
    public Transform[] FirstPoints;
    public Transform[] LastPoints;
    public GameObject[] Anims;
    private bool HasTarget, MultiAttack;
    private int JumpAmount = 5;
    //private Collider2D Target;
    private Collider2D hit;
    private GameObject FirstEnemy;
    public RaycastHit2D rc;
    private int SwitchNum = -1;
    float I = -1;
    float J = 1;
    float radius = 0.8f;
    bool UpDown, LeftRight;
    
    public override string Upgrade1Info()
    {
        if (Level2UpgradeRank > 1 && Level1UpgradeRank == 1)
            return "Max upgrades reached.";
        switch (Level1UpgradeRank)
        {
            case 0:
                return "Increased Damage. ";
            case 1:
                return "Can see Camo enemies. ";
            case 2:
                return "Electric hits many enemies. ";
            default:
                return "Max upgrades reached.";
        }
    }
    public override string Upgrade2Info()
    {
        if (Level1UpgradeRank > 1 && Level2UpgradeRank == 1)
            return "Max upgrades reached.";
        switch (Level2UpgradeRank)
        {
            case 0:
                return "Increases Radius. ";
            case 1:
                return "Increases attack speed. ";
            case 2:
                return "Increases damage. ";
            default:
                return "Max upgrades reached.";
        }
    }
    protected override IEnumerator FindEnemies()
    {
        if (Placed)
        {
            Hit = Physics2D.OverlapCircleAll(transform.position, Radius);
            //Target = null;
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
                        TurretHead.transform.localEulerAngles = new Vector3(0, TurretHead.transform.localEulerAngles.y + 80, 0);
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
                Hit = Physics2D.OverlapCircleAll(Enemy.transform.position, radius);
                foreach (Collider2D enemy in Hit)
                {
                    if (enemy.tag == "Enemy")
                    {
                        enemy.GetComponent<IDamagable>().TakeDamage(Damage, MoneyUpgrade);
                        KillCount++;
                        Player.UpdateKillCount();
                        if (MultiAttack)
                        {
                            MultiAttackFunc();
                        }
                    }

                }               
            }
        }
        yield return new WaitForSeconds(FireRate);
        Enemy = null;
        if (ShootAnim != null)
            ShootAnim.SetActive(false);
        foreach(GameObject anim in Anims)
        {
            anim.SetActive(false);
        }
        StartCoroutine(FindEnemies());
    }

    private void GetEnemy()
    {
        TurretHead.transform.LookAt(Enemy.transform);
        TurretHead.transform.localEulerAngles = new Vector3(0, TurretHead.transform.localEulerAngles.y + 80, 0);

        if (ShootAnim != null)
            ShootAnim.SetActive(true);
        LightningEnd.position = new Vector2(Enemy.transform.position.x, Enemy.transform.position.y);
    }
    private void MultiAttackFunc()
    {
        int t = 0;
        
        for (int i = 0; i < JumpAmount; i++)
        {
            Anims[t].SetActive(true);
            FirstPoints[t].position = new Vector2(Enemy.transform.position.x, Enemy.transform.position.y);
            for (int j = 0; j < 44; j++)
            {
                rc = Physics2D.Raycast(Enemy.transform.position, RayDirection(), 1.5f);
                //Debug.DrawRay(Enemy.transform.position, RayDirection(), Color.red, 5);
                if (rc.collider != null && rc.collider.tag == "Enemy" && rc.collider.gameObject != Enemy.gameObject && rc.collider.GetComponent<BaseEnemy>().DistanceTravelled < Enemy.DistanceTravelled)
                {
                    Enemy = rc.collider.GetComponent<BaseEnemy>();
                    LastPoints[t].position = new Vector2(Enemy.transform.position.x, Enemy.transform.position.y);
                    Enemy.TakeDamage(Damage, MoneyUpgrade);
                    KillCount++;
                    Player.UpdateKillCount();
                    //SwitchNum = -1;
                    t++;
                    break;
                }
            }
            I = -1;
            J = 1;
            UpDown = false;
            LeftRight = false;
            
        }
    }
    private Vector2 RayDirection()//Probably a better way but its my game p**s off
    {
        if(!UpDown)
        {
            if (I < 1)
            {
                I += 0.2f;
                if(I >= 1 && J == -1)
                {
                    UpDown = true;
                    I = 1;
                    J = -1;
                }                    
            }
            else
            {
                J = -1;
                I = -1;               
            }
        }
        else if(!LeftRight)
        {
            if (J < 1)
            {
                J += 0.2f;
            }
            else
            {
                J = -1;
                I = -1;
            }
        }

        //Debug.DrawRay(Enemy.transform.position, new Vector2(I, J), Color.red, 4);
        return new Vector2(I, J);
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
                    T1TurretUpgradeCost += 1000;                  
                    canSeeCamo = true;
                    Level1UpgradeRank++;
                }
                break;
            case 2:
                if (Player.Instance.money >= T1TurretUpgradeCost)
                {
                    Player.Instance.money -= T1TurretUpgradeCost;
                    SellPrice += T1TurretUpgradeCost / 2;
                    radius = 0;
                    MultiAttack = true;
                    FireRate += 0.5f;
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
                    T2TurretUpgradeCost += 500;                    
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
                    T2TurretUpgradeCost += 800;                  
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
                    Damage++;
                    Player.Instance.TurretDamageText.text = "" + Damage;
                    Level2UpgradeRank++;
                    SetPoints();
                }
                break;
        }
        Player.Instance.UpdateUpgradeText();
    } 
}
