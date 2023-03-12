using Data.Item;

namespace ActionGame
{
    public interface IDamageable
    {
        void TakeDamage(BulletItem bullet);
    }
}