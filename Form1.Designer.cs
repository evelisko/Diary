﻿namespace Diary
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dGvTasks = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.tmrCurentTime = new System.Windows.Forms.Timer(this.components);
            this.lblDayOfWeek = new System.Windows.Forms.Label();
            this.dGvCalendar = new System.Windows.Forms.DataGridView();
            this.clmMon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSunday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblCurrentMonth = new System.Windows.Forms.Label();
            this.btnNextMonth = new System.Windows.Forms.Button();
            this.btnRedoMonth = new System.Windows.Forms.Button();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.stStatus = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnDeleteTask = new System.Windows.Forms.Button();
            this.btnTaskModify = new System.Windows.Forms.Button();
            this.lblNoTask = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dGvTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGvCalendar)).BeginInit();
            this.stStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dGvTasks
            // 
            this.dGvTasks.AllowUserToAddRows = false;
            this.dGvTasks.AllowUserToDeleteRows = false;
            this.dGvTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGvTasks.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dGvTasks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dGvTasks.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dGvTasks.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dGvTasks.ColumnHeadersHeight = 34;
            this.dGvTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dGvTasks.ColumnHeadersVisible = false;
            this.dGvTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3,
            this.Column2});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGvTasks.DefaultCellStyle = dataGridViewCellStyle1;
            this.dGvTasks.GridColor = System.Drawing.SystemColors.Window;
            this.dGvTasks.Location = new System.Drawing.Point(475, 57);
            this.dGvTasks.MultiSelect = false;
            this.dGvTasks.Name = "dGvTasks";
            this.dGvTasks.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dGvTasks.RowHeadersWidth = 7;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10);
            this.dGvTasks.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dGvTasks.RowTemplate.Height = 20;
            this.dGvTasks.RowTemplate.ReadOnly = true;
            this.dGvTasks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dGvTasks.Size = new System.Drawing.Size(547, 460);
            this.dGvTasks.TabIndex = 22;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.HeaderText = "Column1";
            this.Column1.MinimumWidth = 8;
            this.Column1.Name = "Column1";
            this.Column1.Width = 8;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.MinimumWidth = 8;
            this.Column3.Name = "Column3";
            this.Column3.Width = 150;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.HeaderText = "Column2";
            this.Column2.MinimumWidth = 8;
            this.Column2.Name = "Column2";
            this.Column2.Width = 8;
            // 
            // tmrCurentTime
            // 
            this.tmrCurentTime.Tick += new System.EventHandler(this.tmrCurentTime_Tick);
            // 
            // lblDayOfWeek
            // 
            this.lblDayOfWeek.AutoSize = true;
            this.lblDayOfWeek.Location = new System.Drawing.Point(305, 423);
            this.lblDayOfWeek.Name = "lblDayOfWeek";
            this.lblDayOfWeek.Size = new System.Drawing.Size(51, 20);
            this.lblDayOfWeek.TabIndex = 4;
            this.lblDayOfWeek.Text = "label1";
            // 
            // dGvCalendar
            // 
            this.dGvCalendar.AllowUserToAddRows = false;
            this.dGvCalendar.AllowUserToDeleteRows = false;
            this.dGvCalendar.AllowUserToResizeColumns = false;
            this.dGvCalendar.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGvCalendar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dGvCalendar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGvCalendar.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dGvCalendar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dGvCalendar.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dGvCalendar.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGvCalendar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dGvCalendar.ColumnHeadersHeight = 42;
            this.dGvCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dGvCalendar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmMon,
            this.clmTus,
            this.clmWen,
            this.clmTh,
            this.clmFr,
            this.clmSat,
            this.clmSunday});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dGvCalendar.DefaultCellStyle = dataGridViewCellStyle12;
            this.dGvCalendar.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dGvCalendar.Location = new System.Drawing.Point(9, 43);
            this.dGvCalendar.MultiSelect = false;
            this.dGvCalendar.Name = "dGvCalendar";
            this.dGvCalendar.ReadOnly = true;
            this.dGvCalendar.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGvCalendar.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dGvCalendar.RowHeadersVisible = false;
            this.dGvCalendar.RowHeadersWidth = 32;
            this.dGvCalendar.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dGvCalendar.RowsDefaultCellStyle = dataGridViewCellStyle14;
            this.dGvCalendar.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dGvCalendar.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dGvCalendar.RowTemplate.Height = 30;
            this.dGvCalendar.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dGvCalendar.Size = new System.Drawing.Size(460, 472);
            this.dGvCalendar.TabIndex = 5;
            this.dGvCalendar.TabStop = false;
            this.dGvCalendar.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGvCalendar_CellClick);
            this.dGvCalendar.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dGvCalendar_CellPainting);
            // 
            // clmMon
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmMon.DefaultCellStyle = dataGridViewCellStyle5;
            this.clmMon.HeaderText = "Пн";
            this.clmMon.MinimumWidth = 8;
            this.clmMon.Name = "clmMon";
            this.clmMon.ReadOnly = true;
            this.clmMon.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmTus
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmTus.DefaultCellStyle = dataGridViewCellStyle6;
            this.clmTus.HeaderText = "Вт";
            this.clmTus.MinimumWidth = 8;
            this.clmTus.Name = "clmTus";
            this.clmTus.ReadOnly = true;
            this.clmTus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmWen
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmWen.DefaultCellStyle = dataGridViewCellStyle7;
            this.clmWen.HeaderText = "Ср";
            this.clmWen.MinimumWidth = 8;
            this.clmWen.Name = "clmWen";
            this.clmWen.ReadOnly = true;
            this.clmWen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmTh
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmTh.DefaultCellStyle = dataGridViewCellStyle8;
            this.clmTh.HeaderText = "Чт";
            this.clmTh.MinimumWidth = 8;
            this.clmTh.Name = "clmTh";
            this.clmTh.ReadOnly = true;
            this.clmTh.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmFr
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmFr.DefaultCellStyle = dataGridViewCellStyle9;
            this.clmFr.HeaderText = "Пт";
            this.clmFr.MinimumWidth = 8;
            this.clmFr.Name = "clmFr";
            this.clmFr.ReadOnly = true;
            this.clmFr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmSat
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmSat.DefaultCellStyle = dataGridViewCellStyle10;
            this.clmSat.HeaderText = "Сб";
            this.clmSat.MinimumWidth = 8;
            this.clmSat.Name = "clmSat";
            this.clmSat.ReadOnly = true;
            this.clmSat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmSunday
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmSunday.DefaultCellStyle = dataGridViewCellStyle11;
            this.clmSunday.HeaderText = "Вс";
            this.clmSunday.MinimumWidth = 8;
            this.clmSunday.Name = "clmSunday";
            this.clmSunday.ReadOnly = true;
            this.clmSunday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lblCurrentMonth
            // 
            this.lblCurrentMonth.AutoSize = true;
            this.lblCurrentMonth.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrentMonth.Location = new System.Drawing.Point(165, 10);
            this.lblCurrentMonth.Name = "lblCurrentMonth";
            this.lblCurrentMonth.Size = new System.Drawing.Size(133, 27);
            this.lblCurrentMonth.TabIndex = 7;
            this.lblCurrentMonth.Text = "Ноябрь 2019";
            this.lblCurrentMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNextMonth
            // 
            this.btnNextMonth.FlatAppearance.BorderSize = 0;
            this.btnNextMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNextMonth.Location = new System.Drawing.Point(368, 5);
            this.btnNextMonth.Name = "btnNextMonth";
            this.btnNextMonth.Size = new System.Drawing.Size(75, 34);
            this.btnNextMonth.TabIndex = 8;
            this.btnNextMonth.Text = ">";
            this.btnNextMonth.UseVisualStyleBackColor = true;
            this.btnNextMonth.Click += new System.EventHandler(this.btnNextMonth_Click);
            // 
            // btnRedoMonth
            // 
            this.btnRedoMonth.FlatAppearance.BorderSize = 0;
            this.btnRedoMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRedoMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRedoMonth.Location = new System.Drawing.Point(24, 5);
            this.btnRedoMonth.Name = "btnRedoMonth";
            this.btnRedoMonth.Size = new System.Drawing.Size(75, 34);
            this.btnRedoMonth.TabIndex = 9;
            this.btnRedoMonth.Text = "<";
            this.btnRedoMonth.UseVisualStyleBackColor = true;
            this.btnRedoMonth.Click += new System.EventHandler(this.btnRedoMonth_Click);
            // 
            // btnAddTask
            // 
            this.btnAddTask.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnAddTask.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnAddTask.FlatAppearance.BorderSize = 0;
            this.btnAddTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAddTask.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAddTask.Location = new System.Drawing.Point(837, 6);
            this.btnAddTask.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(185, 37);
            this.btnAddTask.TabIndex = 10;
            this.btnAddTask.Text = "Добавить задачу";
            this.btnAddTask.UseVisualStyleBackColor = false;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // stStatus
            // 
            this.stStatus.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.stStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.stStatus.Location = new System.Drawing.Point(0, 523);
            this.stStatus.Name = "stStatus";
            this.stStatus.Size = new System.Drawing.Size(1035, 32);
            this.stStatus.TabIndex = 21;
            this.stStatus.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(61, 25);
            this.statusLabel.Text = "_______";
            // 
            // btnDeleteTask
            // 
            this.btnDeleteTask.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnDeleteTask.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnDeleteTask.FlatAppearance.BorderSize = 0;
            this.btnDeleteTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDeleteTask.ForeColor = System.Drawing.SystemColors.Control;
            this.btnDeleteTask.Location = new System.Drawing.Point(570, 6);
            this.btnDeleteTask.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeleteTask.Name = "btnDeleteTask";
            this.btnDeleteTask.Size = new System.Drawing.Size(110, 37);
            this.btnDeleteTask.TabIndex = 24;
            this.btnDeleteTask.Text = "Удалить задачу";
            this.btnDeleteTask.UseVisualStyleBackColor = false;
            this.btnDeleteTask.Click += new System.EventHandler(this.btnDeleteTask_Click);
            // 
            // btnTaskModify
            // 
            this.btnTaskModify.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnTaskModify.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnTaskModify.FlatAppearance.BorderSize = 0;
            this.btnTaskModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaskModify.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnTaskModify.ForeColor = System.Drawing.SystemColors.Control;
            this.btnTaskModify.Location = new System.Drawing.Point(686, 6);
            this.btnTaskModify.Margin = new System.Windows.Forms.Padding(0);
            this.btnTaskModify.Name = "btnTaskModify";
            this.btnTaskModify.Size = new System.Drawing.Size(145, 37);
            this.btnTaskModify.TabIndex = 25;
            this.btnTaskModify.Text = "Редактировать";
            this.btnTaskModify.UseVisualStyleBackColor = false;
            this.btnTaskModify.Click += new System.EventHandler(this.btnTaskModify_Click);
            // 
            // lblNoTask
            // 
            this.lblNoTask.AutoSize = true;
            this.lblNoTask.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblNoTask.ForeColor = System.Drawing.Color.DimGray;
            this.lblNoTask.Location = new System.Drawing.Point(651, 69);
            this.lblNoTask.Name = "lblNoTask";
            this.lblNoTask.Size = new System.Drawing.Size(198, 39);
            this.lblNoTask.TabIndex = 26;
            this.lblNoTask.Text = "Событий нет ";
            this.lblNoTask.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox1.Location = new System.Drawing.Point(475, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(548, 4);
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 555);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblNoTask);
            this.Controls.Add(this.dGvTasks);
            this.Controls.Add(this.btnTaskModify);
            this.Controls.Add(this.btnDeleteTask);
            this.Controls.Add(this.stStatus);
            this.Controls.Add(this.btnAddTask);
            this.Controls.Add(this.btnRedoMonth);
            this.Controls.Add(this.btnNextMonth);
            this.Controls.Add(this.lblCurrentMonth);
            this.Controls.Add(this.dGvCalendar);
            this.Controls.Add(this.lblDayOfWeek);
            this.MinimumSize = new System.Drawing.Size(1057, 611);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dGvTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGvCalendar)).EndInit();
            this.stStatus.ResumeLayout(false);
            this.stStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.Timer tmrCurentTime;
        private System.Windows.Forms.Label lblDayOfWeek;
        private System.Windows.Forms.DataGridView dGvCalendar;
        private System.Windows.Forms.Label lblCurrentMonth;
        private System.Windows.Forms.Button btnNextMonth;
        private System.Windows.Forms.Button btnRedoMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTus;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWen;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTh;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFr;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSat;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSunday;
        private System.Windows.Forms.Button btnAddTask;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.StatusStrip stStatus;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.DataGridView dGvTasks;
        private System.Windows.Forms.Button btnDeleteTask;
        private System.Windows.Forms.Button btnTaskModify;
        private System.Windows.Forms.Label lblNoTask;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}

