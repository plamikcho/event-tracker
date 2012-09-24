
namespace BulstarCheck
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += 
                new ThreadExceptionEventHandler(new ThreadExceptionHandler().ApplicationThreadException);
            Application.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("bg-BG");
            Thread.CurrentThread.CurrentCulture = Application.CurrentCulture;
            Application.Run(new FormMain());
        }

        public class ThreadExceptionHandler
        {
            public void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
            {
                string message = string.Empty;
                string location = string.Empty;
                if (e.Exception is BulstarCheckException)
                {
                    var ex = e.Exception as BulstarCheckException;
                    message = ex.ErrorMessage;
                    location = ex.Location;
                    MessageBox.Show(message, Messages.exception,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    message = e.Exception.ToString();
                    location = "main";
                    MessageBox.Show(message, Messages.exception,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }                
            }
        }
    }
}