using UnityEngine;

public class MedKitExample: MonoBehaviour
{
    [SerializeField] private float _healAmount;

    private void OnTriggerEnter(Collider other)
    {
        IHealable character = other.GetComponent<IHealable>();
        if (character != null)
        {
            character.TakeHeal(_healAmount);
        }
        Destroy(gameObject);
    }
}
