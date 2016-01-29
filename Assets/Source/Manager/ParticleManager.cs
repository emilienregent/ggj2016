using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour
{

	private static ParticleManager _particleManager = null;

	public ParticleObject testParticule = null;


	public static ParticleManager instance 
	{
		get 
		{
			return _particleManager;
		}
	}

	// Use this for initialization
	void Start ()
	{
		_particleManager = this;
	}

	public void Play(ParticleObject particleObject, Vector3 position)
	{
		Game.Instantiate(particleObject, position, particleObject.transform.rotation);
	}

	// Update is called once per frame
	void Update ()
	{

	}
}

