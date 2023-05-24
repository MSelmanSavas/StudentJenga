using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaStacker : IJengaStacker
{
    Vector3 _stackBasePosition;
    Transform _stackParent;

    IJengaBrickFactory _jengaBrickFactory;
    List<IJengaBrick> _stackedBricks = new();
    List<SubjectInfo> _subjectInfos = new();

    ScriptableJengaStackerConfig _config;
    TextMesh _stackText;

    List<Vector3> _localPositionsEvenLayers = new List<Vector3>
        {
            new Vector3(0,0,-2.5f),
            new Vector3(0,0,0f),
            new Vector3(0,0,2.5f),
        };

    List<Vector3> _localPositionsOddLayers = new List<Vector3>
        {
            new Vector3(-2.5f,0,0),
            new Vector3(0f,0,0),
            new Vector3(2.5f,0,0),
        };


    Vector3 _evenLayerRotation = Vector3.zero;
    Vector3 _oddLayerRotation = new Vector3(0, 90f, 0);
    float _layerHeight = 1.5f;


    public JengaStacker(IJengaBrickFactory factory, List<SubjectInfo> subjectInfos, ScriptableJengaStackerConfig config)
    {
        _jengaBrickFactory = factory;
        _subjectInfos = subjectInfos;
        _config = config;
    }

    public bool TrySetConfig(ScriptableJengaStackerConfig config) => _config = config;

    public Vector3 GetStackPosition() => _stackBasePosition;
    public void SetStackPosition(Vector3 position) => _stackBasePosition = position;

    public List<IJengaBrick> GetJengaBricks() => _stackedBricks;

    public bool TrySetJengaFactory(IJengaBrickFactory factory)
    {
        _jengaBrickFactory = factory;
        return true;
    }


    public List<SubjectInfo> GetSubjectInfos() => _subjectInfos;

    public bool TrySetSubjectInfo(List<SubjectInfo> subjectInfos)
    {
        _subjectInfos = new List<SubjectInfo>(subjectInfos);
        return true;
    }

    public bool TryBuildJengaStack()
    {
        if (_subjectInfos == null || _subjectInfos.Count <= 0)
        {
            Debug.LogError("There are no subject infos to build a jenga stack! Aborting building...");
            return false;
        }

        if (_stackParent == null)
        {
            _stackParent = new GameObject("JengaStack").transform;
            _stackParent.position = _stackBasePosition;
        }


        int currentJengaIndex = 0;
        int currentJengaLayer = 0;

        int maxJengaPerLayer = 3;

        foreach (var subjectInfo in _subjectInfos)
        {
            _jengaBrickFactory.TryGetJengaBrick(new JengaFactoryData { MasteryLevel = (MasteryLevel)subjectInfo.Mastery }, out IJengaBrick jengaBrick);

            jengaBrick.GetTransform().SetParent(_stackParent);
            jengaBrick.SetJengaStacker(this);
            _stackedBricks.Add(jengaBrick);

            bool isEven = currentJengaLayer % 2 == 0;

            Vector3 position = _stackBasePosition;
            position += isEven ? _localPositionsEvenLayers[currentJengaIndex] : _localPositionsOddLayers[currentJengaIndex];

            position.y += currentJengaLayer * _layerHeight;

            jengaBrick.SetStartPositionAndCurrentPosition(position);

            Vector3 rotation = isEven ? _evenLayerRotation : _oddLayerRotation;
            jengaBrick.SetStartRotationAndCurrentRotation(rotation);

            jengaBrick.SetSubjectInfo(subjectInfo);

            currentJengaIndex++;

            if (currentJengaIndex % maxJengaPerLayer == 0)
            {
                currentJengaIndex = 0;
                currentJengaLayer++;
            }
        }

        AddStackNameDisplay();

        return true;
    }

    void AddStackNameDisplay()
    {
        GameObject stackTextGO = GameObject.Instantiate(_config.GradeTextPrefab);

        stackTextGO.transform.SetParent(_stackParent);

        _stackText = stackTextGO.GetComponent<TextMesh>();

        _stackText.text = _subjectInfos[0].Grade;
        _stackText.transform.localPosition = Vector3.back * 5f;
    }

}
