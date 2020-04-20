using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AsteroidsManager : MonoBehaviour
{
    public static AsteroidsManager asteroidsManager;
    [SerializeField] Transform[] asteroidsSpawn;
    [SerializeField] AsteroidConfig[] asteroidConfigs;
    [Range(0.01f, 1.00f)]
    [SerializeField] float baseSpawnChancePerTurn;
    float spawnChancePerTurn;
    [Range(0.01f, 1.00f)]
    [SerializeField] float spawnChanceIncreasePerTurn;
    [Range(0.01f, 1.00f)]
    [SerializeField] float[] howManyToSpawnChances;
    [SerializeField] int currentTier1Asteroids = 0;
    [SerializeField] int currentTier2Asteroids = 0;
    [SerializeField] int currentTier3Asteroids = 0;
    [SerializeField] int maxTier1Asteroids = 3;
    [SerializeField] int maxTier2Asteroids = 4;
    [SerializeField] int maxTier3Asteroids = 5;
    [SerializeField] List<AsteroidSpawn> unoccupiedTier1Spawns = new List<AsteroidSpawn>();
    List<AsteroidSpawn> occupiedTier1Spawns = new List<AsteroidSpawn>();
    [SerializeField] List<AsteroidSpawn> unoccupiedTier2Spawns = new List<AsteroidSpawn>();
    List<AsteroidSpawn> occupiedTier2Spawns = new List<AsteroidSpawn>();
    [SerializeField] List<AsteroidSpawn> unoccupiedTier3Spawns = new List<AsteroidSpawn>();
    List<AsteroidSpawn> occupiedTier3Spawns = new List<AsteroidSpawn>();
    public Transform asteroidsViewPosition;
    public Transform asteroidsReturnViewPosition;
    [SerializeField] GameObject asteroidViewHUD;
    public bool mechanicalArmCanFire = false;
    [SerializeField] int maxShotsWithArm = 2;
    public int shotsRemainingWithArm;
    [SerializeField] TextMeshProUGUI shotsRemainingTextInHUD;
    [SerializeField] Inventaire mainInventory;
    RandomisedLoot currentAsteroidLoot;
    [SerializeField] GameObject[] maxRangesImagesForHUD;
    public int armMaxRange = 1;
    [SerializeField] GameObject overheatingText;
    [SerializeField] GameObject mechanicalArm;
    [SerializeField] Transform[] mechanicalArmsTransformForRanges;
    bool asteroidEventsEnabled = false;
    [SerializeField] GameObject[] otherUIToDisable;
    private void Awake()
    {
        if(AsteroidsManager.asteroidsManager == null)
        {
            AsteroidsManager.asteroidsManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        shotsRemainingWithArm = maxShotsWithArm;
        TimeManager.timeManager.OnTimeChanged += OnTimeChange;
        spawnChancePerTurn = baseSpawnChancePerTurn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTimeChange(object sender, EventArgs e)
    {
        if (!asteroidEventsEnabled) return;
        shotsRemainingWithArm = Mathf.Clamp(shotsRemainingWithArm + 1, 0, maxShotsWithArm);

        float rand = UnityEngine.Random.Range(0.00f, 1.00f);
        if(rand < spawnChancePerTurn)
        {
            spawnChancePerTurn = baseSpawnChancePerTurn;
            //asteroids are spawning, now lets see how many
            float ceiling = 0;
            for (int i = 0; i < howManyToSpawnChances.Length; i++)
            {
                ceiling += howManyToSpawnChances[i];
            }
            float random = UnityEngine.Random.Range(0f, ceiling);
            for (int i = 0; i < howManyToSpawnChances.Length; i++)
            {
                if(random > howManyToSpawnChances[i])
                {
                    random -= howManyToSpawnChances[i];
                }
                else
                {
                    SpawnAsteroids(i + 1);
                    break;
                }
            }
        }
        else
        {
            spawnChancePerTurn += spawnChanceIncreasePerTurn;
        }
    }

    public void SpawnAsteroids(int howMany)
    {
        bool addEvent = false;
        for (int i = 0; i < howMany; i++)
        {
            float ceiling = 0;
            GameObject asteroidToSpawn;
            for (int j = 0; j < asteroidConfigs.Length; j++)
            {
                ceiling += asteroidConfigs[j].spawnChance;
            }
            float rand = UnityEngine.Random.Range(0.00f, ceiling);
            //which asteroid are we spawning
            for (int j = 0; j < asteroidConfigs.Length; j++)
            {
                if (rand > asteroidConfigs[j].spawnChance)
                {
                    rand -= asteroidConfigs[j].spawnChance;
                }
                else
                {
                    asteroidToSpawn = asteroidConfigs[j].asteroidPrefab;
                    //which tier will it spawn on ?
                    int randomTier = UnityEngine.Random.Range(0, asteroidConfigs[j].possibleTiers.Length);
                    bool spawn = true;
                    int tier = asteroidConfigs[j].possibleTiers[randomTier];
                    AsteroidSpawn spawnPoint = null;
                    switch (tier)
                    {
                        case 1:
                            if (currentTier1Asteroids < maxTier1Asteroids)
                            {
                                currentTier1Asteroids++;
                                spawnPoint = unoccupiedTier1Spawns[UnityEngine.Random.Range(0, unoccupiedTier1Spawns.Count)];
                                unoccupiedTier1Spawns.Remove(spawnPoint);
                                occupiedTier1Spawns.Add(spawnPoint);
                            }
                            else
                            {
                                //no places so too bad, no spawn
                                spawn = false;
                            }
                            break;
                        case 2:
                            if (currentTier2Asteroids < maxTier2Asteroids)
                            {
                                currentTier2Asteroids++;
                                spawnPoint = unoccupiedTier2Spawns[UnityEngine.Random.Range(0, unoccupiedTier2Spawns.Count)];
                                unoccupiedTier2Spawns.Remove(spawnPoint);
                                occupiedTier2Spawns.Add(spawnPoint);
                            }
                            else
                            {
                                //no places so too bad, no spawn
                                spawn = false;
                            }
                            break;
                        case 3:
                            if (currentTier3Asteroids < maxTier3Asteroids)
                            {
                                currentTier3Asteroids++;
                                spawnPoint = unoccupiedTier3Spawns[UnityEngine.Random.Range(0, unoccupiedTier3Spawns.Count)];
                                unoccupiedTier3Spawns.Remove(spawnPoint);
                                occupiedTier3Spawns.Add(spawnPoint);
                            }
                            else
                            {
                                //no places so too bad, no spawn
                                spawn = false;
                            }
                            break;
                    }
                    if (spawn)
                    {
                        addEvent = true;
                        GameObject asteroid = Instantiate(asteroidToSpawn, spawnPoint.transform.position, spawnPoint.transform.rotation);
                        asteroid.GetComponent<Asteroid>().spawn = spawnPoint;
                        break;
                    }
                }

            }
        }
        if (addEvent)
        {
            ShipEventsManager.shipEventsManager.AddShipEventToQueue(ShipEvent.AsteroidEvent());
        }
    }

    public void RemoveAsteroid(AsteroidSpawn spawn, bool harvested, RandomisedLoot asteroidLoot)
    {
        currentAsteroidLoot = asteroidLoot;
        switch (spawn.spawnTier)
        {
            case 1:
                occupiedTier1Spawns.Remove(spawn);
                unoccupiedTier1Spawns.Add(spawn);
                currentTier1Asteroids--;
                break;
            case 2:
                occupiedTier2Spawns.Remove(spawn);
                unoccupiedTier2Spawns.Add(spawn);
                currentTier2Asteroids--;
                break;
            case 3:
                occupiedTier3Spawns.Remove(spawn);
                unoccupiedTier3Spawns.Add(spawn);
                currentTier3Asteroids--;
                break;
        }
        if (harvested)
        {
            HarvestAsteroid();            
        }
    }

    void HarvestAsteroid()
    {
        shotsRemainingWithArm--;
        RefreshShotsRemainingHUDtext();
        ResourcesPack asteroidRewards = RandomisedLootDecrypter.GetInstance().DecryptRandomisedLoot(currentAsteroidLoot);
        mainInventory.AddManyResources(asteroidRewards);
        RewardsDisplayer.rewardsDisplayer.ReceiveRewardsToDisplay(asteroidRewards.resources, true);
    }

    public void GoInAsteroidsViewForArm()
    {

        RefreshShotsRemainingHUDtext();
        IsArmOverheating();
        CameraController.cameraController.GetToThisPosition(asteroidsViewPosition.position);
        mechanicalArm.SetActive(true);
        asteroidViewHUD.SetActive(true);
        DisableOtherUI();
        
        MouseCursorManager.mouseCursorManager.SetCursor(MouseCursor.asteroidCursor);
        SetupMechanicalArm();
        ManageArmMaxRange();
    }


    private void SetupMechanicalArm()
    {
        mechanicalArmCanFire = true;
        mechanicalArm.transform.position = mechanicalArmsTransformForRanges[armMaxRange - 1].position;
        mechanicalArm.transform.localScale = mechanicalArmsTransformForRanges[armMaxRange - 1].localScale;
    }

    public void RefreshShotsRemainingHUDtext()
    {
        shotsRemainingTextInHUD.text = "Shots before <color=\"red\"> overheat </color> : <b>" + shotsRemainingWithArm + "</b>";
        IsArmOverheating();
    }

    private void IsArmOverheating()
    {
        if(shotsRemainingWithArm <= 0)
        {
            overheatingText.SetActive(true);
        }
        else
        {
            overheatingText.SetActive(false);
        }
    }

    private void ManageArmMaxRange()
    {
        for (int i = 0; i < maxRangesImagesForHUD.Length; i++)
        {
            maxRangesImagesForHUD[i].SetActive(false);
        }
        maxRangesImagesForHUD[armMaxRange - 1].SetActive(true);
    }

    public void ReturnFromAsteroidViewForArm()
    {
        mechanicalArmCanFire = false;
        mechanicalArm.SetActive(false);
        CameraController.cameraController.GetToThisPosition(asteroidsReturnViewPosition.position, () => 
        {
            EnableOtherUI();
            MouseCursorManager.mouseCursorManager.SetCursor(MouseCursor.defaultCursor);
            CameraController.cameraController.playerControlEnabled = true;           

        });
        asteroidViewHUD.SetActive(false);
    }

    

    public void IncreaseArmMaxRange()
    {
        armMaxRange = Mathf.Clamp(armMaxRange + 1, 1, maxRangesImagesForHUD.Length);
    }

    public void EnableAsteroidsEvents()
    {
        asteroidEventsEnabled = true;
        spawnChancePerTurn = 1f;
        //guaranteed asteroids on first turn
    }

    public void DisableOtherUI()
    {
        TimeManager.timeManager.ToggleUI();
        foreach (GameObject item in otherUIToDisable)
        {
            item.SetActive(false);
        }
    }

    private void EnableOtherUI()
    {
        TimeManager.timeManager.ToggleUI();
        foreach (GameObject item in otherUIToDisable)
        {
            item.SetActive(true);
        }
    }

}
