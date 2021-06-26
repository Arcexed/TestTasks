using System;
using System.Collections.Generic;
using System.Linq;
using TestTasks.InternationalTradeTask.Models;

namespace TestTasks.InternationalTradeTask
{
    internal class CommodityRepository
    {
        public double? GetImportTariff(string commodityName)
        {
            foreach (var comodityGroup in _allCommodityGroups)
            {
                foreach (var subgroups in comodityGroup.SubGroups)
                {

                    foreach (var subgroup in subgroups.SubGroups)
                    {
                        foreach (var subgroupss in subgroup.SubGroups)
                        {

                        }
                    }
                }
            }
            //foreach (var comodity in _allCommodityGroups)
            //{
            //    var subgroup = comodity.SubGroups;
            //    return subgroup.Where(s => s.Name== commodityName).FirstOrDefault().ImportTarif;

            //}
            //_allCommodityGroups.Where(g => g.SubGroups => s )
            return 0;
        }

        public double? GetExportTariff(string commodityName)
        {
            throw new NotImplementedException();
        }

        private FullySpecifiedCommodityGroup[] _allCommodityGroups = new FullySpecifiedCommodityGroup[]
        {
            new FullySpecifiedCommodityGroup("06", "Sugar, sugar preparations and honey", 0.05, 0)
            {
                SubGroups = new CommodityGroup[]
                {
                    new CommodityGroup("061", "Sugar and honey")
                    {
                        SubGroups = new CommodityGroup[]
                        {
                            new CommodityGroup("0611", "Raw sugar,beet & cane"),
                            new CommodityGroup("0612", "Refined sugar & other prod.of refining,no syrup"),
                            new CommodityGroup("0615", "Molasses", 0, 0),
                            new CommodityGroup("0616", "Natural honey", 0, 0),
                            new CommodityGroup("0619", "Sugars & syrups nes incl.art.honey & caramel"),
                        }
                    },
                    new CommodityGroup("062", "Sugar confy, sugar preps. Ex chocolate confy", 0, 0)
                }
            },
            new FullySpecifiedCommodityGroup("282", "Iron and steel scrap", 0, 0.1)
            {
                SubGroups = new CommodityGroup[]
                {
                    new CommodityGroup("28201", "Iron/steel scrap not sorted or graded"),
                    new CommodityGroup("28202", "Iron/steel scrap sorted or graded/cast iron"),
                    new CommodityGroup("28203", "Iron/steel scrap sort.or graded/tinned iron"),
                    new CommodityGroup("28204", "Rest of 282.0")
                }
            }
        };
    }
}
