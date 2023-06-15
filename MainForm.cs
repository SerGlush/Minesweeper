using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurs
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class MainForm : Form
    {
        private TreeView treeView;
        private DataGridView dataGridView;
        private DataGridViewTextBoxColumn dataGridName;
        private DataGridViewTextBoxColumn dataGridType;
        private DataGridViewTextBoxColumn dataGridValue;
        private ContextMenuStrip stripTree;
        private ContextMenuStrip stripDataGrid;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ToolStripMenuItem addSectionToolStripMenuItem;
        private ToolStripMenuItem renameToolStripMenuItem;
        private ToolStripMenuItem ddParameterToolStripMenuItem;
        private ToolStripMenuItem addParameterToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem1;
        private ToolStripMenuItem changeToolStripMenuItem;

        private System.ComponentModel.IContainer components = null;
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
            components = new System.ComponentModel.Container();
            treeView = new TreeView();
            stripTree = new ContextMenuStrip(components);
            addToolStripMenuItem = new ToolStripMenuItem();
            addSectionToolStripMenuItem = new ToolStripMenuItem();
            ddParameterToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            renameToolStripMenuItem = new ToolStripMenuItem();
            dataGridView = new DataGridView();
            dataGridName = new DataGridViewTextBoxColumn();
            dataGridType = new DataGridViewTextBoxColumn();
            dataGridValue = new DataGridViewTextBoxColumn();
            stripDataGrid = new ContextMenuStrip(components);
            addParameterToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem1 = new ToolStripMenuItem();
            changeToolStripMenuItem = new ToolStripMenuItem();
            stripTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGridView)).BeginInit();
            stripDataGrid.SuspendLayout();
            SuspendLayout();

            treeView.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right);
            treeView.ContextMenuStrip = stripTree;
            treeView.Location = new System.Drawing.Point(12, 12);
            treeView.Name = "treeView1";
            treeView.Size = new System.Drawing.Size(250, 426);
            treeView.TabIndex = 0;
            treeView.BeforeExpand += new TreeViewCancelEventHandler(this.treeViewBeforeExpand);
            treeView.AfterSelect += new TreeViewEventHandler(this.treeViewAfterExpand);
            treeView.MouseClick += new MouseEventHandler(this.treeViewMouseClick);

            stripTree.ImageScalingSize = new System.Drawing.Size(20, 20);
            stripTree.Items.AddRange(new ToolStripItem[] {
            addToolStripMenuItem,
            removeToolStripMenuItem,
            renameToolStripMenuItem});
            stripTree.Name = "contextMenuStrip1";
            stripTree.Size = new System.Drawing.Size(118, 70);

            addToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            addSectionToolStripMenuItem,
            ddParameterToolStripMenuItem});
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            addToolStripMenuItem.Text = "Add";

            addSectionToolStripMenuItem.Name = "addSectionToolStripMenuItem";
            addSectionToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            addSectionToolStripMenuItem.Text = "Add Section";
            addSectionToolStripMenuItem.Click += new EventHandler(addSectionStripClick);

            ddParameterToolStripMenuItem.Name = "ddParameterToolStripMenuItem";
            ddParameterToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            ddParameterToolStripMenuItem.Text = "Add Parameter";
            ddParameterToolStripMenuItem.Click += new EventHandler(addParameterToolStripMenuItem_Click);

            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            removeToolStripMenuItem.Text = "Remove";
            removeToolStripMenuItem.Click += new EventHandler(removeToolStripMenuItem_Click);

            renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            renameToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            renameToolStripMenuItem.Text = "Rename";
            renameToolStripMenuItem.Click += new EventHandler(renameToolStripMenuItem_Click);

            dataGridView.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right);
            dataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] {
            dataGridName,
            dataGridType,
            dataGridValue});
            dataGridView.ContextMenuStrip = stripDataGrid;
            dataGridView.GridColor = System.Drawing.Color.White;
            dataGridView.Location = new System.Drawing.Point(268, 12);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView1";
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowHeadersWidth = 51;
            dataGridView.RowTemplate.Height = 25;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new System.Drawing.Size(520, 425);
            dataGridView.TabIndex = 1;
            dataGridView.CellMouseDown += new DataGridViewCellMouseEventHandler(dataGridView1_CellMouseDown);

            dataGridName.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridName.HeaderText = "Name";
            dataGridName.MinimumWidth = 6;
            dataGridName.Name = "Name";
            dataGridName.ReadOnly = true;
            dataGridName.Width = 64;

            dataGridType.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridType.HeaderText = "Type";
            dataGridType.MinimumWidth = 6;
            dataGridType.Name = "Type";
            dataGridType.ReadOnly = true;
            dataGridType.Width = 56;

            dataGridValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridValue.HeaderText = "Value";
            dataGridValue.MinimumWidth = 6;
            dataGridValue.Name = "Value";
            dataGridValue.ReadOnly = true;
            dataGridValue.Width = 60;

            stripDataGrid.ImageScalingSize = new System.Drawing.Size(20, 20);
            stripDataGrid.Items.AddRange(new ToolStripItem[] {
            addParameterToolStripMenuItem,
            removeToolStripMenuItem1,
            changeToolStripMenuItem});
            stripDataGrid.Name = "contextMenuStrip2";

            addParameterToolStripMenuItem.Name = "addParameterToolStripMenuItem";
            addParameterToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            addParameterToolStripMenuItem.Text = "Add parameter";
            addParameterToolStripMenuItem.Click += new EventHandler(addParameterToolStripMenuItem_Click);

            removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            removeToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            removeToolStripMenuItem1.Text = "Remove";
            removeToolStripMenuItem1.Click += new EventHandler(removeToolStripMenuItem1_Click);
 
            changeToolStripMenuItem.Name = "changeToolStripMenuItem";
            changeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            changeToolStripMenuItem.Text = "Change";
            changeToolStripMenuItem.Click += new EventHandler(changeToolStripMenuItem_Click);

            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(dataGridView);
            Controls.Add(treeView);
            Text = "Registry Monitor";
            Load += new EventHandler(FormLoad);
            stripTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(dataGridView)).EndInit();
            stripDataGrid.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
