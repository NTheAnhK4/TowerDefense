using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelData", menuName = "Data/Level Data")]
public class LevelData : ScriptableObject
{
    public List<LevelParam> Levels;
}

[Serializable]
public class LevelParam
{
    public int InitialGold;
    public GameObject LevelPrefab;
    public List<WayParam> Ways;
    public List<PathParam> Paths;
    public List<TowerInfor> TowerInfors;
}

[Serializable]
public class PathParam
{
    public Vector3 SignalPosition;
    public float SignalAngle;
    public List<Vector3> Positions;
}

[Serializable]
public class MiniWayParam
{
    public int PathId;
   
    public List<EnemyInfor> EnemyInfors;
}

[Serializable]
public class EnemyInfor
{
    public int EnemyId;
    public EnemyType EnemyType;
}
[Serializable]
public class WayParam
{
    public List<MiniWayParam> MiniWays;
}

[Serializable]
public class TowerInfor
{
    public Vector3 Towerposition;
    public Vector3 flagPosition;
}
public enum EnemyType
{
    MeleeAttack,
    RangedAttack
}