using MonDiabete.Vues;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MonDiabete.Class
{
    public class ToolBars
    {
        public void MenuMainPage(MainPage thisLocal, INavigation Navigation)
        {
            ToolbarItem MenuNavHistorique = new ToolbarItem
            {
                Text = "Historique",
                Priority = 0,
                Order = ToolbarItemOrder.Primary
            };
            MenuNavHistorique.Clicked += (sender, EventArgs) => { Navigation.PushAsync(new Historiques()); };
            thisLocal.ToolbarItems.Add(MenuNavHistorique);

            ToolbarItem MenuNavConfiguration = new ToolbarItem
            {
                Text = "Configuration",
                Priority = 0,
                Order = ToolbarItemOrder.Primary
            };
            MenuNavConfiguration.Clicked += (sender, EventArgs) => { Navigation.PushAsync(new ConfigurationPage()); };

            thisLocal.ToolbarItems.Add(MenuNavConfiguration);

            // return MenuNavHistorique;
        }

        public void MenuUIGeneral(GeneralUI thisLocal, INavigation Navigation)
        {
            ToolbarItem MenuNavHistorique = new ToolbarItem
            {
                Text = "Historique",
                Priority = 0,
                Order = ToolbarItemOrder.Primary
            };
            MenuNavHistorique.Clicked += (sender, EventArgs) => { Navigation.PushAsync(new Historiques()); };
            thisLocal.ToolbarItems.Add(MenuNavHistorique);

            ToolbarItem MenuNavConfiguration = new ToolbarItem
            {
                Text = "Configuration",
                Priority = 0,
                Order = ToolbarItemOrder.Primary
            };
            MenuNavConfiguration.Clicked += (sender, EventArgs) => { Navigation.PushAsync(new ConfigurationPage()); };

            thisLocal.ToolbarItems.Add(MenuNavConfiguration);

            // return MenuNavHistorique;
        }

        public void MenuUIMesure(MesureUI thisLocal, INavigation Navigation)
        {
            ToolbarItem MenuNavHistorique = new ToolbarItem
            {
                Text = "Historique",
                Priority = 0,
                Order = ToolbarItemOrder.Primary
            };
            MenuNavHistorique.Clicked += (sender, EventArgs) => { Navigation.PushAsync(new Historiques()); };
            thisLocal.ToolbarItems.Add(MenuNavHistorique);

            ToolbarItem MenuNavConfiguration = new ToolbarItem
            {
                Text = "Configuration",
                Priority = 0,
                Order = ToolbarItemOrder.Primary
            };
            MenuNavConfiguration.Clicked += (sender, EventArgs) => { Navigation.PushAsync(new ConfigurationPage()); };

            thisLocal.ToolbarItems.Add(MenuNavConfiguration);

            // return MenuNavHistorique;
        }

        public void MenuConfigurationPage(ConfigurationPage thisLocal, INavigation Navigation)
        {
            ToolbarItem MenuNavConfigurationGlycemie = new ToolbarItem
            {
                Text = "Glycémie",
                Priority = 0,
                Order = ToolbarItemOrder.Primary
            };
            MenuNavConfigurationGlycemie.Clicked += (sender, EventArgs) => { Navigation.PushAsync(new ConfGlycemie()); };

            thisLocal.ToolbarItems.Add(MenuNavConfigurationGlycemie);
        }

    }
}
