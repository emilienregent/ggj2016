using UnityEngine;
using System.Collections;
using GamepadInput;

public enum GamePadInput
{
    Default, A, B, X, Y, Up, Right, Down, Left
}

public class ControlManager : MonoBehaviour
{
    private GamePadInput    _currentInput   = GamePadInput.Default;
    private int             _newTileIndex;

    public static   ControlManager  instance        = null;
    public          bool            loadMove        = false;
    public          bool            releaseMove     = false;   
    public          bool            loadAction      = false;
    public          bool            releaseAction   = false;
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

		if(Game.instance.currentPlayer.canAction == false)
        {
            return;
        }

        if (state.Left == true)
        {
            if (releaseMove == true)
            {
				Game.instance.currentPlayer.Move(_newTileIndex);
            }
            else
            {
                _newTileIndex = Game.instance.currentPlayer.tileIndex - (1 * Game.instance.currentPlayer.speed);
                if (Game.instance.currentPlayer.CanMove(_newTileIndex) == true)
                {
                    _currentInput = GamePadInput.Left;
                    loadMove = true;
                }
            }
        }
        else if (_currentInput == GamePadInput.Left)
        {
            _currentInput = GamePadInput.Default;
            loadMove = false;
        }

        if (state.Up == true)
        {
            if (releaseMove == true)
            {
				Game.instance.currentPlayer.Move(_newTileIndex);
            }
            else
            {
                _newTileIndex = Game.instance.currentPlayer.tileIndex - (1 * Game.instance.currentPlayer.speed * Game.instance.mapManager.map.columns);
                if (Game.instance.currentPlayer.CanMove(_newTileIndex) == true)
                {
                    _currentInput = GamePadInput.Up;
                    loadMove = true;
                }
            }
        }
        else if (_currentInput == GamePadInput.Up)
        {
            _currentInput = GamePadInput.Default;
            loadMove = false;
        }

        if (state.Right == true)
        {
            if (releaseMove == true)
            {
				Game.instance.currentPlayer.Move(_newTileIndex);
            }
            else
            {
                _newTileIndex = Game.instance.currentPlayer.tileIndex + (1 * Game.instance.currentPlayer.speed);
                if (Game.instance.currentPlayer.CanMove(_newTileIndex) == true)
                {
                    _currentInput = GamePadInput.Right;
                    loadMove = true;
                }
            }
        }
        else if (_currentInput == GamePadInput.Right)
        {
            _currentInput = GamePadInput.Default;
            loadMove = false;
        }

        if (state.Down == true)
        {
            if (releaseMove == true)
            {
				Game.instance.currentPlayer.Move(_newTileIndex);
            }
            else
            {
                _newTileIndex = Game.instance.currentPlayer.tileIndex + (1 * Game.instance.currentPlayer.speed * Game.instance.mapManager.map.columns);
                if (Game.instance.currentPlayer.CanMove(_newTileIndex) == true)
                {
                    _currentInput = GamePadInput.Down;
                    loadMove = true;
                }
            }
        }
        else if (_currentInput == GamePadInput.Down)
        {
            _currentInput = GamePadInput.Default;
            loadMove = false;
        }

        if (state.A == true)
        {
            if (releaseAction == true)
            {
				Game.instance.currentPlayer.SacrificeMinion(MinionColor.GREEN);
            }
            else
            {
                _currentInput = GamePadInput.A;
                loadAction = true;
            }
        }
        else if (_currentInput == GamePadInput.A)
        {
            _currentInput = GamePadInput.Default;
            loadAction = false;
        }

        if (state.B == true)
        {
            if (releaseAction == true)
            {
				Game.instance.currentPlayer.SacrificeMinion(MinionColor.RED);
            }
            else
            {
                _currentInput = GamePadInput.B;
                loadAction = true;
            }
        }
        else if (_currentInput == GamePadInput.B)
        {
            _currentInput = GamePadInput.Default;
            loadAction = false;
        }

        if (state.Y == true)
        {
            if(releaseAction == true)
            {
				Game.instance.currentPlayer.SacrificeMinion(MinionColor.YELLOW);
            }
            else
            {
                _currentInput = GamePadInput.Y;
                loadAction = true;
            }
        }
        else if (_currentInput == GamePadInput.Y)
        {
            _currentInput = GamePadInput.Default;
            loadAction = false;
        }

        if (state.X == true)
        {
            if(releaseAction == true)
            {
				Game.instance.currentPlayer.SacrificeMinion(MinionColor.BLUE);
            }
            else
            {
                _currentInput = GamePadInput.X;
                loadAction = true;
            }
        }
        else if (_currentInput == GamePadInput.X)
        {
            _currentInput = GamePadInput.Default;
            loadAction = false;
        }

        if(releaseAction == true)
        {
            loadAction = false;
            releaseAction = false;
        }

        if (releaseMove == true)
        {
            loadMove = false;
            releaseMove = false;
        }
    }
}