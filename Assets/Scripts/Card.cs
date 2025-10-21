using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using TNRD;


[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class Card : ScriptableObject
{
    [ResizableTextArea]
    [SerializeField] string mainText;

    [HorizontalLine(color: EColor.Gray)]
    
    [ResizableTextArea]
    [SerializeField] string leftOptionName;
    [SerializeField]
    List<SerializableInterface<ICardEffect>> leftEffects;

    [HorizontalLine(color: EColor.Gray)]

    [ResizableTextArea]
    [SerializeField] string rightOptionName;
    [SerializeField]
    List<SerializableInterface<ICardEffect>> rightEffects;






}
    