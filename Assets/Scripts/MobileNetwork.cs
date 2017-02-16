using UnityEngine;

public class MobileNetwork : Photon.PunBehaviour
{
	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
	}
 
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

	public override void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom ();
	}

	// TODO-2.a: the same as 1.b
	//   and join a room
	public override void OnJoinedRoom()
	{
		GetComponent<MobileShooter>().Activate();

//		var target = PhotonNetwork.Instantiate("Quad", Vector3.zero, Quaternion.identity, 0);
	}

}
