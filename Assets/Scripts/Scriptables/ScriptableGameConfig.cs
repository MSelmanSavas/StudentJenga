using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewGameConfig", menuName = "StudentJenga/Create/Scriptable Objects/Game Config")]
public class ScriptableGameConfig : ScriptableObject
{
    [field: SerializeField]
    public string SubjectInfoApiURI { get; private set; }
    public ScriptableJengaPieceInfo JengaPieceInfos;
    public ScriptableJengaStackerConfig JengaStackerConfig;
}
