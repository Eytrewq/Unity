using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTower : MonoBehaviour
{
    public static int towersActivated = 0;
    public int towerToActivate = 4;
    public SphereCollider selfCollider;
    public bool isActivated = false;
    private Pillar currentPillar;
    public CloudSpawner cloud;
    private float timeStamp = Single.PositiveInfinity;
    private float saveTime = 0.0f;
    private float delai = 1000.0f;
    public Canvas win;

    void Start()
    {
        isActivated = false;
    }

    private void Update()
    {
        bool inside = Territory.Instance.CheckInsideTerritory(selfCollider);
        if (towersActivated == 2)
        {
            win.gameObject.SetActive(true);
        }

        if (timeStamp <= Time.time && !isActivated)
        {
            OnTowerActivated();
        }
        if (cloud && inside)
        {
            OnTerritoryEnter();
        }
        else if (cloud && !inside)
        {
            OnTerritoryExit();
        }
        Debug.Log("Time = " + Time.time + ", timestamp = " + timeStamp);
    }

    public void OnTerritoryEnter()
    {
        delai -= Time.time - saveTime;
        Debug.Log("delai = " + delai);
        timeStamp = Time.time + delai;
    }

    public void OnTerritoryExit()
    {
        saveTime = Time.time;
    }

    private void OnTowerActivated()
    {
        towersActivated++;
        isActivated = true;
        Debug.Log("Tower Activated. Towers activated :" + towersActivated);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ActivePillar"))
        {
            currentPillar = other.GetComponent<Pillar>();
            cloud.attackPillar = currentPillar;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentPillar && other.gameObject == currentPillar.gameObject)
        {
            currentPillar = null;
            cloud.attackPillar = null;
        }
    }
}
