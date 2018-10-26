using Microcharts;
using MonDiabete.Class.Dependency_Services_Class;
using MonDiabete.Fichiers;
using MonDiabete.Objets;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace MonDiabete.Vues
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Historiques : TabbedPage
    {
        public Historiques ()
        {
            InitializeComponent();
            LectureHistoriqueMatin();
            LectureHistoriqueMidi();
            LectureHistoriqueSoir();
        }

        private void LectureHistoriqueSoir()
        {
            string data = DependencyService.Get<IFileReadWrite>().ReadData(PrefsApp.fileSoirGlycemie);
            Console.WriteLine("Data = " + data);

            var list = JsonConvert.DeserializeObject<List<Soir>>(data);

            List<Entry> ChartsListGlycemieSoir = new List<Entry> { };
            List<Entry> ChartsListInsulineSoir = new List<Entry> { };

            foreach (var item in list)
            {

                /*
                 Modifier cette line en prod
                 */
                string GlycChiffre = Regex.Replace(item.Glycemie, "[^0-9.]", "");
                Entry NewItemGlycemie = new Entry(Single.Parse(GlycChiffre))
                {
                    Label = item.DatePriseMesure,
                    ValueLabel = item.Glycemie,
                    Color = SKColor.Parse("#68B9C0"),

                };
                ChartsListGlycemieSoir.Add(NewItemGlycemie);

                /*
                 Modifier cette line en prod
                 */
                string insuChiffre = Regex.Replace(item.Insuline, "[^0-9.]", "");
                Entry NewItemInsuline = new Entry(Single.Parse(insuChiffre))
                {
                    Label = item.Insuline,
                    ValueLabel = item.Insuline + " Unités",
                    Color = SKColor.Parse("#266489")
                };
                ChartsListInsulineSoir.Add(NewItemInsuline);




            }
            // Microcharts.Forms.ChartView
            //Microcharts.Chart;
            //   Microcharts.Forms.ChartView.Chart {get; set;}
            SoirGlycmieCharts.Chart = new LineChart() { Entries = ChartsListGlycemieSoir };
            SoirInsulineCharts.Chart = new LineChart() { Entries = ChartsListInsulineSoir };
        }

        private void LectureHistoriqueMidi()
        {
            string data = DependencyService.Get<IFileReadWrite>().ReadData(PrefsApp.fileMidiGlycemie);
            Console.WriteLine("Data = " + data);
            var list = JsonConvert.DeserializeObject<List<Midi>>(data);

            List<Entry> ChartsListGlycemieMidi = new List<Entry> { };
            List<Entry> ChartsListInsulineMidi = new List<Entry> { };

            foreach (var item in list)
            {

                /*
                 Modifier cette line en prod
                 */
                string GlycChiffre = Regex.Replace(item.Glycemie, "[^0-9.]", "");
                Entry NewItemGlycemie = new Entry(Single.Parse(GlycChiffre))
                {
                    Label = item.DatePriseMesure,
                    ValueLabel = item.Glycemie,
                    Color = SKColor.Parse("#68B9C0"),

                };
                ChartsListGlycemieMidi.Add(NewItemGlycemie);

                /*
                 Modifier cette line en prod
                 */
                string insuChiffre = Regex.Replace(item.Insuline, "[^0-9.]", "");
                Entry NewItemInsuline = new Entry(Single.Parse(insuChiffre))
                {
                    Label = item.Insuline,
                    ValueLabel = item.Insuline + " Unités",
                    Color = SKColor.Parse("#266489")
                };
                ChartsListInsulineMidi.Add(NewItemInsuline);




            }
            // Microcharts.Forms.ChartView
            //Microcharts.Chart;
            //   Microcharts.Forms.ChartView.Chart {get; set;}
            MidiGlycmieCharts.Chart = new LineChart() { Entries = ChartsListGlycemieMidi };
            MidiInsulineCharts.Chart = new LineChart() { Entries = ChartsListInsulineMidi };
        }

        private void LectureHistoriqueMatin()
        {
            string data = DependencyService.Get<IFileReadWrite>().ReadData(PrefsApp.fileMatinGlycemie);
            Console.WriteLine("Data = " + data);
            //infosHistoriqueMatin.Text = data;

            var list = JsonConvert.DeserializeObject<List<Matin>>(data);

            List<Entry> ChartsListGlycemieMatin = new List<Entry> { };
            List<Entry> ChartsListInsulineMatin = new List<Entry> { };

            foreach (var item in list)
            {

                /*
                 Modifier cette line en prod
                 */
                string GlycChiffre = Regex.Replace(item.Glycemie, "[^0-9.]", "");
                Entry NewItemGlycemie = new Entry(Single.Parse(GlycChiffre))
                {
                    Label = item.DatePriseMesure,
                    ValueLabel = item.Glycemie,
                    Color = SKColor.Parse("#68B9C0"),

                };
                ChartsListGlycemieMatin.Add(NewItemGlycemie);

                /*
                 Modifier cette line en prod
                 */
                string insuChiffre = Regex.Replace(item.Insuline, "[^0-9.]", "");
                Entry NewItemInsuline = new Entry(Single.Parse(insuChiffre))
                {

                    Label = item.Insuline,
                    ValueLabel = item.Insuline + " Unités",
                    Color = SKColor.Parse("#266489")
                };
                ChartsListInsulineMatin.Add(NewItemInsuline);




            }
            // Microcharts.Forms.ChartView
            //Microcharts.Chart;
            //   Microcharts.Forms.ChartView.Chart {get; set;}
            MatinGlycmieCharts.Chart = new LineChart() { Entries = ChartsListGlycemieMatin };
            MatinInsulineCharts.Chart = new LineChart() { Entries = ChartsListInsulineMatin };

        }

        private void Bp_export_matin_Clicked(object sender, EventArgs e)
        {
            ExportData("matin");
        }

        private void Bp_export_midi_Clicked(object sender, EventArgs e)
        {
            ExportData("midi");
        }

        private void Bp_export_soir_Clicked(object sender, EventArgs e)
        {
            ExportData("soir");
        }


        private void ExportData(string Moment)
        {
            
            bool IsExportData = false;
            string dataToSend = "";

            
            string JsonData = "";

            if (Moment == "matin")
            {
                JsonData = DependencyService.Get<IFileReadWrite>().ReadData(PrefsApp.fileMatinGlycemie);
                List<Matin> MatinsExport = new List<Matin>();

                var list = JsonConvert.DeserializeObject<List<Matin>>(JsonData);
                foreach (var item in list)
                {
                    if (item.Export =="false")
                    {
                        IsExportData = true;
                        Matin ExportMatin = new Matin();
                        
                        ExportMatin.DatePriseMesure = item.DatePriseMesure;
                        ExportMatin.HeurePriseMesure = item.HeurePriseMesure;
                        ExportMatin.Glycemie = item.Glycemie ;
                        ExportMatin.Insuline = item.Insuline;
                        ExportMatin.Quand = item.Quand;
                        ExportMatin.Export = item.Export;
                        ExportMatin.IdUser = item.IdUser;

                        MatinsExport.Add(ExportMatin);
                    }
                    
                }
                dataToSend = JsonConvert.SerializeObject(MatinsExport);

            }
            else if (Moment == "midi")
            {
                JsonData = DependencyService.Get<IFileReadWrite>().ReadData(PrefsApp.fileMidiGlycemie);
                List<Midi> MidisExport = new List<Midi>();

                var list = JsonConvert.DeserializeObject<List<Midi>>(JsonData);
                foreach (var item in list)
                {
                    if (item.Export == "false")
                    {
                        IsExportData = true;
                        Midi ExportMidi = new Midi();

                        ExportMidi.DatePriseMesure = item.DatePriseMesure;
                        ExportMidi.HeurePriseMesure = item.HeurePriseMesure;
                        ExportMidi.Glycemie = item.Glycemie;
                        ExportMidi.Insuline = item.Insuline;
                        ExportMidi.Quand = item.Quand;
                        ExportMidi.Export = item.Export;
                        ExportMidi.IdUser = item.IdUser;

                        MidisExport.Add(ExportMidi);
                    }

                }
                dataToSend = JsonConvert.SerializeObject(MidisExport);
            }
            else // soir
            {
                JsonData = DependencyService.Get<IFileReadWrite>().ReadData(PrefsApp.fileSoirGlycemie);
                List<Soir> SoirsExport = new List<Soir>();

                var list = JsonConvert.DeserializeObject<List<Soir>>(JsonData);
                foreach (var item in list)
                {
                    if (item.Export == "false")
                    {
                        IsExportData = true;
                        Soir ExportSoir = new Soir();

                        ExportSoir.DatePriseMesure = item.DatePriseMesure;
                        ExportSoir.HeurePriseMesure = item.HeurePriseMesure;
                        ExportSoir.Glycemie = item.Glycemie;
                        ExportSoir.Insuline = item.Insuline;
                        ExportSoir.Quand = item.Quand;
                        ExportSoir.Export = item.Export;
                        ExportSoir.IdUser = item.IdUser;

                        SoirsExport.Add(ExportSoir);
                    }

                }
                dataToSend = JsonConvert.SerializeObject(SoirsExport);
            }
            //string jsonData = @"{""username"" : ""myusername"", ""password"" : ""mypassword""}"
            //var content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            // HttpResponseMessage response = await client.PostAsync("/api.php?nom=bernardi&prenom=fabrice", content);

            if (IsExportData == true)
            {
                string Url = PrefsApp.ApiAddress + "/savehistoriques.php";

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                //var result = await response.Content.ReadAsStringAsync();
                PostBasic(dataToSend, Url, Moment);
            }
            else {
                DisplayAlert("infos", "Il n'y a pas encore de nouvelles données a sauvegardée" , "ok");
            }
           


        }

        private void PostBasic(string contentData, string Url, string Moment)
        {
            string ApiKey = VariablesGlobal.ApiKey;
            string IdUser = VariablesGlobal.Id;
            string DataSend = "{\"apikey\": \"" + ApiKey + "\", \"iduser\": \"" + IdUser + "\", \"moment\": \"" + Moment + "\", \"data\": " + contentData + "}";

            Console.Write("DataSend : " + DataSend);

            var content = new StringContent(JsonConvert.SerializeObject(DataSend));


            var request = (HttpWebRequest)WebRequest.Create(Url);
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
               streamWriter.Write(DataSend);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                AnalyseResponse(result, Moment);
                // await DisplayAlert("", result.ToString(), "ok");
            }

        }

        private void AnalyseResponse(string response, string Moment)
        {

            ReponseServeurStructure result = JsonConvert.DeserializeObject<ReponseServeurStructure>(response);
            if (result.Status == "OK")
            {
                // DisplayAlert("Infos", "Export de l'historique pour le " + Moment + " a été fait avec succès", "ok");


                string JsonData = "";

                if (Moment == "matin")
                {
                    JsonData = DependencyService.Get<IFileReadWrite>().ReadData(PrefsApp.fileMatinGlycemie);
                    var list = JsonConvert.DeserializeObject<List<Matin>>(JsonData);
                    foreach (var item in list)
                    {
                        item.Export = "true";
                    }
                    string json = JsonConvert.SerializeObject(list, Formatting.Indented);
                    DependencyService.Get<IFileReadWrite>().WriteData(PrefsApp.fileMatinGlycemie, json);
                    DisplayAlert("infos", "Historique pour le matin sauvegardé sur le cloud avec success", "ok");
                }
                else if (Moment == "midi")
                {
                    JsonData = DependencyService.Get<IFileReadWrite>().ReadData(PrefsApp.fileMidiGlycemie);
                    var list = JsonConvert.DeserializeObject<List<Midi>>(JsonData);
                    foreach (var item in list)
                    {
                        item.Export = "true";
                    }
                    string json = JsonConvert.SerializeObject(list, Formatting.Indented);
                    DependencyService.Get<IFileReadWrite>().WriteData(PrefsApp.fileMidiGlycemie, json);
                    DisplayAlert("infos", "Historique pour le midi sauvegardé sur le cloud avec success", "ok");
                }
                else // soir
                {
                    JsonData = DependencyService.Get<IFileReadWrite>().ReadData(PrefsApp.fileSoirGlycemie);
                    var list = JsonConvert.DeserializeObject<List<Soir>>(JsonData);
                    foreach (var item in list)
                    {
                        item.Export = "true";
                    }
                    string json = JsonConvert.SerializeObject(list, Formatting.Indented);
                    DependencyService.Get<IFileReadWrite>().WriteData(PrefsApp.fileSoirGlycemie, json);
                    DisplayAlert("infos", "Historique pour le soir sauvegardé sur le cloud avec success", "ok");
                }






            }
            else {
                DisplayAlert("Erreur", "Erreur : " + result.Retour , "ok");
            }
            //    Console.WriteLine(response);
            // await DisplayAlert("infos", response.Content.ToString(), "ok");
        }



        private void Bp_import_matin_Clicked(object sender, EventArgs e)
        {
            ExportDataBeforeImport("matin"); 
        }

        private void Bp_import_midi_Clicked(object sender, EventArgs e)
        {
            ExportDataBeforeImport("midi"); 
        }

        private void Bp_import_soir_Clicked(object sender, EventArgs e)
        {
            ExportDataBeforeImport("soir"); 
        }

        private void ExportDataBeforeImport(string Moment)
        {

            bool IsExportData = false;
            string dataToSend = "";


            string JsonData = "";

            if (Moment == "matin")
            {
                JsonData = DependencyService.Get<IFileReadWrite>().ReadData(PrefsApp.fileMatinGlycemie);
                List<Matin> MatinsExport = new List<Matin>();

                var list = JsonConvert.DeserializeObject<List<Matin>>(JsonData);
                foreach (var item in list)
                {
                    if (item.Export == "false")
                    {
                        IsExportData = true;
                        Matin ExportMatin = new Matin();

                        ExportMatin.DatePriseMesure = item.DatePriseMesure;
                        ExportMatin.HeurePriseMesure = item.HeurePriseMesure;
                        ExportMatin.Glycemie = item.Glycemie;
                        ExportMatin.Insuline = item.Insuline;
                        ExportMatin.Quand = item.Quand;
                        ExportMatin.Export = item.Export;
                        ExportMatin.IdUser = item.IdUser;

                        MatinsExport.Add(ExportMatin);
                    }

                }
                dataToSend = JsonConvert.SerializeObject(MatinsExport);

            }
            else if (Moment == "midi")
            {
                JsonData = DependencyService.Get<IFileReadWrite>().ReadData(PrefsApp.fileMidiGlycemie);
                List<Midi> MidisExport = new List<Midi>();

                var list = JsonConvert.DeserializeObject<List<Midi>>(JsonData);
                foreach (var item in list)
                {
                    if (item.Export == "false")
                    {
                        IsExportData = true;
                        Midi ExportMidi = new Midi();

                        ExportMidi.DatePriseMesure = item.DatePriseMesure;
                        ExportMidi.HeurePriseMesure = item.HeurePriseMesure;
                        ExportMidi.Glycemie = item.Glycemie;
                        ExportMidi.Insuline = item.Insuline;
                        ExportMidi.Quand = item.Quand;
                        ExportMidi.Export = item.Export;
                        ExportMidi.IdUser = item.IdUser;

                        MidisExport.Add(ExportMidi);
                    }

                }
                dataToSend = JsonConvert.SerializeObject(MidisExport);
            }
            else // soir
            {
                JsonData = DependencyService.Get<IFileReadWrite>().ReadData(PrefsApp.fileSoirGlycemie);
                List<Soir> SoirsExport = new List<Soir>();

                var list = JsonConvert.DeserializeObject<List<Soir>>(JsonData);
                foreach (var item in list)
                {
                    if (item.Export == "false")
                    {
                        IsExportData = true;
                        Soir ExportSoir = new Soir();

                        ExportSoir.DatePriseMesure = item.DatePriseMesure;
                        ExportSoir.HeurePriseMesure = item.HeurePriseMesure;
                        ExportSoir.Glycemie = item.Glycemie;
                        ExportSoir.Insuline = item.Insuline;
                        ExportSoir.Quand = item.Quand;
                        ExportSoir.Export = item.Export;
                        ExportSoir.IdUser = item.IdUser;

                        SoirsExport.Add(ExportSoir);
                    }

                }
                dataToSend = JsonConvert.SerializeObject(SoirsExport);
            }
            //string jsonData = @"{""username"" : ""myusername"", ""password"" : ""mypassword""}"
            //var content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            // HttpResponseMessage response = await client.PostAsync("/api.php?nom=bernardi&prenom=fabrice", content);

            if (IsExportData == true)
            {
                string Url = PrefsApp.ApiAddress + "/savehistoriques.php";

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                //var result = await response.Content.ReadAsStringAsync();
                PostBasicBeforeImport(dataToSend, Url, Moment);
            }
            else
            {
                // on importe les historiques
                ImportDataHistorique(Moment);
               
            }



        }

        

        private void PostBasicBeforeImport(string contentData, string Url, string Moment)
        {
            string ApiKey = VariablesGlobal.ApiKey;
            string IdUser = VariablesGlobal.Id;
            string DataSend = "{\"apikey\": \"" + ApiKey + "\", \"iduser\": \"" + IdUser + "\", \"moment\": \"" + Moment + "\", \"data\": " + contentData + "}";

            Console.Write("DataSend : " + DataSend);

            var content = new StringContent(JsonConvert.SerializeObject(DataSend));


            var request = (HttpWebRequest)WebRequest.Create(Url);
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(DataSend);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                AnalyseResponseBeforeImport(result, Moment);
                // await DisplayAlert("", result.ToString(), "ok");
            }

        }

        private void AnalyseResponseBeforeImport(string response, string Moment)
        {

            ReponseServeurStructure result = JsonConvert.DeserializeObject<ReponseServeurStructure>(response);
            if (result.Status == "OK")
            {
                // DisplayAlert("Infos", "Export de l'historique pour le " + Moment + " a été fait avec succès", "ok");


                string JsonData = "";

                if (Moment == "matin")
                {
                    JsonData = DependencyService.Get<IFileReadWrite>().ReadData(PrefsApp.fileMatinGlycemie);
                    var list = JsonConvert.DeserializeObject<List<Matin>>(JsonData);
                    foreach (var item in list)
                    {
                        item.Export = "true";
                    }
                    string json = JsonConvert.SerializeObject(list, Formatting.Indented);
                    DependencyService.Get<IFileReadWrite>().WriteData(PrefsApp.fileMatinGlycemie, json);
                   // DisplayAlert("infos", "Historique pour le matin sauvegardé sur le cloud avec success", "ok");
                }
                else if (Moment == "midi")
                {
                    JsonData = DependencyService.Get<IFileReadWrite>().ReadData(PrefsApp.fileMidiGlycemie);
                    var list = JsonConvert.DeserializeObject<List<Midi>>(JsonData);
                    foreach (var item in list)
                    {
                        item.Export = "true";
                    }
                    string json = JsonConvert.SerializeObject(list, Formatting.Indented);
                    DependencyService.Get<IFileReadWrite>().WriteData(PrefsApp.fileMidiGlycemie, json);
                   // DisplayAlert("infos", "Historique pour le midi sauvegardé sur le cloud avec success", "ok");
                }
                else // soir
                {
                    JsonData = DependencyService.Get<IFileReadWrite>().ReadData(PrefsApp.fileSoirGlycemie);
                    var list = JsonConvert.DeserializeObject<List<Soir>>(JsonData);
                    foreach (var item in list)
                    {
                        item.Export = "true";
                    }
                    string json = JsonConvert.SerializeObject(list, Formatting.Indented);
                    DependencyService.Get<IFileReadWrite>().WriteData(PrefsApp.fileSoirGlycemie, json);
                   // DisplayAlert("infos", "Historique pour le soir sauvegardé sur le cloud avec success", "ok");
                }

                ImportDataHistorique(Moment);




            }
            else
            {
                DisplayAlert("Erreur", "Erreur : " + result.Retour, "ok");
            }
            //    Console.WriteLine(response);
            // await DisplayAlert("infos", response.Content.ToString(), "ok");
        }


        private void ImportDataHistorique(string Moment)
        {
            
            string Url = PrefsApp.ApiAddress + "/getHistoriques.php";
            string DataSend = "{\"apikey\": \"" + VariablesGlobal.ApiKey + "\", \"moment\": \"" + Moment + "\", \"iduser\": \"" + VariablesGlobal.Id + "\"}";
            var content = new StringContent(JsonConvert.SerializeObject(DataSend));
            var request = (HttpWebRequest)WebRequest.Create(Url);
            request.ContentType = "application/json";
            request.Method = "POST";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(DataSend);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                try
                {
                    ReponseServeurStructure resultRetour = JsonConvert.DeserializeObject<ReponseServeurStructure>(result);
                    if (resultRetour.Status != "OK")
                    {
                        DisplayAlert("Erreur", "Erreur lors de l'importation de l'historique. erreur: " + resultRetour.Retour, "ok");
                    }
                    else
                    {
                        try
                        {
                            if (Moment == "matin")
                            {

                                List<Matin> Matins = JsonConvert.DeserializeObject<List<Matin>>(resultRetour.Retour);
                                // DisplayAlert("Erreur", "infos: " + resultRetour.Retour, "ok");
                                string json = JsonConvert.SerializeObject(Matins, Formatting.Indented);
                                DependencyService.Get<IFileReadWrite>().WriteData(PrefsApp.fileMatinGlycemie, json);

                                DisplayAlert("Info", "Historique complète pour les mesures du Matin importé avec succés!", "ok");
                            }
                            else if (Moment == "midi")
                            {
                                List<Midi> Midis = JsonConvert.DeserializeObject<List<Midi>>(resultRetour.Retour);

                                string json = JsonConvert.SerializeObject(Midis, Formatting.Indented);
                                DependencyService.Get<IFileReadWrite>().WriteData(PrefsApp.fileMidiGlycemie, json);

                                DisplayAlert("Info", "Historique complète pour les mesures du Midi importé avec succés!", "ok");
                            }
                            else
                            {
                                List<Soir> Soirs = JsonConvert.DeserializeObject<List<Soir>>(resultRetour.Retour);

                                string json = JsonConvert.SerializeObject(Soirs, Formatting.Indented);
                                DependencyService.Get<IFileReadWrite>().WriteData(PrefsApp.fileSoirGlycemie, json);

                                DisplayAlert("Info", "Historique complète pour les mesures du Soir importé avec succés!", "ok");
                            }

                        }
                        catch (Exception err)
                        {
                            DisplayAlert("Erreur", "Erreur de traitement des data du Serveur lors de l'importation de l'Historique complète pour les mesures du Soir : " + err.Message.ToString(), "ok");

                        }


                    }
                }
                catch (Exception e)
                {
                    DisplayAlert("Erreur", "Erreur Connexion Serveur lors de l'importation de l'Historique complète pour les mesures du Soir : "  + e.Message.ToString(), "ok");
                }
                
            }


           
        }
    }
}
 