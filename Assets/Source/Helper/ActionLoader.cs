using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ActionLoader : MonoBehaviour
{

    private Image   _loader     = null;
    private bool    _isActive   = false;
    private float   _duration   = 1f;
    private Color   _lastTileColor;

    private void Awake()
    {
        _loader = GetComponent<Image>();
    }

    void Update()
    {
        if (_isActive == true)
        {
            _loader.fillAmount += (1f / _duration) * Time.deltaTime;
            if (_loader.fillAmount >= 1f)
            {
                _loader.enabled = false;
                _isActive = false;
                if(ControlManager.instance.loadAction == true)
                {
                    ControlManager.instance.loadAction = false;
                    ControlManager.instance.releaseAction = true;
                }
                else if(ControlManager.instance.loadMove == true)
                {
                    ControlManager.instance.loadMove = false;
                    ControlManager.instance.releaseMove = true;
                    GameObject tileGO = Game.instance.mapManager.map.tiles[ControlManager.instance.newTileIndex].gameObject;
                    tileGO.GetComponent<Renderer>().material.SetColor("_Color", _lastTileColor);
                }
            }
        }
        else if (ControlManager.instance.loadAction == true || ControlManager.instance.loadMove == true)
        {
            _isActive = true;
            _loader.enabled = true;
            _loader.fillAmount = 0f;

            if(ControlManager.instance.loadMove == true)
            {
                GameObject tileGO = Game.instance.mapManager.map.tiles[ControlManager.instance.newTileIndex].gameObject;
                _lastTileColor = tileGO.GetComponent<Renderer>().material.GetColor("_Color");
                tileGO.GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
            }
        }

        if (ControlManager.instance.loadAction == false && ControlManager.instance.loadMove == false)
        {
            if(_isActive == true)
            {
                GameObject tileGO = Game.instance.mapManager.map.tiles[ControlManager.instance.newTileIndex].gameObject;
                tileGO.GetComponent<Renderer>().material.SetColor("_Color", _lastTileColor);
            }
            _isActive = false;
            _loader.enabled = false;

        }
    }
}