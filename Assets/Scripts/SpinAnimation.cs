using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinAnimation : MonoBehaviour
{
    private Material _material;
    private Image _image;
    private int _index;

    public void Initialize()
    {
        this._image = GetComponent<Image>();
        this._material = this._image.material;
        SetVisibility(false);
        SetShaderSettings();
    }
    
    public void SetShaderSettings()
    {
        if(!_material) return;
        _material.SetVector("_Speed",new Vector2(0,1));
        _material.SetFloat("_BlurAmount", 0.989f);
        _material.SetVector("_Tiling",new Vector2(1, 0.4f));
    }

    public void SetVisibility(bool status)
    {
        this.gameObject.SetActive(status);
    }
}
