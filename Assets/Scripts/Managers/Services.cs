using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class Services
{
    private static Dictionary<Type, IProvidable> ProvidableServices = new Dictionary<Type, IProvidable>();

    public static T GetService<T>() where T : class, IProvidable
    {
        if (ProvidableServices.ContainsKey(typeof(T)))
        {
            return ProvidableServices[typeof(T)] as T;
        }
        else
        {
            Debug.LogError("Service " + typeof(T) + " not found");
            return null;
        }
    }
    public static T Register<T>(T service) where T : class, IProvidable
    {
        ProvidableServices.Add(typeof(T), service);
        return service;
    }

    public static FallAndFillManager GetFallAndFill
    {
        get{ return GetService<FallAndFillManager>();}
    }

    public static ParticleManager GetParticle
    {
        get{ return GetService<ParticleManager>();}
    }

    public static CubeManager GetCube
    {
        get{ return GetService<CubeManager>();}
    }

    public static SpriteManager GetSprite
    {
        get{ return GetService<SpriteManager>();}
    }

}
