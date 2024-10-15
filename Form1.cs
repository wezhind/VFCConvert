using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using vCardLib;
using vCardLib.Deserialization;
using vCardLib.Enums;
using vCardLib.Models;
using vCardLib.Serialization;

namespace NextcloudVCF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Populate the ComboBox with version options
            comboBoxVersion.Items.Add("3.0");
            comboBoxVersion.Items.Add("4.0");
            comboBoxVersion.SelectedIndex = 0; // Set default selection to 3.0
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "vCard files (*.vcf)|*.vcf";
                openFileDialog.Title = "Open a VCard File";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtInputFile.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "vCard files (*.vcf)|*.vcf";
                saveFileDialog.Title = "Save a VCard File";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputFile.Text = saveFileDialog.FileName;
                }
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            string inputFilePath = txtInputFile.Text;
            string outputFilePath = txtOutputFile.Text;

            if (string.IsNullOrEmpty(outputFilePath))
            {
                MessageBox.Show("Please specify an output file path.");
                return;
            }

            try
            {
                if (string.IsNullOrEmpty(inputFilePath))
                {
                    MessageBox.Show("Please select a vCard file first.");
                    return;
                }

                // Read the entire file content
                string content = File.ReadAllText(inputFilePath);

                // Check for trailing whitespace
                if (content.EndsWith(" ") || content.EndsWith("\n") || content.EndsWith("\r"))
                {
                    MessageBox.Show("Warning: The vCard file has trailing whitespace at the end. Attempting to clean it up to continue conversion.");
                    content = content.TrimEnd();
                }

                // Deserialize vCards from the selected file
                IEnumerable<vCard> vCards = vCardDeserializer.FromContent(content);

                // Check if vCards were loaded
                if (vCards != null && vCards.Any())
                {
                    MessageBox.Show($"Number of vCards loaded: {vCards.Count()}");

                    // Get selected vCard version
                    vCardVersion selectedVersion = comboBoxVersion.SelectedItem.ToString() == "4.0" ? vCardVersion.v4 : vCardVersion.v3;

                    // Serialize the vCards and save to the output file
                    using (var writer = new StreamWriter(outputFilePath))
                    {
                        foreach (var vCard in vCards)
                        {
                            // Use the static Serialize method directly
                            var serialized = vCardSerializer.Serialize(vCard, selectedVersion);
                            writer.WriteLine(serialized);
                            writer.WriteLine(); // Add a new line between vCards
                        }
                    }

                    MessageBox.Show("Conversion successful! Output saved to: " + outputFilePath);
                }
                else
                {
                    MessageBox.Show("No vCards found in the file.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


    }
}
