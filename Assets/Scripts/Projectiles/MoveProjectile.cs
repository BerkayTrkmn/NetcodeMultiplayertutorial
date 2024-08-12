using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MoveProjectile : NetworkBehaviour
{
    [SerializeField] private float moveSpeed=5f;
    private Rigidbody rb;
    private float projectileLifeTime = 2f;
    private NetworkObject networkObject;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        networkObject = GetComponent<NetworkObject>();
        Invoke("DestroyProjectileServerRpc", projectileLifeTime);
    }

    void Update()
    {
        rb.velocity=rb.transform.forward * moveSpeed;

    }
    [ServerRpc(RequireOwnership =false)]
    private void DestroyProjectileServerRpc()
    {
      networkObject.Despawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!IsOwner) return;
        DestroyProjectileServerRpc();
    }
}
