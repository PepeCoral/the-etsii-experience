using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUIManager : MonoBehaviour
{
    [SerializeField] List<RectTransform> fillers;
    private List<Image> images;
    [SerializeField] float animationSpeed;

    private Dictionary<GameResource, float> currentResources = new();
    private Dictionary<GameResource, int> targetResources = new Dictionary<GameResource, int>();


    private void Start()
    {
        currentResources = GameManager.Instance.getResources().ToDictionary(pair => pair.Key, pair => (float)pair.Value);
        targetResources = GameManager.Instance.getResources();

        images  = new();
        foreach (var item in fillers)
        {
            images.Add(item.transform.GetComponent<Image>());
        }
    }

    private void OnEnable()
    {
        ResourceManager.OnResourcesUpdated += updateUI;
        CardDrag.OnCardMovedToZone += updateResourceNotifiers;
    }

    private void OnDisable()
    {
        ResourceManager.OnResourcesUpdated -= updateUI;
        CardDrag.OnCardMovedToZone -= updateResourceNotifiers;
    }

    private void updateUI()
    {
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
                fillers[(int)resource].localScale = new Vector3(1, newYScale, 1);
                images[(int)resource].color = currentResources[resource] > targetResources[resource] ? Color.red : Color.green;
            }
            else
            {
                images[(int)resource].color = Color.white;

            }
        }
    }

    private void updateResourceNotifiers(ScreenZone screenZone)
    {
        print(screenZone);
    }



}
