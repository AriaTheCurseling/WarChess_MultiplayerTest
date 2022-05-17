using Unity.Netcode;
using UnityEngine;

public class HelloWorldPlayer : NetworkBehaviour
{
    bool visible = true;

    private void Start()
    {
        if (IsOwner)
            MoveServerRpc();
    }

    [ServerRpc]
    public void MoveServerRpc()
    {
        transform.position = GetRandomPositionOnPlane();
    }

    [ServerRpc]
    public void SneakyServerRpc()
    {
        visible = !visible;

        foreach (ulong clientID in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (clientID == 0 || clientID == OwnerClientId)
                continue;

            if (visible)
                NetworkObject.NetworkShow(clientID);
            else
                NetworkObject.NetworkHide(clientID);
        }
    }

    private Vector3 GetRandomPositionOnPlane()
    {
        return new Vector3(Random.Range(0, 8) - 3.5f, (Random.Range(0, 8) - 3.5f) * 14f / 16f);
    }
}
