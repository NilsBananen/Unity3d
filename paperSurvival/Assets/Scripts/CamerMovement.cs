using UnityEngine;
using System.Collections;

public class CamerMovement : MonoBehaviour {

    private Vector3 camereTarget;

    private Transform target;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        camereTarget = new Vector3(target.position.x, transform.position.y, target.position.z - 8);
        transform.position = Vector3.Lerp(transform.position, camereTarget, Time.deltaTime * 8);
    }
}
