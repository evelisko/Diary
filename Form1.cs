using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.Data.Sql;

namespace Diary
{
    public partial class Form1 : Form
    {
        int CurrentMonth, CurrentYear, CurrentDay;
        static string[] Months = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
        static string[] Months_padeg = { "Января", "Февраля", "Марта", "Апреля", "Мая", "Июня", "Июля", "Августа", "Сентября", "Октября", "Ноября", "Декабря" };
        enum CellsDataBase: int {ID,TaskType,TaskName,StartDateTime,FinishDateTime,Location};

        static string strDataBaseName = "TestDataBase"; // Название базы данных.

        int[,] DayWitchTasks; // массив содержащий количество задач для каждого дня текущего месяца.  
        int[] taskIDs;

        bool IsTableCreate; 

        DataTable taskValues;  

        public Form1()
        {
            InitializeComponent();
            btnDeleteTask.Visible = false;
            btnTaskModify.Visible = false;
            tmrCurentTime.Enabled = true;
            dGvTasks.AllowUserToAddRows = false;
            dGvTasks.AllowUserToDeleteRows = false;
            dGvTasks.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dGvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            DayWitchTasks = new int[7, 6]; // массив содержащий информацию о днях месяца для которых есть задачи. 

            dGvTasks.Columns[0].Width = 90;
            dGvTasks.Columns[1].Width = 90;
            dGvTasks.Columns[2].Width = dGvTasks.Width - 180 - dGvTasks.RowHeadersWidth;
            taskValues = new DataTable();  
            CreatDataBase(); // нужно произвести проверку. удалось ли создать БД.

            CurrentMonth = DateTime.Now.Month;
            CurrentYear = DateTime.Now.Year;
            CurrentDay = DateTime.Now.Day;

            CalendarRefresh(CurrentYear, CurrentMonth);
            if (IsTableCreate)
            {            
              TaskRead();
            }
        }

  #region События формы

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            if (IsTableCreate)
                   AddTask();
        }

        private void btnTaskModify_Click(object sender, EventArgs e)
        {
            TaskModify();
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            DeleteTask();
        }

        private void tmrCurentTime_Tick(object sender, EventArgs e)
        {
            this.Text = DateTime.Now.Day.ToString() + " " + Months_padeg[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString() + " / " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            if (CurrentMonth == 12)
            {
                CurrentYear++;
                CurrentMonth = 1;
            }
            else
            {
               CurrentMonth++;
            }
            CalendarRefresh(CurrentYear, CurrentMonth);
        }

        private void btnRedoMonth_Click(object sender, EventArgs e)
        {
            if (CurrentMonth == 1)
            {
                CurrentYear--;
                CurrentMonth = 12;
            }
            else
            {
                CurrentMonth--;
            }
            CalendarRefresh(CurrentYear, CurrentMonth);
        }

        private void dGvCalendar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DayWitchTasks[dGvCalendar.CurrentCell.ColumnIndex, dGvCalendar.CurrentCell.RowIndex] > -1)
            {
              TaskRead();
            }
            else
            {
              CurrentDay = Convert.ToInt32(dGvCalendar.CurrentCell.Value);

                if (DayWitchTasks[dGvCalendar.CurrentCell.ColumnIndex, dGvCalendar.CurrentCell.RowIndex] == -1)
                {
                    if (CurrentMonth == 1)
                    {
                        CurrentYear--;
                        CurrentMonth = 12;
                    }
                    else
                    {
                        CurrentMonth--;
                    }
                }
                else
                {
                    if (CurrentMonth == 12)
                    {
                        CurrentYear++;
                        CurrentMonth = 1;
                    }
                    else
                    {
                        CurrentMonth++;
                    }
              } 
               CalendarRefresh(CurrentYear, CurrentMonth);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            dGvTasks.Columns[0].Width = 90;
            dGvTasks.Columns[1].Width = 90;
            dGvTasks.Columns[2].Width = dGvTasks.Width - 180 - dGvTasks.RowHeadersWidth;
        }

        private void dGvCalendar_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Draw only grid content cells not ColumnHeader cells nor RowHeader cells
            if (e.ColumnIndex > -1 & e.RowIndex > -1)
            {
                // Pen for left and top borders
                using (var backGroundPen = new Pen(e.CellStyle.BackColor, 1))
                // Pen for bottom and right borders
                using (var gridlinePen = new Pen(dGvCalendar.GridColor, 2))
                // Pen for selected cell borders
                using (var selectedPen = new Pen(Color.LightBlue, 1))
                {
                    var topLeftPoint = new Point(e.CellBounds.Left, e.CellBounds.Top);
                    var topRightPoint = new Point(e.CellBounds.Right, e.CellBounds.Top);
                    var bottomRightPoint = new Point(e.CellBounds.Right, e.CellBounds.Bottom);
                    var bottomleftPoint = new Point(e.CellBounds.Left, e.CellBounds.Bottom);
                  
                    // Paint all parts except borders.
                      e.Paint(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

                    // Draw selected cells here
                    if (this.dGvCalendar[e.ColumnIndex, e.RowIndex].Selected)
                    {
                        // Paint all parts except borders.
                        // e.Paint(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

                        // Draw selected cells border here
                        // e.Graphics.DrawRectangle(selectedPen, new Rectangle(e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Width , e.CellBounds.Height));

                        if (DayWitchTasks[e.ColumnIndex, e.RowIndex] > 0) e.Graphics.DrawRectangle(selectedPen, new Rectangle(e.CellBounds.Left + e.CellBounds.Width / 2 - 4, e.CellBounds.Top + 5, 5, 5));

                        // Handled painting for this cell, Stop default rendering.
                        e.Handled = true;
                    }
                    // Draw non-selected cells here
                    else
                    {
                        // Paint all parts except borders.
                        // e.Paint(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

                        // Top border of first row cells should be in background color
                        //   if (e.RowIndex == 0) 
                        //       e.Graphics.DrawLine(backGroundPen, topLeftPoint, topRightPoint);

                        // Left border of first column cells should be in background color
                        //   if (e.ColumnIndex == 0)
                        //       e.Graphics.DrawLine(backGroundPen, topLeftPoint, bottomleftPoint);

                        // Bottom border of last row cells should be in gridLine color
                        //   if (e.RowIndex == dGvCalendar.RowCount - 1)
                        //       e.Graphics.DrawLine(gridlinePen, bottomRightPoint, bottomleftPoint);
                        //   else  //Bottom border of non-last row cells should be in background color
                        //       e.Graphics.DrawLine(backGroundPen, bottomRightPoint, bottomleftPoint);

                        // Right border of last column cells should be in gridLine color
                        //   if (e.ColumnIndex == dGvCalendar.ColumnCount - 1)
                        //       e.Graphics.DrawLine(gridlinePen, bottomRightPoint, topRightPoint);
                        //   else //Right border of non-last column cells should be in background color
                        //       e.Graphics.DrawLine(backGroundPen, bottomRightPoint, topRightPoint);

                        // Top border of non-first row cells should be in gridLine color, and they should be drawn here after right border
                        //   if (e.RowIndex > 0)
                        e.Graphics.DrawLine(gridlinePen, topLeftPoint, topRightPoint);

                        // Left border of non-first column cells should be in gridLine color, and they should be drawn here after bottom border
                        //   if (e.ColumnIndex > 0)
                        e.Graphics.DrawLine(gridlinePen, topLeftPoint, bottomleftPoint);

                        if (DayWitchTasks[e.ColumnIndex, e.RowIndex] > 0) e.Graphics.DrawRectangle(selectedPen, new Rectangle(e.CellBounds.Left + e.CellBounds.Width / 2 - 4, e.CellBounds.Top + 5, 5, 5));

                        // We handled painting for this cell, Stop default rendering.
                        e.Handled = true;
                    }
                }
            }
        }
        #endregion

        #region Diary
        /// <summary>
        /// Производт перерисовку календаря при смене месяца. 
        /// Создает список дней для которых существуют задачи.  
        /// </summary>
        /// <param name="setYear"></param>
        /// <param name="setMonth"></param>
        public void CalendarRefresh(int setYear, int setMonth)
        {
            dGvCalendar.RowTemplate.Height = (int)(Math.Truncate((double)(dGvCalendar.Height - dGvCalendar.ColumnHeadersHeight) / 6));
            dGvCalendar.RowCount = 6;
            int i, j, k, daysInMounth, daysInMounthLast, lastmonth, lastYear;
            int lastDay = 0;
            j = 0;
            i = 0;

            lastmonth = setMonth - 1;
            lastYear = setYear;
            if (lastmonth < 1) { lastmonth = 12; lastYear--; }
            daysInMounthLast = DateTime.DaysInMonth(lastYear, lastmonth);

            lblCurrentMonth.Text = Months[setMonth - 1] + " " + setYear.ToString("0000");

            DateTime dateValue = new DateTime(setYear, setMonth, 1);
            j = ((int)dateValue.DayOfWeek) - 1;
            if (j == -1) j = 6;

            // Задаем текущую активную ячейку в таблице. 
            lastDay = j - 1;

            while (lastDay > -1)  // Предыдущий месяц. 
            {
                dGvCalendar[lastDay, 0].Value = daysInMounthLast.ToString();
                dGvCalendar.Rows[0].Cells[lastDay].Style.ForeColor = Color.DarkGray;// задаем цвет для ячеек предыдущего месяца. 
                DayWitchTasks[lastDay, 0] = -1; // знак того что данная ячейка относится к пердыдущему месяцу
                daysInMounthLast--;
                lastDay--;
            }


            i = 0;
            daysInMounth = DateTime.DaysInMonth(setYear, setMonth);

            string connStr = (string.Format(@"Data Source=(local)\SQLEXPRESS; Initial Catalog= {0}; Integrated Security=True", strDataBaseName));
            SqlConnection conn = new SqlConnection(connStr);
      
         if (IsTableCreate)
            {    
              try
              {
                statusLabel.Text = "Подключение к БД Diary";
                conn.Open();
              }
            catch (SqlException se)
             {
                if (se.Number == 4060)
                {
                    statusLabel.Text = "Ошибка подключения к БД";
                    conn.Close();
                    return;
                }
             }
         }

            DateTime StartDay = new DateTime(CurrentYear, CurrentMonth, 1, 0, 0, 0);
            DateTime EndDay = new DateTime(CurrentYear, CurrentMonth, 1, 23, 59, 0);
            k = 1;
            while (k < daysInMounth + 1)  // Текущий месяц
            {
                if (j > dGvCalendar.ColumnCount - 1) { i++; j = 0; }

        // производим инициализацию списка с задачами 
        // Выводим значение на экран
                using (SqlCommand cmd = new SqlCommand("Select * From Diary WHERE ((StartDateTime >= @StartDay) AND (FinishDateTime <= @EndDay)) OR((StartDateTime <= @EndDay) AND (FinishDateTime >= @StartDay))", conn))
                {
                    if (IsTableCreate)
                    {
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = "@StartDay"; param.Value = StartDay; param.SqlDbType = SqlDbType.DateTime;
                        cmd.Parameters.Add(param);

                        param = new SqlParameter();
                        param.ParameterName = "@EndDay"; param.Value = EndDay; param.SqlDbType = SqlDbType.DateTime;
                        cmd.Parameters.Add(param);
                        DayWitchTasks[j, i] = 0;

                  // посылаем запрос
                       try
                        {
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    DayWitchTasks[j, i]++;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            statusLabel.Text = "Ошибка при выполнении запроса к БД " + e.ToString();
                            return;
                        }
                    }
                    StartDay = StartDay.AddDays(1);
                    EndDay = EndDay.AddDays(1);

                    dGvCalendar[j, i].Value = k.ToString();
                    dGvCalendar.Rows[i].Cells[j].Style.ForeColor = Color.Black;

                    if (CurrentDay == k)
                        dGvCalendar[j, i].Selected = true;
                    k++;
                    j++;
                }
            }
            if (IsTableCreate)
                  conn.Close();

        // Последующий месяц
            k = 1;
            while (j < 7)
            {
                dGvCalendar[j, i].Value = k.ToString();
             // задаем цвет для ячеек предыдущего месяца. 
                dGvCalendar.Rows[i].Cells[j].Style.ForeColor = Color.DarkGray;  
                DayWitchTasks[j, i] = -2; // знак того, что данная ячейка относится к следующему месяцу
                j++;
                k++;
            }

            if (i < dGvCalendar.RowCount - 1)
            {
                i++;
                j = 0;
                while (j < 7)
                {
                  dGvCalendar[j, i].Value = k.ToString();
                  dGvCalendar.Rows[i].Cells[j].Style.ForeColor = Color.DarkGray;
                  DayWitchTasks[j, i] = -2;
                   j++;
                   k++;
                }
            }
        }

        ///<summary>
        /// Производится проверка на наличие дазы данных на сервере. 
        /// Если база данных еще несоздана. Процедура выполняет ее создание и создание таблицы Diary. 
        ///</summary>
        private void CreatDataBase()
        {
            IsTableCreate = false; 
          string connStr = (string.Format(@"Data Source=(local)\SQLEXPRESS; Initial Catalog= {0}; Integrated Security=True", strDataBaseName));

         /*Здесь указал имя БД(хотя для создания БД его указывать не нужно)
              для того, чтобы проверить, может данная БД уже создана
            Создаем экземпляр класса  SqlConnection по имени conn
            и передаем конструктору этого класса, строку подключения
         */
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                statusLabel.Text = "Подключение к БД Diary";
                // пробуем подключится
                conn.Open();
            }
            catch (SqlException se)
            {
                // Если база не обнаружена, то создаем новую
                if (se.Number == 4060)
                {
                    statusLabel.Text = "Подождите, идет создание БД";
                    conn.Close();
                    // переопределяем обьект conn, и передаем новую строку подключения
                    conn = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Integrated Security=True");

                    //  Создаем экземпляр класса  SqlCommand по имени cmdCreateDataBase
                    //  и передаем конструктору этого класса, запрос на создание БД и объект типа SqlConnection

                    SqlCommand cmdCreateDataBase = new SqlCommand(string.Format("CREATE DATABASE [{0}]", strDataBaseName), conn);
                    conn.Open();

                    statusLabel.Text = "Посылаем запрос";
                    cmdCreateDataBase.ExecuteNonQuery();
                    conn.Close();
                    //  задержка, нужна для того, чтоб БД успела создаться
                    Thread.Sleep(5000);
                    // переопределяем обьект conn, и передаем новую строку подключения
                    conn = new SqlConnection(connStr);
                    conn.Open();
                }
            }

            statusLabel.Text = "Соедение успешно произведено ";

            try  // Проверяем созданна ли таблица 
            {
                SqlCommand cmdCreateTableBase = new SqlCommand("Select * From Diary", conn);
                cmdCreateTableBase.ExecuteNonQuery();
                statusLabel.Text = "Таблица Подключена";
                IsTableCreate = true; 
            }
            catch (SqlException se) // Таблица еще не создана.
            {
                if (se.Number == 208)
                    CreateNewTable(conn);  // Выполняем создание таблицы Diary.  
            }
            conn.Close();
            conn.Dispose();
        }

        private void DeleteTable(SqlConnection conn)
        {
            using (SqlCommand cmdDeleteTable = new SqlCommand("DROP TABLE Diary", conn))
            {
                // Посылаем запрос
                try
                {
                    cmdDeleteTable.ExecuteNonQuery();
                }
                catch
                {
                    statusLabel.Text = "Ошибка при удалении таблицы";
                    return;
                }
            }
            statusLabel.Text = "Таблица удалена успешно";
        }

        /// <summary>
        /// Редактирование вырбранной задачи. 
        /// </summary>
        private void TaskModify()
        {
            int taskID = 0;
            CurrentDay = Convert.ToInt32(dGvCalendar.CurrentCell.Value);

            string connStr = (string.Format(@"Data Source=(local)\SQLEXPRESS; Initial Catalog= {0}; Integrated Security=True", strDataBaseName));

            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                statusLabel.Text = "Подключение к БД Diary";
                conn.Open();
            }
            catch (SqlException se)
            {
                if (se.Number == 4060)
                {
                    statusLabel.Text = "Ошибка подключения к БД";
                    conn.Close();
                    return;
                }
            }

            AddTaskDialog taskDialog = new AddTaskDialog();

            taskID = Convert.ToInt32(taskValues.Rows[dGvTasks.CurrentRow.Index].ItemArray[(int)CellsDataBase.ID]);
            taskDialog.TaskName = (taskValues.Rows[dGvTasks.CurrentRow.Index].ItemArray[(int)CellsDataBase.TaskName]).ToString();
            taskDialog.TaskType = (taskValues.Rows[dGvTasks.CurrentRow.Index].ItemArray[(int)CellsDataBase.TaskType]).ToString();
            taskDialog.TaskLocation = (taskValues.Rows[dGvTasks.CurrentRow.Index].ItemArray[(int)CellsDataBase.Location]).ToString();
            taskDialog.StartDateTime = Convert.ToDateTime(taskValues.Rows[dGvTasks.CurrentRow.Index].ItemArray[(int)CellsDataBase.StartDateTime]);
            taskDialog.FinishDateTime = Convert.ToDateTime(taskValues.Rows[dGvTasks.CurrentRow.Index].ItemArray[(int)CellsDataBase.FinishDateTime]);



            conn.Close();
            taskDialog.ShowDialog();

            if (taskDialog.DialogResult != DialogResult.OK) return;

            string strTaskName = taskDialog.TaskName;
            string strTaskType = taskDialog.TaskType;
            string strTaskLocation = taskDialog.TaskLocation;
            DateTime StartDateTime = taskDialog.StartDateTime;
            DateTime FinishDateTime = taskDialog.FinishDateTime;


            taskValues.Rows[dGvTasks.CurrentRow.Index].ItemArray[(int)CellsDataBase.TaskName] = strTaskName;
            taskValues.Rows[dGvTasks.CurrentRow.Index].ItemArray[(int)CellsDataBase.TaskType] = strTaskType;
            taskValues.Rows[dGvTasks.CurrentRow.Index].ItemArray[(int)CellsDataBase.Location] = strTaskLocation; 
            taskValues.Rows[dGvTasks.CurrentRow.Index].ItemArray[(int)CellsDataBase.StartDateTime] = StartDateTime;
            taskValues.Rows[dGvTasks.CurrentRow.Index].ItemArray[(int)CellsDataBase.FinishDateTime] = FinishDateTime;


            taskDialog.Close();

            try
            {
                statusLabel.Text = "Подключение к БД Diary ";
                conn.Open();
            }
            catch (SqlException se)
            {
                if (se.Number == 4060)
                {
                    statusLabel.Text = "Ошибка подключения к БД";
                    conn.Close();
                    return;
                }
            }

            using (SqlCommand cmd = new SqlCommand("UPDATE Diary Set TaskType = @TaskType , TaskName = @TaskName , StartDateTime = @StartDateTime , FinishDateTime = @FinishDateTime , Location =  @Location WHERE ID = @ID", conn))
            { 
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@ID"; param.Value = (taskID); param.SqlDbType = SqlDbType.Int;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@TaskType"; param.Value = strTaskType; param.SqlDbType = SqlDbType.Text;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@TaskName"; param.Value = strTaskName; param.SqlDbType = SqlDbType.Text;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@StartDateTime"; param.Value = StartDateTime; param.SqlDbType = SqlDbType.DateTime;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@FinishDateTime"; param.Value = FinishDateTime; param.SqlDbType = SqlDbType.DateTime;
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@Location"; param.Value = strTaskLocation; param.SqlDbType = SqlDbType.Text;
            cmd.Parameters.Add(param);

            statusLabel.Text = "Измененение записи";

            try
            {
              cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                statusLabel.Text = "Ошибка, при выполнении запроса на изменение записи" + ex.ToString();
                conn.Close();
                return;
            }
        }
          conn.Close();
          conn.Dispose();

            if (strTaskLocation != "")
                dGvTasks[0, dGvTasks.Rows.Count - 1].Value = strTaskType + "\r\n" + strTaskLocation;
            else
                dGvTasks[0, dGvTasks.CurrentRow.Index].Value = strTaskType;

            if ((StartDateTime.Day == CurrentDay) && (FinishDateTime.Day == CurrentDay))
            {
                if ((StartDateTime.Hour < FinishDateTime.Hour) && (strTaskType != "Памятка"))
                {
                    dGvTasks[1, dGvTasks.Rows.Count - 1].Value = StartDateTime.Hour.ToString("00") + ":" + StartDateTime.Minute.ToString("00") + "\r\n" + FinishDateTime.Hour.ToString("00") + ":" + FinishDateTime.Minute.ToString("00");
                }
                else
                {
                    dGvTasks[1, dGvTasks.Rows.Count - 1].Value = StartDateTime.Hour.ToString("00") + ":" + StartDateTime.Minute.ToString("00");
                }
            }
            else
            if ((StartDateTime.Day == CurrentDay) && (FinishDateTime.Day > CurrentDay) && (StartDateTime.Month == CurrentMonth))
            {
                dGvTasks[1, dGvTasks.Rows.Count - 1].Value = "Начало \r\n " + StartDateTime.Hour.ToString("00") + ":" + StartDateTime.Minute.ToString("00");
            }
            else
            if ((StartDateTime.Day > CurrentDay) && (FinishDateTime.Day == CurrentDay) && (FinishDateTime.Month == CurrentMonth))
            {
                dGvTasks[1, dGvTasks.Rows.Count - 1].Value = "Окончание \r\n " + FinishDateTime.Hour.ToString("00") + ":" + FinishDateTime.Minute.ToString("00");
            }
            else // if ((StartTime.Day < CurrentDay) && (FinishTime.Day > CurrentDay))
            {
                dGvTasks[1, dGvTasks.Rows.Count - 1].Value = "Весь день";
            }

            dGvTasks[2, dGvTasks.CurrentRow.Index].Value = strTaskName;

            dGvTasks.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            CalendarTaskReinitialize(StartDateTime, FinishDateTime, true);
        }
      
        /// <summary>
        ///Выполняет считывание списка задач на выдранный (на календаре) день из БД 
        /// </summary>
        private void TaskRead()
        {
            int i = 0;
            CurrentDay = Convert.ToInt32(dGvCalendar.CurrentCell.Value);

            string connStr = (string.Format(@"Data Source=(local)\SQLEXPRESS; Initial Catalog= {0}; Integrated Security=True", strDataBaseName));
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
              statusLabel.Text = "Подключение к БД Diary";
              conn.Open();
            }

            catch (SqlException se)
            {
                if (se.Number == 4060)
                {
                    statusLabel.Text = "Ошибка подключения к БД";
                    conn.Close();
                    return;
                }
            }

              DateTime StartDay = new DateTime(CurrentYear,CurrentMonth,CurrentDay,0,0,0);
              DateTime EndDay = new DateTime(CurrentYear, CurrentMonth, CurrentDay, 23, 59, 0);

            // задание условия для считывания задачи. 
            using (SqlCommand cmd = new SqlCommand("Select * From Diary WHERE ((StartDateTime >= @StartDay) AND (FinishDateTime <= @EndDay)) OR((StartDateTime <= @EndDay) AND (FinishDateTime >= @StartDay))", conn))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@StartDay"; param.Value = StartDay; param.SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@EndDay"; param.Value = EndDay; param.SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.Add(param);


                taskValues = new DataTable();

                try
                {
                    taskValues.Load(cmd.ExecuteReader());
                }
                catch
                {
                    statusLabel.Text = "Ошибка считывания из БД";
                }

                   if (DayWitchTasks[dGvCalendar.CurrentCell.ColumnIndex, dGvCalendar.CurrentCell.RowIndex] > -1)
                    {
                      taskIDs = new int[taskValues.Rows.Count]; 

                      DayWitchTasks[dGvCalendar.CurrentCell.ColumnIndex, dGvCalendar.CurrentCell.RowIndex] = taskValues.Rows.Count;
                    }
                   
                    dGvTasks.Rows.Clear();
                    i = 0;
                    while (i < taskValues.Rows.Count)
                    {

                     if (taskIDs.Length > 0) taskIDs[i] = Convert.ToInt32(taskValues.Rows[i].ItemArray[(int)CellsDataBase.ID]); 
       
                       dGvTasks.Rows.Add(1);

                        DateTime StartDateTime = Convert.ToDateTime(taskValues.Rows[i].ItemArray[(int)CellsDataBase.StartDateTime]);
                        DateTime FinishDateTime = Convert.ToDateTime(taskValues.Rows[i].ItemArray[(int)CellsDataBase.FinishDateTime]);

                    if ((taskValues.Rows[i].ItemArray[(int)CellsDataBase.Location]).ToString() != "")
                            dGvTasks[0, dGvTasks.Rows.Count - 1].Value = (taskValues.Rows[i].ItemArray[(int)CellsDataBase.TaskType]).ToString() + "\r\n" +
                                (taskValues.Rows[i].ItemArray[(int)CellsDataBase.Location]).ToString();
                    else
                        dGvTasks[0, dGvTasks.Rows.Count - 1].Value = (taskValues.Rows[i].ItemArray[(int)CellsDataBase.TaskType]).ToString();

                    if ((StartDateTime.Day == CurrentDay) && (FinishDateTime.Day == CurrentDay))
                    {
                        if ((StartDateTime.Hour < FinishDateTime.Hour) && (taskValues.Rows[i].ItemArray[(int)CellsDataBase.TaskType].ToString() != "Памятка"))
                        {
                            dGvTasks[1, dGvTasks.Rows.Count - 1].Value = StartDateTime.Hour.ToString("00") + ":" + StartDateTime.Minute.ToString("00") + "\r\n" + FinishDateTime.Hour.ToString("00") + ":" + FinishDateTime.Minute.ToString("00");
                        }
                        else
                        {
                            dGvTasks[1, dGvTasks.Rows.Count - 1].Value = StartDateTime.Hour.ToString("00") + ":" + StartDateTime.Minute.ToString("00");
                        }
                    }
                    else
                    if ((StartDateTime.Day == CurrentDay) && (FinishDateTime.Day > CurrentDay) && (StartDateTime.Month == CurrentMonth))
                    {
                        dGvTasks[1, dGvTasks.Rows.Count - 1].Value = "Начало \r\n " + StartDateTime.Hour.ToString("00") + ":" + StartDateTime.Minute.ToString("00");
                    }
                    else
                    if ((StartDateTime.Day < CurrentDay) && (FinishDateTime.Day == CurrentDay)&& (FinishDateTime.Month == CurrentMonth))
                       {
                         dGvTasks[1, dGvTasks.Rows.Count - 1].Value = "Окончание \r\n " + FinishDateTime.Hour.ToString("00") + ":" + FinishDateTime.Minute.ToString("00");
                       }
                    else // if ((StartTime.Day < CurrentDay) && (FinishTime.Day > CurrentDay))
                     {
                       dGvTasks[1, dGvTasks.Rows.Count - 1].Value = "Весь день";
                     }

                     dGvTasks[2, dGvTasks.Rows.Count - 1].Value = (taskValues.Rows[i].ItemArray[(int)CellsDataBase.TaskName]).ToString();
                       
                    dGvTasks.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
                    i++;
                  }
                }

            conn.Close();
            conn.Dispose();
    // Добавляем запись в таблицу просмотра. 

            if (dGvTasks.RowCount > 0)  // Если для текущего дня существуют актмивные задачи делаем видимыми кнопки редактирования и удаления задач.
            {
                btnDeleteTask.Visible = true;
                btnTaskModify.Visible = true;
                lblNoTask.Visible = false;
            }
            else
            {
                btnDeleteTask.Visible = false;
                btnTaskModify.Visible = false;
                lblNoTask.Visible = true;
            }

        }

        /// <summary>
        /// Производит создание таблицы баз данных Diary
        /// </summary>
        /// <param name="conn"> текущее подключнеие </param>
        private void CreateNewTable(SqlConnection conn)
        {
            using (SqlCommand cmdCreateTable = new SqlCommand("CREATE TABLE " + " Diary " +
               "(ID int not null PRIMARY KEY IDENTITY, " + // Нумирация записей производится автоматически. 
               "TaskType text not null, " + 
               "TaskName text not null, " +
               "StartDateTime datetime not null, " +
               "FinishDateTime datetime not null, " +
               "Location text not null )", conn))
            {
              // посылаем запрос
                try
                {
                  cmdCreateTable.ExecuteNonQuery();
                }
                catch
                {
                  statusLabel.Text = "Ошибка при создании таблицы";
                  return;
                }
            }
            statusLabel.Text = "Таблица создана успешно";
            IsTableCreate = true; 
        }

        /// <summary>
        /// Вызывает диалоговое окно и производит добавление нового события к списку существующих задач.  
        /// </summary>
        private void AddTask()
        { // вызываем диалоговое окно для редактирования параметров новой задачи. 
            AddTaskDialog taskDialog = new AddTaskDialog();

            taskDialog.CurrentDate = new DateTime(CurrentYear, CurrentMonth, CurrentDay, 0, 0, 0);

            taskDialog.ShowDialog();
        if (taskDialog.DialogResult != DialogResult.OK) return;

            string strTaskName = taskDialog.TaskName;
            string strTaskType = taskDialog.TaskType;
            string strTaskLocation = taskDialog.TaskLocation;
            DateTime StartDateTime = taskDialog.StartDateTime;
            DateTime FinishDateTime = taskDialog.FinishDateTime;

            taskDialog.Close();

            // Производим сохранение задачи в базе данных 

            string connStr = (string.Format(@"Data Source=(local)\SQLEXPRESS; Initial Catalog= {0}; Integrated Security=True", strDataBaseName));
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                statusLabel.Text = "Подключение к БД Diary ";
                conn.Open();
            }
            catch (SqlException se)
            {
                if (se.Number == 4060)
                {
                    statusLabel.Text = "Ошибка подключения к БД";
                    conn.Close();
                    return;
                }
            }

            // узнаем сколько на текущий день задачь уже существует. 

            using (SqlCommand cmd = new SqlCommand("INSERT INTO Diary (TaskType,TaskName,StartDateTime,FinishDateTime,Location) Values (@TaskType,@TaskName,@StartDateTime,@FinishDateTime,@Location)", conn))
            {
            /*Работаем с параметрами(SqlParameter), эта техника позволяет уменьшить
             кол-во ошибок и достичь большего быстродействия
             но требует и больших усилий в написании кода*/

            // объявляем объект класса SqlParameter

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@TaskType"; param.Value = strTaskType; param.SqlDbType = SqlDbType.Text;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@TaskName"; param.Value = strTaskName; param.SqlDbType = SqlDbType.Text;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@StartDateTime"; param.Value = StartDateTime; param.SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@FinishDateTime"; param.Value = FinishDateTime; param.SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Location"; param.Value = strTaskLocation; param.SqlDbType = SqlDbType.Text;
                cmd.Parameters.Add(param);

                statusLabel.Text = "Вставляем запись";

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    statusLabel.Text = "Ошибка, при выполнении запроса на добавление записи";
                   conn.Close();
                   return;
               }

            }

            conn.Close();
            conn.Dispose();

            CalendarTaskReinitialize(StartDateTime, FinishDateTime, true);
            TaskRead();

        }


        /// <summary>
        /// Удаление задачи. 
        /// </summary>
        private void DeleteTask()      
        {
            CurrentDay = Convert.ToInt32(dGvCalendar.CurrentCell.Value);

            string connStr = (string.Format(@"Data Source=(local)\SQLEXPRESS; Initial Catalog= {0}; Integrated Security=True", strDataBaseName));
            SqlConnection conn = new SqlConnection(connStr);

            try
            {
                statusLabel.Text = "Подключение к БД Diary";
                conn.Open();
            }
            catch (SqlException se)
            {
                if (se.Number == 4060)
                {
                    statusLabel.Text = "Ошибка подключения к БД";
                    conn.Close();
                    return;
                }
            }
          
         // Посылаем запрос на удаление записи из таблицы БД. 
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Diary WHERE ID = @ID", conn))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@ID"; param.Value = (taskIDs[dGvTasks.CurrentRow.Index]); param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                statusLabel.Text = "Удаление записи";

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                  statusLabel.Text = "Ошибка, при выполнении запроса на удаление записи";
                  conn.Close();
                  return;
                }
            }
            conn.Close();
            conn.Dispose();

            DateTime StartDateTime = Convert.ToDateTime(taskValues.Rows[dGvTasks.CurrentRow.Index].ItemArray[(int)CellsDataBase.StartDateTime]);
            DateTime FinishDateTime = Convert.ToDateTime(taskValues.Rows[dGvTasks.CurrentRow.Index].ItemArray[(int)CellsDataBase.FinishDateTime]);

            CalendarTaskReinitialize(StartDateTime, FinishDateTime, false);

           TaskRead();

        }

/// <summary>
/// Обновляет список дней для которых существуют задачи (дела) 
/// </summary>
/// <param name="startDate">Дата начальная задачи </param>
/// <param name="finishDate">Дата Окончания задачи </param>
/// <param name="TaskIncrement">True - Добавить задачу, false - удалить</param>
        private void CalendarTaskReinitialize(DateTime startDate,DateTime finishDate, bool TaskIncrement )
        {
            DateTime dateValue = new DateTime(CurrentYear, CurrentMonth, 1);
            int j = ((int)dateValue.DayOfWeek) - 1;
            int i = 0;
            int k = 1;

            int daysInMounth = DateTime.DaysInMonth(CurrentYear, CurrentMonth);

            while (k < daysInMounth + 1)  // Текущий месяц
            {
                if (j > dGvCalendar.ColumnCount - 1) { i++; j = 0; }

                if ((startDate.Year <= CurrentYear) && (CurrentYear <= finishDate.Year))
                {
                    if (((k < startDate.Day)&&(startDate.Month < CurrentMonth)&&(finishDate.Month >= CurrentMonth))||
                        ((startDate.Day <= k) && (finishDate.Day >= k))||
                        ((k > finishDate.Day) && (finishDate.Month > CurrentMonth)&&(startDate.Month <= CurrentMonth)))
                    {
                        if (TaskIncrement)
                            DayWitchTasks[j, i]++;
                        else
                        if (DayWitchTasks[j, i] > 0)
                            DayWitchTasks[j, i]--;
                    }
                }

                k++;
                j++;
            }
          dGvCalendar.Invalidate(); // Прерисовка таблицы.
        }

    }
    #endregion

}
