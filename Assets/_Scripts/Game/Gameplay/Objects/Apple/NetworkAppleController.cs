using System;
using NaughtyAttributes;
using Services;
using Unity.Netcode;
using UnityEngine;

namespace Harvesting.Collectable.Apple
{
    public class NetworkAppleController : NetworkBehaviour, IAppleController
    {
        [Header("Apple properties"), HorizontalLine(color: EColor.Green)] [SerializeField]
        private AppleProperties[] _appleProperties;

        [SerializeField]
        private GameObject[] _appleStateObj;
        
        [SerializeField]
        private AppleProperties _currentProperties;
        
        private Apple _apple;

        public AppleType type => _apple.type;
        public int cost => _apple.cost;
        public Action onAppleDeactivate { get; set; }

        public override void OnNetworkSpawn()
        {
            _apple = new Apple(_appleProperties, _appleStateObj, this);

            if (!IsOwner)
            {
                RequestSyncServerRpc();
                return;                
            }
            
            onAppleDeactivate += RequestToReturnAppleServerRpc;
            _apple.SetUpTickHolder(OnTimeToChangeState);
        }

        public void RespawnApple(Vector2 pos)
        {
            SendRespawnAppleClientRpc(pos);
        }

        public void HideApple()
        {
            RequestToHideAppleServerRpc();
        }

        public void ChangeAppleState(int appleState)
        {
            if(IsServer) SendChangeAppleStateClientRpc(appleState);
        }
        
        private void OnTimeToChangeState() => _apple.ChangeState();

        #region [ RPC ]

        #region [ Server RPC ]
        [ServerRpc(RequireOwnership = false)]
        private void RequestToHideAppleServerRpc()
        {
            SendHideAppleClientRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        private void RequestSyncServerRpc()
        {
            print(gameObject.activeSelf);
            SendSyncClientRpc(transform.position, _apple.currentState, gameObject.activeSelf);
        }

        [ServerRpc]
        private void RequestToReturnAppleServerRpc()
        {
            print("Return");
            
            ServiceManager.Instance
                .LocalServices.Get<AppleSpawner>()
                ?.ReturnToPool(GetComponent<AppleHandler>());
        }
        #endregion

        #region [ Client RPC ]

        [ClientRpc(RequireOwnership = false)]
        private void SendRespawnAppleClientRpc(Vector2 pos)
        {
            _apple.Respawn(pos);
        }

        [ClientRpc(RequireOwnership = false)]
        private void SendHideAppleClientRpc()
        {
            _apple.Hide();
        }

        [ClientRpc(RequireOwnership = false)]
        private void SendChangeAppleStateClientRpc(int appleState)
        {
            _apple.SetState(appleState);
        }

        [ClientRpc]
        private void SendSyncClientRpc(Vector2 pos, int appleState, bool isActive)
        {
            transform.position = pos;
            _apple.SetState(appleState);
            gameObject.SetActive(isActive);
        }

        #endregion

        #endregion
    }
}

