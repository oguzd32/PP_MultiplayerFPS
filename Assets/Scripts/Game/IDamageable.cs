using Data.Item;

namespace Game
{
    public interface IDamageable
    {
        void TakeDamage(BulletItem bullet);
    }
}