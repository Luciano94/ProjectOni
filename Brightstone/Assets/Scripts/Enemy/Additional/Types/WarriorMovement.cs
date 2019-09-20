﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMovement : EnemyMovement{
    [Header("Strategies Variables")]
    [SerializeField] private float distFromPlayer;
    [SerializeField] private float rotationSpeedStr33;

    private float currentAngleStr31;
    private float currentAngleStr33;

    override public void ApplyMovementStrategy(int chaserIndex){
        base.ApplyMovementStrategy(chaserIndex);

        switch(EnemyBahaviour.Instance.warriorStrategy){
            // 1 enemy
            case Strategies.Melee11:

            break;
            case Strategies.Melee12:

            break;
            case Strategies.Melee13:

            break;
            case Strategies.Melee14:

            break;

            // 2 enemies
            case Strategies.Melee21:

            break;
            case Strategies.Melee22:

            break;
            case Strategies.Melee23:

            break;
            case Strategies.Melee24:

            break;

            // 3 enemies
            case Strategies.Melee31:
                Vector3 dirPlayerFwd31 = Vector3.forward;

                if (chaserIndex == 2)
                    dirPlayerFwd31 = Quaternion.AngleAxis(currentAngleStr31 + 225.0f, Vector3.forward) * dirPlayerFwd31;
                else
                    dirPlayerFwd31 = Quaternion.AngleAxis(currentAngleStr31 + chaserIndex * 45.0f, Vector3.forward) * dirPlayerFwd31;

                Vector3 objectivePos31 = GameManager.Instance.PlayerPos + dirPlayerFwd31 * distFromPlayer;

                if ((objectivePos31 - transform.position).magnitude > 0.1f)
                    MoveToObjective(objectivePos31);
            break;
            case Strategies.Melee32:
                Vector3 playerPos = GameManager.Instance.PlayerPos;
                Vector3 roomOrigin = GameManager.Instance.activeRoom.GetRoomsBehaviour().transform.position;
                roomOrigin.z = playerPos.z;
                Vector3 dirPlayerToRoom32 = (playerPos - roomOrigin).normalized;

                if (chaserIndex == 0)
                    dirPlayerToRoom32 = Quaternion.AngleAxis(45.0f, Vector3.forward) * dirPlayerToRoom32;
                else if (chaserIndex == 2)
                    dirPlayerToRoom32 = Quaternion.AngleAxis(-45.0f, Vector3.forward) * dirPlayerToRoom32;

                Vector3 objectivePos32 = playerPos + dirPlayerToRoom32 * distFromPlayer;

                if ((objectivePos32 - transform.position).magnitude > 0.1f)
                    MoveToObjective(objectivePos32);
            break;
            case Strategies.Melee33:
                currentAngleStr33 += rotationSpeedStr33 * Time.deltaTime;
                if (currentAngleStr33 >= 360.0f)
                    currentAngleStr33 -= 360.0f;
                
                Vector3 dirPlayerFwd33 = Vector3.forward;

                dirPlayerFwd33 = Quaternion.AngleAxis(currentAngleStr33 + chaserIndex * 120.0f, Vector3.forward) * dirPlayerFwd33;

                Vector3 objectivePos33 = GameManager.Instance.PlayerPos + dirPlayerFwd33 * distFromPlayer;

                if ((objectivePos33 - transform.position).magnitude > 0.1f)
                    MoveToObjective(objectivePos33);

            break;
            case Strategies.Melee34:

            break;
            default:

            break;
        }
    }
    public void RandomizeAngleStr31()
    {
        float newAngle = Random.Range(0.0f, 119.0f);
        currentAngleStr31 += newAngle * newAngle % 2 == 0 ? 1.0f : -1.0f;

        if (currentAngleStr31 >= 360.0f)
            currentAngleStr31 -= 360.0f;
        else if (currentAngleStr31 < 0.0f)
            currentAngleStr31 += 360.0f;
    }
}
