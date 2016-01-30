using UnityEngine;
using System.Collections;
using GamepadInput;

public enum Mode
{
    None, Action, Move
}

public class ControlManager : MonoBehaviour
{
    private int             _newTileIndex;

    public static   ControlManager  instance        = null;
    public          bool            loadMove        = false;
    public          bool            releaseMove     = false;   
    public          bool            loadAction      = false;
    public          bool            releaseAction   = false;
    public          bool            isReleased      = false;

    public          bool            isTriggered = false;
    public          Mode            currentMode = Mode.None;
    public          GamePad.Button  buttonTriggered = GamePad.Button.None;

    public          int             newTileIndex { get { return _newTileIndex; } }

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    private void Start ()
	{
	}
	
	// Update is called once per frame
	private void Update ()
	{

        GamePad.Index controller = GamePad.Index.One;
        GamepadState state = GamePad.GetState(controller);

        switch (Game.instance.state)
        {
            case GameState.INTRO:
                UpdateIntro(state);
                break;

            case GameState.GAME:
                controller = Game.instance.currentPlayer.controllerIndex;
                state = GamePad.GetState(controller);
                UpdateGame(state);
                break;                
        }
	}

    private void UpdateIntro (GamepadState state)
    {

    }

    private void UpdateGame (GamepadState state)
    {

        // No action if not your turn
        if (Game.instance.currentPlayer.canAction == false)
        {
            return;
        }

        // Do you press a button ?
        if(isTriggered == false)
        {
            /* Actions */
            if (GamePad.GetButtonDown(GamePad.Button.A, Game.instance.currentPlayer.controllerIndex) == true)
            {
                buttonTriggered = GamePad.Button.A;
                isTriggered = true;
                currentMode = Mode.Action;
                return;
            }
            if (GamePad.GetButtonDown(GamePad.Button.B, Game.instance.currentPlayer.controllerIndex) == true)
            {
                buttonTriggered = GamePad.Button.B;
                isTriggered = true;
                currentMode = Mode.Action;
                return;
            }
            if (GamePad.GetButtonDown(GamePad.Button.X, Game.instance.currentPlayer.controllerIndex) == true)
            {
                buttonTriggered = GamePad.Button.X;
                isTriggered = true;
                currentMode = Mode.Action;
                return;
            }
            if (GamePad.GetButtonDown(GamePad.Button.Y, Game.instance.currentPlayer.controllerIndex) == true)
            {
                buttonTriggered = GamePad.Button.Y;
                isTriggered = true;
                currentMode = Mode.Action;
                return;
            }

            /* Moves */
            if (state.Up == true)
            {
                int tmpTileIndex = Game.instance.currentPlayer.tileIndex - (1 * Game.instance.currentPlayer.speed * Game.instance.mapManager.map.columns);
                if (Game.instance.currentPlayer.CanMove(tmpTileIndex) == true)
                {
                    buttonTriggered = GamePad.Button.Up;
                    isTriggered = true;
                    currentMode = Mode.Move;
                    _newTileIndex = tmpTileIndex;
                    return;
                }
            }
            if (state.Right == true)
            {
                int tmpTileIndex = Game.instance.currentPlayer.tileIndex + (1 * Game.instance.currentPlayer.speed);
                if (Game.instance.currentPlayer.CanMove(tmpTileIndex) == true)
                {
                    buttonTriggered = GamePad.Button.Right;
                    isTriggered = true;
                    currentMode = Mode.Move;
                    _newTileIndex = tmpTileIndex;
                    return;
                }
            }
            if (state.Down == true)
            {
                int tmpTileIndex = Game.instance.currentPlayer.tileIndex + (1 * Game.instance.currentPlayer.speed * Game.instance.mapManager.map.columns);
                if (Game.instance.currentPlayer.CanMove(tmpTileIndex) == true)
                {
                    buttonTriggered = GamePad.Button.Down;
                    isTriggered = true;
                    currentMode = Mode.Move;
                    _newTileIndex = tmpTileIndex;
                    return;
                }
            }
            if (state.Left == true)
            {
                int tmpTileIndex = Game.instance.currentPlayer.tileIndex - (1 * Game.instance.currentPlayer.speed);
                if (Game.instance.currentPlayer.CanMove(tmpTileIndex) == true)
                {
                    buttonTriggered = GamePad.Button.Left;
                    isTriggered = true;
                    currentMode = Mode.Move;
                    _newTileIndex = tmpTileIndex;
                    return;
                }
            }
        }
        // Do you release a button ?
        else
        {
            if (buttonTriggered == GamePad.Button.A && GamePad.GetButtonUp(GamePad.Button.A, Game.instance.currentPlayer.controllerIndex) == true)
            {
                buttonTriggered = GamePad.Button.None;
                isTriggered = false;
                currentMode = Mode.None;
                return;
            }
            if (buttonTriggered == GamePad.Button.B && GamePad.GetButtonUp(GamePad.Button.B, Game.instance.currentPlayer.controllerIndex) == true)
            {
                buttonTriggered = GamePad.Button.None;
                isTriggered = false;
                currentMode = Mode.None;
                return;
            }
            if (buttonTriggered == GamePad.Button.X && GamePad.GetButtonUp(GamePad.Button.X, Game.instance.currentPlayer.controllerIndex) == true)
            {
                buttonTriggered = GamePad.Button.None;
                isTriggered = false;
                currentMode = Mode.None;
                return;
            }
            if (buttonTriggered == GamePad.Button.Y && GamePad.GetButtonUp(GamePad.Button.Y, Game.instance.currentPlayer.controllerIndex) == true)
            {
                buttonTriggered = GamePad.Button.None;
                isTriggered = false;
                currentMode = Mode.None;
                return;
            }
            if (buttonTriggered == GamePad.Button.Up && state.Up == false)
            {
                buttonTriggered = GamePad.Button.None;
                isTriggered = false;
                currentMode = Mode.None;
                ActionLoader.instance.CleanHighlight(_newTileIndex);
                return;
            }
            if (buttonTriggered == GamePad.Button.Right && state.Right == false)
            {
                buttonTriggered = GamePad.Button.None;
                isTriggered = false;
                currentMode = Mode.None;
                ActionLoader.instance.CleanHighlight(_newTileIndex);
                return;
            }
            if (buttonTriggered == GamePad.Button.Down && state.Down == false)
            {
                buttonTriggered = GamePad.Button.None;
                isTriggered = false;
                currentMode = Mode.None;
                ActionLoader.instance.CleanHighlight(_newTileIndex);
                return;
            }
            if (buttonTriggered == GamePad.Button.Left && state.Left == false)
            {
                buttonTriggered = GamePad.Button.None;
                isTriggered = false;
                currentMode = Mode.None;
                ActionLoader.instance.CleanHighlight(_newTileIndex);
                return;
            }

        }

    }

    public void Clean()
    {
        isTriggered = false;
        currentMode = Mode.None;
        buttonTriggered = GamePad.Button.None;
}

    public void DoAction()
    {

        switch (currentMode)
        {
            case Mode.Action:
                switch(buttonTriggered)
                {
                    case GamePad.Button.A:
                        Game.instance.currentPlayer.SacrificeMinion(MinionColor.GREEN);
                        break;
                    case GamePad.Button.B:
                        Game.instance.currentPlayer.SacrificeMinion(MinionColor.RED);
                        break;
                    case GamePad.Button.X:
                        Game.instance.currentPlayer.SacrificeMinion(MinionColor.BLUE);
                        break;
                    case GamePad.Button.Y:
                        Game.instance.currentPlayer.SacrificeMinion(MinionColor.YELLOW);
                        break;
                }
                
                break;

            case Mode.Move:
                Game.instance.currentPlayer.Move(_newTileIndex);
                break;
        }
    }
}