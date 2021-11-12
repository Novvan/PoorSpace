using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.Unity;

public class SpeakerUI : MonoBehaviour
{
    public Image prefab;
    public Speaker speaker;
    public Vector3 offset;
    Image _refImage;
    Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    void Start()
    {
        _refImage = Instantiate<Image>(prefab, GameObject.Find("PlayerUI").transform);
    }

    void Update()
    {
        if (speaker.IsPlaying)
        {
            _refImage.gameObject.SetActive(true);
        }
        else
        {
            _refImage.gameObject.SetActive(false);
        }
        _refImage.transform.position = _camera.WorldToScreenPoint(transform.position + offset);
    }

    void OnDestroy()
    {
        if (_refImage != null)
            Destroy(_refImage.gameObject);
    }
}
