using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BaseTurret : MonoBehaviour
{
    public delegate void Upgrade(BaseTurret Sender);
    public event Upgrade UpgradeTurret;
    //public delegate void PlacedEvent();
    //public event PlacedEvent RadarPlaced;


    public string TurretName;
    public int Damage;
    public int T1TurretUpgradeCost, T2TurretUpgradeCost, Level1UpgradeRank, Level2UpgradeRank, KillCount;
    public int SellPrice;
    public int VertexCount;
    public float LineWidth = 0.2f;
    public float Radius;
    //public float UpgradeRadiusAmount;
    public float FireRate;
    public bool canSeeCamo, InUpgradePanel;
    public bool MoneyUpgrade;
    public Material Mat;
    public Collider2D[] Hit;
    public BaseEnemy Enemy;
    protected Player Player;    
    protected Vector3 MousePos;
    public bool Placed = false;
    public bool CanBePlaced;
    //public float RadiusAmount;
    public GameObject ShootAnim;
    public GameObject TurretHead;

    protected bool selected;
    public bool Selected
    {
        get { return selected; }
        set
        {
            selected = value;
            if (selected)
                Line.enabled = true;
            else if (!selected)
                Line.enabled = false;

        }
    }

    protected LineRenderer Line;

    private bool radarT1LvlUpgrade;
    public bool RadarT1LvlUpgrade
    {
        get { return radarT1LvlUpgrade; }
        set
        {
            radarT1LvlUpgrade = value;
            if (radarT1LvlUpgrade)
                canSeeCamo = true;            
            else
                canSeeCamo = false;                       
        }
    }

    private bool radarT2LvlUpgrade;
    public bool RadarT2LvlUpgrade
    {
        get { return radarT2LvlUpgrade; }
        set
        {
            radarT2LvlUpgrade = value;
            if (radarT2LvlUpgrade)
                MoneyUpgrade = true;
            else
                MoneyUpgrade = false;
        }
    }

    private bool radarT3LvlUpgrade;
    public bool RadarT3LvlUpgrade
    {
        get { return radarT3LvlUpgrade; }
        set
        {
            radarT3LvlUpgrade = value;
            if (radarT3LvlUpgrade)
                Damage++;
            else
                Damage--;
        }
    }

    private bool radarT4LvlUpgrade;
    public bool RadarT4LvlUpgrade
    {
        get { return radarT4LvlUpgrade; }
        set
        {
            radarT4LvlUpgrade = value;
            if (radarT4LvlUpgrade)
            {
                T1TurretUpgradeCost -= 50;
                T2TurretUpgradeCost -= 50;
            }
            else
            {
                T1TurretUpgradeCost += 50;
                T2TurretUpgradeCost += 50;
            }
            
        }
    }

    private bool radarT5LvlUpgrade;
    public bool RadarT5LvlUpgrade
    {
        get { return radarT5LvlUpgrade; }
        set
        {
            radarT5LvlUpgrade = value;
            if (radarT5LvlUpgrade)
                Radius += 0.7f;
            else
                Radius -= 0.7f;
            SetPoints();
        }
    }

    private bool radarT6LvlUpgrade;
    public bool RadarT6LvlUpgrade
    {
        get { return radarT6LvlUpgrade; }
        set
        {
            radarT6LvlUpgrade = value;
            if (radarT6LvlUpgrade)
                FireRate -= 0.2f;
            else
                FireRate += 0.2f;
        }
    }

    /*
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, Radius);
    }
    */
    protected void Start()
    {
        Line = gameObject.GetComponent<LineRenderer>();
        Player = Player.Instance;
        SetPoints();
        StartCoroutine(FindEnemies());
    }
    public void SetPoints()
    {
        Line.material = new Material(Mat);
        Line.material.color = new Color(0.2f, 0.2f, 0.2f, 0);
        Line.widthMultiplier = LineWidth;
        float DeltaTheta = (2f * Mathf.PI) / VertexCount;
        float Theta = 0f;
        Line.positionCount = VertexCount;
        for (int i = 0; i < Line.positionCount; i++)
        {
            Vector2 pos = new Vector2(Radius * Mathf.Cos(Theta), Radius * Mathf.Sin(Theta));
            Line.SetPosition(i, pos);            
            Theta += DeltaTheta;
        }
    }

    protected void OnMouseDown()
    {
        if(Player.CurrentTurret != null)
        Player.DeSelectTurret();
        Selected = true;
        Player.CurrentTurret = this;
    }
    
    protected void Update()
    {
        if (!Placed)
        {
            MousePos = Input.mousePosition;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);
            transform.position = new Vector2(MousePos.x, MousePos.y);
            if (Input.GetMouseButtonUp(0) && CanBePlaced)
            {
                Placed = true;
                Selected = true;
                Player.CurrentTurret = this;
                Player.ScanRadars(false);
                //RadarPlaced?.Invoke();
            }
                
        }
    }
    public virtual string Upgrade1Info() { return null; }
    public virtual string Upgrade2Info() { return null; }
    protected virtual IEnumerator FindEnemies()
    {
        return null;
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Path" || collision.tag == "Turret" && !Placed)
        {
            Line.material.color = new Color(1f, 0, 0, 0);
            CanBePlaced = false;
        }
        else if (collision.tag == "Untagged" && !Placed)
        {
            Line.material.color = new Color(0.2f, 0.2f, 0.2f, 0);
            CanBePlaced = true;
        }
        else if(Placed && collision.tag == "SellBack")
        {
            Destroy(gameObject);
            Player.DeSelectTurret();
            Player.money += (SellPrice *2);
        }
            
    }

    public virtual void UpgradeType1() { }
    public virtual void UpgradeType2() { }

}
public interface IUpgrades
{
    void UpgradeType1();
    void UpgradeType2();
}

