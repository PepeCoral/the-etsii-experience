using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using TNRD;
using System.Linq;


[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class Card : ScriptableObject
{
    [ResizableTextArea]
    [SerializeField] public string mainText;
    [SerializeField] public string characterName;

    [HorizontalLine(color: EColor.Gray)]
    
    [ResizableTextArea]
    [SerializeField] public string leftOptionText;
    [SerializeField]
    List<SerializableInterface<ICardEffect>> leftEffects;

    [HorizontalLine(color: EColor.Gray)]

    [ResizableTextArea]
    [SerializeField] public string rightOptionText;
    [SerializeField]
    List<SerializableInterface<ICardEffect>> rightEffects;

    [SerializeField]
    public Sprite sprite;


    public List<ICardEffect> getLeftEffects()
    {
        return leftEffects.Select(x => x.Value).ToList();
    }
    public List<ICardEffect> getRightEffects()
    {
        return rightEffects.Select(x => x.Value).ToList();
    }





}
    