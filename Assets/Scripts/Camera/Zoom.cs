using UnityEngine;
using Zenject;
using System.Collections;

public class Zoom : ObservableLogger
{
    [SerializeField] private Camera _camera;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _zoomMultiplier;
    [Inject] private InputManager _inputManager;

    private bool _isZooming = false;

    private void Start()
    {
        AddObservable(_inputManager.ScrollDirection);
        _audioSource = _camera.gameObject.GetComponent<AudioSource>();
    }

    private IEnumerator StopSoundCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        AudioControl(false);
        _isZooming = false;
    }

    private void ChangeFOV(float zoomDir)
    {
        _camera.fieldOfView -= zoomDir * _zoomMultiplier;
    }

    private void AudioControl(bool playSound)
    {
        if (playSound && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
        else if (!playSound && _audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }

    protected override void OnChanged(object obj)
    {
        float zoomDir = (float)obj;
        ChangeFOV(zoomDir);

        if (zoomDir != 0f)
        {
            if (!_isZooming)
            {
                _isZooming = true;
                StartCoroutine(StopSoundCoroutine());
            }
            AudioControl(true);
        }
        else
        {
            // Если зума нет, стартуем корутину для остановки звука
            if (_isZooming)
            {
                _isZooming = false;
                StopAllCoroutines();
                StartCoroutine(StopSoundCoroutine());
            }
        }
    }
}