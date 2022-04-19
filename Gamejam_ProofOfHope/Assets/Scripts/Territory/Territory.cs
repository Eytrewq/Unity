using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Territory : MonoBehaviour
{
    public static Territory Instance;
    private MeshFilter meshFilter;
    private MeshCollider meshColl;
    private Mesh mesh;
    private GK.ConvexHullCalculator calc = new GK.ConvexHullCalculator();
    private List<Vector3> verts = new List<Vector3>();
    private List<int> tris = new List<int>();
    private List<Vector3> normals = new List<Vector3>();
    private List<Pillar> pillars = new List<Pillar>();
    [SerializeField] private float shrinkSize = 0.25f;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            this.enabled = false;
            return;
        }
        Instance = this;
        meshFilter = GetComponent<MeshFilter>();
        meshColl = GetComponent<MeshCollider>();
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;

    }

    private void Start()
    {
        pillars = FindObjectsOfType<Pillar>().Where(x => x.active).ToList();
        UpdateTerritory();
    }

    public void ShrinkTerritory(float value = -1f)
    {
        if (value <= 0f)
        {
            value = shrinkSize;
        }
        foreach (Pillar pil in pillars)
        {
            pil.transform.position += (pil.centerPillar.transform.position - pil.transform.position) * value;
        }
        UpdateTerritory();
    }

    public void SetCentralPillar(Pillar pillar)
    {
        foreach (Pillar pil in pillars)
        {
            pil.centerPillar = pillar;
            pil.tag = "Pillar";
        }
        pillar.tag = "CentralPillar";
    }

    public void MovePillar(Pillar pillar)
    {
        UpdateTerritory();
    }

    public void RemovePillar(Pillar pillar)
    {
        pillars.Remove(pillar);
        UpdateTerritory();
    }

    public void AddPillar(Pillar pillar)
    {
        pillars.Add(pillar);
        UpdateTerritory();
    }

    public bool CheckInsideTerritory(GameObject obj)
    {
        if (Physics.ComputePenetration(obj.GetComponent<Collider>(), obj.transform.position, obj.transform.rotation,
            meshColl, Vector3.zero, Quaternion.identity, out _, out _))
        {
            return (true);
        }

        return (false);
    }

    public bool CheckInsideTerritory(Collider obj)
    {
        if (Physics.ComputePenetration(obj, obj.transform.position, obj.transform.rotation,
            meshColl, Vector3.zero, Quaternion.identity, out _, out _))
        {
            return (true);
        }

        return (false);
    }

    private void UpdateTerritory()
    {
        List<Vector3> listVertices = new List<Vector3>();
        foreach (Pillar item in pillars)
        {
            listVertices.Add(new Vector3(item.transform.position.x, -item.transform.localScale.y / 2f, item.transform.position.z));
            listVertices.Add(new Vector3(item.transform.position.x, item.transform.localScale.y, item.transform.position.z));
        }
        calc.GenerateHull(listVertices, true, ref verts, ref tris, ref normals);
        mesh = new Mesh();
        mesh.SetVertices(verts);
        mesh.SetTriangles(tris, 0);
        mesh.SetNormals(normals);
        meshFilter.mesh = mesh;
        meshColl.sharedMesh = mesh;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.L))
        {
            ShrinkTerritory();
        }
        UpdateTerritory();
#endif   
    }
}
