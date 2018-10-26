using MonDiabete.Class.Dependency_Services_Class;
using MonDiabete.Fichiers;
using MonDiabete.Objets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonDiabete.Vues
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfGlycemie : ContentPage
	{
        //VariablesGlobal VariablesGlobal = new VariablesGlobal();
        public ConfGlycemie ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {

            LectureInfosGlycemie();
        }

        private void LectureInfosGlycemie()
        {
            try
            {
                string fileName = PrefsApp.fileConfigGlycemie;
                string data = DependencyService.Get<IFileReadWrite>().ReadData(fileName);
                var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(data);
                GlycemieInfosObjectStruct InfosGlycemie = (GlycemieInfosObjectStruct)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonObj.ToString(), typeof(GlycemieInfosObjectStruct));
                if (InfosGlycemie.GlycemieConfigRecorded == "true")
                {
                    g70matin.Text = InfosGlycemie.GlycemieMoins70Matin;
                    g70midi.Text = InfosGlycemie.GlycemieMoins70Midi;
                    g70soir.Text = InfosGlycemie.GlycemieMoins70Soir;

                    g70A100matin.Text = InfosGlycemie.Glycemie70A100Matin;
                    g70A100midi.Text = InfosGlycemie.Glycemie70A100Midi;
                    g70A100soir.Text = InfosGlycemie.Glycemie70A100Soir;

                    g101A150matin.Text = InfosGlycemie.Glycemie101A150Matin;
                    g101A150midi.Text = InfosGlycemie.Glycemie101A150Midi;
                    g101A150soir.Text = InfosGlycemie.Glycemie101A150Soir;

                    g151A200matin.Text = InfosGlycemie.Glycemie151A200Matin;
                    g151A200midi.Text = InfosGlycemie.Glycemie151A200Midi;
                    g151A200soir.Text = InfosGlycemie.Glycemie151A200Soir;

                    g201A250matin.Text = InfosGlycemie.Glycemie201A250Matin;
                    g201A250midi.Text = InfosGlycemie.Glycemie201A250Midi;
                    g201A250soir.Text = InfosGlycemie.Glycemie201A250Soir;

                    g251A300matin.Text = InfosGlycemie.Glycemie251A300Matin;
                    g251A300midi.Text = InfosGlycemie.Glycemie251A300Midi;
                    g251A300soir.Text = InfosGlycemie.Glycemie251A300Soir;

                    g300matin.Text = InfosGlycemie.GlycemiePlus300Matin;
                    g300midi.Text = InfosGlycemie.GlycemiePlus300Midi;
                    g300soir.Text = InfosGlycemie.GlycemiePlus300Soir;

                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Utlisateur non enregistré. ou erreur:" + err.Message.ToString());
            }
        }


        private async Task Bp_delete_glycemie_ClickedAsync(object sender, EventArgs e)
        {

            Boolean answer = await DisplayAlert("Question?", "êtes-vous certain de vouloir effacer les Infos?", "Oui", "Non");
            if (answer == true)
            {
                string fileName = PrefsApp.fileConfigGlycemie;
                GlycemieInfosObjectStruct GlycemieInfos = new GlycemieInfosObjectStruct
                {
                    GlycemieConfigRecorded = "false",

                    GlycemieMoins70Matin = "",
                    GlycemieMoins70Midi = "",
                    GlycemieMoins70Soir = "",

                    Glycemie70A100Matin = "",
                    Glycemie70A100Midi = "",
                    Glycemie70A100Soir = "",

                    Glycemie101A150Matin = "",
                    Glycemie101A150Midi = "",
                    Glycemie101A150Soir = "",

                    Glycemie151A200Matin = "",
                    Glycemie151A200Midi = "",
                    Glycemie151A200Soir = "",

                    Glycemie201A250Matin = "",
                    Glycemie201A250Midi = "",
                    Glycemie201A250Soir = "",

                    Glycemie251A300Matin = "",
                    Glycemie251A300Midi = "",
                    Glycemie251A300Soir = "",

                    GlycemiePlus300Matin = "",
                    GlycemiePlus300Midi = "",
                    GlycemiePlus300Soir = ""
                 };

                try
                {
                    string json = JsonConvert.SerializeObject(GlycemieInfos);
                    DependencyService.Get<IFileReadWrite>().WriteData(fileName, json);

                    g70matin.Text = "";
                    g70midi.Text = "";
                    g70soir.Text = "";

                    g70A100matin.Text = "";
                    g70A100midi.Text = "";
                    g70A100soir.Text = "";

                    g101A150matin.Text = "";
                    g101A150midi.Text = "";
                    g101A150soir.Text = "";

                    g151A200matin.Text = "";
                    g151A200midi.Text = "";
                    g151A200soir.Text = "";

                    g201A250matin.Text = "";
                    g201A250midi.Text = "";
                    g201A250soir.Text = "";

                    g251A300matin.Text = "";
                    g251A300midi.Text = "";
                    g251A300soir.Text = "";

                    g300matin.Text = "";
                    g300midi.Text = "";
                    g300soir.Text = "";


                    bool isUpdateVariablesGlobals = UpdateVariablesGlobalGlycemie(GlycemieInfos);
                    if (isUpdateVariablesGlobals == true)
                    {
                        await DisplayAlert("Info", "configuration Glycémie effacé.", "ok");
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Erreur lors de la mise a jour des Variables Globals.", "ok");
                    }
                    

                    

                }
                catch (Exception err)
                {
                    await DisplayAlert("Info", "Il y a eu une erreur, veuillez recommencer. ", "ok");
                    Console.WriteLine("Erreur Reset Glycemie Ecriture Fichier : " + err.Message.ToString());
                }
            }

        }

        private bool CheckIfEntryCompleted()
        {
            bool isCompletedRecord = true;
            // <70
            if (g70matin.Text == "" || g70matin.Text == null)
            {
                isCompletedRecord = false;
            }
            if (g70midi.Text == "" || g70midi.Text == null)
            {
                isCompletedRecord = false;
            }
            if (g70soir.Text == "" || g70soir.Text == null)
            {
                isCompletedRecord = false;
            }

            // 70 - 100
            if (g70A100matin.Text == "" || g70A100matin.Text == null)
            {
                isCompletedRecord = false;
            }
            if (g70A100midi.Text == "" || g70A100midi.Text == null)
            {
                isCompletedRecord = false;
            }
            if (g70A100soir.Text == "" || g70A100soir.Text == null)
            {
                isCompletedRecord = false;
            }

            // 101 - 150
            if (g101A150matin.Text == "" || g101A150matin.Text == null)
            {
                isCompletedRecord = false;
            }
            if (g101A150midi.Text == "" || g101A150midi.Text == null)
            {
                isCompletedRecord = false;
            }
            if (g101A150soir.Text == "" || g101A150soir.Text == null)
            {
                isCompletedRecord = false;
            }

            // 151 - 200
            if (g151A200matin.Text == "" || g151A200matin.Text == null)
            {
                isCompletedRecord = false;
            }
            if (g151A200midi.Text == "" || g151A200midi.Text == null)
            {
                isCompletedRecord = false;
            }
            if (g151A200soir.Text == "" || g151A200soir.Text == null)
            {
                isCompletedRecord = false;
            }


            // 201 - 250
            if (g201A250matin.Text == "" || g201A250matin.Text == null)
            {
                isCompletedRecord = false;
            }
            if (g201A250midi.Text == "" || g201A250midi.Text == null)
            {
                isCompletedRecord = false;
            }
            if (g201A250soir.Text == "" || g201A250soir.Text == null)
            {
                isCompletedRecord = false;
            }

            // 251 - 300
            if (g251A300matin.Text == "" || g251A300matin.Text == null)
            {
                isCompletedRecord = false;
            }
            if (g251A300midi.Text == "" || g251A300midi.Text == null)
            {
                isCompletedRecord = false;
            }
            if (g251A300soir.Text == "" || g251A300soir.Text == null)
            {
                isCompletedRecord = false;
            }

            // > 300
            if (g300matin.Text == "" || g300matin.Text == null)
            {
                isCompletedRecord = false;
            }
            if (g300midi.Text == "" || g300midi.Text == null)
            {
                isCompletedRecord = false;
            }
            if (g300soir.Text == "" || g300soir.Text == null)
            {
                isCompletedRecord = false;
            }
            return isCompletedRecord;
        }

        public async Task Bp_record_glycemie_ClickedAsync(object sender, EventArgs e)
        {

            bool EntryIsCompleted = CheckIfEntryCompleted();
            if (EntryIsCompleted == true)
            {
                try
                {
                    string fileName = PrefsApp.fileConfigGlycemie;
                    GlycemieInfosObjectStruct GlycemieInfos = new GlycemieInfosObjectStruct
                    {
                        GlycemieConfigRecorded = "true",

                        GlycemieMoins70Matin = g70matin.Text,
                        GlycemieMoins70Midi = g70midi.Text,
                        GlycemieMoins70Soir = g70soir.Text,

                        Glycemie70A100Matin = g70A100matin.Text,
                        Glycemie70A100Midi = g70A100midi.Text,
                        Glycemie70A100Soir = g70A100soir.Text,

                        Glycemie101A150Matin = g101A150matin.Text,
                        Glycemie101A150Midi = g101A150midi.Text,
                        Glycemie101A150Soir = g101A150soir.Text,

                        Glycemie151A200Matin = g151A200matin.Text,
                        Glycemie151A200Midi = g151A200midi.Text,
                        Glycemie151A200Soir = g151A200soir.Text,

                        Glycemie201A250Matin = g201A250matin.Text,
                        Glycemie201A250Midi = g201A250midi.Text,
                        Glycemie201A250Soir = g201A250soir.Text,

                        Glycemie251A300Matin = g251A300matin.Text,
                        Glycemie251A300Midi = g251A300midi.Text,
                        Glycemie251A300Soir = g251A300soir.Text,

                        GlycemiePlus300Matin = g300matin.Text,
                        GlycemiePlus300Midi = g300midi.Text,
                        GlycemiePlus300Soir = g300soir.Text
                    };

                    bool isUpdateOK =  UpdateVariablesGlobalGlycemie(GlycemieInfos);
                    if (isUpdateOK == true)
                    {
                        string json = JsonConvert.SerializeObject(GlycemieInfos);
                        DependencyService.Get<IFileReadWrite>().WriteData(fileName, json);

                        string Url = PrefsApp.ApiAddress + "/saveconfglycemie.php";
                        string isRecordedOnline = PostBasic(json, Url);
                       // await DisplayAlert("Erreur", "isRecordedOnline = " + isRecordedOnline, "ok");
                        ReponseServeurStructure result = JsonConvert.DeserializeObject<ReponseServeurStructure>(isRecordedOnline);
                        if (result.Status != "OK") {
                            await DisplayAlert("Erreur", "Erreur sauvegarde configuration Glycémie en ligne mais sauvegardé en local. erreur: " + result.Retour, "ok");
                        }
                        else
                        {
                            await DisplayAlert("Info", "configuration Glycémie sauvegardé. ", "ok");
                        }
                        /* if (isRecordedOnline == "error")
                         {
                             await DisplayAlert("Erreur", "Erreur sauvegarde configuration Glycémie en ligne mais sauvegardé en local. ", "ok");
                         }
                         else {
                             await DisplayAlert("Info", "configuration Glycémie sauvegardé. ", "ok");
                         }*/


                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Erreur lors de la mise a jour des Variables Globals. ", "ok");
                    }  

                    
                }
                catch (Exception erreur)
                {
                    await DisplayAlert("Info", "Il y a eu une erreur, veuillez recommencer. ", "ok");
                    Console.WriteLine("Erreur Reset Glycemie Ecriture Fichier : " + erreur.Message.ToString());
                }


            }
            else
            {
                await DisplayAlert("Infos", "Tout les champs doivent être complété.", "OK");
            }
        }


        public bool UpdateVariablesGlobalGlycemie(GlycemieInfosObjectStruct GlycemieInfos) {
            try
            {
                VariablesGlobal.Glycemie101A150Matin = GlycemieInfos.Glycemie101A150Matin;
                VariablesGlobal.Glycemie101A150Midi = GlycemieInfos.Glycemie101A150Midi;
                VariablesGlobal.Glycemie101A150Soir = GlycemieInfos.Glycemie101A150Soir;

                VariablesGlobal.Glycemie151A200Matin = GlycemieInfos.Glycemie151A200Matin;
                VariablesGlobal.Glycemie151A200Midi = GlycemieInfos.Glycemie151A200Midi;
                VariablesGlobal.Glycemie151A200Soir = GlycemieInfos.Glycemie151A200Soir;

                VariablesGlobal.Glycemie201A250Matin = GlycemieInfos.Glycemie201A250Matin;
                VariablesGlobal.Glycemie201A250Midi = GlycemieInfos.Glycemie201A250Midi;
                VariablesGlobal.Glycemie201A250Soir = GlycemieInfos.Glycemie201A250Soir;

                VariablesGlobal.Glycemie251A300Matin = GlycemieInfos.Glycemie251A300Matin;
                VariablesGlobal.Glycemie251A300Midi = GlycemieInfos.Glycemie251A300Midi;
                VariablesGlobal.Glycemie251A300Soir = GlycemieInfos.Glycemie251A300Soir;

                VariablesGlobal.Glycemie70A100Matin = GlycemieInfos.Glycemie70A100Matin;
                VariablesGlobal.Glycemie70A100Midi = GlycemieInfos.Glycemie70A100Midi;
                VariablesGlobal.Glycemie70A100Soir = GlycemieInfos.Glycemie70A100Soir;

                VariablesGlobal.GlycemieConfigRecorded = GlycemieInfos.GlycemieConfigRecorded;

                VariablesGlobal.GlycemieMoins70Matin = GlycemieInfos.GlycemieMoins70Matin;
                VariablesGlobal.GlycemieMoins70Midi = GlycemieInfos.GlycemieMoins70Midi;
                VariablesGlobal.GlycemieMoins70Soir = GlycemieInfos.GlycemieMoins70Soir;

                VariablesGlobal.GlycemiePlus300Matin = GlycemieInfos.GlycemiePlus300Matin;
                VariablesGlobal.GlycemiePlus300Midi = GlycemieInfos.GlycemiePlus300Midi;
                VariablesGlobal.GlycemiePlus300Soir = GlycemieInfos.GlycemiePlus300Soir;

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur Update Variable GLobals : " + e.Message.ToString());
                return false;
            }
           
        }

        private string PostBasic(string contentData, string Url)
        {
            string Retour = "";
            string ApiKey = VariablesGlobal.ApiKey;
            string IdUser = VariablesGlobal.Id;
            string DataSend = "{\"apikey\": \"" + ApiKey + "\", \"iduser\": \"" + IdUser + "\", \"data\": " + contentData + "}";

            Console.Write("DataSend : " + DataSend);

            var content = new StringContent(JsonConvert.SerializeObject(DataSend));


            var request = (HttpWebRequest)WebRequest.Create(Url);
            request.ContentType = "application/json";
            request.Method = "POST";

            Console.Write("Url : " + Url);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(DataSend);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.Write("result : " + result);
                Retour = result;
                return Retour;

                //  AnalyseResponse(result, Moment);
                // await DisplayAlert("", result.ToString(), "ok");
            }

        }
    }
}