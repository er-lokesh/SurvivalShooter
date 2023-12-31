﻿using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, IDataPersistence
{
 
	public float speed = 6f;

	Vector3 movement;
	Animator animator;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;

	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		animator = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move(h, v);
		Turning();
		Animating(h, v);
	}

	void Move(float h, float v)
	{
		movement.Set (h, 0, v);
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0;

			var rotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(rotation);
		}
	}

	void Animating(float h, float v)
	{
		bool walking = h != 0f || v != 0f;
		animator.SetBool("IsWalking", walking);
	}

    public void LoadData(GameData data)
    {
		var playerData = data.playerMovementData;
		transform.position = new Vector3(playerData.position.x, playerData.position.y, playerData.position.z);
		transform.rotation = Quaternion.Euler(playerData.rotation.x, playerData.rotation.y, playerData.rotation.z);
	}

	public void SaveData(GameData data)
	{
        data.playerMovementData.position = new SerializableVector3(transform.position.x, transform.position.y, transform.position.z);
        data.playerMovementData.rotation = new SerializableVector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
    }

    public void ClearData(GameData data)
    {
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
    }
}