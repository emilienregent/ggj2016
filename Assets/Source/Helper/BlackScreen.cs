using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{	
	public 	float 	duration 	= 1;
	public 	Image 	blackScreen = null;

	private bool 	_isActive 	= true;
	private bool 	_appear 	= true;
	private float 	_limit 		= 1f;


	// Use this for initialization
	private void Start ()
	{
		Color color = blackScreen.color;

		blackScreen.color = new Color(color.r, color.g, color.b, _appear ? 0f : 1f);
	}

	// Update is called once per frame
	private void Update ()
	{
		if(_isActive == true)
		{
			Color color =  blackScreen.color;

			if(_appear == true)
			{
				blackScreen.color = new Color(color.r, color.g, color.b, color.a + (1f/ duration) * Time.deltaTime );

				if(blackScreen.color.a > _limit)
				{
					_isActive = false;
				}
			}
			else
			{
				blackScreen.color = new Color(color.r, color.g, color.b, color.a - (1f/ duration) * Time.deltaTime );

				if(blackScreen.color.a < (1 - _limit))
				{
					_isActive = false;
				}
			}

		}
	}

	public void Start(bool isAppearing, float duration)
	{
		Color color = blackScreen.color;
		blackScreen.color = new Color(color.r, color.g, color.b, isAppearing ? 0 : 1);
		_isActive = true;
		_appear = isAppearing;
		this.duration = duration;
		_limit = 1;
	}

	public void Start(bool isAppearing, float duration, float limit)
	{
		Start(isAppearing, duration);

		_limit = limit;
	}
}