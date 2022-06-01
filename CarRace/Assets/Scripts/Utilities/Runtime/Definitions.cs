using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Definitions : MonoBehaviour
{
    public enum EntityEnum
    {
        Human,
        AI
    }

    public enum Powerups
    {
        Undefined = 0,
        Health,
        SlimeOoozy,
        HomingMissile,
        TeleportAnywhere
    }

    public enum GameType
    {
        TurnBased = 0,
        LiveBattle,
    }

    public enum WeaponTypes
    {
        Undefined = -1,
        Canon = 0,
        BlockOfBricks,
        SlimeUzi,   //Ooozy
        Laser,
        Balloon,
        HomingMissile,
        TelportAnywhere,
        Count
    }
    public enum ExplosionTypes
    {
        Shell = 0,
        SlimeUzi,
        Count
    }
    public enum TriggerTypes
    {
        Simple = 0,
        Circular,
    }
    public enum MovementTypes
    {
        Undefined = 0,
        Snail,
        Balloon,
    }
    public enum AimConeType
    {
        None = 0,
        Canon,
        Laser,
        Ooozy
    }
}
