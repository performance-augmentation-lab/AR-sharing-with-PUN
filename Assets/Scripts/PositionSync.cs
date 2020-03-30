using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PositionSync : MonoBehaviour, IPunObservable
{
    public Vector3 LocalPosition;
    public Vector3 LocalScale;
    public Quaternion LocalRotation;

    private Vector3 networkPosition;
    private Vector3 networkScale;
    private Quaternion networkRotation;

    private Camera ARCamera;
    private PhotonView PhotonView;

    public bool isPerson;

    // Start is called before the first frame update
    void Start()
    {
        PhotonView = GetComponent<PhotonView>();
        ARCamera = Camera.main;

        LocalPosition = transform.localPosition;
        LocalScale = transform.localScale;
        LocalRotation = transform.localRotation;

        networkPosition = LocalPosition;
        networkScale = LocalScale;
        networkRotation = LocalRotation;

        Debug.Log(LocalPosition);
        Debug.Log(LocalScale);
        Debug.Log(LocalRotation);

    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

            Debug.Log("ISWriting");

            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            Debug.Log("Else");
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    void FixedUpdate()
    {
        if (!PhotonView.IsMine)
        {
            Debug.Log("IsNotMine");

            transform.localPosition = networkPosition;
            transform.localRotation = networkRotation;

            Debug.Log(networkPosition);
        }

        if (PhotonView.IsMine && isPerson)
        {
            Debug.Log("IsMine");

            transform.position = ARCamera.transform.position;
            transform.rotation = ARCamera.transform.rotation;

            Debug.Log(transform.position);

        }
    }

    void CalculatePosition()
    {

    }

}
