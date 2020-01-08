using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveManager : MonoBehaviour
{
    public delegate void WaveManagement();
    //public event WaveManagement BeginWave;
    private Player player;
    public static WaveManager Instance;
    public Button WaveButton;
    public GameObject WaveBox;
    public Text WaveText, RemainingEnemiesText;
    public int RemainingEnemies;
    public int WaveIndex = 0, CurrentWave = 1;
    public bool WaveStarted, AutoWaves;
    private bool Lvl1Spawned, Lvl2Spawned, Lvl3Spawned, Lvl4Spawned, Lvl5Spawned, Lvl6Spawned, Lvl7Spawned, Lvl8Spawned;
    private int[] camoSpawnedAmount = new int[8];
    public Waves[] waves;
    private Waves wave;
    private ObjectPooler Pool;
    public void UpdateREnemies()
    {
        RemainingEnemiesText.text = "Remaining Enemies: " + RemainingEnemies;
    }
    void Start()
    {       
        Instance = this;
        player = Camera.main.GetComponent<Player>();
        player.WaveWon += EnableWaveButton;
        WaveText = GameObject.Find("WaveText").GetComponent<Text>();
        WaveText.text = "Start Wave " + (CurrentWave);
        RemainingEnemiesText = GameObject.Find("EnemiesRemainingText").GetComponent<Text>();
        GameObject.Find("Normal").GetComponent<Button>().onClick.AddListener(() => NormalWaves());
        GameObject.Find("Random").GetComponent<Button>().onClick.AddListener(() => RandomWaves());
        WaveBox = player.WaveBox;
        WaveButton = GameObject.Find("StartWaveButton").GetComponent<Button>();
        WaveButton.onClick.AddListener(() => StartWave());
        WaveButton.interactable = true;
        WaveButton.GetComponent<Image>().sprite = player.GameSpeed1;
        player.SpeedUp = true;
        player.GameSpeed();
        player.money = 500;//quick fix
        player.lives = 100;//quick fix
        RemainingEnemiesText.text = "Remaining Enemies: 0";
        player.InitAds();
        Pool = ObjectPooler.Instance;
    }

    private void Update()
    {
        if(WaveStarted)
        {
            if(!Lvl1Spawned)
            {
                waves[WaveIndex].Lvl1SpawnDelay -= Time.deltaTime;
                if (waves[WaveIndex].Lvl1SpawnDelay <= 0)
                {
                    StartCoroutine(Spawn());
                    Lvl1Spawned = true;
                }                   
            }
            if (!Lvl2Spawned)
            {
                waves[WaveIndex].Lvl2SpawnDelay -= Time.deltaTime;
                if (waves[WaveIndex].Lvl2SpawnDelay <= 0)
                {
                    StartCoroutine(SpawnLvl2());
                    Lvl2Spawned = true;
                }
            }
            if (!Lvl3Spawned)
            {
                waves[WaveIndex].Lvl3SpawnDelay -= Time.deltaTime;
                if (waves[WaveIndex].Lvl3SpawnDelay <= 0)
                {
                    StartCoroutine(SpawnLvl3());
                    Lvl3Spawned = true;
                }
            }
            if (!Lvl4Spawned)
            {
                waves[WaveIndex].Lvl4SpawnDelay -= Time.deltaTime;
                if (waves[WaveIndex].Lvl4SpawnDelay <= 0)
                {
                    StartCoroutine(SpawnLvl4());
                    Lvl4Spawned = true;
                }
            }
            if (!Lvl5Spawned)
            {
                waves[WaveIndex].Lvl5SpawnDelay -= Time.deltaTime;
                if (waves[WaveIndex].Lvl5SpawnDelay <= 0)
                {
                    StartCoroutine(SpawnLvl5());
                    Lvl5Spawned = true;
                }
            }
            if (!Lvl6Spawned)
            {
                waves[WaveIndex].Lvl6SpawnDelay -= Time.deltaTime;
                if (waves[WaveIndex].Lvl6SpawnDelay <= 0)
                {
                    StartCoroutine(SpawnLvl6());
                    Lvl6Spawned = true;
                }
            }
            if (!Lvl7Spawned)
            {
                waves[WaveIndex].Lvl7SpawnDelay -= Time.deltaTime;
                if (waves[WaveIndex].Lvl7SpawnDelay <= 0)
                {
                    StartCoroutine(SpawnLvl7());
                    Lvl7Spawned = true;
                }
            }
            if (!Lvl8Spawned)
            {
                waves[WaveIndex].Lvl8SpawnDelay -= Time.deltaTime;
                if (waves[WaveIndex].Lvl8SpawnDelay <= 0)
                {
                    StartCoroutine(SpawnLvl8());
                    Lvl8Spawned = true;
                }
            }
        }        
    }
    IEnumerator Spawn()
    {
        //Waves wave = waves[WaveIndex];
        for(int i = 0; i < wave.Lvl1EnemyAmount; i++)
        {
            //GameObject enemy = Instantiate(wave.Enemy);
            GameObject enemy = Pool.SpawnFromPool("Enemy");
            if (wave.Lvl1CamoAmount > 0 && i %  wave.Lvl1CamoFrequency == 0 && camoSpawnedAmount[0] <= wave.Lvl1CamoAmount - 1)
            {
                enemy.GetComponent<BaseEnemy>().isCamo = true;
                camoSpawnedAmount[0]++;
            }
            enemy.GetComponent<BaseEnemy>().enemyLevel = 1;
            yield return new WaitForSeconds(wave.Lvl1SpawnRate);
        } 
    }
    IEnumerator SpawnLvl2()
    {
        //Waves wave = waves[WaveIndex];
        for (int i = 0; i < wave.Lvl2EnemyAmount; i++)
        {
            GameObject enemy = Pool.SpawnFromPool("Enemy");
            if (wave.Lvl2CamoAmount > 0 && i % wave.Lvl2CamoFrequency == 0 && camoSpawnedAmount[1] <= wave.Lvl2CamoAmount - 1)
            {
                enemy.GetComponent<BaseEnemy>().isCamo = true;
                camoSpawnedAmount[1]++;
            }
            enemy.GetComponent<BaseEnemy>().enemyLevel = 2;
            yield return new WaitForSeconds(wave.Lvl2SpawnRate);
        }
    }
    IEnumerator SpawnLvl3()
    {
        //Waves wave = waves[WaveIndex];
        for (int i = 0; i < wave.Lvl3EnemyAmount; i++)
        {
            GameObject enemy = Pool.SpawnFromPool("Enemy");
            if (wave.Lvl3CamoAmount > 0 && i % wave.Lvl3CamoFrequency == 0 && camoSpawnedAmount[2] <= wave.Lvl3CamoAmount - 1)
            {
                enemy.GetComponent<BaseEnemy>().isCamo = true;
                camoSpawnedAmount[2]++;
            }
            enemy.GetComponent<BaseEnemy>().enemyLevel = 3;
            yield return new WaitForSeconds(wave.Lvl3SpawnRate);
        }
    }
    IEnumerator SpawnLvl4()
    {
        //Waves wave = waves[WaveIndex];
        for (int i = 0; i < wave.Lvl4EnemyAmount; i++)
        {
            GameObject enemy = Pool.SpawnFromPool("Enemy");
            if (wave.Lvl4CamoAmount > 0 && i % wave.Lvl4CamoFrequency == 0 && camoSpawnedAmount[3] <= wave.Lvl4CamoAmount - 1)
            {
                enemy.GetComponent<BaseEnemy>().isCamo = true;
                camoSpawnedAmount[3]++;
            }
            enemy.GetComponent<BaseEnemy>().enemyLevel = 4;
            yield return new WaitForSeconds(wave.Lvl4SpawnRate);
        }
    }
    IEnumerator SpawnLvl5()
    {
        //Waves wave = waves[WaveIndex];
        for (int i = 0; i < wave.Lvl5EnemyAmount; i++)
        {
            GameObject enemy = Pool.SpawnFromPool("Enemy");
            if (wave.Lvl5CamoAmount > 0 && i % wave.Lvl5CamoFrequency == 0 && camoSpawnedAmount[4] <= wave.Lvl5CamoAmount - 1)
            {
                enemy.GetComponent<BaseEnemy>().isCamo = true;
                camoSpawnedAmount[4]++;
            }
            enemy.GetComponent<BaseEnemy>().enemyLevel = 5;
            yield return new WaitForSeconds(wave.Lvl5SpawnRate);
        }
    }
    IEnumerator SpawnLvl6()
    {
        //Waves wave = waves[WaveIndex];
        for (int i = 0; i < wave.Lvl6EnemyAmount; i++)
        {
            GameObject enemy = Pool.SpawnFromPool("Enemy");
            if (wave.Lvl6CamoAmount > 0 && i % wave.Lvl6CamoFrequency == 0 && camoSpawnedAmount[5] <= wave.Lvl6CamoAmount - 1)
            {
                enemy.GetComponent<BaseEnemy>().isCamo = true;
                camoSpawnedAmount[5]++;
            }
            enemy.GetComponent<BaseEnemy>().enemyLevel = 6;
            yield return new WaitForSeconds(wave.Lvl6SpawnRate);
        }
    }
    IEnumerator SpawnLvl7()
    {
        //Waves wave = waves[WaveIndex];
        for (int i = 0; i < wave.Lvl7EnemyAmount; i++)
        {
            GameObject enemy = Pool.SpawnFromPool("Enemy");
            if (wave.Lvl7CamoAmount > 0 && i % wave.Lvl7CamoFrequency == 0 && camoSpawnedAmount[6] <= wave.Lvl7CamoAmount - 1)
            {
                enemy.GetComponent<BaseEnemy>().isCamo = true;
                camoSpawnedAmount[6]++;
            }
            enemy.GetComponent<BaseEnemy>().enemyLevel = 7;
            yield return new WaitForSeconds(wave.Lvl7SpawnRate);
        }
    }
    IEnumerator SpawnLvl8()
    {
        Waves wave = waves[WaveIndex];
        for (int i = 0; i < wave.Lvl8EnemyAmount; i++)
        {
            GameObject enemy = Pool.SpawnFromPool("Enemy");
            if (wave.Lvl8CamoAmount > 0 && i % wave.Lvl8CamoFrequency == 0 && camoSpawnedAmount[7] <= wave.Lvl8CamoAmount - 1)
            {
                enemy.GetComponent<BaseEnemy>().isCamo = true;
                camoSpawnedAmount[7]++;
            }
            enemy.GetComponent<BaseEnemy>().enemyLevel = 8;
            yield return new WaitForSeconds(wave.Lvl8SpawnRate);
        }
    }

    public void StartWave()
    {
        if (player.CurrentTurret != null)
            player.DeSelectTurret();
        if (AutoWaves)
            waves[0].waveProgression(CurrentWave);
        if (player.DroneBought)
        player.DronePoints.SetActive(false);
        wave = waves[WaveIndex];
        RemainingEnemies = waves[WaveIndex].TotalEnemies();
        UpdateREnemies();
        WaveButton.interactable = false;
        WaveButton.GetComponent<Image>().sprite = player.GameSpeed2;//gamespeed just because it happens to be the image I want.
        Lvl1Spawned = false;
        Lvl2Spawned = false;
        Lvl3Spawned = false;
        Lvl4Spawned = false;
        Lvl5Spawned = false;
        Lvl6Spawned = false;
        Lvl7Spawned = false;
        Lvl8Spawned = false;
        WaveStarted = true;
        for (int i = 0; i < camoSpawnedAmount.Length; i++)
            camoSpawnedAmount[i] = 0;
        
    }
    public void EnableWaveButton()
    {
        CurrentWave++;
        WaveButton.interactable = true;
        WaveButton.GetComponent<Image>().sprite = player.GameSpeed1;
        WaveStarted = false;
        
        if (!AutoWaves)
            WaveIndex++;
        else
            WaveIndex = 0;

        //WaveIndex++;
        
        WaveText.text = "Start Wave " + (CurrentWave);
        if (player.DroneBought)
            player.DronePoints.SetActive(true);
    }   
    public void NormalWaves()
    {
        AutoWaves = false;
        WaveBox.SetActive(false);
    }
    public void RandomWaves()
    {
        AutoWaves = true;
        WaveBox.SetActive(false);
    }
}
