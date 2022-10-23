using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float speed, raycastLength;

    public Collider2D walkZone;
    private Rigidbody2D rBody;
    private Animator anim;
    private DialogueHolder dialogueManager;

    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;

    private bool hasWalkZone;
    public bool isWalking;

    public float walkTime;
    public float waitTime;

    private float walkCounter;
    private float waitCounter;

    private int walkDirection;

    // Start is called before the first frame update
    void Start()
    {
        rBody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        dialogueManager = gameObject.GetComponent<DialogueHolder>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        if (walkZone != null)
        {
            hasWalkZone = true;
            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            anim.SetBool("is_walking", true);
            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0: // Walk in the upwards direction (y > 0)
                    rBody.velocity = new Vector2(0, speed);
                    anim.SetFloat("input_x", 0);
                    anim.SetFloat("input_y", 1);

                    if (hasWalkZone && transform.position.y > maxWalkPoint.y)
                    {
                        isWalking = false;
                        anim.SetBool("is_walking", false);
                        waitCounter = waitTime;
                    }
                    break;
                case 1: // Walk in the right direction (x > 0)
                    rBody.velocity = new Vector2(speed, 0);
                    anim.SetFloat("input_x", 1);
                    anim.SetFloat("input_y", 0);

                    if (hasWalkZone && transform.position.x > maxWalkPoint.x)
                    {
                        isWalking = false;
                        anim.SetBool("is_walking", false);
                        waitCounter = waitTime;
                    }
                    break;
                case 2: // Walk in the downwards direction (y < 0)
                    rBody.velocity = new Vector2(0, -speed);
                    anim.SetFloat("input_x", 0);
                    anim.SetFloat("input_y", -1);

                    if (hasWalkZone && transform.position.y < minWalkPoint.y)
                    {
                        isWalking = false;
                        anim.SetBool("is_walking", false);
                        waitCounter = waitTime;
                    }
                    break;
                case 3: // Walk in the left direction (x < 0)
                    rBody.velocity = new Vector2(-speed, 0);
                    anim.SetFloat("input_x", -1);
                    anim.SetFloat("input_y", 0);

                    if (hasWalkZone && transform.position.x < minWalkPoint.x)
                    {
                        isWalking = false;
                        anim.SetBool("is_walking", false);
                        waitCounter = waitTime;
                    }
                    break;
            }

            if (walkCounter < 0)
            {
                isWalking = false;
                anim.SetBool("is_walking", false);
                waitCounter = waitTime;
            }

        } else
        {
            waitCounter -= Time.deltaTime;

            rBody.velocity = Vector2.zero;

            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }
    }

    private void FixedUpdate()
    {
        switch (walkDirection)
        {
            case 0: // Walk in the upwards direction
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), raycastLength, LayerMask.GetMask("Player"));
                if (hit)
                {
                    Debug.Log("Hit something: " + hit.collider.name);
                    hit.collider.GetComponent<PlayerController>().isActive = false;

                    // Get position the NPC should walk to
                    Vector3 target = hit.collider.transform.position;
                    target.y = target.y - 10f;

                    hit.collider.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

                    // TODO: play an exclamation point animation and wait for it to end
                    anim.SetFloat("input_x", 0);
                    anim.SetFloat("input_y", 1);

                    // Use coroutine to be able to do this sequentially
                    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

                    anim.SetFloat("input_y", 0);

                    // Trigger the dialogue
                    dialogueManager.isSpotted = true;
                }
                break;
            case 1: // Walk in the right direction
                RaycastHit2D hit2 = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), raycastLength, LayerMask.GetMask("Player"));
                if (hit2)
                {
                    Debug.Log("Hit something: " + hit2.collider.name);
                }
                break;
            case 2: // Walk in the downwards direction
                Debug.DrawRay(transform.position, Vector2.down * raycastLength, Color.red);
                RaycastHit2D hit3 = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), raycastLength, LayerMask.GetMask("Player"));
                if (hit3)
                {
                    Debug.Log("Hit something: " + hit3.collider.name);
                }
                break;
            case 3: // Walk in the left direction
                Debug.DrawRay(transform.position, Vector2.left * raycastLength, Color.red);
                RaycastHit2D hit4 = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), raycastLength, LayerMask.GetMask("Player"));
                if (hit4)
                {
                    Debug.Log("Hit something: " + hit4.collider.name);
                }
                break;
        }
    }

    private void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }

    /*private void WalkUpToPlayer(Vector3 target)
    {
        // NPC walks up to target position
        while (transform.position != target)
        {
            rBody.velocity = new Vector2(0, speed);
            anim.SetFloat("input_x", 0);
            anim.SetFloat("input_y", 1);
        }

        // Trigger the dialogue
        DialogueHolder dialogueManager = gameObject.GetComponent<DialogueHolder>();
        dialogueManager.TriggerDialogue();
    }*/
}
