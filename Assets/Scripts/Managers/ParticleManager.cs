using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour, IProvidable
{
    [SerializeField] private ParticleSystem _blueParticle;
    [SerializeField] private ParticleSystem _greenParticle;
    [SerializeField] private ParticleSystem _pinkParticle;
    [SerializeField] private ParticleSystem _purpleParticle;
    [SerializeField] private ParticleSystem _redParticle;
    [SerializeField] private ParticleSystem _yellowParticle;
    
    [SerializeField] private Transform _particleParent;

    void Awake()
    {
        Services.Register(this);
    }

    public void PlayParticle(Cube cube)
    {
        ParticleSystem _cubeParticle;
        switch (cube.GetCubeType())
        {
            case CubeType.BlueCube:
                _cubeParticle = _blueParticle;
                break;
            case CubeType.GreenCube:
                _cubeParticle = _greenParticle;
                break;
            case CubeType.PinkCube:
                _cubeParticle = _pinkParticle;
                break;
            case CubeType.PurpleCube:  
                _cubeParticle = _purpleParticle;
                break;
            case CubeType.RedCube:
                _cubeParticle = _redParticle;
                break;
            case CubeType.YellowCube:
                _cubeParticle = _yellowParticle;
                break;
            default:
                return;
        }
        var particle = Instantiate(_cubeParticle, cube.transform.position, Quaternion.identity, _particleParent);
        particle.Play();
        Destroy(particle.gameObject, 2f);
    }

}
