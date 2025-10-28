using System;
using System.Collections.Generic;

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


    Dictionary<GameResource, int> resources;

   


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
    }
}
