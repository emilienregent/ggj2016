using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BlackScreen : MonoBehaviour
{	
	private Image 	_blackScreen 	= null;
	private bool 	_isActive 		= true;
	private bool 	_appear 		= false;
	private float 	_limit 			= 1f;
	[SerializeField]
	private float 	_duration 		= 1f;

	private void Awake()
	{
		_blackScreen = GetComponent<Image>();
	}

	// Use this for initialization
	private void Start ()
	{
		Color color = _blackScreen.color;

		_blackScreen.color = new Color(color.r, color.g, color.b, _appear ? 0f : 1f);
	}

	// Update is called once per frame
	private void Update ()
	{
		if(_isActive == true)
		{
			Color color =  _blackScreen.color;

			if(_appear == true)
			{
				_blackScreen.color = new Color(color.r, color.g, color.b, color.a + (1f/ _duration) * Time.deltaTime );

				if(_blackScreen.color.a > _limit)
				{
					_isActive = false;
				}
			}
			else
			{
				_blackScreen.color = new Color(color.r, color.g, color.b, color.a - (1f/ _duration) * Time.deltaTime );

				if(_blackScreen.color.a < (1 - _limit))
				{
					_isActive = false;
				}
			}
		}
	}

	public void Start(bool isAppearing, float duration)
	{
		Color color = _blackScreen.color;

		_blackScreen.color = new Color(color.r, color.g, color.b, isAppearing ? 0 : 1);

		_isActive 	= true;
		_appear 	= isAppearing;
		_duration 	= duration;
		_limit 		= 1f;
	}

	public void Start(bool isAppearing, float duration, float limit)
	{
		Start(isAppearing, duration);

		_limit = limit;
	}
}