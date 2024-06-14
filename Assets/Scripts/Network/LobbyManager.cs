using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField lobby;
    public Menu menu;
    
    public void CreateLobby()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(lobby.text, roomOptions);
        Debug.Log("Creando sala... " + lobby.text);
        LoadingArea.Instance.ShowLoading();
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinRoom(lobby.text);
        Debug.Log("Uniendose a " + lobby.text);
        LoadingArea.Instance.ShowLoading();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Fallo la creacion de la sala: " + message);
        menu.GoBackLobbyButton();
        LoadingArea.Instance.StopLoading();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Fallo la union a la sala: " + message);
        menu.GoBackLobbyButton();
        LoadingArea.Instance.StopLoading();
    }
    
    public override void OnJoinedRoom()
    {
        Debug.Log("Jugador unido a la sala.");
        CheckAndLoadGame();
        LoadingArea.Instance.StopLoading();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Jugador entro: " + newPlayer.NickName);
        CheckAndLoadGame();
    }

    private void CheckAndLoadGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            // TODO: COLOCAR AQUI LA ESCENA
            PhotonNetwork.LoadLevel("testeo intenso y sexoso");
        }
    }
}