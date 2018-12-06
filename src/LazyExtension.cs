using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarCatalog
{
    public static class LazyExtension
    {
        public static T GetValueProgressBarAware<T>(this Lazy<T> lazyObject)
        {
            //Check if value was not actual loaded - show progressbar
            if (!lazyObject.IsValueCreated)
            {
                ProgressBar.Start();
            }

            //Retrieve actual info
            T actualObject = lazyObject.Value;
            ProgressBar.Stop();

            return actualObject;
        }
    }
}
