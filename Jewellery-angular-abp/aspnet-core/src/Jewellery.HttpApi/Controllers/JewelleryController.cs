using Jewellery.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Jewellery.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class JewelleryController : AbpController
    {
        protected JewelleryController()
        {
            LocalizationResource = typeof(JewelleryResource);
        }
    }
}