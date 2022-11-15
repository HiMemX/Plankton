using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System;

namespace Plankton
{
    partial class NewParcelWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.addParcelButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.sectionTypeListBoxLabel = new System.Windows.Forms.Label();
            this.packLangIDListBoxLabel = new System.Windows.Forms.Label();
            this.packLangIDListBox = new System.Windows.Forms.ComboBox();
            this.parcelTypeListBox = new System.Windows.Forms.ComboBox();
            this.parcelTypeListBoxLabel = new System.Windows.Forms.Label();
            this.reversedCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // addParcelButton
            // 
            this.addParcelButton.Location = new System.Drawing.Point(12, 126);
            this.addParcelButton.Name = "addParcelButton";
            this.addParcelButton.Size = new System.Drawing.Size(336, 23);
            this.addParcelButton.TabIndex = 0;
            this.addParcelButton.Text = "Add Parcel";
            this.addParcelButton.UseVisualStyleBackColor = true;
            this.addParcelButton.Click += new System.EventHandler(this.addParcelButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "SECT",
            "P    ",
            "PTEX",
            "PFST"});
            this.comboBox1.Location = new System.Drawing.Point(138, 15);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(210, 23);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // sectionTypeListBoxLabel
            // 
            this.sectionTypeListBoxLabel.AutoSize = true;
            this.sectionTypeListBoxLabel.Location = new System.Drawing.Point(12, 18);
            this.sectionTypeListBoxLabel.Name = "sectionTypeListBoxLabel";
            this.sectionTypeListBoxLabel.Size = new System.Drawing.Size(76, 15);
            this.sectionTypeListBoxLabel.TabIndex = 2;
            this.sectionTypeListBoxLabel.Text = "Section Type";
            // 
            // packLangIDListBoxLabel
            // 
            this.packLangIDListBoxLabel.AutoSize = true;
            this.packLangIDListBoxLabel.Location = new System.Drawing.Point(12, 47);
            this.packLangIDListBoxLabel.Name = "packLangIDListBoxLabel";
            this.packLangIDListBoxLabel.Size = new System.Drawing.Size(101, 15);
            this.packLangIDListBoxLabel.TabIndex = 3;
            this.packLangIDListBoxLabel.Text = "Parcel Language";
            // 
            // packLangIDListBox
            // 
            
            this.packLangIDListBox.FormattingEnabled = true;
            this.packLangIDListBox.Items.AddRange(new object[] {
            HoArchive.LanguageID.Neutral,
            HoArchive.LanguageID.Arabic,
            HoArchive.LanguageID.Chinese,
            HoArchive.LanguageID.Czech,
            HoArchive.LanguageID.Danish,
            HoArchive.LanguageID.German,
            HoArchive.LanguageID.Greek,
            HoArchive.LanguageID.English,
            HoArchive.LanguageID.Spanish,
            HoArchive.LanguageID.Finnish,
            HoArchive.LanguageID.French,
            HoArchive.LanguageID.Italian,
            HoArchive.LanguageID.Japanese,
            HoArchive.LanguageID.Korean,
            HoArchive.LanguageID.Dutch,
            HoArchive.LanguageID.Norwegian,
            HoArchive.LanguageID.Polish,
            HoArchive.LanguageID.Portugese,
            HoArchive.LanguageID.Russian,
            HoArchive.LanguageID.Slovak,
            HoArchive.LanguageID.Swedish,
            HoArchive.LanguageID.Ukrainian,
            HoArchive.LanguageID.ArabicSaudiArabia,
            HoArchive.LanguageID.ChineseTaiwan,
            HoArchive.LanguageID.CzechRepublic,
            HoArchive.LanguageID.DanishDenmark,
            HoArchive.LanguageID.GermanGermany,
            HoArchive.LanguageID.GreekGreece,
            HoArchive.LanguageID.EnglishUS,
            HoArchive.LanguageID.FinnishFinland,
            HoArchive.LanguageID.FrenchFrance,
            HoArchive.LanguageID.ItalianItaly,
            HoArchive.LanguageID.JapaneseJapan,
            HoArchive.LanguageID.KoreanKorea,
            HoArchive.LanguageID.DutchNetherlands,
            HoArchive.LanguageID.PolishPoland,
            HoArchive.LanguageID.PortugeseBrazilian,
            HoArchive.LanguageID.RussianFederation,
            HoArchive.LanguageID.SlovakSlovak,
            HoArchive.LanguageID.SwedishSweden,
            HoArchive.LanguageID.UkrainianUkraine,
            HoArchive.LanguageID.ChineseChina,
            HoArchive.LanguageID.GermanSwiss,
            HoArchive.LanguageID.EnglishUK,
            HoArchive.LanguageID.SpanishMexican,
            HoArchive.LanguageID.NorwegianNynorsk,
            HoArchive.LanguageID.PortugesePortugal,
            HoArchive.LanguageID.ChineseHongKong,
            HoArchive.LanguageID.EnglishAustralia,
            HoArchive.LanguageID.SpanishSpain,
            HoArchive.LanguageID.ChineseSingapore,
            HoArchive.LanguageID.EnglishCanada});
            this.packLangIDListBox.Location = new System.Drawing.Point(138, 44);
            this.packLangIDListBox.Name = "packLangIDListBox";
            this.packLangIDListBox.Size = new System.Drawing.Size(210, 23);
            this.packLangIDListBox.TabIndex = 4;
            // 
            // parcelTypeListBox
            // 
            this.parcelTypeListBox.FormattingEnabled = true;
            this.parcelTypeListBox.Items.AddRange(new object[] {
            HoArchive.enParcelType.PARCEL_TYPE_UNDEFINED,
            HoArchive.enParcelType.PARCEL_TYPE_EXCLUSIVE,
            HoArchive.enParcelType.PARCEL_TYPE_SHARED,
            HoArchive.enParcelType.PARCEL_TYPE_FROMDOMAIN});
            this.parcelTypeListBox.Location = new System.Drawing.Point(138, 73);
            this.parcelTypeListBox.Name = "parcelTypeListBox";
            this.parcelTypeListBox.Size = new System.Drawing.Size(210, 23);
            this.parcelTypeListBox.TabIndex = 5;
            // 
            // parcelTypeListBoxLabel
            // 
            this.parcelTypeListBoxLabel.AutoSize = true;
            this.parcelTypeListBoxLabel.Location = new System.Drawing.Point(12, 76);
            this.parcelTypeListBoxLabel.Name = "parcelTypeListBoxLabel";
            this.parcelTypeListBoxLabel.Size = new System.Drawing.Size(70, 15);
            this.parcelTypeListBoxLabel.TabIndex = 6;
            this.parcelTypeListBoxLabel.Text = "Parcel Type";
            // 
            // reversedCheckBox
            // 
            this.reversedCheckBox.AutoSize = true;
            this.reversedCheckBox.Location = new System.Drawing.Point(269, 101);
            this.reversedCheckBox.Name = "reversedCheckBox";
            this.reversedCheckBox.Size = new System.Drawing.Size(79, 19);
            this.reversedCheckBox.TabIndex = 7;
            this.reversedCheckBox.Text = "Reversed";
            this.reversedCheckBox.UseVisualStyleBackColor = true;
            this.reversedCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // NewParcelWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 161);
            this.Controls.Add(this.reversedCheckBox);
            this.Controls.Add(this.parcelTypeListBoxLabel);
            this.Controls.Add(this.parcelTypeListBox);
            this.Controls.Add(this.packLangIDListBox);
            this.Controls.Add(this.packLangIDListBoxLabel);
            this.Controls.Add(this.sectionTypeListBoxLabel);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.addParcelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewParcelWindow";
            this.ShowIcon = false;
            this.Text = "New Parcel";
            this.Load += new System.EventHandler(this.NewParcelWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addParcelButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label sectionTypeListBoxLabel;
        private System.Windows.Forms.Label packLangIDListBoxLabel;
        private System.Windows.Forms.ComboBox packLangIDListBox;
        private System.Windows.Forms.ComboBox parcelTypeListBox;
        private System.Windows.Forms.Label parcelTypeListBoxLabel;
        private System.Windows.Forms.CheckBox reversedCheckBox;
    }
}