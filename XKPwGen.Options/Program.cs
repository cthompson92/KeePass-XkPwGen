using System;
using System.Windows.Forms;
using XKPwGen.SharedKernel;

namespace XKPwGen.Options
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = Form1.Default();
            mainForm.OnSaveButtonClicked += OptionsManager.SaveOptions;
            Application.Run(mainForm);
        }
    }
}
