using UnityEngine;

public interface IDamagable
{
    int MaxHealth { get; }
    int CurrentHealth { get; }

    void Damage(int damage);
    void Destory();
}
