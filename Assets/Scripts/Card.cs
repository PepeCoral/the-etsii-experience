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

    [HorizontalLine(color: EColor.Gray)]
    
    [ResizableTextArea]
    [SerializeField] public string leftOptionName;
    [SerializeField]
    List<SerializableInterface<ICardEffect>> leftEffects;

    [HorizontalLine(color: EColor.Gray)]

    [ResizableTextArea]
    [SerializeField] public string rightOptionName;
    [SerializeField]
    List<SerializableInterface<ICardEffect>> rightEffects;


    public List<ICardEffect> getLeftEffects()
    {
        return leftEffects.Select(x => x.Value).ToList();
    }
    public List<ICardEffect> getRightEffects()
    {
        return rightEffects.Select(x => x.Value).ToList();
    }





}
    