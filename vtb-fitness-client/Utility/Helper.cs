﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vtb_fitness_client.Network;

namespace vtb_fitness_client.Utility
{
    public static class Helper
    {
        public static async Task<int> GetUserSalePercent(int id)
        {
            var user = await ApiClient._User.GetById(id);
            var since = user.WorkingInVtbSince;

            if (since != null)
            {
                var sinceDate = since.Value;
                var workingYears = DateOnly.FromDateTime(DateTime.Now).Year - sinceDate.Year;

                if (workingYears > 5)
                    return 25;
                return workingYears switch
                {
                    1 => 3,
                    2 => 6,
                    3 => 9,
                    4 => 15,
                    5 => 20,
                    _ => 0
                };
            }

            return 0;
        }

        public static double GetDiscountedPrice(double price, int salePercent)
        {
            return Math.Round(price * ((100 - salePercent) / 100.0), 2);
        }

        public static string GetDiscountedPriceAsString(double price, int salePercent)
        {
            return Math.Round(price * ((100 - salePercent) / 100.0), 0).ToString();
        }
    }
}
