using System.Collections.Generic;

namespace Manisero.YouShallNotPass.SampleApp.Utils
{
    public static class ObjectExtensions
    {
        public static IEnumerable<TItem> AsEnumerable<TItem>(this TItem item)
        {
            yield return item;
        }
    }
}
