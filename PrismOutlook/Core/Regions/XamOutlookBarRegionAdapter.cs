using Infragistics.Windows.OutlookBar;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrismOutlook.Core.Regions
{
    public class XamOutlookBarRegionAdapter : RegionAdapterBase<XamOutlookBar>
    {
        public XamOutlookBarRegionAdapter(IRegionBehaviorFactory behaviorFactory)
            : base(behaviorFactory)
        {

        }

        protected override void Adapt(IRegion region, XamOutlookBar regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (OutlookBarGroup group in e.NewItems)
                    {
                        regionTarget.Groups.Add(group);

                        if (regionTarget.Groups[0] == group)
                        {
                            regionTarget.SelectedGroup = group;
                        }
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (OutlookBarGroup group in e.OldItems)
                    {
                        regionTarget.Groups.Remove(group);
                    }
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
