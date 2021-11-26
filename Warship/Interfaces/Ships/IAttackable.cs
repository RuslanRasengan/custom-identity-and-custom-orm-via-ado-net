namespace Warship.Interfaces.Ships
{
    public interface IAttackable
    {
        public int Range { get; set; }
        public void Attack();
    }
}
