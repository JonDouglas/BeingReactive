using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BeingReactive.Droid.Models
{
    public class Hamburger
    {
        public Meat Meat { get; set; }
        public Lettuce Lettuce { get; set; }
        public Bun Bun { get; set; }
    }

    public class Meat
    {
        public bool Cooked { get; set; }
        public bool Rotten { get; set; }
    }

    public class Lettuce
    {
        public bool Fresh { get; set; } = true;
    }

    public class Bun
    {
        public bool Heated { get; set; }
    }

    public static class BBQHelpers
    {
        public static Meat Cook(Meat meat)
        {
            meat.Cooked = true;
            return meat;
        }

        public static Bun Heat(Bun bun)
        {
            bun.Heated = true;
            return bun;
        }
    }

}