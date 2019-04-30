﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRoom : MonoBehaviour
{
    private Transform activeRoom = null;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 15){
            if(activeRoom != null){
                ChangeLayer(15);
            }
            activeRoom = other.gameObject.GetComponent<RoomReference>().thePadre;
            if(!activeRoom.GetComponent<RoomsBehaviour>().Complete){
                activeRoom.GetComponent<RoomsBehaviour>().ActiveEnemies();
                activeRoom.GetComponent<DoorManager>().ActiveRoom();
            }
            ChangeLayer(9);
            Camera.main.GetComponent<CameraFollow>().MoveTo(activeRoom.position);
        }
    }

    private void ChangeLayer(int layer){
        foreach (Transform child in activeRoom){
            if(child.gameObject.layer != layer)
                child.gameObject.layer = layer;
        }
    }
}
