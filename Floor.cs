using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    Player player;
    private void Start()
    {
        player = Camera.main.GetComponent<Player>();
    }
    private void OnMouseDown()
    {
        if (player.CurrentTurret != null)
        {
            player.DeSelectTurret();
        }
    }
}
