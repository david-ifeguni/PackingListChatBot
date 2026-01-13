using PackingListChatBot.Models;

namespace PackingListChatBot.Services.Packing
{
    public class PackingListGenerator : IPackingListGenerator
    {
        public PackingList GeneratePackingList(PackingConstraints packingConstraints, string location, DateOnly startTime, DateOnly endTime, List<TravelActivities> activities)
        {
            PackingList packingList = new PackingList()
            {
                Location = location,
                StartTime = startTime,
                EndTime = endTime,
                Activities = activities
            };

            AddClothing(packingList, packingConstraints.Clothing);
            AddActivityItems(packingList, packingConstraints.Activities);
            AddGeneralItems(packingList);

            return packingList;
        }

        public static void AddClothing(PackingList packingList, ClothingConstraints clothingConstraints)
        {
            if (clothingConstraints.NeedsWarmLayers)
            {
                packingList.PackingItems.Add(new PackingItem() {
                    Category = "Warm Layers",
                    Examples = { "Sweaters", "Jackets", "Long-sleeve shirts" },
                    Reason = "Colder temperatures are expected during the mornings and evenings."
                });
            }
            if (clothingConstraints.NeedsRainProtection)
            {
                packingList.PackingItems.Add(new PackingItem()
                {
                    Category = "Rain Protection Gear",
                    Examples = { "Rain jackets", "Umbrella", "Poncho", "Rain coats" },
                    Reason = "Expected to have rainy days during your trip."
                });
            }
            if (clothingConstraints.NeedsBreathableClothing)
            {
                packingList.PackingItems.Add(new PackingItem()
                {
                    Category = "Breathable Clothing",
                    Examples = { "Light T-Shirts", "Breathable pants", "Moisture-wicking fabric clothing" },
                    Reason = "Expected to have warm and/or humid conditions throuhgout the day."
                });
            }
            if (clothingConstraints.NeedsSunProtection)
            {
                packingList.PackingItems.Add(new PackingItem()
                {
                    Category = "Sun Protection",
                    Examples = { "Hats", "Sunglasses", "Sunscreen" },
                    Reason = "With higher elevation, there will be more sun exposure, leading to higher UV intensity."
                });
            }
            if (clothingConstraints.LargeTempSwing)
            {
                packingList.PackingItems.Add(new PackingItem()
                {
                    Category = "Layerable Wear",
                    Examples = { "Removable outer layers", "Zip-up layers", "Light sweaters" },
                    Reason = "Expected to have large temperature differences between day and night."
                });
            }
        }

        private static void AddActivityItems(PackingList packingList, ActivityConstraints activityConstraints)
        {
            if (activityConstraints.NeedsComfortableWalkingFootwear)
            {
                packingList.PackingItems.Add(new PackingItem()
                {
                    Category = "Walking Footwear",
                    Examples = { "Sneakers", "Walkig shoes" },
                    Reason = "Expected to do extended walking."
                });
            }
            if (activityConstraints.NeedsWaterProofFootwear)
            {
                packingList.PackingItems.Add(new PackingItem()
                {
                    Category = "Waterproof Footwear",
                    Examples = { "Waterproof shoes", "Flip-flops" },
                    Reason = "Expected water-based activities may result in wet conditions."
                });
            }
            if (activityConstraints.NeedsFormalWear)
            {
                packingList.PackingItems.Add(new PackingItem()
                {
                    Category = "Formal Attire",
                    Examples = { "Dress shirt", "Nice dress", "Dress shoes" },
                    Reason = "Evening and formal/upscale events are planned."
                });
            }
            if (activityConstraints.NeedsSwimwear)
            {
                packingList.PackingItems.Add(new PackingItem()
                {
                    Category = "Swimwear",
                    Examples = { "Swimsuit", "Goggles", "Towel", "Water shoes" },
                    Reason = "Water-related activies such as beach visits and/or rafting."
                });
            }
            if (activityConstraints.NeedsAthleticWear)
            {
                packingList.PackingItems.Add(new PackingItem()
                {
                    Category = "Athletic Wear",
                    Examples = { "Athletic shirts", "Workout shorts or leggings", "Hiking pants" },
                    Reason = "Expected to engage in physically demanding activities."
                });
            }
            if (activityConstraints.NeedsChangeOfClothes)
            {
                packingList.PackingItems.Add(new PackingItem()
                {
                    Category = "Extra Change of Clothes",
                    Examples = { "Spare shirts", "Spare pants", "Spare socks", "Spare underwear" },
                    Reason = "May require change of clothes after getting wet or otherwise dirty due to activity."
                });
            }
            if (activityConstraints.NeedsQuickDryClothing)
            {
                packingList.PackingItems.Add(new PackingItem()
                {
                    Category = "Quick Drying Clothing",
                    Examples = { "Quick-dry shirts", "Synthetic shorts", "Travel underwear" },
                    Reason = "Activities may involve getting wet with limited drying time"
                });
            }
            if (activityConstraints.NeedsWeatherFlexibleWear)
            {
                packingList.PackingItems.Add(new PackingItem()
                {
                    Category = "Weather Flexible Wear",
                    Examples = { "Convertible pants", "Packable jackets" },
                    Reason = "Outdoor activities may be affected by variable weather conditions"
                });
            }
        }

        private static void AddGeneralItems(PackingList packingList)
        {
            packingList.PackingItems.Add(new PackingItem()
            {
                Category = "General travelling essentials",
                Examples = { "Reusable water bottle", "Backpack", "Travel documents", "ID" },
                Reason = "Useful for most travel, regardless of location or activities."
            });
        }
    }
}
