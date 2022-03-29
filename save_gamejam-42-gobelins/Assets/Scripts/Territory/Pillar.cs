using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    public bool active = false;
    public Pillar centerPillar;
    [SerializeField]private float moveSpeed = 0.005f;
    // Start is called before the first frame update
    void Awake()
    {
        if (tag == "CentralPillar" || tag == "ActivePillar")
        {
            active = true;
            centerPillar = GameObject.FindWithTag("CentralPillar").GetComponent<Pillar>();
        }
    }

    public void TakePillar()
    {
        active = true;
        tag = "ActivePillar";
        centerPillar = GameObject.FindWithTag("CentralPillar").GetComponent<Pillar>();
        Territory.Instance.RemovePillar(this);
    }

    public void PlantPillar()
    {
        Territory.Instance.AddPillar(this);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
    
    }

    private void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Cloud") && active)
        {
            transform.position += (centerPillar.transform.position - transform.position) * moveSpeed;
            Territory.Instance.MovePillar(this);
        }
    }
}
