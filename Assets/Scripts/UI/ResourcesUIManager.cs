using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesUIManager : MonoBehaviour
{
    [SerializeField] List<RectTransform> fillers;
    [SerializeField] float animationSpeed;

    private Dictionary<GameResource, float> currentResources = new();
    private Dictionary<GameResource, int> targetResources = new Dictionary<GameResource, int>();


    private void Start()
    {
        currentResources = GameManager.Instance.getResources().ToDictionary(pair => pair.Key, pair => (float)pair.Value); 
        targetResources = GameManager.Instance.getResources();
    }

    private void OnEnable()
    {
        ResourceManager.OnResourcesUpdated += updateUI;
    }

    private void OnDisable()
    {
        ResourceManager.OnResourcesUpdated -= updateUI;
    }

    private void updateUI()
    {
        print("Hola");
        targetResources = GameManager.Instance.getResources();
    }

    private void Update()
    {
        foreach (GameResource resource in Enum.GetValues(typeof(GameResource)))
        {
            if (Mathf.Abs(currentResources[resource] - targetResources[resource]) > 0.01f)
            {

                currentResources[resource] = Mathf.Lerp(currentResources[resource], targetResources[resource], Time.deltaTime * animationSpeed);
                float newYScale = (currentResources[resource] - ResourceManager.MIN_RESOURCE) / (float)ResourceManager.MAX_RESOURCE;
                print(newYScale);
                fillers[(int)resource].localScale = new Vector3(1, newYScale, 1);
            }
        }
    }


}
