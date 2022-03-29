using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputHandler))]
public class Player : Singleton<Player>
{
    private Pillar zonePillar = null;
    private Pillar currentPillar = null;
    private int nbrPillar = 0;
    private float timeStamp = 0.0f;
    private InputHandler input;
    public Slider slider;
    public TextMeshProUGUI delaiPillar;
    [SerializeField] private Canvas deadCanvas;
    [SerializeField] private Canvas winCanvas;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private Camera Camera;
    [SerializeField] private float energy = 100.0f;
    [SerializeField] private float cooldownTakePilar = 5.0f;

    public TextMeshProUGUI textNbrPillar;
    public float TimeEnergyLost = 10.0f;

    void Start()
    {
        input = GetComponent<InputHandler>();
        slider.value = energy;
    }

    // Update is called once per frame
    void Update()
    {
        if (energy <= 0 && !winCanvas.gameObject.activeSelf)
        {
            deadCanvas.gameObject.SetActive(true);
            return;
        }
        if (timeStamp <= Time.time && Input.GetKeyDown(KeyCode.E) && zonePillar != null && currentPillar == null)
        {
            currentPillar = zonePillar;
            nbrPillar += 1;
            zonePillar.TakePillar();
            zonePillar.gameObject.SetActive(false);
            zonePillar = null;
            textNbrPillar.text = nbrPillar.ToString();
            timeStamp = Time.time + cooldownTakePilar;
        }
        else if (Input.GetKeyDown(KeyCode.E) && currentPillar != null)
        {
            nbrPillar -= 1;
            textNbrPillar.text = nbrPillar.ToString();
            currentPillar.transform.position = transform.position + transform.forward * 0.5f;
            currentPillar.PlantPillar();
            currentPillar.gameObject.SetActive(true);
            currentPillar = null;
        }

        if (timeStamp <= Time.time)
            delaiPillar.text = "Ready";
        else
            delaiPillar.text = (Mathf.Abs(Time.time - timeStamp)).ToString();


        // Movement
        var targetVector = new Vector3(input.InputVector.x, 0, input.InputVector.y);
        MoveTowardTarget(targetVector);

        // Territory
        if (Territory.Instance.CheckInsideTerritory(gameObject))
        {
            energy += 100 * Time.deltaTime / TimeEnergyLost;
            if (energy > 100.0f)
                energy = 100.0f;
            slider.value = energy;
        }
        else
        {
            energy -= 100 * Time.deltaTime / TimeEnergyLost;
            if (energy < 0.0f)
                energy = 0.0f;
            slider.value = energy;
        }

    }
    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        float speed = MovementSpeed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
        Vector3 targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ActivePillar") || other.CompareTag("InactivePillar"))
        {
            zonePillar = other.GetComponent<Pillar>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (zonePillar != null && other.gameObject == zonePillar.gameObject)
        {
            zonePillar = null;
        }
    }
}

