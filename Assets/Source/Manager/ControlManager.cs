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
    private bool            _isKeyStroke = false;

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

        switch (Game.instance.state)
        {
            case GameState.MENU:
                UpdateMenu();
                break;

            case GameState.GAME:
                UpdateGame(GamePad.GetState(Game.instance.currentPlayer.controllerIndex));
                break;

            case GameState.END:
                UpdateEnd();
                break;

        }
	}

    private void UpdateMenu ()
    {
        if(GamePad.GetButtonDown(GamePad.Button.Start, GamePad.Index.Any) == true || Input.GetButton("Start"))
        {
            PlayerPrefs.SetInt("splashscreen", 1);
            Game.instance.SwitchState(GameState.TUTO);
        }

    }

    private void UpdateEnd()
    {
        if (GamePad.GetButtonDown(GamePad.Button.Start, GamePad.Index.Any) == true || Input.GetButton("Start"))
        {
            Application.LoadLevel("Main");
        }
        if (GamePad.GetButtonDown(GamePad.Button.Back, GamePad.Index.Any) == true)
        {
            PlayerPrefs.SetInt("splashscreen", 0);
            Application.LoadLevel("Main");
        }

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
            /****** Actions ******/
            /* Button A */
			if (GamePad.GetButtonDown(GamePad.Button.A, Game.instance.currentPlayer.controllerIndex) == true && Game.instance.currentPlayer.GetCountMinions(MinionColor.GREEN) > 0)
            {
                buttonTriggered = GamePad.Button.A;
                isTriggered = true;
                _isKeyStroke = false;
                currentMode = Mode.Action;
                return;
            } else if (Input.GetButton("Button_A") == true && Game.instance.currentPlayer.GetCountMinions(MinionColor.GREEN) > 0) {
                buttonTriggered = GamePad.Button.A;
                isTriggered = true;
                _isKeyStroke = true;
                currentMode = Mode.Action;
                return;
            }

            /* Button B */
			if (GamePad.GetButtonDown(GamePad.Button.B, Game.instance.currentPlayer.controllerIndex) == true && Game.instance.currentPlayer.GetCountMinions (MinionColor.RED) > 0)
            {
                buttonTriggered = GamePad.Button.B;
                isTriggered = true;
                _isKeyStroke = false;
                currentMode = Mode.Action;
                return;
            } else if (Input.GetButton("Button_B") == true && Game.instance.currentPlayer.GetCountMinions(MinionColor.RED) > 0) {
                buttonTriggered = GamePad.Button.B;
                isTriggered = true;
                _isKeyStroke = true;
                currentMode = Mode.Action;
                return;
            }

            /* Button X */
            if (GamePad.GetButtonDown(GamePad.Button.X, Game.instance.currentPlayer.controllerIndex) == true && Game.instance.currentPlayer.GetCountMinions (MinionColor.BLUE) > 0)
            {
                buttonTriggered = GamePad.Button.X;
                isTriggered = true;
                _isKeyStroke = false;
                currentMode = Mode.Action;
                return;
            } else if (Input.GetButton("Button_X") == true && Game.instance.currentPlayer.GetCountMinions(MinionColor.BLUE) > 0) {
                buttonTriggered = GamePad.Button.X;
                isTriggered = true;
                _isKeyStroke = true;
                currentMode = Mode.Action;
                return;
            }

            /* Button Y */
            if (GamePad.GetButtonDown(GamePad.Button.Y, Game.instance.currentPlayer.controllerIndex) == true && Game.instance.currentPlayer.GetCountMinions (MinionColor.YELLOW) > 0)
            {
                buttonTriggered = GamePad.Button.Y;
                isTriggered = true;
                _isKeyStroke = false;
                currentMode = Mode.Action;
                return;
            } else if (Input.GetButton("Button_Y") == true && Game.instance.currentPlayer.GetCountMinions(MinionColor.YELLOW) > 0) {
                buttonTriggered = GamePad.Button.Y;
                isTriggered = true;
                _isKeyStroke = true;
                currentMode = Mode.Action;
                return;
            }

            /* Moves */
			if (Input.GetButton("Up") == true || state.Up == true)
            {
                int tmpTileIndex = Game.instance.currentPlayer.tileIndex - (1 * Game.instance.currentPlayer.speed * Game.instance.mapManager.map.columns);
                if (Game.instance.currentPlayer.CanMove(tmpTileIndex) == true)
                {
                    buttonTriggered = GamePad.Button.Up;
                    isTriggered = true;
                    _isKeyStroke = Input.GetButton("Up") == true;
                    currentMode = Mode.Move;
                    _newTileIndex = tmpTileIndex;
                    return;
                }
            }
            if (Input.GetButton("Right") == true || state.Right == true)
            {
                int tmpTileIndex = Game.instance.currentPlayer.tileIndex + (1 * Game.instance.currentPlayer.speed);
                if (Game.instance.currentPlayer.CanMove(tmpTileIndex) == true)
                {
                    buttonTriggered = GamePad.Button.Right;
                    isTriggered = true;
                    _isKeyStroke = Input.GetButton("Right") == true;
                    currentMode = Mode.Move;
                    _newTileIndex = tmpTileIndex;
                    return;
                }
            }
            if (Input.GetButton("Down") == true || state.Down == true)
            {
                int tmpTileIndex = Game.instance.currentPlayer.tileIndex + (1 * Game.instance.currentPlayer.speed * Game.instance.mapManager.map.columns);
                if (Game.instance.currentPlayer.CanMove(tmpTileIndex) == true)
                {
                    buttonTriggered = GamePad.Button.Down;
                    isTriggered = true;
                    _isKeyStroke = Input.GetButton("Down") == true;
                    currentMode = Mode.Move;
                    _newTileIndex = tmpTileIndex;
                    return;
                }
            }
            if (Input.GetButton("Left") == true || state.Left == true)
            {
                int tmpTileIndex = Game.instance.currentPlayer.tileIndex - (1 * Game.instance.currentPlayer.speed);
                if (Game.instance.currentPlayer.CanMove(tmpTileIndex) == true)
                {
                    buttonTriggered = GamePad.Button.Left;
                    isTriggered = true;
                    _isKeyStroke = Input.GetButton("Left") == true;
                    currentMode = Mode.Move;
                    _newTileIndex = tmpTileIndex;
                    return;
                }
            }
        }
        // Do you release a button ?
        else
        {
            if (buttonTriggered == GamePad.Button.A && 
                ((GamePad.GetButtonUp(GamePad.Button.A, Game.instance.currentPlayer.controllerIndex) == true && _isKeyStroke == false) || 
                (Input.GetButton("Button_A") == false && _isKeyStroke == true))
            )
            {
                buttonTriggered = GamePad.Button.None;
                isTriggered = false;
                _isKeyStroke = false;
                currentMode = Mode.None;
                return;
            }
            if (buttonTriggered == GamePad.Button.B && 
                ((GamePad.GetButtonUp(GamePad.Button.B, Game.instance.currentPlayer.controllerIndex) == true && _isKeyStroke == false) || 
                (Input.GetButton("Button_B") == false && _isKeyStroke == true))
            )
            {
                buttonTriggered = GamePad.Button.None;
                isTriggered = false;
                _isKeyStroke = false;
                currentMode = Mode.None;
                return;
            }
            if (buttonTriggered == GamePad.Button.X && 
                ((GamePad.GetButtonUp(GamePad.Button.X, Game.instance.currentPlayer.controllerIndex) == true && _isKeyStroke == false) ||
                (Input.GetButton("Button_X") == false && _isKeyStroke == true))
            )
            {
                buttonTriggered = GamePad.Button.None;
                isTriggered = false;
                _isKeyStroke = false;
                currentMode = Mode.None;
                return;
            }
            if (buttonTriggered == GamePad.Button.Y && 
                ((GamePad.GetButtonUp(GamePad.Button.Y, Game.instance.currentPlayer.controllerIndex) == true && _isKeyStroke == false) ||
                (Input.GetButton("Button_Y") == false && _isKeyStroke == true))
            )
            {
                buttonTriggered = GamePad.Button.None;
                isTriggered = false;
                _isKeyStroke = false;
                currentMode = Mode.None;
                return;
            }
            if (buttonTriggered == GamePad.Button.Up && 
                ((state.Up == false && _isKeyStroke == false) ||
                (Input.GetButton("Up") == false && _isKeyStroke == true))
            )
            {
                buttonTriggered = GamePad.Button.None;
                isTriggered = false;
                _isKeyStroke = false;
                currentMode = Mode.None;
                ActionLoader.instance.CleanHighlight(_newTileIndex);
                return;
            }
            if (buttonTriggered == GamePad.Button.Right && 
                ((state.Right == false && _isKeyStroke == false) || 
                (Input.GetButton("Right") == false && _isKeyStroke == true))
            )
            {
                buttonTriggered = GamePad.Button.None;
                isTriggered = false;
                _isKeyStroke = false;
                currentMode = Mode.None;
                ActionLoader.instance.CleanHighlight(_newTileIndex);
                return;
            }
            if (buttonTriggered == GamePad.Button.Down &&
                ((state.Down == false && _isKeyStroke == false) ||
                (Input.GetButton("Down") == false && _isKeyStroke == true))
            )
            {
                buttonTriggered = GamePad.Button.None;
                isTriggered = false;
                _isKeyStroke = false;
                currentMode = Mode.None;
                ActionLoader.instance.CleanHighlight(_newTileIndex);
                return;
            }
            if (buttonTriggered == GamePad.Button.Left &&
                ((state.Left == false && _isKeyStroke == false) ||
                (Input.GetButton("Left") == false && _isKeyStroke == true))
            )
            {
                buttonTriggered = GamePad.Button.None;
                isTriggered = false;
                _isKeyStroke = false;
                currentMode = Mode.None;
                ActionLoader.instance.CleanHighlight(_newTileIndex);
                return;
            }

        }

    }

    public void Clean()
    {
        _isKeyStroke = false;
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
				Game.instance.currentPlayer.Move (_newTileIndex);
		                break;
        }
    }
}