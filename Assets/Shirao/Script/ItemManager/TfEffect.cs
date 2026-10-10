using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;

//using System.Numerics;
using UnityEngine;

public class TfEffect : MonoBehaviour
{
    public PlayerControlle playerControlle;
    public TfControlle tfControlle;
    public RayCastForSmall rayCastForSmall;
    public CollisionManager downCollision;
    public CollisionManager upAndSideCollision;
    public CollisionManager waterTrigger;
    public Transform player;
    [SerializeField] private string dashPhase;
    [SerializeField] private string playerDirection;
    [SerializeField] private float dashForce;
    [SerializeField] private float upDashForce;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashColeDownTime;
    [SerializeField] private float doubleJumpSpeed;
    [SerializeField] private float swimWaterSpeed;
    [SerializeField] private float swimFloatSpeed;
    [SerializeField] private float swimWaterAcceleration;
    [SerializeField] private float swimWaterDeceleration;
    [SerializeField] private Vector3 normalScale;
    [SerializeField] private Vector3 smallScale;
    [SerializeField] private float smallTime;
    private bool notDoubleJumped=true;
    private Vector2 swimVelocity;
    [SerializeField] private string scalePhase;

    //初期ダッシュ状態
    void Start()
    {
        dashPhase="notDash";
        scalePhase="learger";
    }

    //変身効果処理
    void Update()
    {
        switch(tfControlle.tfPhase)
        {
            case "idle":
                if(scalePhase=="smaller")
                {
                    StartCoroutine(Learger());
                }
                break;
            case "dash":
                if(scalePhase=="smaller")
                {
                    StartCoroutine(Learger());
                }
                if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    playerDirection="right";
                }
                else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    playerDirection="left";
                }

                if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetMouseButton(1)) && dashPhase=="notDash")
                {
                    StartCoroutine(Dash(playerDirection));
                }
                break;
            case "doubleJump":
                if(scalePhase=="smaller")
                {
                    StartCoroutine(Learger());
                }
                if(downCollision.triggerPhase=="stage")
                {
                    notDoubleJumped=true;
                }

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    if(downCollision.triggerPhase=="air" && notDoubleJumped)
                    {
                        playerControlle.rb.linearVelocity = new Vector2(playerControlle.rb.linearVelocity.x, doubleJumpSpeed);
                        notDoubleJumped=false;
                    }
                }
                break;
            case "swim":
                if(scalePhase=="smaller")
                {
                    StartCoroutine(Learger());
                }
                if((waterTrigger.triggerPhase=="water"))
                {
                    Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

                    if (input.sqrMagnitude > 1f)
                    {
                        input.Normalize();
                    }

                    Vector2 targetVelocity=input*swimWaterSpeed;
                    float acceleration;

                    if (input.sqrMagnitude > 0f)
                    {
                        acceleration = swimWaterAcceleration;
                    }
                    else
                    {
                        acceleration = swimWaterDeceleration;
                    }

                    if(upAndSideCollision.triggerPhase=="rightWall")
                    {
                        if(input.x>0f)
                        {
                            swimVelocity.x=0f;
                        }
                    }
                    else if(upAndSideCollision.triggerPhase=="leftWall")
                    {
                        if(input.x<0f)
                        {
                            swimVelocity.x=0f;
                        }
                    }

                    swimVelocity = Vector2.MoveTowards(swimVelocity,targetVelocity,acceleration*Time.fixedDeltaTime);
                    playerControlle.rb.linearVelocity = new Vector2(swimVelocity.x, swimVelocity.y+swimFloatSpeed);
                }
                else
                {
                    swimVelocity = playerControlle.rb.linearVelocity;
                }
                break;
            case "small":
                if(scalePhase=="learger")
                {
                    StartCoroutine(Smaller());
                }
                break;
            case "climb":
                if(scalePhase=="smaller")
                {
                    StartCoroutine(Learger());
                }
                break;
        }
    }

    //ダッシュ効果処理
    public IEnumerator Dash(string playerDirection)
    {
        float time=0f;
        dashPhase="dash";

        if(playerDirection=="right")
        {
            playerControlle.rb.AddForce(Vector2.up*upDashForce);
            while(time<dashTime)
            {
                //playerControlle.rb.AddForce(Vector2.up*50);
                playerControlle.rb.AddForce(Vector2.right * dashForce);
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
        else if(playerDirection=="left")
        {
            playerControlle.rb.AddForce(Vector2.up*upDashForce);
            while(time<dashTime)
            {
                //playerControlle.rb.AddForce(Vector2.up*50);
                playerControlle.rb.AddForce(Vector2.left * dashForce);
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }

        yield return new WaitForSeconds(dashColeDownTime);
        dashPhase="notDash";
    }

    private IEnumerator Learger()
    {
        scalePhase="learger";
        float timer = 0f;
            Debug.Log("巨大化中");
            
            
            while(timer<smallTime)
            {
                timer += Time.deltaTime;
                Debug.Log($"Time.deltaTime={Time.deltaTime}");
                Debug.Log($"timer={timer}, smallTime={smallTime}");
                float t = Mathf.Clamp01(timer/smallTime);
                player.localScale = Vector3.Lerp(smallScale, normalScale, t);
                yield return null;
            }
            player.localScale=normalScale;
    }

    private IEnumerator Smaller()
    {
        scalePhase="smaller";
        // Debug.Log("Smaller");
        float timer =0f;
        // Debug.Log(timer,smallTime);
        while(timer<smallTime)
        {
            Debug.Log("small");
            timer += Time.deltaTime;
            float t =Mathf.Clamp01(timer/smallTime);
            player.localScale = Vector3.Lerp(normalScale, smallScale, t);
            yield return null;
        }
        player.localScale=smallScale;
    }

    // private IEnumerator Swim()
    // {
    //     float time=0f;
    //     while(time<swimTime)
    //     {
    //         playerControlle.rb.AddForce(Vector2.up * swimJumpSpeed);
    //         time += Time.fixedDeltaTime;
    //         yield return new WaitForFixedUpdate();
    //     }
    // }
}
