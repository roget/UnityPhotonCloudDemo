using UnityEngine;
using System.Collections;

public class PhotonMoverStream : Photon.MonoBehaviour
{
    private Vector3 correctPlayerPos = Vector3.zero;
    private Vector3 correctPlayerRot = Vector3.zero;

    private MoveByKeys moveByKeys;
    private string sendAniName = "";//发送的动画
    private string receiveAniName = "";//接收的动画

    void Awake()
    {
        correctPlayerPos = this.transform.position;
        correctPlayerRot = this.transform.eulerAngles;
        moveByKeys = this.GetComponent<MoveByKeys>();
        moveByKeys.enabled = false;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.eulerAngles);
            stream.SendNext(sendAniName);
        }
        else
        {
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Vector3)stream.ReceiveNext();
            receiveAniName = (string)stream.ReceiveNext();
        }
    }

    void Update()
    {
        float moveSpeed = 3;
        if(PhotonNetwork.connectionStateDetailed != PeerState.Joined)
        {
            return;
        }
        if (PhotonNetwork.isMasterClient)
        {
            if(!moveByKeys.enabled)
            {
                moveByKeys.enabled = true;
            }
            sendAniName = "";
        }
        else
        {
            this.transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * moveSpeed);
            this.transform.eulerAngles = correctPlayerRot;
            if(receiveAniName.Length>0)
            {
                this.animation.Play(receiveAniName);
            }
        }
    }
}
