using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Plankton.Special_Editors.LinkAssetEditor
{
    internal static class LinkAssetCaster
    {
        static Type[] targetTypes = new Type[] { typeof(SB09WiiAsset.LinkAsset) }; // Pretty weird way of implementing it, perhaps update it in the future

        public static IEnumerable<GlobalLinkAsset> GetLinkAssets(object obj)
        {


            if (obj == null) yield break;


            var type = obj.GetType();

            // Look at public instance fields and properties
            var members = type.GetMembers(BindingFlags.Public | BindingFlags.Instance)
                              .Where(m => m.MemberType == MemberTypes.Field ||
                                          m.MemberType == MemberTypes.Property);

            foreach (var member in members)
            {
                object value = null;

                switch (member)
                {
                    case FieldInfo f:
                        if (targetTypes.Contains(f.FieldType))
                            value = f.GetValue(obj);
                        break;

                    case PropertyInfo p:
                        if (targetTypes.Contains(p.PropertyType) && p.CanRead)
                            value = p.GetValue(obj);
                        break;
                }

                if (value != null)
                    yield return Cast(value);
            }
        }

        public static GlobalLinkAsset Cast(object linkasset)
        {
            if (linkasset is SB09WiiAsset.LinkAsset) return new SB09WiiLinkAsset((SB09WiiAsset.LinkAsset)linkasset);

            return null;
        }
    }

}
