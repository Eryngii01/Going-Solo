using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public string startPoint;
    public bool isActive = true;

    private Rigidbody2D rBody;
    private Animator anim;

    private static bool playerExists;
    private float moveHorizontal, moveVertical;

    // Start is called before the first frame update
    void Start()
    {
        rBody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            moveVertical = Input.GetAxisRaw("Vertical");

            Vector2 movementVector = new Vector2(moveVertical, moveHorizontal);

            if (movementVector != Vector2.zero)
            {
                anim.SetBool("is_walking", true);
                anim.SetFloat("input_x", moveHorizontal);
                anim.SetFloat("input_y", moveVertical);
            }
            else
            {
                anim.SetBool("is_walking", false);
            }
        }
    }

    private void FixedUpdate()
    {
        rBody.velocity = new Vector2(moveHorizontal, moveVertical) * speed;
    }
}
