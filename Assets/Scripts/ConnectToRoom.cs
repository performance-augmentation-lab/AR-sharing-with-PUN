using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using System.IO;
using Photon.Pun;
using Vuforia;
using UnityEngine;

public class ConnectToRoom : MonoBehaviour
{
    [SerializeField]
    private byte maxPlayersPerRoom = 8;

    private bool roomJoined;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vuforia.VuforiaRuntimeUtilities.GetActiveFusionProvider());

        if (!roomJoined)
        {
            if (isTracking("PALImageTarget"))
            {
                Debug.Log("PalTracking");
                PhotonNetwork.JoinOrCreateRoom("PALImageTarget", new RoomOptions { MaxPlayers = maxPlayersPerRoom }, PhotonNetwork.CurrentLobby);
                roomJoined = true;
                GameObject.Find("DickensImageTarget").SetActive(false);
                //GameObject player = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
                //player.transform.parent = Camera.main.transform;
            }
            else if (isTracking("DickensImageTarget"))
            {
                Debug.Log("DickensTracking");
                PhotonNetwork.JoinOrCreateRoom("DickensImageTarget", new RoomOptions { MaxPlayers = maxPlayersPerRoom }, PhotonNetwork.CurrentLobby);
                roomJoined = true;
                GameObject.Find("PALImageTarget").SetActive(false);
                //GameObject player = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
                //player.transform.parent = Camera.main.transform;
            }
        }
    }


    public bool isTracking(string TargetName)
    {
        var imageTarget = GameObject.Find(TargetName);
        var trackable = imageTarget.GetComponent<TrackableBehaviour>();
        var status = trackable.CurrentStatus;
        return status == TrackableBehaviour.Status.TRACKED;
    }

    public void OnJoinedRoom()
    {
        Debug.Log("Client now in a room");
    }

    public void OnJoinRoomFailed()
    {
        Debug.Log("RoomJoinFail");
    }
}
