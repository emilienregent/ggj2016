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

    public static   ControlManager  instance        = null;
    public          bool            loadAction      = false;
    public          bool            releaseAction   = false;

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
                UpdateGame(state);
                break;                
        }
	}

    private void UpdateIntro (GamepadState state)
    {

    }

    private void UpdateGame (GamepadState state)
    {

        if(Game.instance.GetCurrentPlayer().CanAction() == false)
        {
            return;
        }

        if (state.Left == true)
        {
            if (releaseAction == true)
            {
                Game.instance.GetCurrentPlayer().MoveLeft();
            }
            else
            {
                _currentInput = GamePadInput.Left;
                loadAction = true;
            }
        }
        else if (_currentInput == GamePadInput.Left)
        {
            _currentInput = GamePadInput.Default;
            loadAction = false;
        }

        if (state.Up == true)
        {
            if (releaseAction == true)
            {
                Game.instance.GetCurrentPlayer().MoveUp();
            }
            else
            {
                _currentInput = GamePadInput.Up;
                loadAction = true;
            }
        }
        else if (_currentInput == GamePadInput.Up)
        {
            _currentInput = GamePadInput.Default;
            loadAction = false;
        }

        if (state.Right == true)
        {
            if (releaseAction == true)
            {
                Game.instance.GetCurrentPlayer().MoveRight();
            }
            else
            {
                _currentInput = GamePadInput.Right;
                loadAction = true;
            }
        }
        else if (_currentInput == GamePadInput.Right)
        {
            _currentInput = GamePadInput.Default;
            loadAction = false;
        }

        if (state.Down == true)
        {
            if (releaseAction == true)
            {
                Game.instance.GetCurrentPlayer().MoveDown();
            }
            else
            {
                _currentInput = GamePadInput.Down;
                loadAction = true;
            }
        }
        else if (_currentInput == GamePadInput.Down)
        {
            _currentInput = GamePadInput.Default;
            loadAction = false;
        }

        if (state.A == true)
        {
            if (releaseAction == true)
            {
                Game.instance.GetCurrentPlayer().SacrificeMinion(MinionColor.GREEN);
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
                Game.instance.GetCurrentPlayer().SacrificeMinion(MinionColor.RED);
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
                Game.instance.GetCurrentPlayer().SacrificeMinion(MinionColor.YELLOW);
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
                Game.instance.GetCurrentPlayer().SacrificeMinion(MinionColor.BLUE);
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
            releaseAction = false;
            loadAction = false;
        }
    }
}