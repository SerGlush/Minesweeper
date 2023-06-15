using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurs
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class InputParameterWindow : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button buttonOK;
        private Button buttonCancel;
        private ComboBox comboBox1;
        private ErrorProvider errorProvider1;
        private ErrorProvider errorProvider2;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            buttonOK = new Button();
            buttonCancel = new Button();
            comboBox1 = new ComboBox();
            errorProvider1 = new ErrorProvider(components);
            errorProvider2 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider2).BeginInit();
            SuspendLayout();

            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 14.0f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(200, 10);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(355, 38);
            label1.TabIndex = 0;
            label1.Text = "Parameter Info";

            textBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBox1.Location = new System.Drawing.Point(33, 125);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(163, 34);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += new EventHandler(textBox1_TextChanged);

            textBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBox2.Location = new System.Drawing.Point(404, 125);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(237, 34);
            textBox2.TabIndex = 2;
            textBox2.TextChanged += new EventHandler(textBox2_TextChanged);

            buttonOK.AutoSize = true;
            buttonOK.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonOK.Location = new System.Drawing.Point(144, 186);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new System.Drawing.Size(160, 41);
            buttonOK.TabIndex = 4;
            buttonOK.Text = "Ok";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += new EventHandler(buttonOK_Click);

            buttonCancel.AutoSize = true;
            buttonCancel.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonCancel.Location = new System.Drawing.Point(404, 186);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(160, 41);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;

            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label2.Location = new System.Drawing.Point(33, 80);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(114, 31);
            label2.TabIndex = 6;
            label2.Text = "Name";

            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label3.Location = new System.Drawing.Point(237, 80);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(52, 31);
            label3.TabIndex = 7;
            label3.Text = "Type";

            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label4.Location = new System.Drawing.Point(404, 80);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(154, 31);
            label4.TabIndex = 8;
            label4.Text = "Registry Value";

            errorProvider1.ContainerControl = this;

            errorProvider2.ContainerControl = this;

            comboBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(237, 125);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(151, 36);
            comboBox1.TabIndex = 9;

            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(687, 239);
            Controls.Add(comboBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "AddFile";
            Text = "AddFile";
            Load += new EventHandler(AddFile_Load);
            ((System.ComponentModel.ISupportInitialize)(errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(errorProvider2)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private RegistryKey registryKey;

        public string Name { get; set; }

        public object Value { get; set; }

        private string _name;

        public InputParameterWindow(RegistryKey registryKey)
        {
            InitializeComponent();
            this.registryKey = registryKey;
        }
        public InputParameterWindow(RegistryKey registryKey, string Name)
        {
            InitializeComponent();
            this.registryKey = registryKey;
            _name = Name;
        }
        private string getStringByType(RegistryValueKind kind, object value)
        {
            switch (kind)
            {
                case RegistryValueKind.Binary:
                    StringBuilder result = new StringBuilder();
                    foreach (byte b in (byte[])value) result.Append(Convert.ToString(b, 16) + " ");
                    return result.ToString();
                case RegistryValueKind.DWord:
                default: return value.ToString();
            }
        }
        private void AddFile_Load(object sender, EventArgs e)
        {
            buttonCancel.DialogResult = DialogResult.Cancel;
            comboBox1.Items.Add(RegistryValueKind.Binary);
            comboBox1.Items.Add(RegistryValueKind.DWord);
            comboBox1.Items.Add(RegistryValueKind.String);
            comboBox1.SelectedItem = comboBox1.Items[0];
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            if (_name != null)
            {
                RegistryValueKind kind = registryKey.GetValueKind(_name);
                textBox1.Text = _name;
                textBox2.Text = getStringByType(kind, registryKey.GetValue(_name));
                int index;
                if (kind == RegistryValueKind.Binary) index = 0;
                else if (kind == RegistryValueKind.DWord) index = 1;
                else if (kind == RegistryValueKind.String) index = 2;
                else
                    index = comboBox1.Items.Add(kind);
                comboBox1.SelectedItem = comboBox1.Items[index];
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string value = textBox2.Text.Trim();
            if (!string.IsNullOrEmpty(name) && (!registryKey.GetValueNames().Contains(name) || _name != null && _name == name))
            {
                if (ValidateValue(value, comboBox1.Text, out object result))
                {
                    Name = name;
                    Value = result;
                    DialogResult = DialogResult.OK;
                }
                else errorProvider2.SetError(textBox2, "Parameter value is not correct");
            }
            else errorProvider1.SetError(textBox1, "Parameter value string is not correct");
        }
        private bool ValidateValue(string value, string Type, out object result)
        {
            try
            {
                switch (Type)
                {
                    case "Binary":
                        if (string.IsNullOrEmpty(value)) { result = null; return false; }
                        string[] strs = value.Split(" ");
                        byte[] bytes = new byte[strs.Length];
                        for (int i = 0; i < strs.Length; i++) bytes[i] = Convert.ToByte(strs[i].Trim(), 16);
                        result = bytes;
                        return true;
                    case "DWord": result = Convert.ToInt32(value); return true;
                    default: result = value; return true;
                }
            }
            catch (Exception e) { result = null; return false; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
        }
    }
}
