using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BeingReactive.Droid.Models;
using ReactiveUI;

namespace BeingReactive.Droid.ViewModels
{
    public class BurgerViewModel : ReactiveObject
    {
        public IObservable<Bun> BunStream()
        {
            return Observable.Interval(TimeSpan.FromSeconds(5))
                .Select(_ => BBQHelpers.Heat(new Bun()))
                .Take(4)
                .Publish()
                .RefCount();
        }

        public IObservable<Meat> RawMeatStream()
        {
            return Observable.Interval(TimeSpan.FromSeconds(3))
                .Select(_ => new Meat())
                .Take(4)
                .Publish()
                .RefCount();
        }

        public IObservable<Meat> CookedMeatStream()
        {
            return RawMeatStream().Select(meat => BBQHelpers.Cook(meat))
                .Take(4)
                .Publish()
                .RefCount();
        }

        public IObservable<Lettuce> LettuceStream()
        {
            return Observable.Interval(TimeSpan.FromSeconds(2))
                .Select(_ => new Lettuce())
                .Take(4)
                .Publish()
                .RefCount();
        }

        public IObservable<Hamburger> HamburgerStream()
        {
            return Observable.Zip(CookedMeatStream(), BunStream(), LettuceStream(), (meat, bun, lettuce) => new Hamburger { Meat = meat, Bun = bun, Lettuce = lettuce })
                .Take(4)
                .Publish()
                .RefCount();
        }
    }
}