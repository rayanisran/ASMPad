using System;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using ASMPad.Properties;

namespace ASMPad
{
    class ProgramHex
    {
        public const string SOFTWARENAME = "ASMPad :: Hex Editor";
        public static DialogResult ShowError(Exception ex)
        {
            return ShowError("An error occured.\n\n" + ex.Message);
        } 


        internal static DialogResult ShowError(string text)
        {
            DialogResult result = MessageBox.Show(text, SOFTWARENAME,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return result;
        }

        public static void ShowMessage(string text)
        {
            MessageBox.Show(text, SOFTWARENAME,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        public static DialogResult ShowQuestion(string text)
        {
            DialogResult result = MessageBox.Show(text, SOFTWARENAME,
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);
            return result;
        }
    }
}
