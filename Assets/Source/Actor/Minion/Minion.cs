using UnityEngine;
using System.Collections;
using Type;

public enum MinionColor
{
	ANY,
	GREEN,
	RED, 
	BLUE,
	YELLOW
}


public abstract class Minion : MonoBehaviour {
    private static float RANGE = 5f;
    private static float SPEED = 5f;
	protected	int				_maxPowers		= 0;
	[SerializeField]
	protected 	MinionColor 	_color;
	private		int			    _tileIndex 		= 	0;
	public		int			    tileIndex		{ get { return _tileIndex; } set { 
			_tileIndex = value; 
			gameObject.transform.position = Game.instance.mapManager.map.GetPositionFromIndex(value);
		} }
	
	// Trigger one bonus of the minion
    protected	Transform 	    _anchor		    = null;
	protected 	bool			_canSacrifice 	= true;
    private     bool            _isAnchored     = false;
    private     bool            _isMoving       = false;
    private     Vector3         _targetPosition = Vector3.zero;

	public		MinionColor		color			{ get { return _color; } set { _color = value; } }
	public		bool			canSacrifice	{ get { return _canSacrifice; } set { _canSacrifice = value; } }
    public		Transform		anchor		    
    { 
        get 
        { 
            return _anchor; 
        } 
        set 
        { 
            _anchor         = value; 
            _isMoving       = false;
            _targetPosition = _anchor.position;
        }
    }

	// Trigger one bonus of the minion, and kill the minion
	public void Sacrifice() {
		if(this.canSacrifice == true) {
			#if DEBUG
				Debug.Log ("Minion's power activated !");
			#endif
			this.ExecuteRandomPower ();
		}

	}

	// Kill the minion
	public void Kill() {
		Destroy (this);
	}

	// Execute one random power of the minion
	protected abstract void ExecuteRandomPower ();

	// Use this for initialization
	private void Start () {
		#if DEBUG
			Debug.Log ("New Minion with color " + this._color);
		#endif
	}

    private void Move()
    {
        float speed = _isAnchored == true ? SPEED * 0.5f : SPEED;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, _targetPosition);

        if (Mathf.Approximately(distance, 0f) == true)
        {
            _isMoving = false;
        }
    }
	
	// Update is called once per frame
    public virtual void Update () 
    {
        if (_isMoving == true)
        {
            Move();
        }
        else
        {
            float distance = Vector3.Distance(transform.position, _anchor.position);

            if (distance > RANGE)
            {
                _isAnchored     = false;
                _targetPosition = _anchor.position;
            }
            else
            {
                _isAnchored     = true;
                _targetPosition = _anchor.position + new Vector3(Random.Range(-RANGE, RANGE), 0f, Random.Range(-RANGE, RANGE));
            }

            _isMoving = true;
            transform.LookAt(_targetPosition);
            transform.rotation = Quaternion.Euler(270f, transform.rotation.eulerAngles.y, 0f);
        }

	}
}
