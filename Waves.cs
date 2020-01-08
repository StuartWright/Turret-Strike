using UnityEngine;

[System.Serializable]
public class Waves
{
    public GameObject Enemy;
    //public int EnemyLevel;
    public int Lvl1EnemyAmount;
    public float Lvl1SpawnRate, Lvl1SpawnDelay;
    public int Lvl1CamoFrequency, Lvl1CamoAmount;
    public int Lvl2EnemyAmount;
    public float Lvl2SpawnRate, Lvl2SpawnDelay;
    public int Lvl2CamoFrequency, Lvl2CamoAmount;
    public int Lvl3EnemyAmount;
    public float Lvl3SpawnRate, Lvl3SpawnDelay;
    public int Lvl3CamoFrequency, Lvl3CamoAmount;
    public int Lvl4EnemyAmount;
    public float Lvl4SpawnRate, Lvl4SpawnDelay;
    public int Lvl4CamoFrequency, Lvl4CamoAmount;
    public int Lvl5EnemyAmount;
    public float Lvl5SpawnRate, Lvl5SpawnDelay;
    public int Lvl5CamoFrequency, Lvl5CamoAmount;
    public int Lvl6EnemyAmount;
    public float Lvl6SpawnRate, Lvl6SpawnDelay;
    public int Lvl6CamoFrequency, Lvl6CamoAmount;
    public int Lvl7EnemyAmount;
    public float Lvl7SpawnRate, Lvl7SpawnDelay;
    public int Lvl7CamoFrequency, Lvl7CamoAmount;
    public int Lvl8EnemyAmount;
    public float Lvl8SpawnRate, Lvl8SpawnDelay;
    public int Lvl8CamoFrequency, Lvl8CamoAmount;
    private int TotalEnemyAmount;

    private int Lvl1ToSpawn = 5;
    private float Lvl1MinSpawnRate = 0.6f, Lvl1MaxSpawnRate = 1.5f, Lvl1SpawnDel = 1;

    private int Lvl2ToSpawn = 5;
    private float Lvl2MinSpawnRate = 0.6f, Lvl2MaxSpawnRate = 2, Lvl2SpawnDel = 6;
    private bool CanSpawnLvl2;

    private int Lvl3ToSpawn = 10;
    private float Lvl3MinSpawnRate = 0.6f, Lvl3MaxSpawnRate = 2, Lvl3SpawnDel = 6;
    private bool CanSpawnLvl3;

    private int Lvl4ToSpawn = 10;
    private float Lvl4MinSpawnRate = 0.6f, Lvl4MaxSpawnRate = 2, Lvl4SpawnDel = 6;
    private bool CanSpawnLvl4;

    private int Lvl5ToSpawn = 3;
    private float Lvl5MinSpawnRate = 0.6f, Lvl5MaxSpawnRate = 2, Lvl5SpawnDel = 6;
    private bool CanSpawnLvl5;

    private int Lvl6ToSpawn = 2;
    private float Lvl6MinSpawnRate = 0.6f, Lvl6MaxSpawnRate = 2, Lvl6SpawnDel = 6;
    private bool CanSpawnLvl6;

    private int Lvl7ToSpawn = 2;
    private float Lvl7MinSpawnRate = 0.6f, Lvl7MaxSpawnRate = 2, Lvl7SpawnDel = 6;
    private bool CanSpawnLvl7;

    private int Lvl8ToSpawn = 1;
    private float Lvl8MinSpawnRate = 0.6f, Lvl8MaxSpawnRate = 2, Lvl8SpawnDel = 6;
    private bool CanSpawnLvl8;
    public int TotalEnemies()
    {
        TotalEnemyAmount = Lvl1EnemyAmount + Lvl2EnemyAmount + Lvl3EnemyAmount + Lvl4EnemyAmount + Lvl5EnemyAmount + Lvl6EnemyAmount + Lvl7EnemyAmount + Lvl8EnemyAmount;
        return TotalEnemyAmount;
    }

    public void waveProgression(int Wave)
    {
        if(Wave % 5 == 0)
        {
            if (!CanSpawnLvl2)
                CanSpawnLvl2 = true;
            else if(!CanSpawnLvl3)
                CanSpawnLvl3 = true;
            else if(!CanSpawnLvl4)
                CanSpawnLvl4 = true;
            else if(!CanSpawnLvl5)
                CanSpawnLvl5 = true;
            else if (!CanSpawnLvl6)
                CanSpawnLvl6 = true;
            else if (!CanSpawnLvl7)
                CanSpawnLvl7 = true;
            else if (!CanSpawnLvl8)
                CanSpawnLvl8 = true;

            Lvl1MaxSpawnRate += 1;
            //Lvl1MinSpawnRate += 0.5f;
            Lvl1EnemyAmount = Random.Range(0, Lvl1EnemyAmount);
            Lvl1SpawnDel -= 4;
            if (CanSpawnLvl2)
            {
                Lvl2MaxSpawnRate += 0.5f;
                //Lvl2MinSpawnRate += 0.5f;
                Lvl2EnemyAmount = Random.Range(0, Lvl2EnemyAmount);
                Lvl2SpawnDel -= 4;
            }                
            if (CanSpawnLvl3)
            {                
                Lvl3MaxSpawnRate += 0.5f;
                //Lvl3MinSpawnRate += 0.5f;
                Lvl3EnemyAmount = Random.Range(0, Lvl3EnemyAmount);
                Lvl3SpawnDel -= 4;
            }
            if (CanSpawnLvl4)
            {
                Lvl4MaxSpawnRate += 0.5f;
                //Lvl4MinSpawnRate += 0.5f;
                Lvl4EnemyAmount = Random.Range(0, Lvl4EnemyAmount);
                Lvl4SpawnDel -= 4;
            }
            if (CanSpawnLvl5)
            {
                Lvl5MaxSpawnRate += 0.5f;
                //Lvl5MinSpawnRate += 0.5f;
                Lvl5EnemyAmount = Random.Range(0, Lvl5EnemyAmount);
                Lvl5SpawnDel -= 4;
            }
            if (CanSpawnLvl6)
            {
                Lvl6MaxSpawnRate += 0.5f;
                //Lvl6MinSpawnRate += 0.5f;
                Lvl6EnemyAmount = Random.Range(0, Lvl6EnemyAmount);
                Lvl6SpawnDel -= 4;
            }
            if (CanSpawnLvl7)
            {
                Lvl7MaxSpawnRate += 0.5f;
                //Lvl7MinSpawnRate += 0.5f;
                Lvl7EnemyAmount = Random.Range(0, Lvl7EnemyAmount);
                Lvl7SpawnDel -= 4;
            }
            if (CanSpawnLvl8)
            {
                Lvl8MaxSpawnRate += 0.5f;
                //Lvl8MinSpawnRate += 0.5f;
                Lvl8EnemyAmount = Random.Range(0, Lvl8EnemyAmount);
                Lvl8SpawnDel -= 4;
            }
        }

        Lvl1EnemyAmount += Random.Range(2, Lvl1ToSpawn);
        Lvl1SpawnRate = Random.Range(Lvl1MinSpawnRate, Lvl1MaxSpawnRate);
        Lvl1SpawnDelay = Random.Range(0, Lvl1SpawnDel);
        Lvl1MaxSpawnRate -= 0.2f;
        //Lvl1MinSpawnRate -= 0.1f;
        Lvl1SpawnDel += 1;

        if (CanSpawnLvl2)
        {
            Lvl2EnemyAmount += Random.Range(2, Lvl2ToSpawn);
            Lvl2SpawnRate = Random.Range(Lvl2MinSpawnRate, Lvl2MaxSpawnRate);
            Lvl2SpawnDelay = Random.Range(0, Lvl2SpawnDel);
            Lvl2MaxSpawnRate -= 0.1f;
            //Lvl2MinSpawnRate -= 0.1f;
            Lvl2SpawnDel += 1;
        }

        if (CanSpawnLvl3)
        {
            Lvl3EnemyAmount += Random.Range(2, Lvl3ToSpawn);
            Lvl3SpawnRate = Random.Range(Lvl3MinSpawnRate, Lvl3MaxSpawnRate);
            Lvl3SpawnDelay = Random.Range(0, Lvl3SpawnDel);
            Lvl3MaxSpawnRate -= 0.1f;
            //Lvl3MinSpawnRate -= 0.1f;
            Lvl3SpawnDel += 1;
        }

        if (CanSpawnLvl4)
        {
            Lvl4EnemyAmount += Random.Range(2, Lvl4ToSpawn);
            Lvl4SpawnRate = Random.Range(Lvl4MinSpawnRate, Lvl4MaxSpawnRate);
            Lvl4SpawnDelay = Random.Range(0, Lvl4SpawnDel);
            Lvl4MaxSpawnRate -= 0.1f;
            //Lvl4MinSpawnRate -= 0.1f;
            Lvl4SpawnDel += 1;
        }

        if (CanSpawnLvl5)
        {
            Lvl5EnemyAmount += Random.Range(2, Lvl5ToSpawn);
            Lvl5SpawnRate = Random.Range(Lvl5MinSpawnRate, Lvl5MaxSpawnRate);
            Lvl5SpawnDelay = Random.Range(0, Lvl5SpawnDel);
            Lvl5MaxSpawnRate -= 0.1f;
            //Lvl5MinSpawnRate -= 0.1f;
            Lvl5SpawnDel += 1;
        }

        if (CanSpawnLvl6)
        {
            Lvl6EnemyAmount += Random.Range(2, Lvl6ToSpawn);
            Lvl6SpawnRate = Random.Range(Lvl6MinSpawnRate, Lvl6MaxSpawnRate);
            Lvl6SpawnDelay = Random.Range(0, Lvl6SpawnDel);
            Lvl6MaxSpawnRate -= 0.1f;
            //Lvl6MinSpawnRate -= 0.1f;
            Lvl6SpawnDel += 1;
        }

        if (CanSpawnLvl7)
        {
            Lvl7EnemyAmount += Random.Range(2, Lvl7ToSpawn);
            Lvl7SpawnRate = Random.Range(Lvl7MinSpawnRate, Lvl7MaxSpawnRate);
            Lvl7SpawnDelay = Random.Range(0, Lvl7SpawnDel);
            Lvl7MaxSpawnRate -= 0.1f;
            //Lvl7MinSpawnRate -= 0.1f;
            Lvl7SpawnDel += 1;
        }

        if (CanSpawnLvl8)
        {
            Lvl8EnemyAmount += Random.Range(2, Lvl8ToSpawn);
            Lvl8SpawnRate = Random.Range(Lvl8MinSpawnRate, Lvl8MaxSpawnRate);
            Lvl8SpawnDelay = Random.Range(0, Lvl8SpawnDel);
            Lvl8MaxSpawnRate -= 0.1f;
            //Lvl8MinSpawnRate -= 0.1f;
            Lvl8SpawnDel += 1;
        }
    }
}
