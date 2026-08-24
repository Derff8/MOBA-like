using UnityEngine;

public interface IDamageble
{
    bool IsDead {  get; }

    Transform transform { get; }

    void TakeDamage(float damage);

}
