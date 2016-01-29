using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameText : MonoBehaviour
{
	private const float _duration 	= 0.75f;

	private Timer 		_dieTimer  	= null;
	private TextMesh 	_text		= null;
	private Color 		_color 		= Color.white;

	// Use this for initialization
	private void Start ()
	{
		_dieTimer 	= new Timer(_duration);
		_text 		= GetComponent<TextMesh>();

		_dieTimer.Start();
	}

	// Update is called once per frame
	private void Update ()
	{
		_color.a -= (1f / _duration) * Time.deltaTime;
		_text.color = _color;

		if(_dieTimer.IsFinished() == true)
		{
			Destroy(gameObject);
		}
	}
}