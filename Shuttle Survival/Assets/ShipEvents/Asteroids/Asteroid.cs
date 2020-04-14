using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] RandomisedLoot asteroidLoot;
    [SerializeField] int turnsBeforeGone = 2;
    [SerializeField] float maxRotateSpeed = 30f;
    [SerializeField] float scaleVariation = 0.2f;
    public AsteroidSpawn spawn;
    bool canBeHarvested = false;
    [TextArea(2, 5)]
    [SerializeField] string toolTipDescription;
    // Start is called before the first frame update
    void Start()
    {
        TimeManager.timeManager.OnTimeChanged += OnTimeChanged;
        maxRotateSpeed = UnityEngine.Random.Range(-maxRotateSpeed, maxRotateSpeed);
        transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(0, 359));
        float rand = UnityEngine.Random.Range(-scaleVariation, scaleVariation);
        transform.localScale = new Vector3(transform.localScale.x + rand, transform.localScale.y + rand, transform.localScale.z + rand);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, maxRotateSpeed * Time.deltaTime));
        if (canBeHarvested && AsteroidsManager.asteroidsManager.shotsRemainingWithArm > 0 && Input.GetMouseButtonDown(0))
        {
            if (AsteroidsManager.asteroidsManager.armMaxRange >= spawn.spawnTier)
            {
                RemoveThisAsteroid(true);
            }
            else
            {
                MessagePopup.MessagePopupManager.SetStringAndShowPopup("Out of range");
            }
        }
    }

    private void OnMouseEnter()
    {
        if (AsteroidsManager.asteroidsManager.mechanicalArmCanFire)
        {
            canBeHarvested = true;
        }
    }

    private void OnMouseExit()
    {
        canBeHarvested = false;
    }

    public string GetTooltipDescription()
    {
        return toolTipDescription;
    }

    public Vector3 GetSpawnPosition()
    {
        return spawn.transform.position;
    }

    public void OnTimeChanged(object sender, EventArgs e)
    {
        turnsBeforeGone--;
        if(turnsBeforeGone <= 0)
        {
            RemoveThisAsteroid();
        }
    }

    private void RemoveThisAsteroid(bool harvested = false)
    {
        AudioManager.audioManager.PlaySoundEffect(SoundEffectsType.AsteroidBlowUp);
        TimeManager.timeManager.OnTimeChanged -= OnTimeChanged;
        GetComponent<TooltipHandler>().OnPointerExit(null);
        AsteroidsManager.asteroidsManager.RemoveAsteroid(spawn, harvested, asteroidLoot);
        Destroy(this.gameObject);
    }
}
