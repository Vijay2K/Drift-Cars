using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private Transform target = null;

    private void LateUpdate() {
        transform.position = target.position;
    }
}
