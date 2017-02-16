using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Photon;

public class BoardBehavior : Photon.MonoBehaviour {

    public GameObject SplatterPrefab;
	public int score;
	public Text ScoreText;
	public Text TimerText;

	private float time;

    private List<GameObject> splatters = new List<GameObject>();

	// Use this for initialization
	void Start () {
		score = 0;
		time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
		var seconds = time % 60;//Use the euclidean division for the seconds.
		var fraction = (time * 100) % 100;

		TimerText.text = string.Format ("Time past: " + "{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);

		if (minutes == 2)
		{
			// game over
			Application.Quit();
		}

	}


    void OnCollisionEnter(Collision collision)
    {
        var other = collision.collider.gameObject;
        Vector3 hit_position = other.transform.position;
        if (other.CompareTag("Ball"))
        {
            PhotonNetwork.Destroy(other);
            Quaternion rot =  Quaternion.AngleAxis(Random.Range(0f, 360f), new Vector3(0, 0, 1)) ; //*transform.rotation;
            var splatter = Instantiate(SplatterPrefab, hit_position, rot) as GameObject;

            splatter.GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;

            splatters.Add(splatter);

			Vector3 hitPosition = splatter.transform.position;

			if (hitPosition.x >= -25 && hitPosition.x <= 8 && hitPosition.y >= -10 && hitPosition.y <= 10) {
				score += 1;
				ScoreText.text = "Score: " + score.ToString();
			}


        }
    }
}
