using UnityEngine;
using Zenject;
using System.Collections;

public class Zoom : ObservableLogger
{
    [SerializeField] private Camera _camera;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _zoomMultiplier;
    [Inject] private InputManager _inputManager;

    private bool isZooming = false;

    private void Start()
    {
        AddObservable(_inputManager.ScrollDirection);
        _audioSource = _camera.gameObject.GetComponent<AudioSource>();
    }

    private IEnumerator StopSoundCoroutine()
    {
        yield return new WaitForSeconds(0.3f); // Ждем 0.3 секунды
        AudioControl(false); // Останавливаем звук
        isZooming = false; // Указываем, что зум закончен
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
            if (!isZooming)
            {
                isZooming = true;
                StartCoroutine(StopSoundCoroutine());
            }
            AudioControl(true);
        }
        else
        {
            // Если зума нет, стартуем корутину для остановки звука
            if (isZooming)
            {
                isZooming = false;
                StopAllCoroutines();
                StartCoroutine(StopSoundCoroutine());
            }
        }

        Debug.Log($"FOV - {zoomDir}");
    }
}