using UnityEngine;
using UnityEngine.SceneManagement;
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

        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                Debug.Log("Connecting");
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinRandomRoom();
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
    }
}