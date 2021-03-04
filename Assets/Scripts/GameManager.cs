using System;
using System.Collections;
using System.Collections.Generic;
using Dialogue;
using Events.NoType;
using UI;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum GameState
    {
        Cutscene,
        InGame,
        Pause
    }

    public GameState gameState;
    private DialogueManager _dm;
    public Menu menu;
    private GameState _previousState;
    [SerializeField] private EventNoType generateParticlesEvent, destroyParticlesEvent;
    [SerializeField] private DialogueTrigger dt;
    private void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.InGame;
        _dm = DialogueManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            //Se met en pause
            if (!menu.gameObject.activeSelf)
            {
                menu.gameObject.SetActive(true);
                menu.PauseUnpause();
            }
            else
            {
                menu.PauseUnpause();
                menu.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && gameState != GameState.Pause)
        {
            if (DialogueManager.instance.HasStarted)
            {
                _dm.ContinueOrDisplay();
            }
        }
    }

    public void StartCutscene()
    {
        if (gameState != GameState.Pause)
        {
            _previousState = gameState;
        }
        gameState = GameState.Cutscene;
    }

    public void StopCutscene()
    {
        gameState = _previousState;
    }

    public void Pause()
    {
        if (gameState == GameState.Pause)
        {
            StopCutscene();
        }
        else
        {
            _previousState = gameState;
            gameState = GameState.Pause;
        }
    }

    public void GenerateParticles()
    {
        generateParticlesEvent.Raise();
    }

    public void DestroyParticle()
    {
        destroyParticlesEvent.Raise();
    }
}
