using MonDiabete.Fichiers;
using MonDiabete.Vues;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MonDiabete.Class
{
    public class Horloges : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Tools Tools = new Tools();
        Notifications Notifications = new Notifications();

        string _HeureMain;
        public string HeureMain
        {
            get { return _HeureMain; }
            set
            {
                if (_HeureMain != value)
                {
                    _HeureMain = value;
                    OnPropertyChanged("HeureMain");
                }
            }
        }

        string _HeureMesure;
        public string HeureMesure
        {
            get { return _HeureMesure; }
            set
            {
                if (_HeureMesure != value)
                {
                    _HeureMesure = value;
                    OnPropertyChanged("HeureMesure");
                }
            }
        }

        private void OnPropertyChanged(string Name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        public void SetTimerMain(Label Timer1, INavigation Navigation)
        {
            Binding HeureMainBinding = new Binding
            {
                Source = this,
                Path = "HeureMain"
            };
            Timer1.SetBinding(Label.TextProperty, HeureMainBinding);

            Device.StartTimer(TimeSpan.FromSeconds(1), (Func<bool>)(() =>
            {

                this.HeureMain = DateTime.Now.ToString("HH:mm:ss");
                // Console.WriteLine("*******************************************TIMER 1*******************************************   --  " + this.HeureMain);


                string DateNowTimer = DateTime.Now.ToString("HH:mm");
                // Heure = "18:32";
                if (DateNowTimer == VariablesGlobal.HeureProchaineMesure)
                {
                    VariablesGlobal._isRunningMainTimer = false;
                    VariablesGlobal.MesureIsActive = true;


                    VariablesGlobal.MessageNotification = "C'est l'heure de mesurer votre glycémie et de prendre vos médicaments pour le repas " + VariablesGlobal.HeureProchaineMesureMessage; 
                    Notifications.Sendnotification("mesure", "C'est l'heure de mesurer votre glycémie et de prendre vos médicaments pour le repas " + VariablesGlobal.HeureProchaineMesureMessage);
                    
                    // GO TO MESURE UI
                    Navigation.PushAsync(new MesureUI());


                }

                return VariablesGlobal._isRunningMainTimer;

            }));


        }




        public void SetTimerMesure(Label Timer2, INavigation Navigation, Label Alert)
        {
            Binding HeureMesureBinding = new Binding
            {
                Source = this,
                Path = "HeureMesure"
            };
            Timer2.SetBinding(Label.TextProperty, HeureMesureBinding);

            
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {

                this.HeureMesure = DateTime.Now.ToString("HH:mm:ss");
                string Interval = Tools.RetourIntervalTime(VariablesGlobal.HeureRefMesure, DateTime.Now.ToString("HH:mm"));
                int IntervalInt = int.Parse(Interval);
                string IntervalHoursMin = Tools.GetTimeString(IntervalInt);
                string VueNotification = "";
                bool isNotificationMustSend = false;

                if (IntervalInt == 1)
                {
                    // AlarmeNotification.StartAlarmTéléphone();
                    Console.WriteLine("************************************* INTERVAL 1 Min ----------------------************** ");
                    Alert.Text = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";

                    VariablesGlobal.MessageAlertColorMesurelUI = Color.Orange;
                    VariablesGlobal.MessageAlertMesureUI = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";

                    // AlarmeMesure(thisObj, "", IntervalHoursMin);
                    isNotificationMustSend = true;
                    VueNotification = "mesure";
                    VariablesGlobal.MessageNotification = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";

                }
                else if (IntervalInt == 30)
                {
                    // AlarmeNotification.StartAlarmTéléphone();
                   
                    Alert.Text = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";

                    VariablesGlobal.MessageAlertColorMesurelUI = Color.Orange;
                    VariablesGlobal.MessageAlertMesureUI = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";

                    isNotificationMustSend = true;
                    VueNotification = "mesure";
                    VariablesGlobal.MessageNotification = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                }
                else if (IntervalInt == 45)
                {
                    // AlarmeNotification.StartAlarmTéléphone();
                    Alert.Text = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";

                    VariablesGlobal.MessageAlertColorMesurelUI = Color.Orange;
                    VariablesGlobal.MessageAlertMesureUI = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                    // AlarmeMesure(thisObj, "", IntervalHoursMin);

                    isNotificationMustSend = true;
                    VueNotification = "mesure";
                    VariablesGlobal.MessageNotification = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                }
                else if (IntervalInt == 60)
                {
                    // AlarmeNotification.StartAlarmTéléphone();
                   
                    Alert.Text = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                    //AlarmeMesure(thisObj, "", IntervalHoursMin);

                    VariablesGlobal.MessageAlertColorMesurelUI = Color.Orange;
                    VariablesGlobal.MessageAlertMesureUI = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";

                    isNotificationMustSend = true;
                    VueNotification = "mesure";
                    VariablesGlobal.MessageNotification = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                }
                else if (IntervalInt == 75)
                {
                    //  AlarmeNotification.StartAlarmTéléphone();
                   
                    Alert.Text = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                    //AlarmeMesure(thisObj ,"", IntervalHoursMin);

                    VariablesGlobal.MessageAlertColorMesurelUI = Color.Orange;
                    VariablesGlobal.MessageAlertMesureUI = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";

                    isNotificationMustSend = true;
                    VueNotification = "mesure";
                    VariablesGlobal.MessageNotification = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                }
                else if (IntervalInt == 90)
                {
                    //  AlarmeNotification.StartAlarmTéléphone();
                   
                    Alert.Text = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                    //AlarmeMesure(thisObj, "", IntervalHoursMin);

                    VariablesGlobal.MessageAlertColorMesurelUI = Color.Orange;
                    VariablesGlobal.MessageAlertMesureUI = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";

                    isNotificationMustSend = true;
                    VueNotification = "mesure";
                    VariablesGlobal.MessageNotification = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                }
                else if (IntervalInt == 105)
                {
                    //  AlarmeNotification.StartAlarmTéléphone();
                   
                    Alert.Text = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                    //AlarmeMesure(thisObj ,"", IntervalHoursMin);

                    VariablesGlobal.MessageAlertColorMesurelUI = Color.Orange;
                    VariablesGlobal.MessageAlertMesureUI = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";

                    isNotificationMustSend = true;
                    VueNotification = "mesure";
                    VariablesGlobal.MessageNotification = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                }
                else if (IntervalInt == 120)
                {
                    //  AlarmeNotification.StartAlarmTéléphone();
                   
                    Alert.Text = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                    //AlarmeMesure(thisObj ,"", IntervalHoursMin);

                    VariablesGlobal.MessageAlertColorMesurelUI = Color.Red;
                    VariablesGlobal.MessageAlertMesureUI = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";

                    isNotificationMustSend = true;
                    VueNotification = "mesure";
                    VariablesGlobal.MessageNotification = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                }
                else if (IntervalInt == 135)
                {
                    //  AlarmeNotification.StartAlarmTéléphone();
                    
                    Alert.Text = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                    //AlarmeMesure(thisObj ,"", IntervalHoursMin);

                    VariablesGlobal.MessageAlertColorMesurelUI = Color.Red;
                    VariablesGlobal.MessageAlertMesureUI = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";

                    isNotificationMustSend = true;
                    VueNotification = "mesure";
                    VariablesGlobal.MessageNotification = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                }
                else if (IntervalInt == 150)
                {
                    //   AlarmeNotification.StartAlarmTéléphone();
                    
                    Alert.Text = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                    //AlarmeMesure(thisObj, "", IntervalHoursMin);

                    VariablesGlobal.MessageAlertColorMesurelUI = Color.Red;
                    VariablesGlobal.MessageAlertMesureUI = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";

                    isNotificationMustSend = true;
                    VueNotification = "mesure";
                    VariablesGlobal.MessageNotification = "Cela fait : " + IntervalHoursMin + " que vous auriez du prendre vos médicaments et faire votre mesure de glycémie.";
                }
                else if (IntervalInt == 165)
                {
                    VariablesGlobal.MessageAlertColorMesurelUI = Color.Red;
                    VariablesGlobal.MessageAlertMesureUI = "";
                   // Alert.Text = "Vous avez raté la prise de mesure et de médicament. Attention !!!";

                    VariablesGlobal.MesureIsActive = false;

                    VariablesGlobal.MessageAlertGeneralUI = "Vous avez raté la prise de mesure et de médicament " + VariablesGlobal.HeureProchaineMesureMessage + ". Attention !!!";
                    VariablesGlobal.MessageAlertColorGeneralUI = Color.Red;



                    isNotificationMustSend = true;
                    VueNotification = "general";
                    VariablesGlobal.MessageNotification = "Vous avez raté la prise de mesure et de médicament " + VariablesGlobal.HeureProchaineMesureMessage + ". Attention !!!";

                   

                   
                }

                if (isNotificationMustSend == true)
                {
                    if (VueNotification == "general")
                    {
                        Notifications.Sendnotification(VueNotification, VariablesGlobal.MessageAlertGeneralUI);
                        // GO TO General UI
                        Navigation.PushAsync(new GeneralUI());
                        // MainPageLocal.UIBase("rater", "Vous avez raté la prise de mesure et de médicament pour " + PrefsApp.NomHeure + ". Attention !!!");
                    }
                    else {
                        Notifications.Sendnotification(VueNotification, VariablesGlobal.MessageAlertMesureUI);
                    }
                    
                }

                return VariablesGlobal._isRunningMesureTimer;


            });
        }



    }
}
