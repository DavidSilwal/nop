using System;
using System.Collections.Generic;
using System.Text;
using Jewellery.Localization;
using Volo.Abp.Application.Services;

namespace Jewellery
{
    /* Inherit your application services from this class.
     */
    public abstract class JewelleryAppService : ApplicationService
    {
        protected JewelleryAppService()
        {
            LocalizationResource = typeof(JewelleryResource);
        }
    }
}
