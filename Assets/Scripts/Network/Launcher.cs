using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public void StartNetwork()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "sa";
        PhotonNetwork.ConnectUsingSettings();
        LoadingArea.Instance.ShowLoading();
    }

    public void DisconnectLobby()
    {
        PhotonNetwork.Disconnect();
        LoadingArea.Instance.ShowLoading();
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("Nos hemos unido al server..." + PhotonNetwork.CloudRegion);
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("True");
        LoadingArea.Instance.StopLoading();
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError("Desconectado del servidor por" + cause);
        LoadingArea.Instance.StopLoading();
    }
}
