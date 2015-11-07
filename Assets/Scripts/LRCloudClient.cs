using UnityEngine;
using ExitGames.Client.Photon;

public class LRCloudClient : IPhotonPeerListener
{
    PhotonPeer peer;
    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

    public bool Connect()
    {
        stopwatch.Start();

        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        //连接Photon Cloud
        if (peer.Connect("app.exitgamescloud.com:5055", "d2482745-4d35-474c-96ce-4f5362e385a6"))
        {
            return true;
        }
        ////连接Photon Server
        //if (peer.Connect("localhost:5055", "LRServer"))
        //{
        //    return true;
        //}
        return false;
    }

    public void Disconnect()
    {
        peer.Disconnect();
    }

    public void Service()
    {
        peer.Service();
    }

    public void DebugReturn(DebugLevel level, string message)
    {
    }

    public void OnEvent(EventData eventData)
    {
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log("OnStatusChanged:" + statusCode.ToString());
        switch (statusCode)
        {
            case StatusCode.Connect:
                stopwatch.Stop();
                Debug.Log(string.Format("连线成功,耗时：{0}秒",stopwatch.Elapsed.TotalSeconds.ToString()));
                stopwatch.Reset();
                break;
            case StatusCode.Disconnect:
                Debug.Log("断线");
                break;
            case StatusCode.DisconnectByServerUserLimit:
                Debug.Log("人数达上线");
                break;
            case StatusCode.ExceptionOnConnect:
                Debug.Log("连线时意外错误");
                break;
            case StatusCode.DisconnectByServer:
                Debug.Log("被Server强制断线");
                break;
            case StatusCode.TimeoutDisconnect:
                Debug.Log("超时断线");
                break;
            case StatusCode.Exception:
            case StatusCode.ExceptionOnReceive:
                Debug.Log("其他例外");
                break;
        }
    }
}
