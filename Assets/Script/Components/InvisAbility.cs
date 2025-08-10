using System.Collections;
using UnityEngine;

public class InvisAbility : MonoBehaviour, IAbility
{
    [SerializeField] private SkinnedMeshRenderer _MeshRender;
    private string _nameFloat = "_Amount";
    private bool _isInvis = false;
    private float treshold = 0;

    public float Delay = 1f;

    private float _time = float.MinValue;
    private Coroutine _currentRoutine;

    public void Execute()
    {
        if (Time.time < _time + Delay) return;
        _time = Time.time;

        if (_currentRoutine != null)
            StopCoroutine(_currentRoutine);

        if (_isInvis)
            _currentRoutine = StartCoroutine(NotInvisible());
        else
            _currentRoutine = StartCoroutine(Invisible());

        Debug.Log("WORK");
    }

    private IEnumerator Invisible()
    {
        _isInvis = true;
        float elapsed = 0f;

        while (elapsed < Delay)
        {
            elapsed += Time.deltaTime;
            treshold = Mathf.Clamp01(elapsed / Delay);
            _MeshRender.material.SetFloat(_nameFloat, treshold);
            yield return null;
        }
    }

    private IEnumerator NotInvisible()
    {
        _isInvis = false;
        float elapsed = 0f;

        while (elapsed < Delay)
        {
            elapsed += Time.deltaTime;
            treshold = 1f - Mathf.Clamp01(elapsed / Delay);
            _MeshRender.material.SetFloat(_nameFloat, treshold);
            yield return null;
        }
    }
}
