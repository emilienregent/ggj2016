using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OpacityHelper : MonoBehaviour {

    private     List<Image> _images;

    public void Initialize(Player player)
    {
        player.portrait = this;
        _images = new List<Image>(GetComponentsInChildren<Image>());
    }

    public void FadeIn()
    {
        for (int i = 0; i < _images.Count; i++)
        {
            Image image = _images[i];
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
}
