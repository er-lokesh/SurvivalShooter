using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour, IDataPersistence
{

    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCameraPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position,
            targetCameraPos, smoothing * Time.deltaTime);
    }

    public void LoadData(GameData data)
    {
        var playerData = data.cameraPositionData;
        transform.position = new Vector3(playerData.position.x, playerData.position.y, playerData.position.z);
        //transform.position = data.cameraPositionData.position;
    }

    public void SaveData(GameData data)
    {
		data.cameraPositionData.position = new SerializableVector3(transform.position.x, transform.position.y, transform.position.z);
        //data.cameraPositionData.position = transform.position;
    }

    public void ClearData(GameData data)
    {
        transform.position = new Vector3(1, 15, -22);
    }
}