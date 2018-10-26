using MonDiabete.Class;
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
	public partial class ConfigurationPage : ContentPage
	{
        ToolBars ToolBars = new ToolBars();
        Tools Tools = new Tools();
       // VariablesGlobal VariablesGlobal = new VariablesGlobal();

        public ConfigurationPage ()
		{
			InitializeComponent ();
            ToolBars.MenuConfigurationPage(this, this.Navigation);
        }

        protected override void OnAppearing()
        {
            LectureInfos();

        }

        private void LectureInfos()
        {
            try
            {
                string fileName = PrefsApp.fileUserPref;
                string data = DependencyService.Get<IFileReadWrite>().ReadData(fileName);

                //  Console.WriteLine("*************************************************************userinfo=" + data);

                var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(data);
                UserInfosObjectStruct infos = (UserInfosObjectStruct)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonObj.ToString(), typeof(UserInfosObjectStruct));

                if (infos.Recorded == "true")
                {
                    prenom.Text = infos.Prenom;
                    nom.Text = infos.Nom;
                    mail.Text = infos.Mail;
                    gsm.Text = infos.Gsm;

                    dateNaissance.Date = Tools.ConvertDateStringToDate(infos.DateNaissance);
                    nomContact.Text = infos.NomContact;
                    prenomContact.Text = infos.PrenomContact;
                    gsmContact.Text = infos.GsmContact;
                    mailContact.Text = infos.MailContact;

                    heureMatin.Time = Tools.ConvertTimeStringToTime(infos.HeureMatin);
                    heureMidi.Time = Tools.ConvertTimeStringToTime(infos.HeureMidi);
                    heureSoir.Time = Tools.ConvertTimeStringToTime(infos.HeureSoir);
                    if (infos.ApiKey != null && infos.ApiKey != "")
                    {
                        apikey.Text = infos.ApiKey;
                    }

                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Utlisateur non enregistré. ou erreur:" + err.Message.ToString());
            }
        }

        public void ResetInfos()
        {

            prenom.Text = "";
            nom.Text = "";
            mail.Text = "";
            gsm.Text = "";

            dateNaissance.Date = Tools.ConvertDateStringToDate("20/07/2018");
            nomContact.Text = "";
            prenomContact.Text = "";
            gsmContact.Text = "";
            mailContact.Text = "";

            heureMatin.Time = Tools.ConvertTimeStringToTime("00:00");
            heureMidi.Time = Tools.ConvertTimeStringToTime("00:00");
            heureSoir.Time = Tools.ConvertTimeStringToTime("00:00");


        }

        private async Task Bp_delete_ClickedAsync(object sender, EventArgs e) {
            Boolean answer = await DisplayAlert("Question?", "êtes-vous certain de vouloir effacer cet utilisateur?", "Oui", "Non");
            if (answer == true)
            {
                string fileName = PrefsApp.fileUserPref;
                UserInfosObjectStruct UserInfos = new UserInfosObjectStruct
                {
                    Recorded = "",
                    Prenom = "",
                    Nom = "",
                    Mail = "",
                    Gsm = "",
                    DateNaissance = "",
                    NomContact = "",
                    PrenomContact = "",
                    GsmContact = "",
                    MailContact = "",
                    HeureMatin = "",
                    HeureMidi = "",
                    HeureSoir = ""
                };

                UpdateVariablesGlobalUser(UserInfos);

                string json = JsonConvert.SerializeObject(UserInfos);
                DependencyService.Get<IFileReadWrite>().WriteData(fileName, json);
                await DisplayAlert("Info", "Utilisateur effacé.: ", "ok");
                ResetInfos();

            }
        }

        private async Task Bp_record_ClickedAsync(object sender, EventArgs e) {
            Boolean FormCompleted = true;
            Boolean DateNaissanceCompleted = true;
            Boolean HeureCompleted = true;
            Boolean mailUserOk = true;
            Boolean mailContactOk = true;
            Boolean gsmOk = true;
            Boolean gsmContactOk = true;


            /* Création de l'api key */
            string fileNameRead = PrefsApp.fileUserPref;
            string data = DependencyService.Get<IFileReadWrite>().ReadData(fileNameRead);
            var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(data);
            UserInfosObjectStruct infosRead = (UserInfosObjectStruct)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonObj.ToString(), typeof(UserInfosObjectStruct));

            string ApiKey = "";

            if (infosRead.ApiKey == null || infosRead.ApiKey == "")
            {
                // await DisplayAlert("infos", "dedans", "ok");
                ApiKey = Tools.CreateApiKey();
            }
            else
            {
                ApiKey = infosRead.ApiKey;

            }

            string IdSQL = "";
            if (infosRead.Id == null || infosRead.Id == "")
            {
                // await DisplayAlert("infos", "dedans", "ok");
                IdSQL = "";
            }
            else
            {
                IdSQL = infosRead.Id;

            }




            string fileName = PrefsApp.fileUserPref;
            UserInfosObjectStruct UserInfos = new UserInfosObjectStruct();

            if (nom.Text == "")
            {
                FormCompleted = false;
            }
            UserInfos.Nom = nom.Text;
            if (prenom.Text == "")
            {
                FormCompleted = false;
            }
            UserInfos.Prenom = prenom.Text;
            if (mail.Text == "")
            {
                FormCompleted = false;
            }
            UserInfos.Mail = mail.Text;
            if (mail.Text == "")
            {
                FormCompleted = false;
            }
            else
            {
                if (Tools.ValidMail(UserInfos.Mail) == false)
                {
                    mailUserOk = false;
                }
            }

            UserInfos.Gsm = gsm.Text;
            if (gsm.Text == "")
            {
                FormCompleted = false;
            }
            if (Tools.ValidPhone(UserInfos.Gsm) == false)
            {
                gsmOk = false;
            }



            if (dateNaissance.Date.ToString("dd/MM/yyyy") == "20/07/2018")
            {
                DateNaissanceCompleted = false;
            }
            UserInfos.DateNaissance = dateNaissance.Date.ToString("dd/MM/yyyy");

            if (nomContact.Text == "")
            {
                FormCompleted = false;
            }
            UserInfos.NomContact = nomContact.Text;
            if (prenomContact.Text == "")
            {
                FormCompleted = false;
            }
            UserInfos.PrenomContact = prenomContact.Text;

            UserInfos.GsmContact = gsmContact.Text;
            if (gsmContact.Text == "")
            {
                FormCompleted = false;
            }
            if (Tools.ValidPhone(UserInfos.GsmContact) == false)
            {
                gsmContactOk = false;
            }





            UserInfos.MailContact = mailContact.Text;
            if (mailContact.Text == "")
            {
                FormCompleted = false;
            }
            else
            {
                if (Tools.ValidMail(mailContact.Text) == false)
                {
                    mailContactOk = false;
                }
            }






            if (heureMatin.Time.Hours.ToString() + ":" + heureMatin.Time.Minutes.ToString() == "0:0")
            {
                HeureCompleted = false;
            }
            //   Class.Tools.ConvertTime1Chiffre(heureMatin.Time.Hours)  Class.Tools.ConvertTime1Chiffre(heureMatin.Time.Minutes)
            UserInfos.HeureMatin = Tools.ConvertTime1Chiffre(heureMatin.Time.Hours).ToString() + ":" + Tools.ConvertTime1Chiffre(heureMatin.Time.Minutes).ToString();

            if (heureMidi.Time.Hours.ToString() + ":" + heureMidi.Time.Minutes.ToString() == "0:0")
            {
                HeureCompleted = false;
            }
            UserInfos.HeureMidi = Tools.ConvertTime1Chiffre(heureMidi.Time.Hours).ToString() + ":" + Tools.ConvertTime1Chiffre(heureMidi.Time.Minutes).ToString();

            if (heureSoir.Time.Hours.ToString() + ":" + heureSoir.Time.Minutes.ToString() == "0:0")
            {
                HeureCompleted = false;
            }
            UserInfos.HeureSoir = Tools.ConvertTime1Chiffre(heureSoir.Time.Hours).ToString() + ":" + Tools.ConvertTime1Chiffre(heureSoir.Time.Minutes).ToString();

            if (FormCompleted == true && DateNaissanceCompleted == true && HeureCompleted == true && mailUserOk == true && mailContactOk == true && gsmOk == true && gsmContactOk == true)
            {
                UserInfos.ApiKey = ApiKey;
                UserInfos.Recorded = "true";
                UserInfos.Id = IdSQL;
                try
                {
                    string json = JsonConvert.SerializeObject(UserInfos);
                    DependencyService.Get<IFileReadWrite>().WriteData(fileName, json);

                    UpdateVariablesGlobalUser(UserInfos);
                    // Console.WriteLine("json=" + json);
                    /* Requete HTTP */



                    ReponseServeurStructure retourRecordOnline = await RecordOnlineAsync(UserInfos);


                    // nouvel user
                    await DisplayAlert("Info", "Préférences Utilisateur sauvegardé.", "ok");
                    LectureInfos();
                }
                catch (Exception err)
                {
                    await DisplayAlert("Erreur", "erreur: " + err.Message.ToString(), "ok");

                }

            }
            else
            {
                Boolean DateEtHeure = true;
                if (FormCompleted == false)
                {
                    await DisplayAlert("Erreur", "Tout les champs texte doivent être rempli", "OK");
                }
                else
                {
                    if (mailUserOk == false || mailContactOk == false || gsmOk == false || gsmContactOk == false)
                    {
                        if (mailUserOk == false)
                        {
                            await DisplayAlert("Erreur", "le mail utilisateur est incorrecte", "OK");
                        }
                        if (mailContactOk == false)
                        {
                            await DisplayAlert("Erreur", "le mail de la personne de contact est incorrecte", "OK");
                        }
                        if (gsmOk == false)
                        {
                            await DisplayAlert("Erreur", "le gsm de l'utilisateur est incorrecte", "OK");
                        }

                        if (gsmContactOk == false)
                        {
                            await DisplayAlert("Erreur", "le gsm de la personne de contact est incorrecte", "OK");
                        }
                    }
                    else
                    {
                        if (DateNaissanceCompleted == false)
                        {
                            Boolean answer = await DisplayAlert("Question?", "Est-ce que votre date de naissance est bien le 20/07/2018 ? si c'estnon veuillez changer la date, Merci.", "Oui", "Non");
                            if (answer == false)
                            {
                                DateEtHeure = false;
                            }

                        }
                        if (HeureCompleted == false)
                        {

                            var answer = await DisplayAlert("Question?", "Voici Vos heures de repas: Matin->" + heureMatin.Time.Hours.ToString() + ":" + heureMatin.Time.Minutes.ToString() + ", Midi->" + heureMidi.Time.Hours.ToString() + ":" + heureMidi.Time.Minutes.ToString() + "et soir->" + heureSoir.Time.Hours.ToString() + ":" + heureSoir.Time.Minutes.ToString() + ". Est-ce Correcte ? si non veuillez modifie, merci.", "Oui", "Non");
                            if (answer == false)
                            {
                                DateEtHeure = false;
                            }
                        }
                    }

                }


                if (DateEtHeure == true && FormCompleted == true && mailUserOk == true && mailContactOk == true && gsmOk == true && gsmContactOk == true)
                {
                    UserInfos.Recorded = "true";
                    UserInfos.ApiKey = ApiKey;
                    UserInfos.Id = IdSQL;

                    try
                    {
                        string json = JsonConvert.SerializeObject(UserInfos);
                        DependencyService.Get<IFileReadWrite>().WriteData(fileName, json);

                        UpdateVariablesGlobalUser(UserInfos);
                        /* Requete HTTP */


                        // Console.WriteLine("json=" + json);
                        ReponseServeurStructure retourRecordOnline = await RecordOnlineAsync(UserInfos);



                       // await DisplayAlert("Info", "Préférences Utilisateur sauvegardé.", "ok");
                        LectureInfos();
                    }
                    catch (Exception err)
                    {
                        await DisplayAlert("Erreur", "erreur: " + err.Message.ToString(), "ok");

                    }
                }



            }
        }

        public async Task<ReponseServeurStructure> RecordOnlineAsync(UserInfosObjectStruct UserInfos)
        {
            // string Retour = "";
            string Url = "";
            //Console.WriteLine("id=" + UserInfos.Id);
            if (UserInfos.Id != null && UserInfos.Id != "")
            {
                // http://web2.telecom4all.be/diabete_assistant/www/api
                Url = PrefsApp.ApiAddress + "/updateuser.php?recorded=" + UserInfos.Recorded + "&prenom=" + UserInfos.Prenom + "&nom=" + UserInfos.Nom + "&mail=" + UserInfos.Mail + "&gsm=" + UserInfos.Gsm + "&dateNaissance=" + UserInfos.DateNaissance;
                Url = Url + "&nomContact=" + UserInfos.NomContact + "&prenomContact=" + UserInfos.PrenomContact + "&gsmContact=" + UserInfos.GsmContact + "&mailContact=" + UserInfos.MailContact;
                Url = Url + "&heureMatin=" + UserInfos.HeureMatin + "&heureMidi=" + UserInfos.HeureMidi + "&heureSoir=" + UserInfos.HeureSoir + "&apiKey=" + UserInfos.ApiKey + "&id=" + UserInfos.Id;
            }
            else
            {
                // http://web2.telecom4all.be/diabete_assistant/www/api
                Url = PrefsApp.ApiAddress + "/updateuser.php?recorded=" + UserInfos.Recorded + "&prenom=" + UserInfos.Prenom + "&nom=" + UserInfos.Nom + "&mail=" + UserInfos.Mail + "&gsm=" + UserInfos.Gsm + "&dateNaissance=" + UserInfos.DateNaissance;
                Url = Url + "&nomContact=" + UserInfos.NomContact + "&prenomContact=" + UserInfos.PrenomContact + "&gsmContact=" + UserInfos.GsmContact + "&mailContact=" + UserInfos.MailContact;
                Url = Url + "&heureMatin=" + UserInfos.HeureMatin + "&heureMidi=" + UserInfos.HeureMidi + "&heureSoir=" + UserInfos.HeureSoir + "&apiKey=" + UserInfos.ApiKey;

            }


            var client = new HttpClient();
            //client.BaseAddress = new Uri(PrefsApp.ApiAddress);
           // Console.WriteLine("url=" + Url);
            var response = await client.GetAsync(Url);

            //  Console.WriteLine("url=" + response.Content.ReadAsStringAsync().Result);
            ReponseServeurStructure result = JsonConvert.DeserializeObject<ReponseServeurStructure>(response.Content.ReadAsStringAsync().Result);
            if (result.Status == "EXIST")
            {
                if (result.Type == "newUser")
                {

                    // var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(result.Retour);
                    UserInfosObjectStruct infosExist = (UserInfosObjectStruct)Newtonsoft.Json.JsonConvert.DeserializeObject(result.Retour.ToString(), typeof(UserInfosObjectStruct));
                   // Console.WriteLine("infosExist.nom=" + infosExist.Nom);
                   // Console.WriteLine("infosExist.prenom=" + infosExist.Prenom);
                   // Console.WriteLine("infosExist.heure_matin=" + infosExist.HeureMatin);
                   // Console.WriteLine("infosExist.id=" + infosExist.Id);


                    

                    //result.Retour
                    string fileNameReturnExist = PrefsApp.fileUserPref;
                    string dataReturnExist = DependencyService.Get<IFileReadWrite>().ReadData(fileNameReturnExist);

                    var jsonObjReturnExist = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(dataReturnExist);
                    UserInfosObjectStruct infosReturnExist = (UserInfosObjectStruct)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonObjReturnExist.ToString(), typeof(UserInfosObjectStruct));
                    string IdLocal = result.Retour.ToString();
                    // infosReturnExist.Id = IdLocal;
                    infosReturnExist.Recorded = infosExist.Recorded;
                    infosReturnExist.Prenom = infosExist.Prenom;
                    infosReturnExist.Nom = infosExist.Nom;
                    infosReturnExist.Mail = infosExist.Mail;
                    infosReturnExist.Gsm = infosExist.Gsm;
                    infosReturnExist.DateNaissance = infosExist.DateNaissance;
                    infosReturnExist.NomContact = infosExist.NomContact;
                    infosReturnExist.PrenomContact = infosExist.PrenomContact;
                    infosReturnExist.GsmContact = infosExist.GsmContact;
                    infosReturnExist.MailContact = infosExist.MailContact;
                    infosReturnExist.HeureMatin = infosExist.HeureMatin;
                    infosReturnExist.HeureMidi = infosExist.HeureMidi;
                    infosReturnExist.HeureSoir = infosExist.HeureSoir;
                    infosReturnExist.ApiKey = infosExist.ApiKey;
                    infosReturnExist.Id = infosExist.Id;


                    string jsonExist = JsonConvert.SerializeObject(infosReturnExist);
                    Console.WriteLine("*************************************************************json=" + jsonExist);
                    string fileName = PrefsApp.fileUserPref;
                    DependencyService.Get<IFileReadWrite>().WriteData(fileName, jsonExist);
                    UpdateVariablesGlobalUser(infosExist);

                    /* demande mise a jour glycemie infos */
                    bool isReceptionInfosGlycemie =  DemandeInfosGlycemieOnline(infosExist.Id, infosExist.ApiKey);
                    if (isReceptionInfosGlycemie == true)
                    {
                        await DisplayAlert("Infos", "Données Sauvegardé et importé avec succés.", "ok");
                    }
                    else {
                        await DisplayAlert("Erreur", "Il y a eu une erreur lors de l'importation de votre profil.", "ok");
                    }

                }
                else
                {
                    await DisplayAlert("Erreur", "il y a eu une erreur", "ok");
                }
            }
            else if (result.Status == "OK")
            {
                if (result.Type == "newUser")
                {
                    if (result.Retour != "" && result.Retour != null)
                    {
                        string fileNameReturn = PrefsApp.fileUserPref;
                        string dataReturn = DependencyService.Get<IFileReadWrite>().ReadData(fileNameReturn);

                        var jsonObjReturn = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(dataReturn);
                        UserInfosObjectStruct infosReturn = (UserInfosObjectStruct)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonObjReturn.ToString(), typeof(UserInfosObjectStruct));
                        string IdLocal = result.Retour.ToString();
                        infosReturn.Id = IdLocal;
                        string json = JsonConvert.SerializeObject(infosReturn);
                       // Console.WriteLine("*************************************************************json=" + json);
                        string fileName = PrefsApp.fileUserPref;
                        DependencyService.Get<IFileReadWrite>().WriteData(fileName, json);
                        UpdateVariablesGlobalUser(infosReturn);
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "il y a eu une erreur : " + result.Retour, "ok");
                    }

                }
                else
                {
                    //update
                    await DisplayAlert("Infos", "Données Utilisateur Sauvegardée", "ok");
                }
            }
            else
            {
                await DisplayAlert("Erreur", "il y a eu une erreur : " + result.Retour, "ok");
            }




            //   await DisplayAlert("infos", response.Content.ReadAsStringAsync().Result.ToString(), "ok");
            return result;
        }

        private bool DemandeInfosGlycemieOnline(string id, string apiKey)
        {
            bool Retour = false;
            string Url = PrefsApp.ApiAddress + "/readconfglycemie.php";

            string DataSend = "{\"apikey\": \"" + apiKey + "\", \"iduser\": \"" + id + "\"}";

          //  Console.Write("DataSend : " + DataSend);

            var content = new StringContent(JsonConvert.SerializeObject(DataSend));


            var request = (HttpWebRequest)WebRequest.Create(Url);
            request.ContentType = "application/json";
            request.Method = "POST";

         //   Console.Write("Url : " + Url);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(DataSend);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                ReponseServeurStructure resultRetour = JsonConvert.DeserializeObject<ReponseServeurStructure>(result);
                if (resultRetour.Status != "OK")
                {
                    DisplayAlert("Erreur", "Erreur lors de l'importation de votre profil. erreur: " + resultRetour.Retour, "ok");
                }
                else
                {
                    GlycemieInfosObjectStruct infosGlycemieRetour = JsonConvert.DeserializeObject<GlycemieInfosObjectStruct>(resultRetour.Retour);
                    bool isUpdateOK = UpdateVariablesGlobalGlycemie(infosGlycemieRetour);
                    if (isUpdateOK == true)
                    {
                        string json = JsonConvert.SerializeObject(infosGlycemieRetour);
                        DependencyService.Get<IFileReadWrite>().WriteData(PrefsApp.fileConfigGlycemie, json);
                        Retour = true;
                    }
                       // DisplayAlert("Info", "Glycemie101A150Matin = " + infosGlycemieRetour.Glycemie101A150Matin + "Glycemie151A200Midi = " + infosGlycemieRetour.Glycemie151A200Midi, "ok");
                }
            }


            /*
            string fileName = PrefsApp.fileConfigGlycemie;
            string data = DependencyService.Get<IFileReadWrite>().ReadData(fileName);
            var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(data);
            GlycemieInfosObjectStruct InfosGlycemie = (GlycemieInfosObjectStruct)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonObj.ToString(), typeof(GlycemieInfosObjectStruct));
            */


            return Retour;
        }

        public bool UpdateVariablesGlobalGlycemie(GlycemieInfosObjectStruct GlycemieInfos)
        {
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

        public bool UpdateVariablesGlobalUser(UserInfosObjectStruct UserInfos)
        {
            try
            {
                VariablesGlobal.ApiKey = UserInfos.ApiKey;
                VariablesGlobal.DateNaissance = UserInfos.DateNaissance;
                VariablesGlobal.Gsm = UserInfos.Gsm;

                VariablesGlobal.GsmContact = UserInfos.GsmContact;
                VariablesGlobal.HeureMatin = UserInfos.HeureMatin;
                VariablesGlobal.HeureMidi = UserInfos.HeureMidi;

                VariablesGlobal.HeureSoir = UserInfos.HeureSoir;
                VariablesGlobal.Id = UserInfos.Id;
                VariablesGlobal.Mail = UserInfos.Mail;

                VariablesGlobal.MailContact = UserInfos.MailContact;
                VariablesGlobal.Nom = UserInfos.Nom;
                VariablesGlobal.NomContact = UserInfos.NomContact;

                VariablesGlobal.Prenom = UserInfos.Prenom;
                VariablesGlobal.PrenomContact = UserInfos.PrenomContact;
                VariablesGlobal.Recorded = UserInfos.Recorded;

               

                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine("Erreur Update Variable GLobals : " + err.Message.ToString());
                return false;
            }

        }
    }
}