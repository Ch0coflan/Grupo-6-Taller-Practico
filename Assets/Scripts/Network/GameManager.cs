using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Photon.Pun.Demo.PunBasics;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public GameObject ballPrefab;
    public Transform[] spawnPoints;
    public Transform spawnBall;


    private void Start()
    {
        StartCoroutine(SpawnPlayersWhenReady());
        if (PhotonNetwork.IsMasterClient)
        {
            // Solo el MasterClient instancia la pelota para asegurar sincronización
            Vector3 spawnPosition = spawnBall.position; // Posición de spawn inicial
            Quaternion spawnRotation = Quaternion.identity; // Rotación inicial

            // Instanciar la pelota a través de PhotonNetwork
            PhotonNetwork.Instantiate(ballPrefab.name, spawnPosition, spawnRotation);
        }
    }

    private IEnumerator SpawnPlayersWhenReady()
    {
        while (PhotonNetwork.CurrentRoom.PlayerCount < PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2); // Espera adicional para asegurar que todos los jugadores están listos

         if (PhotonNetwork.LocalPlayer != null)
        {
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if (PhotonNetwork.PlayerList[i] == PhotonNetwork.LocalPlayer)
                {
                    Vector3 spawnPosition = spawnPoints[i].position;
                    Quaternion spawnRotation = spawnPoints[i].rotation;

                    GameObject playerObj = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, spawnRotation);
                    //AirplaneMovement controller = playerObj.GetComponent<AirplaneMovement>();
                    CameraWork cameraWork = playerObj.GetComponent<CameraWork>();

                    if (cameraWork != null)
                    {
                        cameraWork.OnStartFollowing();
                    }
                    else
                    {
                        Debug.LogError("No CameraWork script found on playerPrefab");
                    }
                    break;
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Jugador entró: " + newPlayer.NickName);
        StartCoroutine(SpawnPlayersWhenReady());
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Jugador salió: " + otherPlayer.NickName);
        // Opcional: manejar la lógica cuando un jugador se desconecta
    }
}
