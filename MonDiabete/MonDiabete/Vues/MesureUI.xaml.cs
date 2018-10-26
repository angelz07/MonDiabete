using MonDiabete.Class;
using MonDiabete.Class.Dependency_Services_Class;
using MonDiabete.Fichiers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MonDiabete.Objets;
using System.Text.RegularExpressions;

namespace MonDiabete.Vues
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MesureUI : ContentPage
	{
       // VariablesGlobal VariablesGlobal = new VariablesGlobal();
        ToolBars ToolBars = new ToolBars();
        Horloges Horloges = new Horloges();
        ToolsGlycemie ToolsGlycemie = new ToolsGlycemie();
        bool IsSwitchValidate = false;

        public MesureUI ()
		{
			InitializeComponent ();
            ToolBars.MenuUIMesure(this, this.Navigation);
        }

        protected override void OnAppearing()
        {
            //DisplayAlert("Infos UI General", "UI Mesure VariablesGlobal.Nom=" + VariablesGlobal.Nom, "ok");
            StartMesureUI();
        }

        public void StartMesureUI() {
            if (VariablesGlobal.MessageAlertMesureUI != "" && VariablesGlobal.MessageAlertMesureUI != null)
            {
                LabelAlert.Text = VariablesGlobal.MessageAlertMesureUI;
                LabelAlert.TextColor = VariablesGlobal.MessageAlertColorMesurelUI;
            }

            BienvenuLabel.Text = "C'est l'heure de mesurer votre glycémie et de prendre vos médicaments pour le repas " + VariablesGlobal.HeureProchaineMesureMessage;

            VariablesGlobal._isRunningMesureTimer = true;

            Horloges.SetTimerMesure(HorlogeLabel, Navigation, LabelAlert); 
        }

        private void GlycemieEntry_Unfocused(object sender, FocusEventArgs e)
        {
            VariablesGlobal.GlycemieMesure = GlycemieEntry.Text;
            //DisplayAlert("infos", "GlycemieMesure:" + GlycemieMesure + " ", "ok");
            string resultCalculInsuline = ToolsGlycemie.FindNombreUniteInsuline(VariablesGlobal.GlycemieMesure);
            // mainpage.ShowMessage("Erreur", resultCalculInsuline, "OK");
            // DisplayAlert("infos", resultCalculInsuline, "ok");
            if (resultCalculInsuline != "erreur")
            {
                InsulineLabel.BackgroundColor = Color.Aqua;
                InsulineLabel.TextColor = Color.Green;
                InsulineLabel.FontAttributes = FontAttributes.Bold;
                VariablesGlobal.InsulineNbUnite = resultCalculInsuline;
                InsulineLabel.Text = VariablesGlobal.InsulineNbUnite + " Unités";
            }
            else
            {
                ShowMessage("Erreur", "Il y a eu une erreur veuillez recommencer.", "OK");
            }
        }

        private void GlycemieEntry_Focused(object sender, FocusEventArgs e)
        {

        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
           // DisplayAlert("info", "valeur = " + e.Value.ToString() , "ok");
            IsSwitchValidate = e.Value;
        }

        private void BouttonSave_Clicked(object sender, EventArgs e)
        {
            bool isCompleteGlycemie = true;
            VariablesGlobal._isRunningMesureTimer = false;
            VariablesGlobal.MesureIsActive = false;
          
            if (VariablesGlobal.GlycemieMesure == "" || VariablesGlobal.GlycemieMesure == null)
            {
                isCompleteGlycemie = false;
            }

            if (VariablesGlobal.InsulineNbUnite == "" || VariablesGlobal.InsulineNbUnite == null)
            {
                isCompleteGlycemie = false;
            }

            if (isCompleteGlycemie == false || IsSwitchValidate == false)
            {
                if (IsSwitchValidate == false) {
                    ShowMessage("Erreur", "Avez-vous fait votre picure ? si oui validez avec le boutton avant d'enregister !", "OK");
                }
                ShowMessage("Erreur", "Vous devez inscrire votre taux de Glycémie et valider!", "OK");
            }
            else {
                if (VariablesGlobal.NomMomentRefMesure == "matin")
                {
                    //  DisplayAlert("infos", "Matin", "ok");
                    string fileName = PrefsApp.fileMatinGlycemie;

                    string data = DependencyService.Get<IFileReadWrite>().ReadData(fileName);
                    var list = JsonConvert.DeserializeObject<List<Matin>>(data);

                    //PrefsApp.GlycemieMesure = Glycemie.Text;
                    VariablesGlobal.InsulineNbUnite = Regex.Replace(VariablesGlobal.InsulineNbUnite, "[^0-9.]", "");

                    Matin infosMatin = new Matin();
                    DateTime dateNow = DateTime.Now;
                    infosMatin.DatePriseMesure = DateTime.Now.ToString("dd-MM-yyyy");
                    infosMatin.HeurePriseMesure = DateTime.Now.ToString("HH:mm");
                    infosMatin.Glycemie = VariablesGlobal.GlycemieMesure;
                    infosMatin.Insuline = VariablesGlobal.InsulineNbUnite;
                    infosMatin.Quand = VariablesGlobal.NomMomentRefMesure;
                    infosMatin.Export = "false";
                    infosMatin.IdUser = VariablesGlobal.Id;

                    list.Add(infosMatin);
                    string json = JsonConvert.SerializeObject(list, Formatting.Indented);
                    DependencyService.Get<IFileReadWrite>().WriteData(fileName, json);

                    string message = "votre glycemie pour " + VariablesGlobal.HeureProchaineMesureMessage + " était de : " + VariablesGlobal.GlycemieMesure + " vous avez pris : " + VariablesGlobal.InsulineNbUnite + " Unités d'insuline à : " + HorlogeLabel.Text + " .";

                    VariablesGlobal.MessageAlertGeneralUI = message;
                    VariablesGlobal.MessageAlertColorGeneralUI = Color.Green;

                    // AlertSonore.StopVibrerTéléphone();
                    //  AlertSonore.StopAlarmTéléphone();
                    Navigation.PushAsync(new GeneralUI());
                   // mainPage.UIBase("valider", message);
                }


                else if (VariablesGlobal.NomMomentRefMesure == "midi")
                {

                    //  DisplayAlert("infos", "Midi", "ok");
                    string fileName = PrefsApp.fileMidiGlycemie;

                    string data = DependencyService.Get<IFileReadWrite>().ReadData(fileName);
                    var list = JsonConvert.DeserializeObject<List<Midi>>(data);


                    // PrefsApp.GlycemieMesure = Glycemie.Text;
                    VariablesGlobal.InsulineNbUnite = Regex.Replace(VariablesGlobal.InsulineNbUnite, "[^0-9.]", "");


                    Midi infosMidi = new Midi();
                    DateTime dateNow = DateTime.Now;
                    infosMidi.DatePriseMesure = DateTime.Now.ToString("dd-MM-yyyy");
                    infosMidi.HeurePriseMesure = DateTime.Now.ToString("HH:mm");
                    infosMidi.Glycemie = VariablesGlobal.GlycemieMesure;
                    infosMidi.Insuline = VariablesGlobal.InsulineNbUnite;
                    infosMidi.Quand = VariablesGlobal.NomMomentRefMesure;
                    infosMidi.Export = "false";
                    infosMidi.IdUser = VariablesGlobal.Id;

                    list.Add(infosMidi);
                    string json = JsonConvert.SerializeObject(list, Formatting.Indented);
                    DependencyService.Get<IFileReadWrite>().WriteData(fileName, json);

                    // AlertSonore.StopVibrerTéléphone();
                    // AlertSonore.StopAlarmTéléphone();
                    string message = "votre glycemie pour " + VariablesGlobal.HeureProchaineMesureMessage + " était de : " + VariablesGlobal.GlycemieMesure + " vous avez pris : " + VariablesGlobal.InsulineNbUnite + " Unités d'insuline à : " + HorlogeLabel.Text + " .";

                    VariablesGlobal.MessageAlertGeneralUI = message;
                    VariablesGlobal.MessageAlertColorGeneralUI = Color.Green;

                    Navigation.PushAsync(new GeneralUI());
                }
                else if (VariablesGlobal.NomMomentRefMesure == "soir")
                {

                    // DisplayAlert("infos", "Soir", "ok");
                    string fileName = PrefsApp.fileSoirGlycemie;

                    string data = DependencyService.Get<IFileReadWrite>().ReadData(fileName);
                    var list = JsonConvert.DeserializeObject<List<Soir>>(data);

                    // PrefsApp.GlycemieMesure = Glycemie.Text;
                    VariablesGlobal.InsulineNbUnite = Regex.Replace(VariablesGlobal.InsulineNbUnite, "[^0-9.]", "");

                    Soir infosSoir = new Soir();
                    DateTime dateNow = DateTime.Now;
                    infosSoir.DatePriseMesure = DateTime.Now.ToString("dd-MM-yyyy");
                    infosSoir.HeurePriseMesure = DateTime.Now.ToString("HH:mm");
                    infosSoir.Glycemie = VariablesGlobal.GlycemieMesure;
                    infosSoir.Insuline = VariablesGlobal.InsulineNbUnite;
                    infosSoir.Quand = VariablesGlobal.NomMomentRefMesure;
                    infosSoir.Export = "false";
                    infosSoir.IdUser = VariablesGlobal.Id;

                    list.Add(infosSoir);
                    string json = JsonConvert.SerializeObject(list, Formatting.Indented);
                    DependencyService.Get<IFileReadWrite>().WriteData(fileName, json);


                    //AlertSonore.StopVibrerTéléphone();
                    // AlertSonore.StopAlarmTéléphone();
                    string message = "votre glycemie pour " + VariablesGlobal.HeureProchaineMesureMessage + " était de : " + VariablesGlobal.GlycemieMesure + " vous avez pris : " + VariablesGlobal.InsulineNbUnite + " Unités d'insuline à : " + HorlogeLabel.Text + " .";

                    VariablesGlobal.MessageAlertGeneralUI = message;
                    VariablesGlobal.MessageAlertColorGeneralUI = Color.Green;
                    Navigation.PushAsync(new GeneralUI());
                }
                else
                {
                    Console.WriteLine("erreur Record");
                }
            }

        }


        public void ShowMessage(string Titre, string Message, string TextBoutton)
        {
            DisplayAlert(Titre, Message, TextBoutton);
        }
    }
}