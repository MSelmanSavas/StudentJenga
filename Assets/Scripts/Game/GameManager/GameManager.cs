using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    ScriptableGameConfig _gameConfig;

    ISubjectsLoaderPerGrade _subjectsLoader;
    IJengaBrickFactory _jengaBrickFactory;
    List<IJengaStacker> _jengaStackers = new();
    IJengaStacker _currentlySelectedStacker;

    [SerializeField]
    IJengaStackEvent _onSelectedJengaStackChange;


    private void OnEnable()
    {
        RefBook.Add(this);
    }

    private void OnDisable()
    {
        RefBook.Remove(this);
    }


    private void Start()
    {
        Initialize();
        TryStartGame();
    }

    void Initialize()
    {
        _jengaBrickFactory = new JengaBrickFactory(_gameConfig.JengaPieceInfos);
        _subjectsLoader = new SubjectsLoader();
    }

    async void TryStartGame()
    {
        bool response = await _subjectsLoader.TryLoadSubjects(_gameConfig.SubjectInfoApiURI);

        if (response == false)
        {
            Debug.LogError("Error while loading subject datas! Cannot continue spawning jengas! Aborting...");
            return;
        }

        _subjectsLoader.Sort(new SubjectsSorter());

        LoadJengas();
    }

    void LoadJengas()
    {
        _jengaStackers.Clear();

        List<string> grades = _subjectsLoader.GetGrades();

        Vector3 startPosition = Vector3.zero;
        Vector3 offsetPosition = Vector3.right * 10;

        Vector3 currentPosition = startPosition;

        foreach (string grade in grades)
        {
            IJengaStacker stacker = new JengaStacker(_jengaBrickFactory, _subjectsLoader.GetSubjectInfosPerGrade()[grade], _gameConfig.JengaStackerConfig);

            _jengaStackers.Add(stacker);

            stacker.SetStackPosition(currentPosition);
            stacker.TryBuildJengaStack();

            currentPosition += offsetPosition;
        }

        _currentlySelectedStacker = _jengaStackers.Count > 0 ? _jengaStackers[0] : null;
        _onSelectedJengaStackChange?.Raise(_currentlySelectedStacker);
    }

    public void OnJengaBrickClicked(IJengaBrick jengaBrick)
    {
        _currentlySelectedStacker = jengaBrick.GetJengaStacker();
        _onSelectedJengaStackChange?.Raise(_currentlySelectedStacker);
    }

    public void TestMyStack()
    {
        if (_currentlySelectedStacker == null)
        {
            Debug.LogError("Cannot test my stack! There is not stack selected...");
            return;
        }

        List<IJengaBrick> jengaBricks = _currentlySelectedStacker.GetJengaBricks();

        foreach (IJengaBrick jengaBrick in jengaBricks)
        {
            if (jengaBrick.GetSubjectInfo().Mastery == MasteryLevel.NeedToLearn)
            {
                jengaBrick.GetTransform().gameObject.SetActive(false);
                continue;
            }

            Rigidbody jengaRB = jengaBrick.GetRigidbody();
            jengaRB.isKinematic = false;
        }
    }

    public void ResetStack()
    {
        if (_currentlySelectedStacker == null)
        {
            Debug.LogError("Cannot reset stack! There is not stack selected...");
            return;
        }

        List<IJengaBrick> jengaBricks = _currentlySelectedStacker.GetJengaBricks();

        foreach (IJengaBrick jengaBrick in jengaBricks)
        {
            jengaBrick.GetTransform().gameObject.SetActive(true);


            Rigidbody jengaRB = jengaBrick.GetRigidbody();
            jengaRB.isKinematic = true;

            Vector3 startPosition = jengaBrick.GetStartPosition();
            Vector3 startRotation = jengaBrick.GetStartRotation();

            jengaBrick.SetStartPositionAndCurrentPosition(startPosition);
            jengaBrick.SetStartRotationAndCurrentRotation(startRotation);
        }
    }
}
