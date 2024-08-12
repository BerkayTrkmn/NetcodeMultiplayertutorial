using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ShootProjectile : NetworkBehaviour
{
    [SerializeField] private GameObject projectilePref;
    [SerializeField] private Transform shootTransform;
    void Start()
    {
        
    }

    void Update()
    {
        if (!IsOwner) return;
        if(Input.GetKeyDown(KeyCode.X)) { 
        
            CreateProjectileServerRpc();
        }
    }
    [ServerRpc]
    public void CreateProjectileServerRpc()
    {
        GameObject _projectile  = Instantiate(projectilePref, shootTransform.position, shootTransform.rotation);
        _projectile.GetComponent<NetworkObject>().Spawn();
    }
}
