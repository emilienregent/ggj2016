using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ActionLoader : MonoBehaviour
{

    private Image   _loader     = null;
    private bool    _isActive   = false;
    private float   _duration   = 1f;

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
                ControlManager.instance.releaseAction = true;
                ControlManager.instance.loadAction = false;
            }
        }
        else if (ControlManager.instance.loadAction == true)
        {
            _isActive = true;
            _loader.enabled = true;
            _loader.fillAmount = 0f;
        }

        if (ControlManager.instance.loadAction == false)
        {
            _isActive = false;
            _loader.enabled = false;
        }
    }
}