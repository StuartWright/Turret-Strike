using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradePointManager : MonoBehaviour
{
    public GameObject[] Turrets;
    private BaseTurret CurrentTurret;
    public Text[] T1Text;
    public Text[] T2Text;
    private void SetText()
    {
        for (int i = 0; i < 3; i++)
        {
            CurrentTurret.Level1UpgradeRank = i;
            T1Text[i].text = CurrentTurret.Upgrade1Info();
        }
        for (int i = 0; i < 3; i++)
        {
            CurrentTurret.Level2UpgradeRank = i;
            T2Text[i].text = CurrentTurret.Upgrade2Info();
        }
    }
    public void HideTurrets()
    {
        foreach(GameObject turret in Turrets)
        {
            turret.SetActive(false);
        }
    }
    public void BasicTurret()
    {
        HideTurrets();
        Turrets[0].SetActive(true);
        CurrentTurret = Turrets[0].GetComponent<BasicTurret>();
        SetText();
    }
    public void AutoTurret()
    {
        HideTurrets();
        Turrets[1].SetActive(true);
        CurrentTurret = Turrets[1].GetComponent<BaseTurret>();
        SetText();
    }
    public void RocketTurret()
    {
        HideTurrets();
        Turrets[2].SetActive(true);
        CurrentTurret = Turrets[2].GetComponent<BaseTurret>();
        SetText();
    }
    public void SlowTurret()
    {
        HideTurrets();
        Turrets[3].SetActive(true);
        CurrentTurret = Turrets[3].GetComponent<BaseTurret>();
        SetText();
    }
    public void RadarTurret()
    {
        HideTurrets();
        Turrets[4].SetActive(true);
        CurrentTurret = Turrets[4].GetComponent<BaseTurret>();
        SetText();
    }
    public void ElectricTurret()
    {
        HideTurrets();
        Turrets[5].SetActive(true);
        CurrentTurret = Turrets[5].GetComponent<BaseTurret>();
        SetText();
    }
    public void BigBerthaTurret()
    {
        HideTurrets();
        Turrets[6].SetActive(true);
        CurrentTurret = Turrets[6].GetComponent<BaseTurret>();
        SetText();
    }
    public void DroneTurret()
    {
        HideTurrets();
        Turrets[7].SetActive(true);
        CurrentTurret = Turrets[7].GetComponent<BaseTurret>();
        SetText();
    }
}
