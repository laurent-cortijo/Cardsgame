using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactereMotor : MonoBehaviour
{
    Animation animations;

    public float walkSpeed, runSpeed, coteSpeed;

    public string inputLeft, inputRight;

    public bool winZone = false, first = false, isGrounded, fall = false, parcours = false;

    public Vector3 jumpSpeed;
    
    public GameObject choose;

    GameObject[] spawns;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        animations = gameObject.GetComponent<Animation>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void OnCollisionExit()
    {
        isGrounded = false;
    }

    IEnumerator Wait_fall()
    {
        animations.Play("idle");
        yield return new WaitForSeconds(1);
        fall = false;
    }

    public void jumpAction()
    {
        if (isGrounded && parcours && !winZone)
            rb.AddForce(jumpSpeed, ForceMode.Impulse);
        
    }

    public void leftAction()
    {
        if (parcours && !winZone)
            transform.Translate(-coteSpeed, 0, 0);
    }

    public void rightAction()
    {
        if (parcours && !winZone)
            transform.Translate(coteSpeed, 0, 0);
    }


        // Update is called once per frame
        void Update()
    {
        if(parcours)
        {
        	
            if (winZone)
            {
                if (first)
                    animations.Play("victory");
                else
                    animations.Play("die");
            }
            else if (fall)
            {
                StartCoroutine(Wait_fall());
            }
            else
            {
                // Déplacer à gauche
                if (Input.GetKeyDown(inputLeft))
                {
                    transform.Translate(-coteSpeed, 0, 0);
                }


                // Déplacer à droite
                if (Input.GetKeyDown(inputRight))
                {
                    transform.Translate(coteSpeed, 0, 0);
                }

                //Sauter
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                {
                    rb.AddForce(jumpSpeed, ForceMode.Impulse);

                }
                transform.Translate(0, 0, runSpeed * Time.deltaTime);
                animations.Play("run");


            }
        }
        
    }
}
