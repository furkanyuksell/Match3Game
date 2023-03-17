using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour, IProvidable
{
    [SerializeField] private CubeBase CubeBasePrefab;    
    [Header("Pick Cube Types")]
    public bool BlueCube;
    public bool GreenCube;
    public bool PinkCube;
    public bool PurpleCube;
    public bool RedCube;
    public bool YellowCube;
    private readonly List<CubeType> ActiveCubeTypes = new List<CubeType>();
    void Awake()
    {
        Services.Register(this);
    
        if (BlueCube)
            ActiveCubeTypes.Add(CubeType.BlueCube);
        if (GreenCube)
            ActiveCubeTypes.Add(CubeType.GreenCube);
        if (PinkCube)
            ActiveCubeTypes.Add(CubeType.PinkCube);
        if (PurpleCube)
            ActiveCubeTypes.Add(CubeType.PurpleCube);
        if (RedCube)
            ActiveCubeTypes.Add(CubeType.RedCube);
        if (YellowCube)
            ActiveCubeTypes.Add(CubeType.YellowCube);

        if (ActiveCubeTypes.Count == 0)
        {
            ActiveCubeTypes.Add(CubeType.BlueCube);
            BlueCube = true;
        }
    }

    public CubeType GetRandomCubeType()
    {
        var randomIndex = Random.Range(0, ActiveCubeTypes.Count);
        return ActiveCubeTypes[randomIndex];
    }

    public Cube CreateCubeBase(Transform parent)
    {
        var cubeBase = Instantiate(CubeBasePrefab, Vector3.zero, Quaternion.identity, parent);
        return CreateCube(cubeBase, GetRandomCubeType());
    }

    private Cube CreateCube(CubeBase cubeBase, CubeType cubeType)
    {
        var cube = cubeBase.gameObject.AddComponent<Cube>();
        cube.PrepareCube(cubeBase, cubeType);

        return cube;
    }

}
