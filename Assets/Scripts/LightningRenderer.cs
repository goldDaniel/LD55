using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningRenderer : MonoSingleton<LightningRenderer>
{
    [SerializeField]
    private GameObject _lightningPrefab;

    public void CreateLightning(Vector2 start, Vector2 end, int nodeCount, float branchProbability, Vector2 variance)
    {
        var lightning = Instantiate(_lightningPrefab);
        lightning.GetComponent<Lightning>().DrawLightning(start, end, nodeCount, branchProbability, variance);
    }
}
