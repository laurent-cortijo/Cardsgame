﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactereMotor : MonoBehaviour
{
    Animation animations;

    public float walkSpeed, runSpeed, coteSpeed;

    public bool winZone = false, first = false, isGrounded, fall = false, parcours = false;

    public Vector3 jumpSpeed;

    private int audiosound = 0, audiosoundsplash = 0;

    public SoundManager soundmanager;

    public AudioClip MusicParcours;
    public AudioClip CollisionWallSound;
    public AudioClip VictorySound;
    public AudioClip DefeathSound;
    public AudioClip JumpSound;
    public AudioClip WaterSplash;

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
        //SoundManager.Instance.Play(CollisionWallSound);
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
        audiosoundsplash = 0;
    }

    public void jumpAction()
    {
        if (isGrounded && parcours && !winZone)
        {
            rb.AddForce(jumpSpeed, ForceMode.Impulse);
            //SoundManager.Instance.Play(JumpSound);
            soundmanager.EffectsSource.clip = JumpSound;
            soundmanager.EffectsSource.Play();
        }
        
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
                    //SoundManager.Instance.MusicSource.Stop();
                    if(audiosound == 0)
                    {
                        //SoundManager.Instance.Play(VictorySound);
                        soundmanager.EffectsSource.clip = VictorySound;
                        soundmanager.EffectsSource.Play();
                        audiosound++;
                    }
                    //SoundManager.Instance.Play(VictorySound);
                }

                else
                {
                    animations.Play("die");
                    //SoundManager.Instance.MusicSource.Stop();
                    if (audiosound == 0)
                    {
                        //SoundManager.Instance.MusicSource.Stop();
                        //SoundManager.Instance.Play(DefeathSound);
                        soundmanager.MusicSource.clip = MusicParcours;
                        soundmanager.MusicSource.Stop();
                        soundmanager.EffectsSource.clip = DefeathSound;
                        soundmanager.EffectsSource.Play();
                        audiosound++;
                    }
                    //SoundManager.Instance.Play(DefeathSound);
                }
            }
            else if (fall)
            {
            	if (audiosoundsplash == 0)
            	{
            		soundmanager.EffectsSource.clip = WaterSplash;
        			soundmanager.EffectsSource.Play();
        			audiosoundsplash++;
            	}
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
