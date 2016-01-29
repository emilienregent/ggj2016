using UnityEngine;
using System.Collections;

public class ControlManager : MonoBehaviour
{

	private Player _player = null;

	// Use this for initialization
	public void Start ()
	{
		_player = Game.instance.player;	
	}
	
	// Update is called once per frame
	public void Update ()
	{
		if (Input.GetKey("left") == true)
		{
			Game.instance.player.MoveLeft();
		}

		if (Input.GetKey("right") == true)
		{
			Game.instance.player.MoveRight();
		}

		if (Input.GetKey("up") == true)
		{
			Game.instance.player.MoveUp();
		}

		if (Input.GetKey("down") == true)
		{
			Game.instance.player.MoveDown();
		}
	}
}

