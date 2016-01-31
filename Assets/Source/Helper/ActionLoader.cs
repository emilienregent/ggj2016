using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GamepadInput;

[RequireComponent(typeof(Image))]
public class ActionLoader : MonoBehaviour
{

    private Image _loader = null;
    private bool _isActive = false;
    public float _delayAction = 1f;
    public float _delayMove = 1f;
    private Camera _camera;

    public static   ActionLoader    instance    = null;
    public          Color           lastTileColor;
    public          bool            endTurn     = false;
    public          bool            isActive    { get { return _isActive; } }
    public          float           delayAction { get { return _delayAction; } set { _delayAction = value; } }
    public          float           delayMove   { get { return _delayMove; } set { _delayMove = value; } }

    private void Awake()
    {
        instance = this;
        _loader = GetComponent<Image>();
    }

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
		if(this._isActive == true && Game.instance.currentPlayer.canAction == false) {
			DesactiveLoader ();
		}
        // Inactive and we press a button ?
        if (_isActive == false && ControlManager.instance.isTriggered == true)
        {
            ActiveLoader();
        }
        else if(_isActive == true && ControlManager.instance.isTriggered == true)
        {
            switch (ControlManager.instance.currentMode)
            {
                case Mode.Action:
                    _loader.fillAmount += (1f / delayAction) * Time.deltaTime;
                    break;

                case Mode.Move:
                    _loader.fillAmount += (1f / delayMove) * Time.deltaTime;
                    break;
            }
            
            if (_loader.fillAmount >= 1f)
            {
                DesactiveLoader();
                ControlManager.instance.DoAction();
                
            }
        }
        else if(_isActive == true && ControlManager.instance.isTriggered == false)
        {
            DesactiveLoader();
            if(this.endTurn == true)
            {
                Game.instance.EndTurn();
            }
        }
    }

    // Active delay loader
    private void ActiveLoader()
    {
        switch (ControlManager.instance.currentMode)
        {
            case Mode.Action:
                // Color
                switch (ControlManager.instance.buttonTriggered)
                {
                    case GamePad.Button.A:
                        GetComponent<Image>().color = Color.green;
                        break;
                    case GamePad.Button.B:
                        GetComponent<Image>().color = Color.red;
                        break;
                    case GamePad.Button.Y:
                        GetComponent<Image>().color = Color.yellow;
                        break;
                    case GamePad.Button.X:
                        GetComponent<Image>().color = Color.blue;
                        break;
                }
                break;

            case Mode.Move:
                // Color
                GetComponent<Image>().color = Color.white;
                // Highlight
                GameObject tileGO = Game.instance.mapManager.map.tiles[ControlManager.instance.newTileIndex].gameObject;
                lastTileColor = tileGO.GetComponent<Renderer>().material.GetColor("_Color");
                tileGO.GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
                break;

            default:
                return;
        }

        // Show delay
        _isActive = true;
        _loader.enabled = true;
        _loader.fillAmount = 0f;

        // Position : See WorldToViewportPoint
        Vector3 targetPosition = Game.instance.mapManager.map.tiles[Game.instance.currentPlayer.tileIndex].gameObject.transform.position;
        targetPosition.x -= 5f;
        targetPosition.z -= 10f;
        Vector3 screenPosision = _camera.WorldToViewportPoint(targetPosition);
        screenPosision.x *= transform.parent.GetComponent<RectTransform>().rect.width;
        screenPosision.y *= transform.parent.GetComponent<RectTransform>().rect.height;
        transform.position = screenPosision;
    }

    // Desactive delay loader
    private void DesactiveLoader()
    {
        switch (ControlManager.instance.currentMode)
        {
            case Mode.Move:
                CleanHighlight(ControlManager.instance.newTileIndex);
                break;
        }

        _isActive = false;
        _loader.enabled = false;
    }

    // Clean highlight
    public void CleanHighlight(int tileIndex)
    {
        GameObject tileGO = Game.instance.mapManager.map.tiles[tileIndex].gameObject;
        tileGO.GetComponent<Renderer>().material.SetColor("_Color", lastTileColor);
    }
}