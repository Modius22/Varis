using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlatformerController : PhysikObjekcts {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed= 7;
    public Text greenAppleText;
	public Text winText;
	public Text start;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
	private int greenApple;
	
	// Use this for initialization
	void Awake () {

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
		greenApple = 0;
		winText.text ="";
		SetGreenApple();
	}

    protected override void ComputeVelocity()
    {
        //base.ComputeVelocity();
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;

        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * .5f;
        }


        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        targetVelocity = move * maxSpeed;
    }
	
	    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup")){
			other.gameObject.SetActive (false);
			greenApple++;
			SetGreenApple();
		}
		if (greenApple >= 4){
			winText.text = "You win!";
		}
    }

	void SetGreenApple()
	{
		greenAppleText.text = "greenApple: " + greenApple.ToString();
	}
}
