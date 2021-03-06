using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using static HelloWorldManager;

public class HelloWorldManager : MonoBehaviour
{
    public SO_GameMode gameMode;

    private string _lobbyID;

    private RelayData _hostData;
    private RelayData _joinData;

    private async void Start()
    {
        await UnityServices.InitializeAsync();

        SetupEvents();

        await SignInAnonymouslyAsync();
    }

    void SetupEvents()
    {
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");


            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");
        };

        AuthenticationService.Instance.SignInFailed += (err) =>
        {
            Debug.Log(err);
        };

        AuthenticationService.Instance.SignedOut += () =>
        {
            Debug.Log("Player signed out");
        };
    }

    async Task SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in anonymously succeeded!");
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        
        StatusLabels();
        SubmitNewPosition();

        GUILayout.EndArea();
    }

    //static void StartButtons()
    //{
    //    if (GUILayout.Button("Host")) 
    //        NetworkManager.Singleton.StartHost();
    //    if (GUILayout.Button("Client")) 
    //        NetworkManager.Singleton.StartClient();
    //    if (GUILayout.Button("Server")) 
    //        NetworkManager.Singleton.StartServer();
    //}

    static void StatusLabels()
    {
        var mode = NetworkManager.Singleton.IsHost ?
            "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " + NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }

    static void SubmitNewPosition()
    {
        var player = NetworkManager.Singleton.LocalClient?.PlayerObject?.GetComponent<HelloWorldPlayer>();

        if (player is null)
            return;

        if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
            player.MoveServerRpc();

        if (GUILayout.Button("Sneaky"))
            player.SneakyServerRpc();
    }

    public async void FindMatch()
    {
        Debug.Log("Looking for a lobby...");

        try
        {
            QuickJoinLobbyOptions options = new QuickJoinLobbyOptions();

            Lobby lobby = await Lobbies.Instance.QuickJoinLobbyAsync();

            Debug.Log($"Joined lobby: {lobby.Id}");

            string joinCode = lobby.Data["joinCode"].Value;

            JoinAllocation allocation = await Relay.Instance.JoinAllocationAsync(joinCode);

            _joinData = new RelayData(allocation);

            NetworkManager.Singleton.GetComponent<UnityTransport>()
                .SetRelayServerData(_joinData)
                .StartClient();
        }
        catch (Exception e)
        {
            CreateMatch();
        }
    }

    private async void CreateMatch()
    {
        Allocation allocation = await Relay.Instance.CreateAllocationAsync(1);

        _hostData = new RelayData(allocation);

        var options = new CreateLobbyOptions();

        options.Data = new Dictionary<string, DataObject>
        {
            {
                "joinCode", new DataObject(
                    DataObject.VisibilityOptions.Member,
                    await _hostData.GetJoinCodeAsync())
            },
        };

        Lobby lobby = await Lobbies.Instance.CreateLobbyAsync("AutoGeneratedLobby", 2, options);

        _lobbyID = lobby.Id;

        Debug.Log($"Created lobby: {lobby.Id}");

        StartCoroutine(HeartbeatLobbyCoroutine(lobby.Id, 15));

        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(_hostData);

        NetworkManager.Singleton.StartHost();
    }

    IEnumerator HeartbeatLobbyCoroutine(string id, int v)
    {
        var delay = new WaitForSecondsRealtime(v);
        while (true)
        {
            Lobbies.Instance.SendHeartbeatPingAsync(id);
            yield return delay;
        }
    }

    public struct RelayData
    {
        public string IPv4Address;
        public ushort Port;
        public Guid AllocationID;
        public byte[] AllocationIDBytes;
        public byte[] ConnectionData;
        public byte[] HostConnectionData;
        public byte[] Key;

        public async Task<string> GetJoinCodeAsync() => await Relay.Instance.GetJoinCodeAsync(AllocationID);

        public RelayData(Allocation allocation)
        {
            Key = allocation.Key;
            Port = (ushort)allocation.RelayServer.Port;
            AllocationID = allocation.AllocationId;
            AllocationIDBytes = allocation.AllocationIdBytes;
            ConnectionData = allocation.ConnectionData;
            HostConnectionData = null;
            IPv4Address = allocation.RelayServer.IpV4;
        }

        public RelayData(JoinAllocation allocation)
        {
            Key = allocation.Key;
            Port = (ushort)allocation.RelayServer.Port;
            AllocationID = allocation.AllocationId;
            AllocationIDBytes = allocation.AllocationIdBytes;
            ConnectionData = allocation.ConnectionData;
            HostConnectionData = null;
            IPv4Address = allocation.RelayServer.IpV4;
        }
    }
}

internal static class UnityTransportExtentions
{
    public static UnityTransport SetRelayServerData(this UnityTransport unityTransport, RelayData relayData)
    {
        unityTransport.SetRelayServerData(
            relayData.IPv4Address,
            relayData.Port,
            relayData.AllocationIDBytes,
            relayData.Key,
            relayData.ConnectionData,
            relayData.HostConnectionData);

        return unityTransport;
    }
}
