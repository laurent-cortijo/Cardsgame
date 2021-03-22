using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactereMotor : MonoBehaviour
{
    Animation animations;

    public float walkSpeed, runSpeed, coteSpeed;

    public bool winZone = false, first = false, isGrounded, fall = false, parcours = false;

    public Vector3 jumpSpeed;

    public AudioClip MusicParcours;
    public AudioClip CollisionWallSound;
    public AudioClip VictorySound;
    public AudioClip DefeathSound;
    public AudioClip JumpSound;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        animations = gameObject.GetComponent<Animation>();
        rb = gameObject.GetComponent<Rigidbody>();
        //SoundManager.Instance.PlayMusic(MusicParcours);
    }

    void OnCollisionStay()
    {
        isGrounded = true;
        SoundManager.Instance.PlayMusic(CollisionWallSound);
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
        SoundManager.Instance.PlayMusic(JumpSound);

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
                {
                    animations.Play("victory");
                    SoundManager.Instance.PlayMusic(VictorySound);
                }

                else
                {
                    animations.Play("die");
                    SoundManager.Instance.PlayMusic(DefeathSound);
                }
            }
            else if (fall)
            {
                StartCoroutine(Wait_fall());
            }
            else
            {
                transform.Translate(0, 0, runSpeed * Time.deltaTime);
                animations.Play("run");
            }
        }
        
    }
}
