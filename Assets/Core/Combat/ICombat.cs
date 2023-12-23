
public interface ICombat 
{
    void TakeDamage(float dmg);

    void TakeDamage(float dmg, int actor);

    void RestoreHealth(float amount);
}
