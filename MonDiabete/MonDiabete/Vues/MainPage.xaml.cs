using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MonDiabete.Class;
using MonDiabete.Vues;
using MonDiabete.Fichiers;

namespace MonDiabete
{
    public partial class MainPage : ContentPage
    {
        ToolBars ToolBars = new ToolBars();
        ToolsCheck ToolsCheck = new ToolsCheck();
        ToolsGlycemie ToolsGlycemie = new ToolsGlycemie();
        //VariablesGlobal VariablesGlobal = new VariablesGlobal();

        public MainPage()
        {
            InitializeComponent();

            // Ajout Menu Tool Bar
            ToolBars.MenuMainPage(this, Navigation);
            // VariablesGlobal.MesureIsActive = true;
            bool FileMomentExist = ToolsGlycemie.TestIfMomentFilesJsonExist();
            if (FileMomentExist == false)
            {
                DisplayAlert("Erreur", "Les Fichiers de Configuration Moment de Journée n'existe pas veuillez redémarer l'app!", "Ok");
            }

        }

        protected override void OnAppearing() {
            ToolsCheck.TestIfRecorded();
            if (ToolsCheck.TestIfRecorded() == false)
            {
                Navigation.PushAsync(new ConfigurationPage());
            }
            else if (ToolsCheck.TestIfGlycemieRecorded() == false)
            {
                Navigation.PushAsync(new ConfGlycemie());
            }
            else {
                if (VariablesGlobal.MesureIsActive == true)
                {
                    Navigation.PushAsync(new MesureUI());
                }
                else
                {
                    Navigation.PushAsync(new GeneralUI());
                }
            }
        }
    }
}
