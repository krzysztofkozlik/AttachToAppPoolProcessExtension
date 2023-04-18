//namespace AttachToAppPoolProcessExtension.Options
//{
//    partial class GenearlOptionsControl
//    {
//        /// <summary> 
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary> 
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Component Designer generated code

//        /// <summary> 
//        /// Required method for Designer support - do not modify 
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.textBoxName = new System.Windows.Forms.TextBox();
//            this.buttonUpdate = new System.Windows.Forms.Button();
//            this.listViewProcesses = new System.Windows.Forms.ListView();
//            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
//            this.columnHeaderAppPool = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
//            this.buttonRemove = new System.Windows.Forms.Button();
//            this.buttonAdd = new System.Windows.Forms.Button();
//            this.labelName = new System.Windows.Forms.Label();
//            this.labelAppPool = new System.Windows.Forms.Label();
//            this.textBoxAppPool = new System.Windows.Forms.TextBox();
//            this.buttonImport = new System.Windows.Forms.Button();
//            this.SuspendLayout();
//            // 
//            // textBoxName
//            // 
//            this.textBoxName.Location = new System.Drawing.Point(122, 293);
//            this.textBoxName.Name = "textBoxName";
//            this.textBoxName.Size = new System.Drawing.Size(463, 31);
//            this.textBoxName.TabIndex = 0;
//            this.textBoxName.Leave += new System.EventHandler(this.textBox1_Leave);
//            // 
//            // buttonUpdate
//            // 
//            this.buttonUpdate.Location = new System.Drawing.Point(591, 293);
//            this.buttonUpdate.Name = "buttonUpdate";
//            this.buttonUpdate.Size = new System.Drawing.Size(133, 71);
//            this.buttonUpdate.TabIndex = 1;
//            this.buttonUpdate.Text = "Update";
//            this.buttonUpdate.UseVisualStyleBackColor = true;
//            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
//            // 
//            // listViewProcesses
//            // 
//            this.listViewProcesses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
//            this.columnHeaderName,
//            this.columnHeaderAppPool});
//            this.listViewProcesses.HideSelection = false;
//            this.listViewProcesses.Location = new System.Drawing.Point(3, 41);
//            this.listViewProcesses.Name = "listViewProcesses";
//            this.listViewProcesses.Size = new System.Drawing.Size(582, 235);
//            this.listViewProcesses.TabIndex = 2;
//            this.listViewProcesses.UseCompatibleStateImageBehavior = false;
//            this.listViewProcesses.View = System.Windows.Forms.View.Details;
//            this.listViewProcesses.SelectedIndexChanged += new System.EventHandler(this.listViewProcesses_SelectedIndexChanged);
//            // 
//            // columnHeaderName
//            // 
//            this.columnHeaderName.Text = "Name";
//            this.columnHeaderName.Width = 200;
//            // 
//            // columnHeaderAppPool
//            // 
//            this.columnHeaderAppPool.Text = "App Pool";
//            this.columnHeaderAppPool.Width = 300;
//            // 
//            // buttonRemove
//            // 
//            this.buttonRemove.Location = new System.Drawing.Point(591, 118);
//            this.buttonRemove.Name = "buttonRemove";
//            this.buttonRemove.Size = new System.Drawing.Size(133, 71);
//            this.buttonRemove.TabIndex = 3;
//            this.buttonRemove.Text = "Remove";
//            this.buttonRemove.UseVisualStyleBackColor = true;
//            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
//            // 
//            // buttonAdd
//            // 
//            this.buttonAdd.Location = new System.Drawing.Point(591, 41);
//            this.buttonAdd.Name = "buttonAdd";
//            this.buttonAdd.Size = new System.Drawing.Size(133, 71);
//            this.buttonAdd.TabIndex = 4;
//            this.buttonAdd.Text = "Add";
//            this.buttonAdd.UseVisualStyleBackColor = true;
//            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
//            // 
//            // labelName
//            // 
//            this.labelName.AutoSize = true;
//            this.labelName.Location = new System.Drawing.Point(3, 296);
//            this.labelName.Name = "labelName";
//            this.labelName.Size = new System.Drawing.Size(68, 25);
//            this.labelName.TabIndex = 5;
//            this.labelName.Text = "Name";
//            // 
//            // labelAppPool
//            // 
//            this.labelAppPool.AutoSize = true;
//            this.labelAppPool.Location = new System.Drawing.Point(3, 336);
//            this.labelAppPool.Name = "labelAppPool";
//            this.labelAppPool.Size = new System.Drawing.Size(99, 25);
//            this.labelAppPool.TabIndex = 6;
//            this.labelAppPool.Text = "App Pool";
//            // 
//            // textBoxAppPool
//            // 
//            this.textBoxAppPool.Location = new System.Drawing.Point(122, 333);
//            this.textBoxAppPool.Name = "textBoxAppPool";
//            this.textBoxAppPool.Size = new System.Drawing.Size(463, 31);
//            this.textBoxAppPool.TabIndex = 7;
//            // 
//            // buttonImport
//            // 
//            this.buttonImport.Location = new System.Drawing.Point(591, 195);
//            this.buttonImport.Name = "buttonImport";
//            this.buttonImport.Size = new System.Drawing.Size(133, 71);
//            this.buttonImport.TabIndex = 8;
//            this.buttonImport.Text = "Import";
//            this.buttonImport.UseVisualStyleBackColor = true;
//            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
//            // 
//            // GenearlOptionsControl
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.Controls.Add(this.buttonImport);
//            this.Controls.Add(this.textBoxAppPool);
//            this.Controls.Add(this.labelAppPool);
//            this.Controls.Add(this.labelName);
//            this.Controls.Add(this.buttonAdd);
//            this.Controls.Add(this.buttonRemove);
//            this.Controls.Add(this.listViewProcesses);
//            this.Controls.Add(this.buttonUpdate);
//            this.Controls.Add(this.textBoxName);
//            this.Name = "GenearlOptionsControl";
//            this.Size = new System.Drawing.Size(736, 382);
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }

//        #endregion

//        private System.Windows.Forms.TextBox textBoxName;
//        private System.Windows.Forms.Button buttonUpdate;
//        private System.Windows.Forms.ListView listViewProcesses;
//        private System.Windows.Forms.ColumnHeader columnHeaderName;
//        private System.Windows.Forms.ColumnHeader columnHeaderAppPool;
//        private System.Windows.Forms.Button buttonRemove;
//        private System.Windows.Forms.Button buttonAdd;
//        private System.Windows.Forms.Label labelName;
//        private System.Windows.Forms.Label labelAppPool;
//        private System.Windows.Forms.TextBox textBoxAppPool;
//        private System.Windows.Forms.Button buttonImport;
//    }
//}
