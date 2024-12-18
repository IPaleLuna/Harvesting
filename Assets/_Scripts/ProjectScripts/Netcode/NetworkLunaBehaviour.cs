using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace PaleLuna.Network
{
    public abstract class NetworkLunaBehaviour : NetworkBehaviour
    {
        public abstract void InitNetworkBehaviour();
    }
}


