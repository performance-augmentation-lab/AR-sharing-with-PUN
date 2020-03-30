using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace com.Avinash.AR_Sharing
{
    public class ConnectionStatus : MonoBehaviour
    {
        public TextMeshProUGUI ConnectionInfo;
        public int PlayerCount;
        public bool ConnStatus;
        // Start is called before the first frame update
        void Start()
        {
            ConnectionInfo = GetComponent<TextMeshProUGUI>();
            PlayerCount = 0;
            ConnStatus = false; 
        }

        // Update is called once per frame
        void Update()
        {
            ConnStatus = PhotonNetwork.IsConnected;
            if (PhotonNetwork.CurrentRoom != null){
                PlayerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            }
            
            ConnectionInfo.text = "Connection Status: " + ConnStatus
                + '\n' + "Players in Room: " + PlayerCount;
        }
    }
}

