using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

using Shared;

namespace DNC.NewsReader
{
public class HackerNewsPage : ContentPage
{
    private ListView listView;
    public HackerNewsPage()
    {
        Title = "Hacker News Stories";

        listView = new ListView
        {
            RowHeight = 80
        };

        Content = new StackLayout
        {
            VerticalOptions = LayoutOptions.FillAndExpand,
            Children = { listView }
        };
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var entries = await new HackerNewsRepository().TopEntriesAsync();
        listView.ItemTemplate = new DataTemplate(typeof(HackerNewsEntryCell));
        listView.ItemsSource = entries;
    }
}
public class HackerNewsEntryCell : ViewCell
{
    public HackerNewsEntryCell()
    { 
        var title = new Label();
        title.SetBinding(Label.TextProperty, "Title");
            
        var postedBy = new Label();
        postedBy.SetBinding(Label.TextProperty, "PostedBy");

        View = new StackLayout
        {
            Children = { title, postedBy }
        };
    }

    protected override void OnTapped()
    {
        base.OnTapped();

        var entry = BindingContext as Shared.Entry;

        var article = new WebView
        {
            Source = new UrlWebViewSource
            {
                Url = entry.Url,
            },
            VerticalOptions = LayoutOptions.FillAndExpand
        };

        App.Navigation.PushAsync(new ContentPage { Title = entry.Title, Content = article });
    }
}
public class App
{
    public static INavigation Navigation { get; private set; }
	public static Xamarin.Forms.Page GetMainPage()
	{
		var navigationPage = new NavigationPage(new HackerNewsPage());
        Navigation = navigationPage.Navigation;

        return navigationPage;
	}
}
}
