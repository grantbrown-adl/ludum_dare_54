using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;
    public static DialogueManager Instance { get => _instance; set => _instance = value; }
    public HashSet<int> PlayedDialogueIndexes { get => _playedDialogueIndexes; set => _playedDialogueIndexes = value; }

    [SerializeField] private Queue<string> _dialogueStrings;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _continueText;
    [SerializeField] private Image _portraitImage;
    [SerializeField] private Animator _animator;
    [SerializeField] private Dialogue[] _dialogues;
    [SerializeField] private HashSet<int> _playedDialogueIndexes = new();

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(gameObject);
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        _dialogueStrings = new Queue<string>();
        PlayerManager.Instance.InputAllowed = false;
        StartCoroutine(DelayedDialogStart(1.0f, _dialogues[0]));
    }

    public IEnumerator DelayedDialogStart(float delay, Dialogue dialogue)
    {
        yield return new WaitForSeconds(delay);
        StartDialogue(0);        
        PlayerManager.Instance.InputAllowed = true;
        Invoke(nameof(InitialiseSpawner), 10.0f);        
    }

    private void InitialiseSpawner()
    {
        RuneSpawner.Instance.StartSpawner();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _portraitImage.sprite = dialogue.Sprite;
        _continueText.text = "Continue";
        TimeManager.Instance.IsPaused = true;
        TimeManager.Instance.IsDialogShowing = true;        
        _animator.SetBool("isOpen", true);
        _nameText.text = dialogue.CharacterName;
        _dialogueStrings.Clear();

        foreach (string  sentence in dialogue.Sentences)
        {
            _dialogueStrings.Enqueue(sentence);
        }

        DisplayNextString();
    }

    public void StartDialogue(int dialogueIndex)
    {
        if (_playedDialogueIndexes.Contains(dialogueIndex)) return;
        
        if (dialogueIndex >= 0 && dialogueIndex < _dialogues.Length)
        {
            _playedDialogueIndexes.Add(dialogueIndex);

            Dialogue dialogue = _dialogues[dialogueIndex];

            StartDialogue(dialogue);
        }
        else
        {
            Debug.LogWarning($"Invalid dialogue index: {dialogueIndex}");
        }
    }

    public void DisplayNextString()
    {
        if(_dialogueStrings.Count == 1) _continueText.text = "exit";

        if (_dialogueStrings.Count == 0)
        {            
            EndDialog();
            return;
        }

        string sentence = _dialogueStrings.Dequeue();
        //_dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(DelayedText(sentence));
    }

    private void EndDialog()
    {
        _animator.SetBool("isOpen", false);
        TimeManager.Instance.IsPaused = false;
        TimeManager.Instance.IsDialogShowing = false;
        StartDialogue(3);
    }

    IEnumerator DelayedText(string text)
    {
        _dialogueText.text = "";
        foreach (char character in text.ToCharArray())
        {
            _dialogueText.text += character;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
}
