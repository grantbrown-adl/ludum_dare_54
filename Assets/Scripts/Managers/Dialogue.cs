using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    [SerializeField] private string _dialogName;
    [SerializeField] private string _characterName;
    [SerializeField] private Sprite _sprite;

    [TextArea(3,10)]
    [SerializeField] private string[] _sentences;

    public string DialogName { get => _dialogName; set => _dialogName = value; }
    public string CharacterName { get => _characterName; set => _characterName = value; }
    public string[] Sentences { get => _sentences; set => _sentences = value; }
    public Sprite Sprite { get => _sprite; set => _sprite = value; }
}
