using Jewellery.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Jewellery.Permissions
{
    public class JewelleryPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(JewelleryPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(JewelleryPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<JewelleryResource>(name);
        }
    }
}
