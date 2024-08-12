using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float positionRange = 3f;
    Animator playerAnimator;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    public override void OnNetworkSpawn()
    {
        UpdatePositionServerRpc();
    }
    void FixedUpdate()
    {
        if (!IsOwner) return;
        float _moveHorizontal = Input.GetAxis("Horizontal");
        float _moveVertical = Input.GetAxis("Vertical");

        Vector3 _movement = new Vector3(_moveHorizontal, 0f, _moveVertical);

        if (_movement.magnitude > 0.1f)
        {
            Quaternion _targetRotation = Quaternion.LookRotation(_movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, _targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        rb.MovePosition(transform.position+ (_movement*speed*Time.fixedDeltaTime));
        playerAnimator.SetFloat("Speed", _movement.magnitude);

    }

    [ServerRpc(RequireOwnership =false)] 
    private void UpdatePositionServerRpc()
    {
        if (!IsOwner) return;
        transform.position = new Vector3(Random.Range(positionRange, -positionRange), 0, Random.Range(positionRange, -positionRange));
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }
}
