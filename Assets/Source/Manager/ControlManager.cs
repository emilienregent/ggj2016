using UnityEngine;
using System.Collections;

public class ControlManager : MonoBehaviour
{
	private Player _player = null;

	// Use this for initialization
	private void Start ()
	{
		_player = Game.instance.player;	
	}
	
	// Update is called once per frame
	private void Update ()
	{
		if (Input.GetKey("left") == true)
		{
			_player.MoveLeft();
		}

		if (Input.GetKey("right") == true)
		{
			_player.MoveRight();
		}

		if (Input.GetKey("up") == true)
		{
			_player.MoveUp();
		}

		if (Input.GetKey("down") == true)
		{
			_player.MoveDown();
		}
	}
}