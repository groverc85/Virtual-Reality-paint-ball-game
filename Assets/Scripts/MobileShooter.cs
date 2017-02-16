﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MobileShooter : MonoBehaviour {

    //public GameObject Ball;
    public GameObject ARCamera;
    public Button ShootFrontButton;

    bool started = false;
    //float swipespeed_max = 5; // 0.2s cross screen height
    float swipespeed_min = 1; // 1s cross screen height
    //float ballspeed_max = 25f;
    //float ballspeed_min = 2f;
    Vector3 mousedown_pos;
    float mousedowned_time;

    bool bMouseDown = false;
    float ballSpeedFixed = 25f;

	Vector3 swipe_vel;

    // Use this for initialization
    void Start()
    {
        ShootFrontButton.enabled = false;
    }

    public void Activate()
    {
        ShootFrontButton.enabled = true;
        ShootFrontButton.onClick.AddListener(ShootBallFront);
        started = true;
    }

    // shoot ball on swipe
    void Update()
    {
        if (!started) return;

        if (bMouseDown)
        {
            mousedowned_time += Time.deltaTime;
        }

        if (!bMouseDown && Input.GetMouseButtonDown(0))
        {
            mousedown_pos = Input.mousePosition;
            mousedowned_time = 0;
            bMouseDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!bMouseDown || mousedowned_time <= 0.05f) return;


            Vector3 mouseup_pos = Input.mousePosition;
            Vector3 delta = (mouseup_pos - mousedown_pos) / Screen.height;

            swipe_vel = delta / mousedowned_time;


            if (swipe_vel.y > swipespeed_min)
            {
                ShootBallUp();
            }


            bMouseDown = false;
            mousedowned_time = 0;
        }
    }

    public void ShootBall(Vector3 velocity)
    {

        GetComponent<AudioSource>().Play();

        // You may want to use a random nice color so there is one!
        Color color = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f, 1f, 1f);
        Vector3 color_v = new Vector3(color.r, color.g, color.b);

        // TODO-2.c PhotonNetwork.Instantiate to shoot a ball!
        // You may want to initialize a RPC function call to RPCInitialize() 
        //   (See BallBehavior.cs) to set the velocity and color
        //   of the ball across all clients (PhotonTargets.All) and transfer 
        //   the ownership of the ball to PC so the ball is correctly destroyed
        //   upon hitting a wall.

		GameObject ball = PhotonNetwork.Instantiate("ball", ARCamera.transform.position, Quaternion.identity, 0);
		BallBehavior behavior = ball.GetComponent<BallBehavior>();
		PhotonView ballPhotonView = ball.GetComponent<PhotonView>();
		ballPhotonView.RPC("RPCInitialize", PhotonTargets.All, velocity, color_v);
    }



    public void ShootBallFront()
    {
//		ShootBall(swipe_vel.magnitude * ARCamera.transform.forward);
		ShootBall(ballSpeedFixed * ARCamera.transform.forward);
    }

    public void ShootBallUp()
    {
		// normal swipe speed is around 4-5
		ShootBall(5 * swipe_vel.magnitude * ARCamera.transform.up + swipe_vel.magnitude * ARCamera.transform.forward);
//		ShootBall(ballSpeedFixed * ARCamera.transform.up);
		Debug.Log("swipe_vel.maginitue = " + swipe_vel.magnitude);
    }
}