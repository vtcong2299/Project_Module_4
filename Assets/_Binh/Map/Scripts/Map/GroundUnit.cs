using UnityEngine;

public class GroundUnit : MonoBehaviour
{
    IMapGenerattable mapGenerator;

    [SerializeField]
    float offset;

    public float _offset { get => offset; }

    public void Active(IMapGenerattable generator)
    {
        mapGenerator = generator;
    }

    private void OnTriggerEnter(Collider other)
    {
        mapGenerator.GenerateUnits(() => transform.position);
    }
}