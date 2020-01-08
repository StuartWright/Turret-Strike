using UnityEngine;
using UnityEngine.EventSystems;
public class BuyTurret : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum Turrets
    {
        BasicTurret,
        AutoTurret,
        rocketTurret,
        SlowTurret,
        Radar,
        ElectricTurret,
        BigBertha
    }

    float Timer = 0.1f;
    private bool CanSpawnTurret;
    public Turrets Type;
    public GameObject Turret;
    private Player player;
    private void Start()
    {
        player = Camera.main.GetComponent<Player>();
    }
    private void Update()
    {
        if(CanSpawnTurret)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                CanSpawnTurret = false;
                switch (Type)
                {
                    case Turrets.BasicTurret:
                        player.BuyTurret();
                        break;
                    case Turrets.AutoTurret:
                        player.BuyAutomaticTurret();
                        break;
                    case Turrets.rocketTurret:
                        player.BuyRocketTurret();
                        break;
                    case Turrets.SlowTurret:
                        player.BuySlowTurret();
                        break;
                    case Turrets.Radar:
                        player.BuyRadarTurret();
                        break;
                    case Turrets.ElectricTurret:
                        player.BuyElectricTurret();
                        break;
                    case Turrets.BigBertha:
                        player.BuyBigBertha();
                        break;
                }

            }
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CanSpawnTurret = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CanSpawnTurret = false;
        Timer = 0.1f;
    }
}
