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

    private Vector3 ImageTargetVector;

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

        ImageTargetVector = GetMyTargetPosition();

        Debug.Log(LocalPosition);
        Debug.Log(LocalScale);
        Debug.Log(LocalRotation);

    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            stream.SendNext(MyPositionFromTarget());
            stream.SendNext(transform.rotation);
        }
        else
        {
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    void FixedUpdate()
    {

        if (!PhotonView.IsMine)
        {
            transform.localPosition = calcNewNetworkPos(networkPosition);
            transform.localRotation = networkRotation;
        }

        if (PhotonView.IsMine && isPerson)
        {

            transform.position = ARCamera.transform.position;
            transform.rotation = ARCamera.transform.rotation;
        }
    }

    private Vector3 GetMyTargetPosition()
    {
        var imageTarget = transform.parent.gameObject;
        var targetTransform = imageTarget.GetComponent<Transform>();
        Vector3 targetPosition = targetTransform.position;
        return targetPosition;
    }

    private Vector3 MyPositionFromTarget()
    {
        Vector3 newVector = ImageTargetVector - ARCamera.transform.position;
        return newVector;
    }

    private Vector3 calcNewNetworkPos(Vector3 RecievedVector)
    {
        Vector3 normalisedVector = ImageTargetVector - RecievedVector;
        return normalisedVector;
    }

}
