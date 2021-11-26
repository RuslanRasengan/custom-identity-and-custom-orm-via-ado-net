using Warship.Models;

namespace Warship.Extensions
{
    public static class AreaMapToEntityArea
    {
        public static Entities.Area.Area Map(this Area area)
        {
            return new Entities.Area.Area
            {
                Id = area.Id,
                Width = area.Width,
                Height = area.Height,
            };
        }
    }
}