﻿using Infragistics.Windows.OutlookBar;
using Infragistics.Windows.Ribbon;
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
    public class XamRibbonRegionAdapter : RegionAdapterBase<XamRibbon>
    {
        public XamRibbonRegionAdapter(RegionBehaviorFactory behaviorFactory)
            : base(behaviorFactory)
        {

        }

        protected override void Adapt(IRegion region, XamRibbon regionTarget)
        {
            if (region == null) throw new ArgumentNullException(nameof(region));
            if (region == null) throw new ArgumentNullException(nameof(regionTarget));

            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var view in e.NewItems)
                    {
                        AddViewToRegion(view, regionTarget);
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var view in e.OldItems)
                    {
                        RemoveViewToRegion(view, regionTarget);
                    }
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }

        static void AddViewToRegion(object view, XamRibbon xamRibbon)
        {
            if (view is RibbonTabItem ribbonTabItem)
            {
                xamRibbon.Tabs.Add(ribbonTabItem);
            }
        }

        static void RemoveViewToRegion(object view, XamRibbon xamRibbon)
        {
            if (view is RibbonTabItem ribbonTabItem)
            {
                xamRibbon.Tabs.Remove(ribbonTabItem);
            }
        }
    }
}
