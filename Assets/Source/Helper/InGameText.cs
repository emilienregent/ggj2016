using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameText : MonoBehaviour
{
	private Timer _dieTimer  = null;
	private TextMesh _text;
	private Color _color = Color.white;
	private const float _duration = 0.75f;

	// Use this for initialization
	void Start ()
	{
		_dieTimer = new Timer(_duration);
		_dieTimer.Start();
		_text = GetComponent<TextMesh>();
	}

	// Update is called once per frame
	void Update ()
	{
		_color.a -= (1 / _duration) * Time.deltaTime;
		_text.color = _color;
		if(_dieTimer.isFinished() == true)
		{
			Destroy(this.gameObject);
		}
	}
}

