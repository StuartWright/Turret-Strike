using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public LineRenderer Line;
    public BasicTurret Turret;
    public Transform FirePoint;
    void Start()
    {
        //Turret.UpdateLaser += UpdateLaser;
    }

    void Update()
    {

            
    }

    public void UpdateLaser(Transform Sender)
    {
        
        //Line.positionCount = 1;
        //Line.SetPosition(0, transform.localPosition);
         
        Line.SetPosition(0, FirePoint.localPosition);
        Vector2 T = new Vector2(Sender.transform.localPosition.x, Sender.transform.localPosition.y);
        print(T);
        Line.SetPosition(1, T);
    }
}
