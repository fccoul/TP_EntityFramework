using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
 
namespace TaskAsynchronous
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region synchrone
        private void btnCall_Click(object sender, EventArgs e)
        {
           txtLable.Text=   longRunningMethod(txtInput.Text);
        }

        string longRunningMethod(string message)
        {
            Thread.Sleep(5000);
            return "Bonjour " + message;
        }
        #endregion


        #region asynchrone

        private void btnCallAsynchron_Click(object sender, EventArgs e)
        {
            CallLongRunningmethod();
           txtLable.Text=   "Traitement en cours...";
        }

        private async void CallLongRunningmethod()
        {
            //Le mot clé « Await » assure que le reste de la méthode n’est pas exécuté tant que l’exécution de LongRunningMethod() n’est pas complétée
            string result = await LongRunningmethodAsync(txtInput.Text);
                //-du coup l'instruction qui suit attendra :ici txtLable
            //-Une fois que la méthode est terminée, le contenu de notre label pourra être mis à jour. 
            //Toutefois, le mot-clé « Await » ne bloque pas le thread complètement.
            txtLable.Text = result;
        }


        // Il est d’usage de donner le même nom de lamethode qui doit etre asynchrone, mais d’y ajouter « Async ».
        private Task<string> LongRunningmethodAsync(string p)
        {
            //-C’est-à-dire que c’est une tâche qui retourne un string.
            return Task.Run<string>(() => longRunningMethod_1(p));
           
        }

        private string longRunningMethod_1(string p)
        {
            Thread.Sleep(5000);
            return "Hello " + p;
        }
        #endregion

        private void btnCallAsync2_Click(object sender, EventArgs e)
        {
            this.txtAsync2.Text += "\r\n Debut procedure principal";
            //LancerTraitement();
             LancerTraitementAsync();
            this.txtAsync2.Text += "\r\n Suite procédure principal";
        }

        private async Task LancerTraitementAsync()
        {
            this.txtAsync2.Text += "\r\n Lancement Traitement";
            //await Task.Run(() => Task.Delay(10000));//OK
            //await Task.Run(() => LancerTraitement());//--- pareil au lancement precedent :Task.Delay(10000));


            //-meme exemple avec progression
            //---Mise à jour de la progression dans la textbox 
            var progress=new Progress<int>(percent=>
            {
                this.txtAsync2.Text+=string.Format("\r\n Attente :{0} %",percent);
            });

            //lancement asynchrone
            await Task.Run(() => LancerTraitement(progress));

            this.txtAsync2.Text += "\r\n Fin Traitement";
        }

        private void LancerTraitement()
        {
            for(int i=0;i<10;i++)
            {
                Thread.Sleep(1000);
            }
        }

        private void LancerTraitement(IProgress<int> progress)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                //--rapporte la progression
                if (progress != null)
                    progress.Report((i + 1) * 10);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             
        }
    }
}
