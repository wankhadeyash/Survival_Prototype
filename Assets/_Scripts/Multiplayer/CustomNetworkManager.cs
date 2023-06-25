using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.UIElements;
public class CustomNetworkManager : Mirror.NetworkManager
{
    public override void OnServerConnect(NetworkConnectionToClient conn) { }


}

