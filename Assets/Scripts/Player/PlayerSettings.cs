using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerSettings : NetworkBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private TextMeshProUGUI playerNameText;
    private NetworkVariable<FixedString128Bytes> networkPlayerName = new NetworkVariable<FixedString128Bytes>("Player: 0",NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);

    public List<Color> colors = new List<Color>();

    public override void OnNetworkSpawn()
    {
        networkPlayerName.Value = "Player " +(OwnerClientId + 1);
        playerNameText.text = networkPlayerName.Value.ToString();
        meshRenderer.material.color = colors[(int)OwnerClientId];
    }


}
