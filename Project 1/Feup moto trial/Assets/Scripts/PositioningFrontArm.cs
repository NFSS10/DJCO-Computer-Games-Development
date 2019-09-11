using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositioningFrontArm : MonoBehaviour
{

	public GameObject frontWheel;
	public GameObject bikebody;
	
	private Vector3 offset;
	
	// Use this for initialization
	void Start ()
	{
		offset = frontWheel.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		transform.position = bikebody.transform.position;
		transform.rotation = bikebody.transform.rotation;
		transform.position = frontWheel.transform.position - offset;
	}
}
