using UnityEngine;

public class GroundUnit : MonoBehaviour
{
    IMapGeneratable mapGenerator;

    [SerializeField]
    float offset;

    public float _offset { get => offset; }

    public void Active(IMapGeneratable generator)
    {
        mapGenerator = generator;
    }

    private void OnTriggerEnter(Collider other)
    {
        mapGenerator.GenerateUnits(() => transform.position);
    }
}