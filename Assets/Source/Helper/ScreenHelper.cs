using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScreenHelper : MonoBehaviour
{	
	private Image 	_screen 	= null;
	private bool 	_isActive 		= false;
	private bool 	_appear 		= false;
	private float 	_limit 			= 1f;
	[SerializeField]
	private float 	_duration 		= 1f;

    public GameObject   screenObject;
    public bool         isActive { get { return _isActive; } set { _isActive = value; } }

	private void Awake()
	{
		_screen = GetComponent<Image>();
	}

	// Use this for initialization
	private void Start ()
	{
		Color color = _screen.color;

		_screen.color = new Color(color.r, color.g, color.b, _appear ? 0f : 1f);
	}

	// Update is called once per frame
	private void Update ()
	{
		if(_isActive == true)
		{
			Color color =  _screen.color;

			if(_appear == true)
			{
				_screen.color = new Color(color.r, color.g, color.b, color.a + (1f/ _duration) * Time.deltaTime );

				if(_screen.color.a > _limit)
				{
					_isActive = false;
				}
			}
			else
			{
				_screen.color = new Color(color.r, color.g, color.b, color.a - (1f/ _duration) * Time.deltaTime );

				if(_screen.color.a < (1 - _limit))
				{
					_isActive = false;
				}
			}
		}
	}

	public void Play(bool isAppearing, float duration)
	{
		Color color = _screen.color;

		_screen.color = new Color(color.r, color.g, color.b, isAppearing ? 0 : 1);

		_isActive 	= true;
		_appear 	= isAppearing;
		_duration 	= duration;
		_limit 		= 1f;
	}

	public void Play(bool isAppearing, float duration, float limit)
	{
		Play(isAppearing, duration);

		_limit = limit;
	}
}