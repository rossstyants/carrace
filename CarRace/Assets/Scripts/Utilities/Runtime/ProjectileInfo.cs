using System.Collections;
using System.Collections.Generic;
//using Niantic.ARDK.Networking.HLAPI.Object.Unity;
using UnityEngine;

[System.Serializable]
public class ProjectileInfo
{
    public enum ProjectileType
    {
        None = -1,
        Shell,
        ScatterBomb,
        SlimeOozy,
        Laser,
        HomingMissile
    }

    public ProjectileType projectileType;
    public GameObject projectilePrefab;
//    public NetworkedUnityObject
    public GameObject explosionPrefab;
    public Definitions.ExplosionTypes explosionType;
    public Vector2 splashDamageRadius;
    public Vector2 splashDamagePower;
    public float directHitPower = 100f;

    public float maxScatter = 0.02f;
    public float scatterPower = 0.02f;
    public float GravityMultiplier = 1f;
    public int burstAmount = 1;
    public float burstGap = 0.1f;

    public float minPower = 0f;
    public float maxPower = 1f;


    //maybe these things should be on the explosion
    //as it's the explosion that causes the damage...
    //how far from explosion point does it destroy bricks...
    public float destructibleDistanceMin, destructibleDistanceMax;
    //how much health it knocks off the bricks in range
    public float destructPowerMin,destructPowerMax;

    //possible to move other snails with blast
    public bool canBlastPosition;
    public float directHitBlastPositionPower;
    public float blastPositionDistMin, blastPositionDistMax, blastPositionPowerMin, blastPositionPowerMax;
}
