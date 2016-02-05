using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour
{

	private static ParticleManager _particleManager = null;

	public ParticleObject appearSoldier = null;
	public ParticleObject disappearSoldier = null;

	public ParticleObject appearTank = null;
	public ParticleObject disappearTank = null;

	public ParticleObject appearFlash = null;
	public ParticleObject disappearFlash = null;

	public ParticleObject appearEnv = null;
	public ParticleObject disappearEnv = null;


	public static ParticleManager instance 
	{
		get 
		{
			return _particleManager;
		}
	}

	// Use this for initialization
	private void Start ()
	{
		_particleManager = this;
	}

	public void Play(ParticleObject particleObject, Vector3 position)
	{
		Game.Instantiate(particleObject, position, particleObject.transform.rotation);
	}

	// Update is called once per frame
	private void Update ()
	{
	}
}