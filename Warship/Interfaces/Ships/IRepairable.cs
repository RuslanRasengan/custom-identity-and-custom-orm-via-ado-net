namespace Warship.Interfaces.Ships
{
    public interface IRepairable
    {
        public int Range { get; set; }
        public void Repair();
    }
}
