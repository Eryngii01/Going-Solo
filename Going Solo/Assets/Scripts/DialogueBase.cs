using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues")]
public class DialogueBase : ScriptableObject 
{

    [System.Serializable]
    public class Info
    {
        public string charName;
        [TextArea(4, 8)]
        public string text;
        public Sprite charPortrait;
    }

    [Header("Insert dialogue information below")]
    public Info[] dialogueInfo;
}
