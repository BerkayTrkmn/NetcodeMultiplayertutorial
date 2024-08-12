using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelloScripts;
public class LookTowards : MonoBehaviour
{

   public Transform lookObject;
    private void Start()
    {
        lookObject = Camera.main.transform;
    }
    void Update()
    {
        Vector3 rotation = Quaternion.LookRotation(transform.position - lookObject.position).eulerAngles;
        transform.eulerAngles = transform.eulerAngles.ChangeX(rotation.x);
    }
}
