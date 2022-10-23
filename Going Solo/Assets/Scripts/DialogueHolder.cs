using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    private GameObject player;

    public DialogueBase dialogue;
    public bool isTalking;
    public bool isSpotted;

    // private DialogueManager dManager;

    // Start is called before the first frame update
    void Start()
    {
        // dManager = FindObjectOfType<DialogueManager>();
        isTalking = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() {
        if (isTalking && Input.GetKeyDown(KeyCode.Space))
        {
            DialogueManager.instance.DequeueDialogue(this);
        } else if (!isTalking && isSpotted)
        {
            player.GetComponent<PlayerController>().isActive = false;
        }
    }

    public void TriggerDialogue() {
        DialogueManager.instance.EnqueueDialogue(dialogue);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && isSpotted && !isTalking)
        {
            // dManager.ShowBox(dialogue);
            isTalking = true;
            // Note that this does not stop player movement and control
            TriggerDialogue();
        }
    }
}
