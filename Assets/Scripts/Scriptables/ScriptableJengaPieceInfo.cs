using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewJengaInfoDictionary", menuName = "StudentJenga/Create/Scriptable Objects/Jenga Info Dictionary")]
public class ScriptableJengaPieceInfo : ScriptableObject
{
    public SerializedDictionary<MasteryLevel, JengaBrickInfo> JengaInfos;
}


[System.Serializable]
public class JengaBrickInfo
{
    public GameObject Prefab;
    public Material Material;
    public PhysicMaterial PhysicMaterial;
}

public enum MasteryLevel
{
    NeedToLearn,
    Learned,
    Mastered
}