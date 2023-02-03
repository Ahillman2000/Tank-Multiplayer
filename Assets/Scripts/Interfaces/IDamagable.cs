using UnityEngine;

public interface IDamagable
{
    int MaxHealth { get; }
    int CurrentHealth { get; }

    void Damage(Tank attacker, int damage);
    void Destory();
}
