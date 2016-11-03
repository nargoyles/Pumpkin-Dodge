using UnityEngine;
using System.Collections;

public class TouchControls : MonoBehaviour {

    public float speed;
    public float magnitude;
    public float jump;
    public Transform groundCheck;
    public bool grounded = false;
    float groundRadius = .2f;
    public LayerMask whatIsGround;
    bool facingRight = true;
	Hashtable beganMap = new Hashtable();
	Hashtable movedMap = new Hashtable();

	// Update is called once per frame
	void Update ()
    {

        //Ground check.
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

		if(Mathf.Abs (Input.GetAxisRaw("Horizontal")) > 0) {
			transform.Translate(new Vector2(Input.GetAxisRaw ("Horizontal") * speed * 160, 0));
			if (Input.GetAxisRaw ("Horizontal") > 0 && !facingRight)
			{
				Flip();
			}
			
			else if (Input.GetAxisRaw ("Horizontal") < 0 && facingRight)
			{
				Flip();
			}
		}

		if (Input.GetAxisRaw ("Vertical") > 0 && grounded || Input.GetKeyDown(KeyCode.Space) && grounded) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, jump);
			grounded = false;
		}


        //Check if Input has registered more than zero touches
        if (Input.touchCount > 0)
        {

            foreach(Touch T in Input.touches)
            {

                //Check if the phase of that touch equals Began
                if (T.phase == TouchPhase.Began)
                {
                    //If so, set touchOrigin to the position of that touch
					beganMap[T.fingerId] = T.position;
                }

                if (T.phase == TouchPhase.Moved || T.phase == TouchPhase.Stationary)
                {
                    //Store the first touch's current position every frame.
                    movedMap[T.fingerId] = T.position;
					Vector2 firstTouch = (Vector2)beganMap[T.fingerId]; 
                    //Store the difference between first touch's current position and first touch's original position every frame.
                    var difference = (Vector2)movedMap[T.fingerId] - (Vector2)beganMap[T.fingerId];

                    //if touch is on the left half of screen...
                    if (firstTouch.x < Screen.width / 2 && Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
                    {
                        if (Mathf.Abs(difference.x) > 15)
                        {
                            transform.Translate(new Vector2(Mathf.Clamp(difference.x, -160, 160) * speed, 0));
                            if (difference.x > 0 && !facingRight)
                            {
                                Flip();
                            }

                            else if (difference.x < 0 && facingRight)
                            {
                                Flip();
                            }
                        }
                    }
                   
                    //if touch is on the right half of screen and the movement of touch on the y axis is greater than that of the x and if ground...
                    else if (firstTouch.x > Screen.width / 2 && Mathf.Abs(difference.x) < Mathf.Abs(difference.y) && grounded)
                    {
                        //check the magnitude of the swipe
                        if (T.deltaPosition.magnitude > magnitude)
                        {
                            GetComponent<Rigidbody2D>().velocity = new Vector2(0, jump);
                            grounded = false;
                        }
                    }
                }

                   

                if (T.phase == TouchPhase.Canceled || T.phase == TouchPhase.Ended)
                {
					beganMap.Remove(T.fingerId);
					movedMap.Remove(T.fingerId);
                }
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
