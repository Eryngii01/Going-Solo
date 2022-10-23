using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    /** public GameObject dBox;
    public Text dText;

    public bool dialogueActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueActive && Input.GetKeyUp(KeyCode.Space))
        {
            dBox.SetActive(false);
            dialogueActive = false;
        }
    }

    public void ShowBox(string dialogue)
    {
        dialogueActive = true;
        dBox.SetActive(true);
        dText.text = dialogue;
    } **/

    public static DialogueManager instance;

    public void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Fix this " + gameObject.name);
        else
            instance = this;
    }

    public Text dialogueText;
    public Text dialogueName;
    public Image charPortrait;
    public GameObject dialogueBox;
    public float textDelay;

    private bool isTyping;
    private string completeText;

    public Queue<DialogueBase.Info> dialogueQueue = new Queue<DialogueBase.Info>();

    public void EnqueueDialogue(DialogueBase dBase)
    {
        dialogueBox.SetActive(true);
        dialogueQueue.Clear();

        foreach (DialogueBase.Info info in dBase.dialogueInfo)
        {
            dialogueQueue.Enqueue(info);
        }

        DequeueDialogue();
    }

    public void DequeueDialogue(DialogueHolder person = null)
    {
        if (isTyping == true)
        {
            dialogueText.text = completeText;
            StopAllCoroutines();
            isTyping = false;
            return;
        }

        if (dialogueQueue.Count == 0)
        {
            dialogueBox.SetActive(false);
            if (person != null)
            {
                person.isTalking = false;
            }
            return;
        }

        DialogueBase.Info info = dialogueQueue.Dequeue();

        completeText = info.text;
        dialogueText.text = info.text;
        dialogueName.text = info.charName;
        charPortrait.sprite = info.charPortrait;

        dialogueText.text = "";
        StartCoroutine(TypeText(info));
    }

    IEnumerator TypeText(DialogueBase.Info info)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in info.text.ToCharArray())
        {
            yield return new WaitForSeconds(textDelay);
            dialogueText.text += letter;
            yield return null;
        }
        isTyping = false;
    }
}
