using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;


namespace com.Avinash.AR_Sharing
{
    public class ConnectToPhoton : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private byte maxPlayersPerRoom = 8;

        string gameVersion = "1";

        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        void Start()
        {
        }

        public void ConnectToLobby()
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to Master");
            SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        }


        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("No room made so making one");

            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Client now in a room");
        }

        public void ConnectToPalTest()
        {
            PhotonNetwork.JoinOrCreateRoom("PALImageTarget", new RoomOptions { MaxPlayers = maxPlayersPerRoom }, PhotonNetwork.CurrentLobby);
            SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
            GameObject.Find("DickensImageTarget").SetActive(false);
            //GameObject player = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
            //player.transform.parent = Camera.main.transform;
        }
        public void ConnectToDickensTest()
        {
            PhotonNetwork.JoinOrCreateRoom("DickensImageTarget", new RoomOptions { MaxPlayers = maxPlayersPerRoom }, PhotonNetwork.CurrentLobby);
            SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
            GameObject.Find("PALImageTarget").SetActive(false);
            //GameObject player = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
            //player.transform.parent = Camera.main.transform;
        }

        public void ConnectToLobbyTest()
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }   
}