using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	private float speed = 0.1f;

	// Use this for initialization
	private void Start ()
	{
		ChangeColor(Color.blue);
	}

	// Update is called once per frame
	private void Update ()
	{

	}

	public void Release ()
	{
	}

	public void MoveLeft()
	{
		Move(-1f, 0f);
		ParticleManager.instance.Play(ParticleManager.instance.testParticule, gameObject.transform.position);
	}

	public void MoveRight()
	{
		Move(1f, 0f);
	}

	public void MoveUp()
	{
		Move(0f, 1f);
	}

	public void MoveDown()
	{
		Move(0f, -1f);
	}

	private void Move(float x, float y)
	{
		gameObject.transform.position += new Vector3(x * speed ,y * speed);
		AudioManager.instance.plop.Play();
	}

	private void ChangeColor(Color color)
	{
		GetComponent<SpriteRenderer>().color = color;
	}
}