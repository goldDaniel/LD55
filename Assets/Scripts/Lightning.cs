using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    [System.NonSerialized]
    public Vector2[] _positions;

    [SerializeField]
    private AudioClip _lightningSound;

    void Init()
    {
        _lineRenderer ??= gameObject.GetComponent<LineRenderer>();

        _lineRenderer.startWidth = 0.2f;
        _lineRenderer.endWidth = 0.2f;
        _lineRenderer.startColor = Color.white;
        _lineRenderer.endColor = Color.white;

        PlayLightningSound();
    }

    IEnumerator FadeOut()
    {
        while (_lineRenderer.startColor.a > 0 || _lineRenderer.endColor.a > 0)
        {
            Color c = _lineRenderer.startColor;
            c.a -= Time.deltaTime;
            _lineRenderer.startColor = c;

            c = _lineRenderer.endColor;
            c.a -= Time.deltaTime;
            _lineRenderer.endColor = c;

            yield return null;
        }

        Destroy(gameObject);
    }

    public void DrawLightning(Vector2 start, Vector2 end, int nodeCount, float branchProbability, Vector2 variance)
    {
        Init();

        List<Vector2> nodes = Subdivide(start, end, nodeCount, branchProbability, variance);
        nodes.Insert(0, start);
        nodes.Add(end);

        _lineRenderer.positionCount = nodes.Count;

        for (int i = 0; i < nodes.Count; i++)
        {
            _lineRenderer.SetPosition(i, nodes[i]);
        }

        StartCoroutine(FadeOut());
    }

    List<Vector2> Subdivide(Vector2 start, Vector2 end, int nodeCount, float branchProbability, Vector2 variance)
    {
        List<Vector2> result = new List<Vector2>();

        if (nodeCount <= 1)
        {
            return result;
        }

        Vector2 randomOffset = new Vector2(Random.value * 2 - 1, Random.value * 2 - 1);
        Vector2 mid = (start + end) / 2 + randomOffset * variance;
        if (Random.value < branchProbability)
        {
            result.AddRange(Subdivide(start, mid, nodeCount / 2, branchProbability, variance));
            result.AddRange(Subdivide(mid, end, nodeCount / 2, branchProbability, variance));
        }
        else
        {
            result.Add(mid);
        }

        return result;
    }
    void PlayLightningSound()
    {
        float volume = 0.5f;

        AudioSystem.instance.audioSource.PlayOneShot(_lightningSound, volume);
    }
}
