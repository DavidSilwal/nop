using Volo.Abp.Settings;

namespace Jewellery.Settings
{
    public class JewellerySettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(JewellerySettings.MySetting1));
        }
    }
}
