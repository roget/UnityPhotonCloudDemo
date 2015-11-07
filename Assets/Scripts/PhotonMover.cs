using UnityEngine;
using System.Collections;

public class PhotonMover : MonoBehaviour
{
    private Vector3 targetPos;
    private Vector3 targetRot;
    private PhotonView photonView;
    private MoveByKeys moveByKeys;

    void Start()
    {
        targetPos = this.transform.position;
        targetRot = this.transform.eulerAngles;
        photonView = this.GetComponent<PhotonView>();
        moveByKeys = this.GetComponent<MoveByKeys>();
        moveByKeys.enabled = false;
    }

    void Update()
    {
        float moveSpeed = 3;
        if (PhotonNetwork.connectionStateDetailed != PeerState.Joined)
        {
            return;
        }
        if (PhotonNetwork.isMasterClient)
        {
            //若是Master则处理游戏逻辑
            if(!moveByKeys.enabled)
            {
                moveByKeys.enabled = true;
            }
            photonView.RPC("SetStatus", PhotonTargets.Others, transform.position, transform.eulerAngles);
        }
        else
        {
            //若是纯属客户端则处理显示
            this.transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
            this.transform.eulerAngles = targetRot;
        }
    }

    [PunRPC]
    void SetStatus(Vector3 newPos,Vector3 newRot)
    {
        targetPos = newPos;
        targetRot = newRot;
    }
}
