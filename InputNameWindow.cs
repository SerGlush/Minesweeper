using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurs
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class InputNameWindow : Form
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            buttonOK = new Button();
            buttonCancel = new Button();
            label1 = new Label();
            textBox1 = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            ((ISupportInitialize)(errorProvider1)).BeginInit();
            SuspendLayout();

            buttonOK.AutoSize = true;
            buttonOK.Font = new Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonOK.Location = new Point(97, 140);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(81, 31);
            buttonOK.TabIndex = 0;
            buttonOK.Text = "Accept";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += new EventHandler(buttonOK_Click);

            buttonCancel.AutoSize = true;
            buttonCancel.Font = new Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonCancel.Location = new Point(257, 140);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 31);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;

            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.Location = new Point(73, 30);
            label1.Name = "label1";
            label1.Size = new Size(284, 30);
            label1.TabIndex = 2;
            label1.Text = "Input Segment name";

            textBox1.Font = new Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBox1.Location = new Point(97, 79);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(235, 29);
            textBox1.TabIndex = 3;
            textBox1.TextChanged += new EventHandler(textBox1_TextChanged);

            errorProvider1.ContainerControl = this;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new Size(438, 189);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Name = "EnterName";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "EnterName";
            Load += new EventHandler(EnterName_Load);
            ((ISupportInitialize)(errorProvider1)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private Button buttonOK;
        private Button buttonCancel;
        private Label label1;
        private TextBox textBox1;
        private ErrorProvider errorProvider1;

        private RegistryNode registryNode;
        public string Name { get; set; }
        public InputNameWindow(RegistryNode node)
        {
            InitializeComponent();
            registryNode = node;
        }

        private void EnterName_Load(object sender, EventArgs e)
        {
            buttonCancel.DialogResult = DialogResult.Cancel;
            textBox1.Focus();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            String name = textBox1.Text.Trim();
            if (!String.IsNullOrEmpty(name) && !registryNode.childNodes.ContainsKey(name))
            {
                Name = name;
                DialogResult = DialogResult.OK;
            }
            else errorProvider1.SetError(textBox1, "String is empty");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
    }
}
