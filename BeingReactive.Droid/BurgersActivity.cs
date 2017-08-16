using System.Reactive.Disposables;
using System.Reactive.Linq;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using BeingReactive.Droid.ViewModels;
using ReactiveUI;
using BeingReactive.Droid.Models;
using System;
using System.Reactive;
using System.Reactive.Concurrency;

namespace BeingReactive.Droid
{
    [Activity(Label = "BeingReactive.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class BurgersActivity : ReactiveActivity<BurgerViewModel>
    {
        private LinearLayout bunLinearLayout;
        private LinearLayout rawMeatLinearLayout;
        private LinearLayout cookedMeatLinearLayout;
        private LinearLayout lettuceLinearLayout;
        private LinearLayout burgerLinearLayout;

        public BurgersActivity()
        {
            this.WhenActivated(() =>
                {
                    var disposable = new CompositeDisposable();

                    disposable.Add(ViewModel.BunStream()
                        .SubscribeOn(Scheduler.Default)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Subscribe(Observer.Create<Bun>(bun => SetBun(bun))));

                    disposable.Add(ViewModel.RawMeatStream()
                        .SubscribeOn(Scheduler.Default)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Subscribe(Observer.Create<Meat>(meat => SetRawMeat(meat))));

                    disposable.Add(ViewModel.LettuceStream()
                        .SubscribeOn(Scheduler.Default)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Subscribe(Observer.Create<Lettuce>(lettuce => SetLettuce(lettuce))));

                    disposable.Add(ViewModel.CookedMeatStream()
                        .SubscribeOn(Scheduler.Default)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Subscribe(Observer.Create<Meat>(meat => SetCookedMeat(meat))));

                    disposable.Add(ViewModel.HamburgerStream()
                        .SubscribeOn(Scheduler.Default)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Subscribe(Observer.Create<Hamburger>(hamburger => SetHamburger(hamburger))));

                    return disposable;
                }

            );
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView (Resource.Layout.activity_bbq);

            this.ViewModel = new BurgerViewModel();

            bunLinearLayout = FindViewById<LinearLayout>(Resource.Id.bun_layout);
            rawMeatLinearLayout = FindViewById<LinearLayout>(Resource.Id.raw_meat_layout);
            cookedMeatLinearLayout = FindViewById<LinearLayout>(Resource.Id.cooked_meat_layout);
            lettuceLinearLayout = FindViewById<LinearLayout>(Resource.Id.lettuce_layout);
            burgerLinearLayout = FindViewById<LinearLayout>(Resource.Id.burger_layout);
        }

        private void SetBun(Bun bun)
        {
            AddImageToContainer(bunLinearLayout, Resource.Drawable.bun);
        }

        private void SetRawMeat(Meat meat)
        {
            if (meat.Rotten)
            {
                AddImageToContainer(rawMeatLinearLayout, Resource.Drawable.rawMeatRotten);
            }
            else
            {
                AddImageToContainer(rawMeatLinearLayout, Resource.Drawable.rawMeat);
            }
        }

        private void SetCookedMeat(Meat meat)
        {
            AddImageToContainer(cookedMeatLinearLayout, Resource.Drawable.cookedMeat);
        }

        private void SetLettuce(Lettuce lettuce)
        {
            AddImageToContainer(lettuceLinearLayout, Resource.Drawable.lettuce);
        }

        private void SetHamburger(Hamburger burger)
        {
            AddImageToContainer(burgerLinearLayout, Resource.Drawable.burger);
        }

        private void AddImageToContainer(LinearLayout container, int imageSource)
        {
            int width = Resources.GetDimensionPixelSize(Resource.Dimension.image_max_width);
            LinearLayout.LayoutParams viewParams = new LinearLayout.LayoutParams(width, ViewGroup.LayoutParams.WrapContent);
            ImageView imageView = new ImageView(this);
            imageView.SetImageResource(imageSource);
            container.AddView(imageView, viewParams);
        }
    }
}

