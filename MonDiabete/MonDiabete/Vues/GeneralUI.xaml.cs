using MonDiabete.Class;
using MonDiabete.Fichiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonDiabete.Vues
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GeneralUI : ContentPage
	{
        // VariablesGlobal VariablesGlobal = new VariablesGlobal();
        ToolBars ToolBars = new ToolBars();
        Tools Tools = new Tools();
        Horloges Horloges = new Horloges();

        public GeneralUI ()
		{
			InitializeComponent ();
            ToolBars.MenuUIGeneral(this, this.Navigation);
            
        }

        protected override void OnAppearing()
        {
            StartGeneralUI();
        }


        public void StartGeneralUI() {

            DateTime dateNow = DateTime.Now;
            DateTime dNow = DateTime.Parse(dateNow.Hour.ToString() + ":" + dateNow.Minute.ToString(), PrefsApp.cultureApp); //Peut être utiliser DateTime.TryParse pour valider l'heure
            DateTime dMatin = DateTime.Parse(VariablesGlobal.HeureMatin, PrefsApp.cultureApp);
            DateTime dMidi = DateTime.Parse(VariablesGlobal.HeureMidi, PrefsApp.cultureApp);
            DateTime dSoir = DateTime.Parse(VariablesGlobal.HeureSoir, PrefsApp.cultureApp);

            string Quand = Tools.MomentJourneeMesure(dMatin, dMidi, dSoir, dNow);
            if (Quand == "midi")
            {
                VariablesGlobal.HeureProchaineMesure = VariablesGlobal.HeureMidi;
                VariablesGlobal.HeureRefMesure = VariablesGlobal.HeureMidi;
                VariablesGlobal.HeureProchaineMesureMessage = "de midi";
                VariablesGlobal.NomMomentRefMesure = "midi";
            }

            else if (Quand == "soir")
            {
                VariablesGlobal.HeureProchaineMesure = VariablesGlobal.HeureSoir;
                VariablesGlobal.HeureRefMesure = VariablesGlobal.HeureSoir;
                VariablesGlobal.HeureProchaineMesureMessage = "du soir";
                VariablesGlobal.NomMomentRefMesure = "soir";
            }

            else
            {
                VariablesGlobal.HeureProchaineMesure = VariablesGlobal.HeureMatin;
                VariablesGlobal.HeureRefMesure = VariablesGlobal.HeureMatin;
                VariablesGlobal.HeureProchaineMesureMessage = "du matin";
                VariablesGlobal.NomMomentRefMesure = "matin";
            }


            if (VariablesGlobal.MessageAlertTypeGeneralUI == "rater")
            {
                Console.WriteLine("RATER " + VariablesGlobal.MessageAlertGeneralUI);
                LabelInfos.Text = VariablesGlobal.MessageAlertGeneralUI;
                LabelInfos.TextColor = Color.Red;
                //                Infos.BackgroundColor = Color.;
            }
            else if (VariablesGlobal.MessageAlertTypeGeneralUI == "valider")
            {
                Console.WriteLine("valider " + VariablesGlobal.MessageAlertGeneralUI);
                LabelInfos.Text = VariablesGlobal.MessageAlertGeneralUI;
                LabelInfos.TextColor = Color.Green;
                //               Infos.BackgroundColor = Color.Indigo;
            }
            else
            {
                //  Console.WriteLine("ELSE");
                LabelInfos.Text = "";
            }

            if (VariablesGlobal.MessageAlertGeneralUI != "" && VariablesGlobal.MessageAlertGeneralUI != null) { 
                LabelInfos.Text = VariablesGlobal.MessageAlertGeneralUI;
                LabelInfos.TextColor = VariablesGlobal.MessageAlertColorGeneralUI;
            }

            LabelBienvenu.Text = "Bonjour " + VariablesGlobal.Prenom + " " + VariablesGlobal.Nom + " votre prochain controle " + VariablesGlobal.HeureProchaineMesureMessage + " est à : " + VariablesGlobal.HeureProchaineMesure;

            VariablesGlobal._isRunningMainTimer = true;
            Horloges.SetTimerMain(LabelHorloge, Navigation);


        }

        public void ShowMessage(string Titre, string Message, string TextBoutton)
        {
            DisplayAlert(Titre, Message, TextBoutton);
        }


    }
}