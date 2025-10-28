using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameResource 
{
    Money,
    Grades,
    Social,
    Mental
}
public class ResourceManager
{
    public const int MIN_RESOURCE = 0;
    public const int MAX_RESOURCE= 100;


    public Dictionary<GameResource, int> resources { private set; get; }

    public delegate void _OnResourcesUpdated();
    public static event _OnResourcesUpdated OnResourcesUpdated;


    public ResourceManager() 
    {
        resources = new Dictionary<GameResource, int>();
        foreach (GameResource resource in Enum.GetValues(typeof(GameResource)))
        {
            resources[resource] = MAX_RESOURCE / 2;
        }
    }

    public void modifyResource(ResourceEffect effect)
    {
        int newValue = Math.Clamp(resources[effect.resource] + effect.value, MIN_RESOURCE, MAX_RESOURCE);

        resources[effect.resource] = newValue;

        if(OnResourcesUpdated != null) { OnResourcesUpdated(); }
    }
}
