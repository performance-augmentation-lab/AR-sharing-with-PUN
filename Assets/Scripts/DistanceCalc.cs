using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using Vuforia;
using UnityEngine;

public class DistanceCalc : MonoBehaviour
{
    public SerializePrivateVariables imageTarget;
    private bool playerCreated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTracking("ImageTarget") && !playerCreated)
        {
            playerCreated = true;
            GameObject player = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
            player.transform.parent = Camera.main.transform;
        }
        else
        {
        }
    }


    public bool isTracking(string TargetName)
    {
        var imageTarget = GameObject.Find(TargetName);
        var trackable = imageTarget.GetComponent<TrackableBehaviour>();
        var status = trackable.CurrentStatus;
        return status == TrackableBehaviour.Status.TRACKED;
    }
}
