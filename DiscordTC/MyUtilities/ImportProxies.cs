using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace DiscordTC
{
    internal class Import
    {
        public static string[] Load(string type)
        {
            while (true)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    try
                    {
                        ofd.Multiselect = false;
                        ofd.CheckFileExists = true;
                        ofd.RestoreDirectory = true;
                        ofd.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
                        ofd.Filter = @"Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                        ofd.Title = $"Load " + type;

                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            var array1 = File.ReadAllLines(ofd.FileName).ToArray();
                            if (array1.Length != 0) return array1;
                            Console.WriteLine("Couldn't load " + type + ", ...\n");
                            Thread.Sleep(-1);

                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An unexpected error occurred!\n");
                        MessageBox.Show(ex.ToString(), $"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Thread.Sleep(-1);
                    }
                }
            }
        }
    }
}