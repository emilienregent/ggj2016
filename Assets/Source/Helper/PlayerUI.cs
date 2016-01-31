using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerUI : MonoBehaviour {

    private     List<Image> _images;
    private     float       _duration = 1f;
    private     float       _targetAmount = 0f;
    private     float       _previousAmount = 0f;
    private     bool        _timerIsActive = false;

    public      GameObject  gauge;
    public      GameObject  timer;
    public      GameObject  minionsWrapper;

    public void Initialize(Player player)
    {
        player.portrait = this;
        _images = new List<Image>(GetComponentsInChildren<Image>());
    }

    public void FadeIn()
    {
        for (int i = 0; i < _images.Count; i++)
        {
            Color newColor = _images[i].color;
            newColor.a = 1f;
            _images[i].color = newColor;
        }
    }

    public void FadeOut()
    {
        for (int i = 0; i < _images.Count; i++)
        {
            Color newColor = _images[i].color;
            newColor.a = 0.5f;
            _images[i].color = newColor;
        }
    }

    public void UpdateScore(int score)
    {
        _previousAmount = _targetAmount;
        _targetAmount = ((float) score / GameConfiguration.MAX_SCORE - _previousAmount);
    }

    public void FillMinionSlots(List<Minion> minions)
    {
        Image[] children    = minionsWrapper.GetComponentsInChildren<Image>(true);
        Image[] childrenUI  = GetComponentsInChildren<Image>(true);
        Color   emptyColor  = childrenUI[0].color;

        for (int i = 0; i < children.Length; i++)
        {
            if(i < minions.Count)
            {
                switch (minions[i].color)
                {
                    case MinionColor.YELLOW:
                        children[i].color = new Color(1f, 0.917f, 0f);
                        break;
                    case MinionColor.RED:
                        children[i].color = new Color(1f, 0f, 0f);
                        break;
                    case MinionColor.GREEN:
                        children[i].color = new Color(0.22f, 0.75f, 0.42f);
                        break;
                    case MinionColor.BLUE:
                        children[i].color = new Color(0.36f, 0.533f, 0.92f);
                        break;
                }
            }
            else
            {
                children[i].color = emptyColor;
            }
        }
    }

    public void InitTimer()
    {
        timer.GetComponent<Image>().fillAmount = 1f;
        _timerIsActive = true;
    }

    public void StopTimer()
    {
        _timerIsActive = false;
        timer.GetComponent<Image>().fillAmount = 0f;
    }

    void Update()
    {
        if(gauge.GetComponent<Image>().fillAmount < _targetAmount)
        {
            gauge.GetComponent<Image>().fillAmount += (_targetAmount / _duration) * Time.deltaTime;
        }

        if(_timerIsActive == true)
        {
            timer.GetComponent<Image>().fillAmount -= (1f / Game.instance.defaultDuration - Game.instance.currentPlayer.timeMalus) * Time.deltaTime;
        }
    }
}
