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
    public static int number = 0;
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
    private     bool            _hasNewTarget   = false;
    private     Vector3         _targetPosition = Vector3.zero;
	private 	Vector3			_offsetPosition = Vector3.zero;

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
            _hasNewTarget   = true;
            _targetPosition = _anchor.position;
        }
    }
	public		Vector3			targetPosition	{ get { return _targetPosition;} set { _targetPosition = value; } }
	public 		Vector3			offsetPosition 	{ get { return _offsetPosition;} set { _offsetPosition = value; } }

	// Trigger one bonus of the minion, and kill the minion
	public void Sacrifice() {
		if(this.canSacrifice == true) {
			#if DEBUG
				Debug.Log ("Minion's power activated !");
			#endif
			this.ExecuteRandomPower ();
            this.Kill();
		}

	}

	// Kill the minion
	public void Kill() {
        Minion.number--;
		Destroy (this.gameObject);
	}

	// Execute one random power of the minion
	protected abstract void ExecuteRandomPower ();

	// Use this for initialization
	private void Start () {
	}

    private void Move()
    {
		float speed = GameConfiguration.getMinionSpeed (this.color, this._isAnchored);

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, _targetPosition);

		if (distance <= 0.5f)
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

			if (distance > GameConfiguration.MINION_RANGE || _hasNewTarget == true)
            {
                _isAnchored     = false;
				_hasNewTarget   = false;
				_targetPosition = _anchor.position + Game.instance.mapManager.map.tiles [this.tileIndex].size / 2.5f * this.offsetPosition;
            }
//			_targetPosition =  _anchor.position + new Vector3 (2f, 0f, 0f);
//            else
//            {
//                _isAnchored     = true;
//				_targetPosition = _anchor.position + new Vector3(Random.Range(-GameConfiguration.MINION_RANGE, GameConfiguration.MINION_RANGE), 0f, Random.Range(-GameConfiguration.MINION_RANGE, GameConfiguration.MINION_RANGE));
//            }

            _isMoving = true;
            transform.LookAt(_targetPosition);
            transform.rotation = Quaternion.Euler(270f, transform.rotation.eulerAngles.y, 0f);
        }
	}

	public void setMinionOffset(int index) {
		switch(index) {
		case 0:
			this.offsetPosition = Vector3.left;
			break;
		case 1:
			this.offsetPosition = new Vector3 (-0.5f, 0f, 1f);
			break;
		case 2:
			this.offsetPosition = new Vector3 (0.5f, 0f, 1f);
			break;
		case 3:
			this.offsetPosition = Vector3.right;
			break;
		case 4:
			this.offsetPosition = new Vector3 (0.5f, 0f, -1f);
			break;
		case 5:
			this.offsetPosition = new Vector3 (-0.5f, 0f, -1f);
			break;
		default :
			break;
		}
	}
}