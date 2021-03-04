using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private List<PlayableDirector> directors;

    private int _indexDirector;

    public List<PlayableDirector> Directors
    {
        get => directors;
        set => directors = value;
    }

    private void Start()
    {
        directors[_indexDirector].Play();
    }

    public void IncreaseIndex()
    {
        _indexDirector++;
    }

    public void PlayDirector()
    {
        if (_indexDirector < directors.Count)
        {
            directors[_indexDirector].gameObject.SetActive(true);
            directors[_indexDirector].Play();
        }
    }
}
