using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace com.Avinash.AR_Sharing
{
    public class SetupUser : MonoBehaviour
    {
    // Start is called before the first frame update
    public void Setup()
    {
            GameObject player = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
            player.transform.parent = Camera.main.transform;
            GameObject.Destroy(GameObject.FindWithTag("ConnectionInfo"));
            GameObject.Destroy(GameObject.FindWithTag("SetupUser"));
        }
    }
}
